<script setup lang="ts">
import { useLocale } from '@/i18n'

const { locale } = useLocale()
</script>

<template>
  <div v-if="locale === 'ja'">
    <h1>1. はじめに</h1>

    <h2>MediaDeckとは</h2>
    <p>MediaDeckは、画像、動画、PDF、アーカイブファイルなどを一元管理し、高度な検索・タグ付け・メタデータ管理機能を提供するWindows向けデスクトップアプリケーションです。</p>

    <table>
      <tbody>
        <tr>
          <th>種別</th>
          <td>Windows向け高機能メディア管理アプリケーション</td>
        </tr>
        <tr>
          <th>対応OS</th>
          <td>Windows 10 (1809) 以降</td>
        </tr>
        <tr>
          <th>言語対応</th>
          <td>日本語 / 英語（アプリ内で切り替え可能、システム既定にも追従）</td>
        </tr>
      </tbody>
    </table>

    <h2>対応メディアタイプ</h2>
    <table>
      <thead>
        <tr>
          <th>カテゴリ</th>
          <th>対応フォーマット</th>
        </tr>
      </thead>
      <tbody>
        <tr>
          <td><strong>画像</strong></td>
          <td>JPEG, PNG, GIF, BMP, TIFF, HEIF, PSD, RAW (RAF), ICO, PCX, Netpbm 等</td>
        </tr>
        <tr>
          <td><strong>動画</strong></td>
          <td>MP4, MKV, AVI, MOV 等 (FFMpeg がサポートする形式)</td>
        </tr>
        <tr>
          <td><strong>PDF</strong></td>
          <td>高速なページ表示・サムネイル生成</td>
        </tr>
        <tr>
          <td><strong>アーカイブ</strong></td>
          <td>ZIP, 7z, RAR 等</td>
        </tr>
      </tbody>
    </table>
  </div>

  <div v-else>
    <h1>1. Getting Started</h1>

    <h2>What is MediaDeck?</h2>
    <p>MediaDeck is a desktop application for Windows that provides unified management of images, videos, PDFs, and archive files, along with advanced search, tagging, and metadata management features.</p>

    <table>
      <tbody>
        <tr>
          <th>Category</th>
          <td>Advanced Media Management Application for Windows</td>
        </tr>
        <tr>
          <th>Supported OS</th>
          <td>Windows 10 (1809) or later</td>
        </tr>
        <tr>
          <th>Language Support</th>
          <td>Japanese / English (Switchable in-app or follows system default)</td>
        </tr>
      </tbody>
    </table>

    <h2>Supported Media Types</h2>
    <table>
      <thead>
        <tr>
          <th>Category</th>
          <th>Supported Formats</th>
        </tr>
      </thead>
      <tbody>
        <tr>
          <td><strong>Images</strong></td>
          <td>JPEG, PNG, GIF, BMP, TIFF, HEIF, PSD, RAW (RAF), ICO, PCX, Netpbm, etc.</td>
        </tr>
        <tr>
          <td><strong>Videos</strong></td>
          <td>MP4, MKV, AVI, MOV, etc. (Formats supported by FFmpeg)</td>
        </tr>
        <tr>
          <td><strong>PDF</strong></td>
          <td>Fast page rendering and thumbnail generation</td>
        </tr>
        <tr>
          <td><strong>Archives</strong></td>
          <td>ZIP, 7z, RAR, etc.</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>
