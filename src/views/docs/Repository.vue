<script setup lang="ts">
import { useLocale } from '@/i18n'

const { locale } = useLocale()
</script>

<template>
  <div v-if="locale === 'ja'">
    <h1>3. リポジトリ管理</h1>

    <p>MediaDeckでは、メディアファイルを管理するために「フォルダリポジトリ」と「アルバムリポジトリ」の2つのモードを利用できます。これらは左上のリポジトリペインから切り替えることができます。</p>

    <h2>フォルダリポジトリ</h2>
    <p>ファイルシステムのディレクトリ構造をそのままツリー表示するモードです。</p>
    <ul>
      <li>フォルダを選択すると、そのフォルダ内のファイルが表示範囲として絞り込まれます。</li>
      <li>サブフォルダ内のファイルを含めるかどうかの選択も可能です。</li>
      <li>スキャン対象のフォルダは「フォルダマネージャー」から登録・編集・削除できます。</li>
    </ul>

    <h2>アルバムリポジトリ</h2>
    <p>ユーザーが自由に作成できる仮想的なグループ（アルバム）でファイルを管理するモードです。</p>
    <ul>
      <li>ファイルをドラッグ＆ドロップするだけで、簡単にアルバムに追加できます。</li>
      <li>アルバムは階層構造（<code>/</code> 区切り）に対応しており、整理しやすくなっています。</li>
    </ul>

    <h2>フォルダマネージャー</h2>
    <p>ツールメニューの「フォルダマネージャー」から、MediaDeckに読み込ませる対象のルートフォルダを一元管理できます。ここで登録されたフォルダがスキャンされ、データベースに情報が構築されます。</p>
  </div>

  <div v-else>
    <h1>3. Repository Management</h1>

    <p>MediaDeck provides two repository modes for organizing your media files: "Folder Repository" and "Album Repository". You can switch between them in the top-left Repository Pane.</p>

    <h2>Folder Repository</h2>
    <p>Displays your filesystem directory structure directly as a navigable tree.</p>
    <ul>
      <li>Selecting a folder narrows the displayed view to the files inside that folder.</li>
      <li>You can toggle whether files inside subfolders are included.</li>
      <li>Scan target folders can be registered, edited, and removed in the "Folder Manager".</li>
    </ul>

    <h2>Album Repository</h2>
    <p>Organizes files into flexible, virtual groups (albums) that you can create freely.</p>
    <ul>
      <li>Simply drag and drop files to add them to an album.</li>
      <li>Albums support hierarchical nesting using <code>/</code> separators for clean organization.</li>
    </ul>

    <h2>Folder Manager</h2>
    <p>Open "Folder Manager" from the Tools menu to centrally manage the root folders scanned by MediaDeck. Registered folders are scanned and indexed into the library database.</p>
  </div>
</template>
