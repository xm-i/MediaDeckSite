<script setup lang="ts">
import { useLocale } from '@/i18n'

const { locale } = useLocale()
</script>

<template>
  <div v-if="locale === 'ja'">
    <h1>9. マップビュー</h1>

    <p>スマートフォンやGPS対応カメラで撮影された、位置情報を含む写真を地図上で直感的に閲覧できるビューモードです。</p>

    <h2>位置情報の活用</h2>
    <ul>
      <li><strong>自動抽出</strong>: 写真のEXIF情報などからGPS座標（緯度・経度）を自動的に抽出し、データベースに保存します。</li>
      <li><strong>地図へのマッピング</strong>: 中央のビューアペインで「マップ表示」を選択すると、位置情報を持つメディアが地図上にピンとして表示されます。</li>
      <li><strong>クラスタリング</strong>: 近接する場所で撮影された複数の写真は、縮尺に合わせて自動的にグループ化（クラスタリング）され、見やすく表示されます。</li>
    </ul>

    <h2>新しいブラウジング体験</h2>
    <p>「あの時、あの場所で撮った写真」という記憶を頼りに、地図をスクロールやズームしながら写真を探し出すことができます。旅行の軌跡をたどるような、リストやグリッドとは異なる新しいメディアの振り返り方が可能です。</p>
  </div>

  <div v-else>
    <h1>9. Map View</h1>

    <p>An intuitive view mode for browsing photos captured with smartphones or GPS-enabled cameras directly on a map.</p>

    <h2>Leveraging Location Data</h2>
    <ul>
      <li><strong>Automatic Extraction</strong>: Automatically reads GPS coordinates (latitude and longitude) from EXIF metadata and stores them in the database.</li>
      <li><strong>Map Plotting</strong>: Selecting "Map View" in the center Viewer Pane plots media containing location data as pins on the map.</li>
      <li><strong>Clustering</strong>: Multiple photos taken near each other are automatically grouped (clustered) according to the map zoom level for clean visibility.</li>
    </ul>

    <h2>A New Way to Browse</h2>
    <p>Find photos by panning and zooming across places you remember visiting, letting you retrace trips and explore your library spatially rather than only by folder or list.</p>
  </div>
</template>
