<script setup lang="ts">
import { useLocale } from '@/i18n'

const { locale } = useLocale()
</script>

<template>
  <div v-if="locale === 'ja'">
    <h1>7. ツール</h1>

    <h2>重複ファイル検出</h2>
    <p>ツールの「重複ファイル検出」から専用ウィンドウを開き、無駄なストレージ容量を占有している重複ファイルを見つけ出すことができます。</p>
    <ul>
      <li><strong>ファイルのハッシュ値ベース</strong>: ファイル名が異なっていても、中身が同じファイルを正確に検出します。</li>
      <li><strong>PreHashとFullHash</strong>: 高速な部分ハッシュ（PreHash）で概算の重複チェックを行い、疑わしいファイルに対して完全ハッシュ（FullHash）を行うことで、高速かつ正確な検出を実現しています。</li>
      <li>重複グループ数や重複ファイル総数の分かりやすい表示により、不要なファイルの削除作業をサポートします。</li>
    </ul>

    <h2>サムネイル管理</h2>
    <p>MediaDeckのサムネイル生成と管理をサポートするツールです。</p>
    <ul>
      <li><strong>サムネイル一括再生成</strong>: 表示が崩れていたり、サイズを変更したい場合に、サムネイルをバックグラウンドで一括再生成できます。</li>
      <li><strong>サムネイルピッカー</strong>: 動画ファイルなどのサムネイルとして、自動抽出されたフレームではなく、好みのシーンをサムネイルとして選択・設定できます。</li>
    </ul>

    <h2>ファイル変更の同期</h2>
    <p>エクスプローラーなど、MediaDeckの外部で行われたファイルの移動、削除、名前の変更を自動的に検出します。</p>
    <ul>
      <li>変更が検出されると、タイトルバーにパルスアニメーション付きの通知ボタンが表示されます。</li>
      <li>通知をクリックすると同期ウィンドウが開き、変更内容をリストで確認できます。</li>
      <li>変更内容をMediaDeckのデータベースに「反映」するか、または「無視」するかを個別、あるいは一括で選択できます。</li>
      <li>「移動のみ」「削除のみ」など、変更の種類に応じてフィルター表示が可能です。</li>
    </ul>
  </div>

  <div v-else>
    <h1>7. Tools</h1>

    <h2>Duplicate File Detection</h2>
    <p>Open "Duplicate Detection" from the Tools menu to locate duplicate files consuming unnecessary storage space.</p>
    <ul>
      <li><strong>Hash-Based Comparison</strong>: Accurately identifies files with identical content even if their filenames differ.</li>
      <li><strong>PreHash &amp; FullHash</strong>: Performs a fast partial hash check (<code>PreHash</code>) first, followed by a complete hash verification (<code>FullHash</code>) on potential matches for both speed and accuracy.</li>
      <li>Displays duplicate group counts and total duplicate file counts to assist with cleanup.</li>
    </ul>

    <h2>Thumbnail Management</h2>
    <p>Utilities for generating and customizing thumbnails in MediaDeck.</p>
    <ul>
      <li><strong>Bulk Thumbnail Regeneration</strong>: Regenerate thumbnails in the background when appearances look broken or after changing thumbnail size settings.</li>
      <li><strong>Thumbnail Picker</strong>: Choose a specific frame from a video as its custom thumbnail instead of relying on the automatically extracted frame.</li>
    </ul>

    <h2>File Change Synchronization</h2>
    <p>Automatically detects file moves, deletions, and renames performed outside MediaDeck (such as in File Explorer).</p>
    <ul>
      <li>When external changes are detected, a pulsing notification button appears in the title bar.</li>
      <li>Clicking the notification opens the synchronization window where you can review all detected changes in a list.</li>
      <li>Choose to "Apply" or "Ignore" changes in the MediaDeck database individually or in bulk.</li>
      <li>Filter the list by change type (such as Moves only or Deletions only).</li>
    </ul>
  </div>
</template>
