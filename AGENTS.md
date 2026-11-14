# AGENTS.md

このリポジトリは、Windows 向けメディア管理アプリケーション **MediaDeck**（[xm-i/MediaDeck](https://github.com/xm-i/MediaDeck)）の公式 Web サイトおよびドキュメントサイト（`MediaDeckSite`）です。

## 技術スタック・開発環境

- **フレームワーク**: Vue 3 (`<script setup lang="ts">`) + Vue Router 5
- **ビルドツール / 言語**: Vite 8 + TypeScript (`~6.0`) + `vue-tsc`
- **ランタイム要件**: Node.js `^20.19.0 || >=22.12.0`（CI では Node 24 を使用）
- **スタイリング**: 素の CSS + CSS カスタムプロパティ（`src/index.css`）。CSS フレームワークは未導入
- **アイコン**: 現状はインライン SVG を使用（`package.json` には `lucide-vue-next` が含まれている）
- **TypeScript 設定**: `tsconfig.app.json` で `"noUncheckedIndexedAccess": true` およびパスエイリアス `@/*`（`./src/*`）が有効
- **Lint / テスト**: ESLint・Prettier・テストランナー（Vitest 等）は現在未導入

## 主要コマンド

```sh
# 依存パッケージのインストール（CI では npm ci）
npm install

# 開発サーバー起動 (vite)
npm run dev

# Vue + TypeScript の型チェック (vue-tsc --build)
npm run type-check

# 型チェックと本番ビルドを並列実行し dist/ へ出力
npm run build

# 型チェックなしでビルドのみ実行 (vite build)
npm run build-only

# ビルド済み dist/ のローカルプレビュー (vite preview)
npm run preview
```

コードを変更した後は、完了前に **`npm run build`**（または最低限 `npm run type-check`）を実行して型エラーやビルドエラーがないことを確認してください。

## リポジトリ構成

- `index.html` — エントリー HTML（`lang="ja"`、Google Fonts `Inter` / `Outfit` 読み込み）
- `src/main.ts` — アプリ初期化とルーター登録
- `src/App.vue` — 共通レイアウト（固定ナビゲーションバー、768px 未満のモバイルメニュー、フッター、`<router-view />`）
- `src/index.css` — ダークテーマの CSS 変数（`--bg-color: #0c0e12`, `--accent-color: #00a3ff` 等）、`.glass-panel`、アニメーション定義
- `src/router/index.ts` — `/`（`HomeView`）および `/docs/*`（遅延読み込み）のルート定義
- `src/views/HomeView.vue` — ランディングページ（ヒーロー、スクリーンショット、機能カード、マップ紹介、対応メディア、技術スタック）
- `src/views/DocsView.vue` — ドキュメント共通レイアウト（サイドバー、768px 未満のモバイルドロワー、`.docs-content` 向けグローバルスタイル）
- `src/views/docs/*.vue` — ドキュメント各ページ（9 ページ構成）
- `src/images/` — サイト・ロゴ用の画像アセット（ES import で参照）
- `mediadeck_docs_info.md` — MediaDeck 本体の機能・UI・画面構成・ドキュメント構成のまとめ（コンテンツ執筆時の一次参照資料）
- `.agents/skills/anti-slop-design/SKILL.md` — UI/UX 設計・スタイリング・デザインレビュー用のワークスペーススキル（`anti-slop-design`）
- `.github/workflows/deploy.yml` — GitHub Actions による本番ビルド・デプロイ定義

## 開発・編集上のルール

1. **日本語での記述**
   - サイト上の UI テキスト・ドキュメント本文は日本語で記述する（`prop.Width` やツール名・技術固有名詞などの識別子は原語のまま）。
2. **UI・デザイン編集時のスキル適用**
   - UI の新規構築・CSS 調整・レスポンシブ改善・デザインレビューを行う場合は、ワークスペーススキル `.agents/skills/anti-slop-design/SKILL.md`（`anti-slop-design`）を読み込み、記載された設計原則・アクセシビリティ基準・品質ゲートに沿って作業する。
3. **ドキュメントページの追加・更新**
   - `src/views/docs/*.vue` は `<template>` 中心のシンプルな HTML（`h1`, `h2`, `h3`, `p`, `ul`, `table`, `code`）で記述し、スタイルは `src/views/DocsView.vue` の `.docs-content` ルールに合わせる。
   - ページを追加・削除・パス変更する場合は、**`src/router/index.ts` の `children`** と **`src/views/DocsView.vue` の `navItems`** を必ず両方同時に更新する。
   - MediaDeck 本体の仕様・機能に関する記述は `mediadeck_docs_info.md` と整合性を取り、確認できない機能や仕様を推測で追加しない。
4. **配列・インデックスアクセス**
   - `noUncheckedIndexedAccess` が有効なため、配列や Record へのインデックスアクセス時は `undefined` の可能性を考慮して型安全に扱う。
5. **生成物の扱い**
   - `dist/` や `*.tsbuildinfo` などのビルド生成物・キャッシュは `.gitignore` 対象のためコミットしない。

## Git・デプロイ運用

- **デフォルトブランチ**: `main`
- **自動デプロイの注意点**: `.github/workflows/deploy.yml` により、**任意の Git タグのプッシュ（`tags: "*"`）または `workflow_dispatch` がトリガーとなって本番サーバー（`/opt/mediadeck.xm-i.net/`）へ `dist/` が `rsync -avz --delete` でデプロイ**されます。
  - タグ形式の例: `v1.0.0`, `v1.0.1`, `v1.0.2`, `v1.0.3`
  - CI では `VITE_APP_ENV=production` を指定して `npm ci` → `npm run build` を実行します。
- **禁止事項**: ユーザーからの明示的な指示がない限り、Git タグの作成・プッシュや `git push` を実行しないでください。
