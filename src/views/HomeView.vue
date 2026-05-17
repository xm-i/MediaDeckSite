<script setup lang="ts">
import { ref, onMounted, onUnmounted } from 'vue'
import '../index.css'
import mainImage from '../images/main.png'
import mapImage from '../images/map.png'

const features = [
  {
    title: 'リポジトリ管理',
    description: '複数のローカルフォルダをリポジトリとして登録し、一括して管理。',
    icon: 'folder'
  },
  {
    title: '高速検索・フィルタリング',
    description: 'SQLiteと非同期fetchの最適化により、数万件のライブラリも待機時間なく瞬時にフィルタリング。',
    icon: 'zap'
  },
  {
    title: '高度なプロパティ検索',
    description: 'prop. プレフィックスを用いた詳細なプロパティ比較（解像度、ファイルサイズ、日付、評価等）を直感的に操作可能。',
    icon: 'sliders'
  },
  {
    title: 'ストリーミング読み込み',
    description: 'IAsyncEnumerable を活用した読み込みにより、UIをブロックせずバックグラウンドで逐次表示。',
    icon: 'activity'
  },
  {
    title: '自動メタデータ・タグ管理',
    description: 'Magick.NET や FFMpegCore による自動抽出。タグカテゴリ、ふりがな、エイリアス機能による高度な整理。',
    icon: 'tag'
  },
  {
    title: '多彩なビューア',
    description: 'リスト表示、グリッド表示、詳細表示に加え、位置情報を利用したマップ表示に対応。',
    icon: 'layout'
  }
]

const mediaTypes = [
  { name: '画像', formats: 'JPEG, PNG, GIF, BMP, TIFF, HEIF, PSD, RAW (RAF), ICO, PCX, Netpbm 等', icon: 'image' },
  { name: '動画', formats: 'MP4, MKV, AVI, MOV 等 (FFMpegがサポートする形式)', icon: 'video' },
  { name: 'PDF', formats: 'Windows.Data.Pdf を利用した高速な表示とサムネイル生成', icon: 'file-text' },
  { name: 'アーカイブ', formats: 'ZIP, 7z, RAR 等', icon: 'archive' },
]

const techStack = [
  '.NET 10', 'WinUI 3', 'R3', 'SQLite & EF Core', 'Magick.NET', 'FFMpegCore', 'Serilog', 'AutoDiAttributes'
]

const updateMousePos = (e: MouseEvent) => {
  const cards = document.querySelectorAll('.feature-card')
  cards.forEach((card) => {
    const rect = (card as HTMLElement).getBoundingClientRect()
    const x = e.clientX - rect.left
    const y = e.clientY - rect.top
    ;(card as HTMLElement).style.setProperty('--mouse-x', `${x}px`)
    ;(card as HTMLElement).style.setProperty('--mouse-y', `${y}px`)
  })
}

onMounted(() => {
  window.addEventListener('mousemove', updateMousePos)
})

onUnmounted(() => {
  window.removeEventListener('mousemove', updateMousePos)
})
</script>

