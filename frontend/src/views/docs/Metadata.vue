<script setup lang="ts">
import { useLocale } from '@/i18n'

const { locale } = useLocale()
</script>

<template>
  <div v-if="locale === 'ja'">
    <h1>6. 詳細情報・メタデータ</h1>

    <p>メインウィンドウ右側の詳細ペイン（プロパティタブ）では、選択したファイルの詳細な情報を確認・編集できます。</p>

    <h2>プロパティの確認と編集</h2>
    <ul>
      <li><strong>基本情報</strong>: ファイル名、解像度、ファイルサイズ、作成日時などの基本的な情報を一目で確認できます。</li>
      <li><strong>複数選択時の集計</strong>: 複数のファイルを選択した状態では、合計ファイルサイズや共通のプロパティが集計されて表示されます。</li>
      <li><strong>評価 (Rate) の設定</strong>: 1〜5のスターでファイルを評価でき、フィルタリングやソートに活用できます。</li>
      <li><strong>説明 (Description) の編集</strong>: ファイルごとに任意のテキスト説明を追加・編集できます。</li>
      <li><strong>使用回数</strong>: ファイルがプレビューされた回数などの統計情報も確認できます。</li>
    </ul>

    <h2>メタデータの閲覧</h2>
    <p>画像や動画ファイルに埋め込まれている高度なメタデータ（EXIF、XMP、IPTC等）を閲覧することができます。カメラの機種や撮影設定など、詳細な情報を確認したい場合に便利です。</p>
  </div>

  <div v-else>
    <h1>6. Details &amp; Metadata</h1>

    <p>In the "Properties" tab of the right-hand Detail Pane, you can inspect and edit detailed information for the selected files.</p>

    <h2>Viewing &amp; Editing Properties</h2>
    <ul>
      <li><strong>Basic Information</strong>: Check filename, resolution, file size, creation date, and more at a glance.</li>
      <li><strong>Multi-Selection Aggregation</strong>: When multiple files are selected, total file size and shared properties are summarized automatically.</li>
      <li><strong>Setting Ratings (Rate)</strong>: Rate files from 1 to 5 stars for use in filters and sorting.</li>
      <li><strong>Editing Descriptions</strong>: Add or update custom text descriptions for individual files.</li>
      <li><strong>Usage Count</strong>: View usage statistics such as how many times a file has been previewed.</li>
    </ul>

    <h2>Viewing Embedded Metadata</h2>
    <p>Browse rich metadata embedded in image and video files (EXIF, XMP, IPTC, etc.), making it easy to check camera models, exposure settings, and other capture details.</p>
  </div>
</template>
