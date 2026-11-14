<script setup lang="ts">
import { useLocale } from '@/i18n'

const { locale } = useLocale()
</script>

<template>
  <div v-if="locale === 'ja'">
    <h1>5. タグ管理</h1>

    <h2>タグの追加・削除</h2>
    <p>メインウィンドウ右側の詳細ペインにある「タグ」タブから、選択したファイルに対してタグの追加や削除が行えます。</p>
    <ul>
      <li>入力中にはサジェストが表示され、タグ名、読み仮名、ローマ字、エイリアスから効率よく入力できます。</li>
      <li>ファイルに付与されているタグをクリックすると、そのタグが付いた他のファイルを即座に検索できます。</li>
    </ul>

    <h2>タグマネージャー</h2>
    <p>ツールメニューの「タグマネージャー」から専用ウィンドウを開き、タグ全体を高度に管理できます。</p>
    <ul>
      <li><strong>タグの作成・編集・削除</strong>: 誤って作成したタグの修正や不要なタグの整理が可能です。</li>
      <li><strong>タグカテゴリ</strong>: 膨大なタグをカテゴリごとに分類し、整理しやすくします。</li>
      <li><strong>タグ属性</strong>: タグには名前だけでなく、読み仮名(Ruby)、ローマ字(自動変換)、説明、使用回数などのメタデータを保持できます。</li>
      <li><strong>タグエイリアス</strong>: タグの別名（エイリアス）を追加でき、表記ゆれを吸収して検索性を向上させます。</li>
    </ul>
  </div>

  <div v-else>
    <h1>5. Tag Management</h1>

    <h2>Adding &amp; Removing Tags</h2>
    <p>Use the "Tags" tab in the right-hand Detail Pane to add or remove tags on the currently selected files.</p>
    <ul>
      <li>Live suggestions appear while typing, matching against tag names, phonetic readings (Ruby), Romaji, and aliases for fast entry.</li>
      <li>Clicking an existing tag on a file immediately searches for other files sharing that tag.</li>
    </ul>

    <h2>Tag Manager</h2>
    <p>Open "Tag Manager" from the Tools menu to manage your entire tag taxonomy in a dedicated window.</p>
    <ul>
      <li><strong>Create, Edit &amp; Delete Tags</strong>: Fix typos or clean up unused tags across your library.</li>
      <li><strong>Tag Categories</strong>: Group large tag collections into categories to keep them structured and easy to browse.</li>
      <li><strong>Tag Attributes</strong>: Each tag can store metadata beyond its name, including phonetic reading (Ruby), auto-converted Romaji, description, and usage count.</li>
      <li><strong>Tag Aliases</strong>: Add alternate names (aliases) to unify spelling variations and improve searchability.</li>
    </ul>
  </div>
</template>
