<script setup lang="ts">
import { useLocale } from '@/i18n'

const { locale } = useLocale()
</script>

<template>
  <div v-if="locale === 'ja'">
    <h1>8. 設定</h1>

    <p>ナビゲーションメニューの「設定」から、MediaDeckの各種動作をカスタマイズできます。</p>

    <table>
      <thead>
        <tr>
          <th>設定ページ</th>
          <th>内容</th>
        </tr>
      </thead>
      <tbody>
        <tr>
          <td><strong>スキャン設定</strong></td>
          <td>対象リポジトリのパスや、スキャン対象とする拡張子の管理を行います。不要な形式のファイルをスキャンから除外できます。</td>
        </tr>
        <tr>
          <td><strong>検索設定</strong></td>
          <td>UIのパフォーマンスを最適化するための、初回読み込み件数、増分読み込み件数、最大読み込み件数を設定します。</td>
        </tr>
        <tr>
          <td><strong>実行プログラム設定</strong></td>
          <td>メディアをダブルクリックした際に起動する外部プログラム（VLCメディアプレイヤーなど）を、メディアタイプごとに設定できます。</td>
        </tr>
        <tr>
          <td><strong>サムネイル設定</strong></td>
          <td>生成されるサムネイルの解像度サイズや品質（圧縮率）を設定します。</td>
        </tr>
        <tr>
          <td><strong>言語設定</strong></td>
          <td>アプリケーションの表示言語（日本語 / 英語）を切り替えます。「システム既定」に設定することで、OSの言語設定に追従させることも可能です。</td>
        </tr>
        <tr>
          <td><strong>環境設定</strong></td>
          <td>その他の全体的な環境設定を行います。</td>
        </tr>
      </tbody>
    </table>
  </div>

  <div v-else>
    <h1>8. Settings</h1>

    <p>Customize MediaDeck's behavior from "Settings" in the navigation menu.</p>

    <table>
      <thead>
        <tr>
          <th>Settings Page</th>
          <th>Description</th>
        </tr>
      </thead>
      <tbody>
        <tr>
          <td><strong>Scan Settings</strong></td>
          <td>Manage target repository paths and scanned file extensions, allowing you to exclude unwanted formats from scanning.</td>
        </tr>
        <tr>
          <td><strong>Search Settings</strong></td>
          <td>Configure initial load count, incremental load count, and maximum load count to optimize UI responsiveness.</td>
        </tr>
        <tr>
          <td><strong>External Program Settings</strong></td>
          <td>Assign external programs (such as VLC Media Player) per media type to launch when double-clicking files.</td>
        </tr>
        <tr>
          <td><strong>Thumbnail Settings</strong></td>
          <td>Configure resolution size and compression quality for generated thumbnails.</td>
        </tr>
        <tr>
          <td><strong>Language Settings</strong></td>
          <td>Switch the application display language (Japanese / English), or set it to "System Default" to follow your OS language.</td>
        </tr>
        <tr>
          <td><strong>Environment Settings</strong></td>
          <td>Configure other global application and environment preferences.</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>
