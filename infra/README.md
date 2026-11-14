# インフラ構築・運用手順 (Ubuntu)

このディレクトリには、Ubuntu 本番サーバー上で `MediaDeckSite`（ASP.NET Core `.NET 10` / ポート `5001`）を稼働させるための設定ファイルを格納しています。

## ファイル一覧

- `mediadeck.service` — systemd サービス定義ファイル（`/opt/dotnet10/dotnet` 使用、ポート `5001` 待受）
- `mediadeck.xm-i.net.conf` — Nginx 設定ファイル（静的ファイル `/opt/mediadeck.xm-i.net/wwwroot/` 配信 + `/api/` を `http://127.0.0.1:5001` へ転送、Cloudflare Origin SSL 使用）
- `mediadeck-deploy.sudoers` — GitHub Actions（`deploy` ユーザー）がパスワードなしで `mediadeck.service` を再起動するための sudoers 設定

---

## サーバー初期セットアップ・切り替え手順

サーバーに SSH ログインし、以下の手順を順に実施してください。

### 1. デプロイ先ディレクトリの権限確認

`deploy` ユーザーが `/opt/mediadeck.xm-i.net` へ `rsync` デプロイおよびファイル削除を行えるよう、ディレクトリを作成して所有権を設定します。

```bash
sudo mkdir -p /opt/mediadeck.xm-i.net
sudo chown -R deploy:deploy /opt/mediadeck.xm-i.net
```

※ 初回のデプロイ完了後、`/opt/mediadeck.xm-i.net/MediaDeckSite.dll` が配置された状態でサービスを起動します（まだ配置されていない場合は先に GitHub Actions の `workflow_dispatch` 等でデプロイを実行してください）。

### 2. systemd サービスの登録・有効化・起動

`infra/mediadeck.service` を `/etc/systemd/system/mediadeck.service` に配置します。

```bash
sudo cp infra/mediadeck.service /etc/systemd/system/mediadeck.service
sudo systemctl daemon-reload
sudo systemctl enable --now mediadeck.service
```

### 3. GitHub Actions 自動再起動用の sudoers 設定

`.github/workflows/deploy.yml` から SSH 経由で `sudo systemctl restart mediadeck.service` を実行できるようにするため、`infra/mediadeck-deploy.sudoers` を `/etc/sudoers.d/mediadeck-deploy` に配置して権限・構文を確認します。

```bash
sudo cp infra/mediadeck-deploy.sudoers /etc/sudoers.d/mediadeck-deploy
sudo chown root:root /etc/sudoers.d/mediadeck-deploy
sudo chmod 440 /etc/sudoers.d/mediadeck-deploy
sudo visudo -cf /etc/sudoers.d/mediadeck-deploy
```

`parsed OK` と表示されれば問題ありません。

### 4. Nginx 設定の更新

既存の `/etc/nginx/sites-available/mediadeck.xm-i.net.conf`（Cloudflare Origin SSL 設定）を `infra/mediadeck.xm-i.net.conf` で上書き更新し、テスト・リロードを行います。

```bash
sudo cp infra/mediadeck.xm-i.net.conf /etc/nginx/sites-available/mediadeck.xm-i.net.conf
sudo nginx -t
sudo systemctl reload nginx
```

### 5. 動作確認・ログ確認

```bash
# サービス状態の確認
sudo systemctl status mediadeck.service

# リアルタイムログの確認
sudo journalctl -u mediadeck -f

# バックエンドのヘルスチェック応答確認（ローカル・HTTPS 経由）
curl http://127.0.0.1:5001/api/health
curl https://mediadeck.xm-i.net/api/health
```

`curl` で `{"status":"ok","timestamp":"..."}` が返り、ブラウザから `https://mediadeck.xm-i.net` および `/docs/getting-started` 等にアクセスできれば完了です。
