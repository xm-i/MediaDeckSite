<template>
  <div>
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
          <th>ランタイム</th>
          <td>.NET 10</td>
        </tr>
        <tr>
          <th>UIフレームワーク</th>
          <td>WinUI 3 (Windows App SDK)</td>
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
          <td>Windows.Data.Pdf による高速表示・サムネイル生成</td>
        </tr>
        <tr>
          <td><strong>アーカイブ</strong></td>
          <td>ZIP, 7z, RAR 等</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>