<template>
  <main>
    <section class="hero container text-center">
      <div class="badge animate-fade-in">
        <span class="pulse"></span>
        <span>v1.0 is now available</span>
      </div>
      <h1 class="hero-title animate-fade-in delay-100">
        <span class="text-gradient">メディア管理を</span><br>
        <span class="text-accent-gradient">もっとスマートに</span>
      </h1>
      <p class="hero-subtitle animate-fade-in delay-200">
        数万件のメディアも瞬時に検索。画像や動画、PDF、アーカイブをWindowsで軽快に管理。
      </p>
      <div class="hero-actions animate-fade-in delay-300">
        <a href="https://github.com/xm-i/MediaDeck/releases" class="btn btn-primary btn-lg">
          ダウンロード
          <svg width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2.5" stroke-linecap="round" stroke-linejoin="round"><path d="M5 12h14"/><path d="m12 5 7 7-7 7"/></svg>
        </a>
        <a href="#features" class="btn btn-secondary btn-lg">機能を詳しく見る</a>
      </div>
    </section>

    <section class="screenshots container">
      <div class="screenshot-wrapper animate-fade-in delay-500">
        <div class="screenshot-container glass-panel">
          <img :src="mainImage" alt="MediaDeck Main Window" class="screenshot-img" />
        </div>
        <div class="screenshot-glow"></div>
      </div>
    </section>

    <section id="features" class="features container">
      <div class="section-header text-center">
        <h2 class="section-title animate-fade-in">使い勝手にこだわった機能</h2>
        <p class="section-subtitle animate-fade-in delay-100">Windowsネイティブならではの軽快なメディア体験を提供します。</p>
      </div>
      
      <div class="features-grid">
        <div v-for="(feature, index) in features" :key="index" :class="['feature-card', 'glass-panel', 'animate-fade-in', 'delay-' + (200 + index * 100)]">
          <div class="feature-icon">
            <svg v-if="feature.icon === 'folder'" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M4 20h16a2 2 0 0 0 2-2V8a2 2 0 0 0-2-2h-7.93a2 2 0 0 1-1.66-.9l-.82-1.2A2 2 0 0 0 7.93 3H4a2 2 0 0 0-2 2v13c0 1.1.9 2 2 2Z"/></svg>
            <svg v-else-if="feature.icon === 'zap'" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><polygon points="13 2 3 14 12 14 11 22 21 10 12 10 13 2"/></svg>
            <svg v-else-if="feature.icon === 'sliders'" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><line x1="4" x2="20" y1="21" y2="14"/><line x1="4" x2="20" y1="10" y2="3"/><line x1="12" x2="12" y1="21" y2="12"/><line x1="8" x2="8" y1="8" y2="3"/><line x1="16" x2="16" y1="21" y2="16"/><line x1="20" x2="20" y1="12" y2="3"/><line x1="2" x2="22" y1="14" y2="14"/><line x1="2" x2="22" y1="8" y2="8"/></svg>
            <svg v-else-if="feature.icon === 'activity'" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M22 12h-4l-3 9L9 3l-3 9H2"/></svg>
            <svg v-else-if="feature.icon === 'tag'" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M12.586 2.586A2 2 0 0 0 11.172 2H4a2 2 0 0 0-2 2v7.172a2 2 0 0 0 .586 1.414l8.704 8.704a2.426 2.426 0 0 0 3.42 0l6.58-6.58a2.426 2.426 0 0 0 0-3.42l-8.704-8.704z"/><circle cx="7.5" cy="7.5" r=".5" fill="currentColor"/></svg>
            <svg v-else-if="feature.icon === 'layout'" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><rect width="18" height="18" x="3" y="3" rx="2" ry="2"/><line x1="3" x2="21" y1="9" y2="9"/><line x1="9" x2="9" y1="21" y2="9"/></svg>
          </div>
          <h3 class="feature-title">{{ feature.title }}</h3>
          <p class="feature-desc">{{ feature.description }}</p>
        </div>
      </div>
    </section>

    <section class="map-view container overflow-hidden">
      <div class="map-content">
        <div class="text-content animate-fade-in">
          <h2 class="section-title">地図で思い出を振り返る</h2>
          <p class="section-subtitle">GPS情報を自動抽出し、メディアを地図上にマッピング。撮影地をたどる新しいブラウジング体験が楽しめます。</p>
          <ul class="feature-list">
            <li><span>✔</span> 高精度な座標マッピング</li>
            <li><span>✔</span> クラスタリング表示</li>
            <li><span>✔</span> 撮影地からの逆引き検索</li>
          </ul>
        </div>
        <div class="map-image-wrapper glass-panel animate-fade-in delay-200">
           <img :src="mapImage" alt="Map View" class="map-img" />
        </div>
      </div>
    </section>

    <section id="media" class="media-types container">
      <div class="section-header text-center">
        <h2 class="section-title">画像も動画もこれひとつで</h2>
        <p class="section-subtitle">RAW画像からアーカイブまで、あらゆるメディアに対応。</p>
      </div>
      
      <div class="media-grid">
        <div v-for="(media, index) in mediaTypes" :key="index" class="media-card glass-panel">
          <div class="media-header">
            <div class="media-icon">
              <svg v-if="media.icon === 'image'" width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><rect width="18" height="18" x="3" y="3" rx="2" ry="2"/><circle cx="9" cy="9" r="2"/><path d="m21 15-3.086-3.086a2 2 0 0 0-2.828 0L6 21"/></svg>
              <svg v-else-if="media.icon === 'video'" width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="m22 8-6 4 6 4V8Z"/><rect width="14" height="12" x="2" y="6" rx="2" ry="2"/></svg>
              <svg v-else-if="media.icon === 'file-text'" width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><path d="M14.5 2H6a2 2 0 0 0-2 2v16a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V7.5L14.5 2z"/><polyline points="14 2 14 8 20 8"/><line x1="16" x2="8" y1="13" y2="13"/><line x1="16" x2="8" y1="17" y2="17"/><line x1="10" x2="8" y1="9" y2="9"/></svg>
              <svg v-else-if="media.icon === 'archive'" width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round"><rect width="20" height="5" x="2" y="4" rx="2"/><path d="M4 9v9a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V9"/><path d="M10 13h4"/></svg>
            </div>
            <h3 class="media-title">{{ media.name }}</h3>
          </div>
          <p class="media-formats">{{ media.formats }}</p>
        </div>
      </div>
    </section>

    <section id="stack" class="tech-stack container">
      <div class="tech-card glass-panel text-center">
        <h2 class="section-title">モダンな技術スタック</h2>
        <p class="section-subtitle mb-8">最新の.NETエコシステムを採用し、パフォーマンスと保守性を追求しています。</p>
        
        <div class="stack-tags">
          <span v-for="tech in techStack" :key="tech" class="stack-tag">{{ tech }}</span>
        </div>
      </div>
    </section>
  </main>
