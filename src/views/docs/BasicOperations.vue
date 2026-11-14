<script setup lang="ts">
import { useLocale } from '@/i18n'

const { locale } = useLocale()
</script>

<template>
  <div v-if="locale === 'ja'">
    <h1>2. 基本操作</h1>

    <h2>メインウィンドウの見方</h2>
    <p>メインウィンドウは以下のゾーンで構成されています：</p>
    <ul>
      <li><strong>タイトルバー</strong>: アイコン、ナビゲーションメニュー、通知ボタン</li>
      <li><strong>タブバー</strong>: タブの切り替え、追加。InfoBarによるシステム情報表示</li>
      <li><strong>リポジトリペイン（左上）</strong>: フォルダ/アルバムのツリー表示</li>
      <li><strong>ソート・フィルタペイン（左下）</strong>: ソート条件やフィルタリング条件の設定</li>
      <li><strong>ビューアペイン（中央）</strong>: メディアの表示（グリッド/リスト/詳細/マップ）</li>
      <li><strong>詳細ペイン（右側）</strong>: 選択中メディアのプロパティやタグ情報の表示</li>
      <li><strong>ステータスバー</strong>: バックグラウンドタスクの進捗表示</li>
    </ul>
    <p>各ペイン間は GridSplitter で自由にリサイズ可能です。ウィンドウの背景には半透明の DesktopAcrylicBackdrop 効果が適用されています。</p>

    <h2>タブの使い方</h2>
    <p>ブラウザのようにタブを追加・削除して並行管理が可能です。</p>
    <ul>
      <li><strong>タブの名前変更</strong>: ダブルクリックまたはコンテキストメニューから「名前の変更」を選択します。</li>
      <li><strong>タブの移動</strong>: コンテキストメニューから「新しいウィンドウで開く」「別のウィンドウに移動」を選択できます。</li>
      <li><strong>タブ状態の永続化</strong>: ソート条件やフィルタ条件などの設定はタブごとに保存され、新しいタブは「デフォルトタブ状態」を引き継ぎます。</li>
    </ul>

    <h2>ファイルの閲覧（ビューモード）</h2>
    <p>中央のビューアペインでは、以下の4つのモードでメディアを閲覧できます：</p>
    <table>
      <thead>
        <tr>
          <th>ビュー</th>
          <th>説明</th>
        </tr>
      </thead>
      <tbody>
        <tr>
          <td><strong>グリッド表示 (Wrap)</strong></td>
          <td>サムネイルをタイル状に並べます。サイズスライダーで拡大縮小可能で、ファイル名などのオーバーレイ表示を切り替えられます。</td>
        </tr>
        <tr>
          <td><strong>リスト表示 (List)</strong></td>
          <td>テーブル形式で表示します。ファイル名、解像度、ファイルサイズ、作成日時、評価などの列があり、カスタマイズ可能です。</td>
        </tr>
        <tr>
          <td><strong>詳細表示 (Detail)</strong></td>
          <td>選択ファイルの大きなプレビューを表示します。</td>
        </tr>
        <tr>
          <td><strong>マップ表示 (Map)</strong></td>
          <td>位置情報(GPS)を持つファイルを地図上にピン表示します。</td>
        </tr>
      </tbody>
    </table>
  </div>

  <div v-else>
    <h1>2. Basic Operations</h1>

    <h2>Main Window Layout</h2>
    <p>The main window is organized into the following zones:</p>
    <ul>
      <li><strong>Title Bar</strong>: App icon, navigation menu, and notification button</li>
      <li><strong>Tab Bar</strong>: Switch and add tabs, plus system notifications via InfoBar</li>
      <li><strong>Repository Pane (Top Left)</strong>: Folder or album tree view</li>
      <li><strong>Sort &amp; Filter Pane (Bottom Left)</strong>: Configure sorting and filtering conditions</li>
      <li><strong>Viewer Pane (Center)</strong>: Media display (Grid / List / Detail / Map)</li>
      <li><strong>Detail Pane (Right)</strong>: Properties and tag information for the selected media</li>
      <li><strong>Status Bar</strong>: Background task progress indicator</li>
    </ul>
    <p>Each pane can be freely resized using <code>GridSplitter</code>. The window background uses a translucent <code>DesktopAcrylicBackdrop</code> effect.</p>

    <h2>Working with Tabs</h2>
    <p>Just like a web browser, you can add and remove tabs to manage multiple views in parallel.</p>
    <ul>
      <li><strong>Rename Tab</strong>: Double-click a tab or select "Rename" from its context menu.</li>
      <li><strong>Move Tab</strong>: Use the context menu to choose "Open in New Window" or "Move to Another Window".</li>
      <li><strong>Tab State Persistence</strong>: Sort, filter, and display settings are saved per tab, and new tabs inherit the "default tab state".</li>
    </ul>

    <h2>Browsing Files (View Modes)</h2>
    <p>The center Viewer Pane supports four display modes for browsing your media:</p>
    <table>
      <thead>
        <tr>
          <th>View</th>
          <th>Description</th>
        </tr>
      </thead>
      <tbody>
        <tr>
          <td><strong>Grid View (Wrap)</strong></td>
          <td>Arranges thumbnails in tiles. Scale sizes with a slider and toggle filename and info overlays.</td>
        </tr>
        <tr>
          <td><strong>List View (List)</strong></td>
          <td>Displays media in a table with customizable columns including filename, resolution, file size, creation date, and rating.</td>
        </tr>
        <tr>
          <td><strong>Detail View (Detail)</strong></td>
          <td>Shows a large preview of the currently selected file.</td>
        </tr>
        <tr>
          <td><strong>Map View (Map)</strong></td>
          <td>Plots files with GPS location data as pins on an interactive map.</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>