</template>

<style scoped>
/* Sections */
section {
  padding: 8rem 0;
}

.hero {
  padding-top: 12rem;
  padding-bottom: 4rem;
}

.badge {
  display: inline-flex;
  align-items: center;
  gap: 0.5rem;
  padding: 0.5rem 1.25rem;
  background: rgba(0, 163, 255, 0.1);
  border: 1px solid rgba(0, 163, 255, 0.2);
  color: #00a3ff;
  border-radius: 9999px;
  font-size: 0.8125rem;
  font-weight: 600;
  margin-bottom: 2.5rem;
  letter-spacing: 0.02em;
}

.pulse {
  width: 6px;
  height: 6px;
  background: #00a3ff;
  border-radius: 50%;
  position: relative;
}

.pulse::after {
  content: '';
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background: inherit;
  border-radius: inherit;
  animation: badge-pulse 2s ease-out infinite;
}

@keyframes badge-pulse {
  0% { transform: scale(1); opacity: 1; }
  100% { transform: scale(3); opacity: 0; }
}

.hero-title {
  font-size: clamp(3.5rem, 8vw, 6rem);
  margin-bottom: 1.5rem;
  font-weight: 800;
  line-height: 1.1;
}

.hero-subtitle {
  font-size: clamp(1.25rem, 2.5vw, 1.5rem);
  color: var(--text-secondary);
  max-width: 800px;
  margin: 0 auto 3.5rem;
  line-height: 1.5;
}

.hero-actions {
  display: flex;
  justify-content: center;
  gap: 1.5rem;
}

.section-header {
  margin-bottom: 4rem;
}

.section-title {
  font-size: 2.5rem;
  margin-bottom: 1rem;
}

.section-subtitle {
  font-size: 1.125rem;
  color: var(--text-secondary);
}

/* Screenshots */
.screenshot-wrapper {
  position: relative;
  padding: 2rem 0;
  perspective: 1000px;
}

.screenshot-container {
  padding: 0.5rem;
  border-radius: 0;
  overflow: hidden;
  box-shadow: 0 30px 60px rgba(0,0,0,0.6), 0 0 0 1px rgba(255,255,255,0.05);
  animation: float 6s ease-in-out infinite;
  position: relative;
  z-index: 2;
  width: fit-content;
  margin: 0 auto;
}

.screenshot-glow {
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  width: 80%;
  height: 80%;
  background: var(--accent-glow);
  filter: blur(100px);
  z-index: 1;
  opacity: 0.5;
}

.screenshot-img {
  max-width: 100%;
  height: auto;
  display: block;
  border-radius: 0;
}

.overflow-hidden {
  overflow: hidden;
}

.feature-list {
  list-style: none;
  margin-top: 2rem;
  display: flex;
  flex-direction: column;
  gap: 1rem;
}

.feature-list li {
  display: flex;
  align-items: center;
  gap: 0.75rem;
  color: var(--text-secondary);
  font-weight: 500;
}
.feature-list li span {
  color: #00a3ff;
}
/* Features */
.features-grid {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(300px, 1fr));
  gap: 1.5rem;
}

.feature-card {
  padding: 2.5rem;
  position: relative;
  overflow: hidden;
  transition: all 0.4s cubic-bezier(0.16, 1, 0.3, 1);
  border: 1px solid rgba(255, 255, 255, 0.05);
}

.feature-card::before {
  content: '';
  position: absolute;
  inset: 0;
  background: radial-gradient(800px circle at var(--mouse-x) var(--mouse-y), rgba(255, 255, 255, 0.06), transparent 40%);
  opacity: 0;
  transition: opacity 0.5s;
  pointer-events: none;
}

.feature-card:hover {
  transform: translateY(-8px);
  border-color: rgba(0, 163, 255, 0.3);
  background: rgba(20, 20, 20, 0.4);
  box-shadow: 0 20px 40px rgba(0, 0, 0, 0.4);
}

.feature-card:hover::before {
  opacity: 1;
}

.feature-icon {
  width: 56px;
  height: 56px;
  background: linear-gradient(135deg, rgba(0, 163, 255, 0.2) 0%, rgba(0, 210, 255, 0.2) 100%);
  color: #00a3ff;
  border-radius: 16px;
  display: flex;
  align-items: center;
  justify-content: center;
  margin-bottom: 2rem;
  border: 1px solid rgba(0, 163, 255, 0.3);
}

.feature-title {
  font-size: 1.25rem;
  margin-bottom: 0.75rem;
}

.feature-desc {
  color: var(--text-secondary);
  font-size: 0.9375rem;
}

/* Map View Section */
.map-view {
  padding-top: 4rem;
}

.map-content {
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap: 4rem;
  align-items: center;
}

.map-image-wrapper {
  padding: 0.5rem;
  border-radius: 20px;
}

.map-img {
  width: 100%;
  border-radius: 12px;
  display: block;
}

/* Media Types */
.media-grid {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(240px, 1fr));
  gap: 1.5rem;
}

.media-card {
  padding: 1.5rem;
}

.media-header {
  display: flex;
  align-items: center;
  gap: 0.75rem;
  margin-bottom: 1rem;
}

.media-icon {
  color: var(--text-secondary);
}

.media-title {
  font-size: 1.125rem;
}

.media-formats {
  color: var(--text-muted);
  font-size: 0.875rem;
}

/* Tech Stack */
.tech-card {
  padding: 4rem 2rem;
}

.stack-tags {
  display: flex;
  flex-wrap: wrap;
  justify-content: center;
  gap: 0.75rem;
}

.stack-tag {
  padding: 0.5rem 1rem;
  background: rgba(255, 255, 255, 0.05);
  border: 1px solid rgba(255, 255, 255, 0.1);
  border-radius: 8px;
  font-size: 0.875rem;
  color: var(--text-secondary);
  transition: color 0.2s ease, border-color 0.2s ease;
}

.stack-tag:hover {
  color: var(--text-primary);
  border-color: rgba(255, 255, 255, 0.2);
}

.mb-8 { margin-bottom: 2rem; }

@media (max-width: 768px) {
  .map-content { grid-template-columns: 1fr; }
  .hero-title { font-size: 2.5rem; }
}
</style>
