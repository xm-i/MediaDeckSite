<script setup lang="ts">
import { computed } from 'vue'
import { useLocale } from '@/i18n'
import mainImage from '../images/main.png'
import mapImage from '../images/map.png'

const { locale } = useLocale()

interface HighlightItem {
  label: string
  detail: string
}

interface BentoItem {
  id: string
  title: string
  description: string
  icon: 'folder-tree' | 'layers' | 'refresh'
}

interface ViewerMode {
  name: string
  key: string
  summary: string
}

interface MediaTypeSpec {
  name: string
  formats: string[]
  note: string
  icon: 'image' | 'video' | 'pdf' | 'archive'
}

interface SystemSpec {
  label: string
  value: string
}

const t = computed(() =>
  locale.value === 'ja'
    ? {
        heroMetaTag: 'Windows 10 / 11 対応 ・ 無料 / オープンソース',
        heroTitlePrefix: '数万件のメディアを、',
        heroTitleSuffix: '瞬時に検索・整理する',
        heroSubtitle:
          '画像・RAW・動画・PDF・アーカイブを一元管理。数万ファイルのライブラリでも待たされることなく、タグ付け・条件検索・マップでの振り返りを軽快に行えます。',
        downloadLatest: '最新リリースをダウンロード',
        readDocs: 'ドキュメントを読む',
        mainImageAlt: 'MediaDeck メインウィンドウの画面例（グリッド表示と検索バー、詳細ペイン）',
        highlightsAriaLabel: '主な特徴',
        featuresTitle: '高速検索と精緻なライブラリ管理',
        featuresSubtitle:
          'ペイン単位でリサイズ可能なマルチタブ構成と、大規模コレクションを前提とした検索・整理ワークフロー。',
        tokenSearchTitle: 'トークン型検索 & prop. プロパティ比較',
        tokenSearchDescBefore: '検索バーへの入力からタグ・フォルダ・アルバム・住所を即座にサジェスト。さらに',
        tokenSearchDescAfter:
          'プレフィックスで解像度・評価・ファイルサイズ・日時・メディア種別の比較条件をトークンとして組み合わせて絞り込めます。',
        mockTagChip: 'Tag: 風景',
        mockSuggestionHeader: 'プロパティ候補サジェスト',
        tagMgmtTitle: '階層タグ・読み仮名(Ruby)・エイリアス管理',
        tagMgmtDesc:
          'タグごとにカテゴリ・読み仮名（Ruby）・自動変換ローマ字・別名（エイリアス）を登録でき、表記ゆれを吸収したタグ検索と詳細ペインからの素早い付与に対応します。',
        mockTagCategory: 'カテゴリ: 場所・景観',
        mockTagMainName: '風景',
        mockTagReadingLabel: '読み仮名 / ローマ字',
        mockTagAliasLabel: 'エイリアス',
        mapSectionTitle: '4つのビューモードと地図ブラウジング',
        mapSectionSubtitle:
          '用途に応じた閲覧モードをワンクリックで切り替え。GPS情報を自動抽出し、撮影地ごとに写真をクラスタリング表示します。',
        mapImageAlt: 'MediaDeck マップビュー（位置情報に基づく写真のピン表示とクラスタリング）',
        mapCaption: 'マップ表示 — GPS座標によるピン留めとクラスター集計',
        mediaSectionTitle: '画像も動画も、これひとつで管理',
        mediaSectionSubtitle:
          '写真やRAWデータから動画・PDF・圧縮書庫まで、普段使うファイルをひとつのライブラリで快適に扱えます。',
        specsTitle: 'システム要件と導入ガイド',
        specsSubtitle:
          'Windows 環境ですぐに使い始められます。導入方法や操作手順はドキュメントをご確認ください。',
      }
    : {
        heroMetaTag: 'For Windows 10 / 11 · Free & Open Source',
        heroTitlePrefix: 'Search and Organize Tens of Thousands ',
        heroTitleSuffix: 'of Media Files Instantly',
        heroSubtitle:
          'Manage images, RAW photos, videos, PDFs, and archives in one unified library. Tag, filter by complex properties, and browse on a map without waiting—even across tens of thousands of files.',
        downloadLatest: 'Download Latest Release',
        readDocs: 'Read Documentation',
        mainImageAlt: 'MediaDeck main window showing grid view, search bar, and detail pane',
        highlightsAriaLabel: 'Key highlights',
        featuresTitle: 'High-Speed Search & Precision Library Management',
        featuresSubtitle:
          'Resizable multi-pane, multi-tab workspace engineered for searching and organizing large-scale media collections.',
        tokenSearchTitle: 'Token Search & prop. Property Comparison',
        tokenSearchDescBefore:
          'Instantly suggests tags, folders, albums, and addresses as you type. Combine property comparisons for resolution, rating, file size, dates, and media type as tokens using the ',
        tokenSearchDescAfter: ' prefix.',
        mockTagChip: 'Tag: Landscape',
        mockSuggestionHeader: 'Property Suggestions',
        tagMgmtTitle: 'Hierarchical Tags, Reading (Ruby) & Aliases',
        tagMgmtDesc:
          'Assign categories, phonetic readings (Ruby), auto-converted Romaji, and aliases to each tag for flexible lookup and rapid tagging directly from the detail pane.',
        mockTagCategory: 'Category: Places & Scenery',
        mockTagMainName: '風景 (Landscape)',
        mockTagReadingLabel: 'Reading / Romaji',
        mockTagAliasLabel: 'Aliases',
        mapSectionTitle: 'Four View Modes & Map Browsing',
        mapSectionSubtitle:
          'Switch viewing modes in one click. Automatically extracts GPS coordinates and clusters photos by capture location.',
        mapImageAlt: 'MediaDeck Map View showing photo pins and location clustering',
        mapCaption: 'Map View — GPS pin plotting and zoom-aware clustering',
        mediaSectionTitle: 'Manage Images, Videos, and More in One Place',
        mediaSectionSubtitle:
          'From photos and RAW camera files to videos, PDFs, and compressed archives—manage everyday files smoothly in one library.',
        specsTitle: 'System Requirements & Getting Started',
        specsSubtitle:
          'Ready to run on Windows. Check the documentation for installation instructions and workflows.',
      }
)

const coreHighlights = computed<HighlightItem[]>(() =>
  locale.value === 'ja'
    ? [
        {
          label: '大量ファイルでも待たない表示',
          detail: '読み込み中も操作が止まらず、数万件のメディアをスムーズに一覧・プレビュー',
        },
        {
          label: '瞬時に絞り込める高速検索',
          detail: 'タグ・フォルダ・解像度・評価・撮影日などを組み合わせて即座にフィルタリング',
        },
        {
          label: 'マルチタブ・マルチウィンドウ',
          detail: 'ブラウザのように複数タブや別ウィンドウを開き、並行して整理作業が可能',
        },
      ]
    : [
        {
          label: 'Instant Browsing at Scale',
          detail:
            'Stream tens of thousands of media files smoothly without blocking UI interactions while loading',
        },
        {
          label: 'High-Speed Multi-Faceted Search',
          detail: 'Filter immediately by combining tags, folders, resolution, rating, and capture date',
        },
        {
          label: 'Multi-Tab & Multi-Window',
          detail:
            'Open multiple tabs or separate windows like a browser to organize collections in parallel',
        },
      ]
)

const searchSuggestionRows = computed(() =>
  locale.value === 'ja'
    ? [
        { token: 'prop.CreationTime >= 2026-01-01', type: '日時' },
        { token: 'prop.FileSize > 100MB', type: '数値' },
        { token: 'prop.MediaType = Image', type: '種別' },
      ]
    : [
        { token: 'prop.CreationTime >= 2026-01-01', type: 'Date' },
        { token: 'prop.FileSize > 100MB', type: 'Number' },
        { token: 'prop.MediaType = Image', type: 'Enum' },
      ]
)

const tagAliases = ['landscape', 'scenery', '自然景観']

const secondaryFeatures = computed<BentoItem[]>(() =>
  locale.value === 'ja'
    ? [
        {
          id: 'repository',
          title: 'リポジトリ管理（フォルダ & アルバム）',
          description:
            'OSのディレクトリ構造をそのまま辿るフォルダリポジトリ（サブフォルダ包含切り替え対応）と、ドラッグ＆ドロップや「/」区切りの階層で整理できる仮想アルバムリポジトリを切り替えて管理できます。',
          icon: 'folder-tree',
        },
        {
          id: 'duplicates',
          title: '重複ファイルの検出とサムネイル管理',
          description:
            'ファイル名が異なっていても中身が同じ重複ファイルを高速・正確に検出し、空き容量の確保をサポート。動画の好きなシーンを選べるサムネイルピッカーも搭載しています。',
          icon: 'layers',
        },
        {
          id: 'sync',
          title: '外部ファイル変更の自動検出・同期',
          description:
            'エクスプローラー等でのファイル移動・リネーム・削除を検出し、タイトルバーに通知。専用ウィンドウから移動のみ・削除のみ等のフィルターで確認し、データベースへ個別または一括で反映できます。',
          icon: 'refresh',
        },
      ]
    : [
        {
          id: 'repository',
          title: 'Repository Management (Folders & Albums)',
          description:
            'Switch between Folder Repositories that mirror your OS directory tree (with subfolder inclusion toggles) and virtual Album Repositories organized via drag-and-drop or "/" hierarchies.',
          icon: 'folder-tree',
        },
        {
          id: 'duplicates',
          title: 'Duplicate Detection & Thumbnail Control',
          description:
            'Quickly and accurately detect identical files even with different filenames to reclaim disk space, plus pick any video frame as a custom thumbnail.',
          icon: 'layers',
        },
        {
          id: 'sync',
          title: 'Automatic External File Change Sync',
          description:
            'Detects moves, renames, and deletions made in File Explorer with a title-bar notification. Review changes filtered by move or deletion and apply them individually or in bulk.',
          icon: 'refresh',
        },
      ]
)

const viewerModes = computed<ViewerMode[]>(() =>
  locale.value === 'ja'
    ? [
        {
          name: 'グリッド表示',
          key: 'Wrap',
          summary: 'サイズスライダーとオーバーレイ表示切り替えに対応したサムネイル一覧',
        },
        {
          name: 'リスト表示',
          key: 'List',
          summary: '解像度・ファイルサイズ・作成日時・評価を列カスタマイズ可能なテーブル表示',
        },
        {
          name: '詳細表示',
          key: 'Detail',
          summary: '選択中の画像やドキュメントを大きく確認できるプレビューモード',
        },
        {
          name: 'マップ表示',
          key: 'Map',
          summary: 'EXIFのGPS座標から撮影地点を地図上にマッピングして閲覧',
        },
      ]
    : [
        {
          name: 'Grid View',
          key: 'Wrap',
          summary: 'Thumbnail grid with size slider and customizable filename/info overlays',
        },
        {
          name: 'List View',
          key: 'List',
          summary: 'Table view with customizable columns for resolution, file size, creation date, and rating',
        },
        {
          name: 'Detail View',
          key: 'Detail',
          summary: 'Large preview mode for inspecting the selected image or document',
        },
        {
          name: 'Map View',
          key: 'Map',
          summary: 'Plot files with EXIF GPS coordinates as pins on an interactive map',
        },
      ]
)

const mapHighlights = computed(() =>
  locale.value === 'ja'
    ? [
        'EXIF等から緯度・経度を自動抽出しデータベースへ保存',
        '近接スポットを縮尺に応じてグループ化するクラスタリング表示',
        '住所・地名トークンによる位置情報ベースの検索と絞り込み',
      ]
    : [
        'Automatically extracts latitude and longitude from EXIF metadata into the database',
        'Zoom-aware clustering groups nearby capture spots for clean browsing',
        'Search and filter collections by address or place name tokens',
      ]
)

const mediaTypes = computed<MediaTypeSpec[]>(() =>
  locale.value === 'ja'
    ? [
        {
          name: '画像',
          formats: ['JPEG', 'PNG', 'GIF', 'BMP', 'TIFF', 'HEIF', 'PSD', 'RAW (RAF)', 'ICO', 'PCX', 'Netpbm'],
          note: '写真・イラストからカメラのRAWデータまで。EXIF等の撮影情報も自動で読み取ります。',
          icon: 'image',
        },
        {
          name: '動画',
          formats: ['MP4', 'MKV', 'AVI', 'MOV', '各種動画形式'],
          note: '主要な動画フォーマットを網羅。動画内の好きなシーンを選んでサムネイルに設定できます。',
          icon: 'video',
        },
        {
          name: 'PDF',
          formats: ['PDF'],
          note: '複数ページのPDFドキュメントも軽快にプレビュー・サムネイル表示できます。',
          icon: 'pdf',
        },
        {
          name: 'アーカイブ',
          formats: ['ZIP', '7z', 'RAR'],
          note: 'ZIPなどの圧縮ファイルを解凍することなく、そのままライブラリ内で管理・閲覧できます。',
          icon: 'archive',
        },
      ]
    : [
        {
          name: 'Images',
          formats: ['JPEG', 'PNG', 'GIF', 'BMP', 'TIFF', 'HEIF', 'PSD', 'RAW (RAF)', 'ICO', 'PCX', 'Netpbm'],
          note: 'From everyday photos and illustrations to camera RAW files, with automatic EXIF metadata extraction.',
          icon: 'image',
        },
        {
          name: 'Videos',
          formats: ['MP4', 'MKV', 'AVI', 'MOV', 'FFmpeg formats'],
          note: 'Supports major video formats. Pick any frame from a video to set as its custom thumbnail.',
          icon: 'video',
        },
        {
          name: 'PDF',
          formats: ['PDF'],
          note: 'Fast multi-page PDF preview rendering and thumbnail generation.',
          icon: 'pdf',
        },
        {
          name: 'Archives',
          formats: ['ZIP', '7z', 'RAR'],
          note: 'Organize and browse compressed archive files directly in your library without extracting.',
          icon: 'archive',
        },
      ]
)

const systemSpecs = computed<SystemSpec[]>(() =>
  locale.value === 'ja'
    ? [
        { label: '対応OS', value: 'Windows 10 (1809) 以降 / Windows 11' },
        { label: '表示言語', value: '日本語 / 英語（切替・OS追従）' },
        { label: 'ライセンス', value: '無料・オープンソース (MIT License)' },
      ]
    : [
        { label: 'Supported OS', value: 'Windows 10 (1809) or later / Windows 11' },
        { label: 'Display Language', value: 'Japanese / English (In-app switch & OS default)' },
        { label: 'License', value: 'Free & Open Source (MIT License)' },
      ]
)
</script>

<template>
  <main class="home-main">
    <!-- Hero Section -->
    <section class="hero container">
      <div class="hero-copy">
        <div class="hero-meta-tag">
          {{ t.heroMetaTag }}
        </div>
        <h1 class="hero-title">
          <span class="hero-phrase">{{ t.heroTitlePrefix }}</span
          ><template v-if="locale === 'en'"> </template
          ><span class="hero-phrase">{{ t.heroTitleSuffix }}</span>
        </h1>
        <p class="hero-subtitle">
          {{ t.heroSubtitle }}
        </p>
        <div class="hero-actions">
          <a
            href="https://github.com/xm-i/MediaDeck/releases"
            target="_blank"
            rel="noopener noreferrer"
            class="btn btn-primary btn-lg"
          >
            {{ t.downloadLatest }}
            <svg
              width="16"
              height="16"
              viewBox="0 0 24 24"
              fill="none"
              stroke="currentColor"
              stroke-width="2"
              stroke-linecap="round"
              stroke-linejoin="round"
              aria-hidden="true"
            >
              <path d="M21 15v4a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2v-4" />
              <polyline points="7 10 12 15 17 10" />
              <line x1="12" x2="12" y1="15" y2="3" />
            </svg>
          </a>
          <router-link to="/docs" class="btn btn-secondary btn-lg">
            {{ t.readDocs }}
          </router-link>
        </div>
      </div>

      <div class="hero-showcase">
        <div class="showcase-frame">
          <img
            :src="mainImage"
            :alt="t.mainImageAlt"
            class="showcase-img"
            width="1150"
            height="822"
          />
        </div>

        <div class="highlights-strip" :aria-label="t.highlightsAriaLabel">
          <div
            v-for="item in coreHighlights"
            :key="item.label"
            class="highlight-cell"
          >
            <div class="highlight-label">{{ item.label }}</div>
            <p class="highlight-detail">{{ item.detail }}</p>
          </div>
        </div>
      </div>
    </section>

    <!-- Asymmetric Bento Grid Section -->
    <section id="features" class="section container">
      <div class="section-header">
        <h2 class="section-title">{{ t.featuresTitle }}</h2>
        <p class="section-subtitle">
          {{ t.featuresSubtitle }}
        </p>
      </div>

      <div class="bento-grid">
        <!-- Primary Card 1 (7 cols): Token Search & prop. -->
        <article class="bento-card bento-span-7">
          <div class="bento-card-header">
            <div class="bento-icon" aria-hidden="true">
              <svg
                width="18"
                height="18"
                viewBox="0 0 24 24"
                fill="none"
                stroke="currentColor"
                stroke-width="1.75"
                stroke-linecap="round"
                stroke-linejoin="round"
              >
                <circle cx="11" cy="11" r="8" />
                <path d="m21 21-4.3-4.3" />
              </svg>
            </div>
            <div>
              <h3 class="bento-title">{{ t.tokenSearchTitle }}</h3>
              <p class="bento-desc">
                {{ t.tokenSearchDescBefore }}<code class="inline-code">prop.</code
                >{{ t.tokenSearchDescAfter }}
              </p>
            </div>
          </div>

          <!-- Rich HTML/CSS Mini-Mockup: Search Bar + Suggestion Popover -->
          <div class="mini-mockup" aria-hidden="true">
            <div class="search-bar-mock">
              <span class="token-chip">Folder=C:\Photos</span>
              <span class="token-chip">{{ t.mockTagChip }}</span>
              <span class="token-chip token-chip-accent mono">prop.Width &gt;= 3840</span>
              <span class="token-chip token-chip-accent mono">prop.Rate &gt;= 4</span>
              <span class="search-cursor mono">prop.</span>
            </div>

            <div class="suggestion-popover">
              <div class="suggestion-header">
                <span>{{ t.mockSuggestionHeader }}</span>
                <span class="mono">prop.*</span>
              </div>
              <div
                v-for="(row, idx) in searchSuggestionRows"
                :key="row.token"
                :class="['suggestion-row', { 'is-selected': idx === 0 }]"
              >
                <code class="mono suggestion-token">{{ row.token }}</code>
                <span class="suggestion-type">{{ row.type }}</span>
              </div>
            </div>
          </div>
        </article>

        <!-- Primary Card 2 (5 cols): Hierarchical Tag & Ruby/Alias Management -->
        <article class="bento-card bento-span-5">
          <div class="bento-card-header">
            <div class="bento-icon" aria-hidden="true">
              <svg
                width="18"
                height="18"
                viewBox="0 0 24 24"
                fill="none"
                stroke="currentColor"
                stroke-width="1.75"
                stroke-linecap="round"
                stroke-linejoin="round"
              >
                <path
                  d="M12.586 2.586A2 2 0 0 0 11.172 2H4a2 2 0 0 0-2 2v7.172a2 2 0 0 0 .586 1.414l8.704 8.704a2.426 2.426 0 0 0 3.42 0l6.58-6.58a2.426 2.426 0 0 0 0-3.42l-8.704-8.704z"
                />
                <circle cx="7.5" cy="7.5" r=".5" fill="currentColor" />
              </svg>
            </div>
            <div>
              <h3 class="bento-title">{{ t.tagMgmtTitle }}</h3>
              <p class="bento-desc">
                {{ t.tagMgmtDesc }}
              </p>
            </div>
          </div>

          <!-- Rich HTML/CSS Mini-Mockup: Tag Inspector -->
          <div class="mini-mockup" aria-hidden="true">
            <div class="tag-inspector">
              <div class="tag-inspector-top">
                <div>
                  <div class="tag-category-label">{{ t.mockTagCategory }}</div>
                  <div class="tag-main-name">{{ t.mockTagMainName }}</div>
                </div>
                <span class="tag-usage-badge mono tabular-nums">UsageCount: 142</span>
              </div>

              <div class="tag-meta-rows">
                <div class="tag-meta-row">
                  <span class="tag-meta-key">{{ t.mockTagReadingLabel }}</span>
                  <span class="tag-meta-val mono">ふうけい / fuukei</span>
                </div>
                <div class="tag-meta-row">
                  <span class="tag-meta-key">{{ t.mockTagAliasLabel }}</span>
                  <div class="tag-alias-list">
                    <span v-for="alias in tagAliases" :key="alias" class="alias-pill">
                      {{ alias }}
                    </span>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </article>

        <!-- Secondary Cards (4 cols each) -->
        <article
          v-for="item in secondaryFeatures"
          :key="item.id"
          class="bento-card bento-span-4"
        >
          <div class="bento-icon" aria-hidden="true">
            <svg
              v-if="item.icon === 'folder-tree'"
              width="18"
              height="18"
              viewBox="0 0 24 24"
              fill="none"
              stroke="currentColor"
              stroke-width="1.75"
              stroke-linecap="round"
              stroke-linejoin="round"
            >
              <path
                d="M4 20h16a2 2 0 0 0 2-2V8a2 2 0 0 0-2-2h-7.93a2 2 0 0 1-1.66-.9l-.82-1.2A2 2 0 0 0 7.93 3H4a2 2 0 0 0-2 2v13c0 1.1.9 2 2 2Z"
              />
            </svg>
            <svg
              v-else-if="item.icon === 'layers'"
              width="18"
              height="18"
              viewBox="0 0 24 24"
              fill="none"
              stroke="currentColor"
              stroke-width="1.75"
              stroke-linecap="round"
              stroke-linejoin="round"
            >
              <polygon points="12 2 2 7 12 12 22 7 12 2" />
              <polyline points="2 17 12 22 22 17" />
              <polyline points="2 12 12 17 22 12" />
            </svg>
            <svg
              v-else
              width="18"
              height="18"
              viewBox="0 0 24 24"
              fill="none"
              stroke="currentColor"
              stroke-width="1.75"
              stroke-linecap="round"
              stroke-linejoin="round"
            >
              <path d="M21 12a9 9 0 0 0-9-9 9.75 9.75 0 0 0-6.74 2.74L3 8" />
              <path d="M3 3v5h5" />
              <path d="M3 12a9 9 0 0 0 9 9 9.75 9.75 0 0 0 6.74-2.74L21 16" />
              <path d="M16 16h5v5" />
            </svg>
          </div>
          <h3 class="bento-title">{{ item.title }}</h3>
          <p class="bento-desc">{{ item.description }}</p>
        </article>
      </div>
    </section>

    <!-- Split-Panel Viewer & Map Showcase -->
    <section id="map" class="section container">
      <div class="split-panel">
        <div class="split-copy">
          <h2 class="section-title">{{ t.mapSectionTitle }}</h2>
          <p class="section-subtitle">
            {{ t.mapSectionSubtitle }}
          </p>

          <div class="viewer-mode-grid">
            <div v-for="mode in viewerModes" :key="mode.key" class="viewer-mode-item">
              <div class="viewer-mode-top">
                <span class="viewer-mode-name">{{ mode.name }}</span>
                <span class="viewer-mode-key mono">{{ mode.key }}</span>
              </div>
              <p class="viewer-mode-summary">{{ mode.summary }}</p>
            </div>
          </div>

          <ul class="check-list">
            <li v-for="point in mapHighlights" :key="point">
              <svg
                class="check-icon"
                width="16"
                height="16"
                viewBox="0 0 24 24"
                fill="none"
                stroke="currentColor"
                stroke-width="1.75"
                stroke-linecap="round"
                stroke-linejoin="round"
                aria-hidden="true"
              >
                <polyline points="20 6 9 17 4 12" />
              </svg>
              <span>{{ point }}</span>
            </li>
          </ul>
        </div>

        <div class="map-showcase">
          <div class="showcase-frame">
            <img
              :src="mapImage"
              :alt="t.mapImageAlt"
              class="showcase-img"
              width="1336"
              height="811"
            />
          </div>
          <p class="frame-caption">
            {{ t.mapCaption }}
          </p>
        </div>
      </div>
    </section>

    <!-- Media Formats -->
    <section id="media" class="section container">
      <div class="section-header">
        <h2 class="section-title">{{ t.mediaSectionTitle }}</h2>
        <p class="section-subtitle">
          {{ t.mediaSectionSubtitle }}
        </p>
      </div>

      <div class="media-spec-grid">
        <div v-for="media in mediaTypes" :key="media.name" class="media-spec-card">
          <div class="media-spec-top">
            <div class="media-spec-title-group">
              <div class="media-spec-icon" aria-hidden="true">
                <svg
                  v-if="media.icon === 'image'"
                  width="18"
                  height="18"
                  viewBox="0 0 24 24"
                  fill="none"
                  stroke="currentColor"
                  stroke-width="1.75"
                  stroke-linecap="round"
                  stroke-linejoin="round"
                >
                  <rect width="18" height="18" x="3" y="3" rx="2" ry="2" />
                  <circle cx="9" cy="9" r="2" />
                  <path d="m21 15-3.086-3.086a2 2 0 0 0-2.828 0L6 21" />
                </svg>
                <svg
                  v-else-if="media.icon === 'video'"
                  width="18"
                  height="18"
                  viewBox="0 0 24 24"
                  fill="none"
                  stroke="currentColor"
                  stroke-width="1.75"
                  stroke-linecap="round"
                  stroke-linejoin="round"
                >
                  <path d="m22 8-6 4 6 4V8Z" />
                  <rect width="14" height="12" x="2" y="6" rx="2" ry="2" />
                </svg>
                <svg
                  v-else-if="media.icon === 'pdf'"
                  width="18"
                  height="18"
                  viewBox="0 0 24 24"
                  fill="none"
                  stroke="currentColor"
                  stroke-width="1.75"
                  stroke-linecap="round"
                  stroke-linejoin="round"
                >
                  <path d="M14.5 2H6a2 2 0 0 0-2 2v16a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V7.5L14.5 2z" />
                  <polyline points="14 2 14 8 20 8" />
                  <line x1="16" x2="8" y1="13" y2="13" />
                  <line x1="16" x2="8" y1="17" y2="17" />
                </svg>
                <svg
                  v-else
                  width="18"
                  height="18"
                  viewBox="0 0 24 24"
                  fill="none"
                  stroke="currentColor"
                  stroke-width="1.75"
                  stroke-linecap="round"
                  stroke-linejoin="round"
                >
                  <rect width="20" height="5" x="2" y="4" rx="2" />
                  <path d="M4 9v9a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V9" />
                  <path d="M10 13h4" />
                </svg>
              </div>
              <h3 class="media-spec-name">{{ media.name }}</h3>
            </div>
          </div>

          <p class="media-spec-note">{{ media.note }}</p>

          <div class="format-list">
            <span v-for="fmt in media.formats" :key="fmt" class="format-token mono">
              {{ fmt }}
            </span>
          </div>
        </div>
      </div>
    </section>

    <!-- System Specs & Final CTA -->
    <section id="requirements" class="section section-last container">
      <div class="specs-panel">
        <div class="specs-header">
          <div>
            <h2 class="specs-title">{{ t.specsTitle }}</h2>
            <p class="specs-subtitle">
              {{ t.specsSubtitle }}
            </p>
          </div>
          <div class="specs-cta">
            <a
              href="https://github.com/xm-i/MediaDeck/releases"
              target="_blank"
              rel="noopener noreferrer"
              class="btn btn-primary btn-md"
            >
              {{ t.downloadLatest }}
            </a>
            <router-link to="/docs" class="btn btn-secondary btn-md">
              {{ t.readDocs }}
            </router-link>
          </div>
        </div>

        <dl class="system-spec-row">
          <div v-for="spec in systemSpecs" :key="spec.label" class="system-spec-cell">
            <dt class="spec-label">{{ spec.label }}</dt>
            <dd class="spec-value tabular-nums">{{ spec.value }}</dd>
          </div>
        </dl>
      </div>
    </section>
  </main>
</template>

<style scoped>
.home-main {
  padding-top: 60px;
}

.section {
  padding: 56px 0;
}

.section-last {
  padding-bottom: 80px;
}

.section-header {
  margin-bottom: 32px;
  max-width: 68ch;
}

.section-title {
  font-size: clamp(1.5rem, 2.5vw, 1.875rem);
  font-weight: 700;
  color: var(--text-primary);
  margin-bottom: 12px;
}

.section-subtitle {
  font-size: 1rem;
  color: var(--text-secondary);
  line-height: 1.65;
}

/* Hero Section */
.hero {
  padding-top: 64px;
  padding-bottom: 48px;
}

.hero-copy {
  max-width: 760px;
  margin: 0 auto 40px;
  text-align: center;
}

.hero-meta-tag {
  display: inline-flex;
  align-items: center;
  padding: 6px 12px;
  margin-bottom: 20px;
  font-size: 0.8125rem;
  font-weight: 500;
  color: var(--text-secondary);
  background-color: var(--bg-surface);
  border: 1px solid var(--border-subtle);
  border-radius: 9999px;
}

.hero-title {
  font-size: clamp(1.875rem, 4.5vw, 3.25rem);
  font-weight: 700;
  line-height: 1.22;
  letter-spacing: -0.025em;
  color: var(--text-primary);
  margin-bottom: 16px;
}

.hero-phrase {
  display: inline-block;
}

.hero-subtitle {
  font-size: 1.0625rem;
  color: var(--text-secondary);
  max-width: 62ch;
  margin: 0 auto 28px;
  line-height: 1.7;
}

.hero-actions {
  display: flex;
  align-items: center;
  justify-content: center;
  flex-wrap: wrap;
  gap: 12px;
}

.hero-showcase {
  max-width: 1080px;
  margin: 0 auto;
}

.showcase-frame {
  padding: 8px;
  background-color: var(--bg-surface);
  border: 1px solid var(--border-subtle);
  border-radius: var(--radius-lg);
  box-shadow: 0 24px 64px rgba(0, 0, 0, 0.55);
}

.showcase-img {
  display: block;
  width: 100%;
  height: auto;
  border-radius: var(--radius-xs);
}

.highlights-strip {
  display: grid;
  grid-template-columns: repeat(3, minmax(0, 1fr));
  gap: 16px;
  margin-top: 16px;
}

.highlight-cell {
  padding: 16px 20px;
  background-color: var(--bg-surface);
  border: 1px solid var(--border-subtle);
  border-radius: var(--radius-md);
}

.highlight-label {
  font-size: 0.9375rem;
  font-weight: 600;
  color: var(--accent-primary);
  margin-bottom: 6px;
}

.highlight-detail {
  font-size: 0.875rem;
  color: var(--text-secondary);
  line-height: 1.55;
}

/* Bento Grid */
.bento-grid {
  display: grid;
  grid-template-columns: repeat(12, minmax(0, 1fr));
  gap: 16px;
}

.bento-card {
  display: flex;
  flex-direction: column;
  padding: 24px;
  background-color: var(--bg-surface);
  border: 1px solid var(--border-subtle);
  border-radius: var(--radius-lg);
  min-width: 0;
  transition: border-color 140ms cubic-bezier(0.23, 1, 0.32, 1);
}

.bento-card:hover {
  border-color: var(--border-hover);
}

.bento-span-7 {
  grid-column: span 7;
}

.bento-span-5 {
  grid-column: span 5;
}

.bento-span-4 {
  grid-column: span 4;
}

.bento-card-header {
  display: flex;
  flex-direction: column;
  gap: 12px;
}

.bento-icon {
  width: 36px;
  height: 36px;
  display: inline-flex;
  align-items: center;
  justify-content: center;
  border-radius: var(--radius-sm);
  background-color: var(--accent-subtle);
  border: 1px solid var(--accent-border);
  color: var(--accent-primary);
  margin-bottom: 12px;
  flex-shrink: 0;
}

.bento-card-header .bento-icon {
  margin-bottom: 0;
}

.bento-title {
  font-size: 1.125rem;
  font-weight: 600;
  color: var(--text-primary);
  margin-bottom: 8px;
}

.bento-desc {
  font-size: 0.9375rem;
  color: var(--text-secondary);
  line-height: 1.65;
}

.inline-code {
  font-family: var(--font-mono);
  font-size: 0.85em;
  color: var(--accent-primary);
  background-color: var(--bg-surface-elevated);
  border: 1px solid var(--border-subtle);
  padding: 1px 6px;
  border-radius: var(--radius-xs);
}

/* Rich Mini-Mockups inside Bento Cards */
.mini-mockup {
  margin-top: 20px;
  padding: 12px;
  background-color: var(--bg-canvas);
  border: 1px solid var(--border-subtle);
  border-radius: var(--radius-sm);
  display: flex;
  flex-direction: column;
  gap: 8px;
}

.search-bar-mock {
  display: flex;
  flex-wrap: wrap;
  align-items: center;
  gap: 6px;
  padding: 8px 10px;
  background-color: var(--bg-surface);
  border: 1px solid var(--border-subtle);
  border-radius: var(--radius-xs);
}

.token-chip {
  display: inline-flex;
  align-items: center;
  padding: 3px 8px;
  font-size: 0.75rem;
  color: var(--text-primary);
  background-color: var(--bg-surface-elevated);
  border: 1px solid var(--border-subtle);
  border-radius: var(--radius-xs);
}

.token-chip-accent {
  color: var(--accent-primary);
  background-color: var(--accent-subtle);
  border-color: var(--accent-border);
}

.search-cursor {
  font-size: 0.75rem;
  color: var(--text-secondary);
  padding-left: 2px;
}

.suggestion-popover {
  background-color: var(--bg-surface-elevated);
  border: 1px solid var(--border-subtle);
  border-radius: var(--radius-xs);
  overflow: hidden;
}

.suggestion-header {
  display: flex;
  align-items: center;
  justify-content: space-between;
  padding: 6px 10px;
  font-size: 0.75rem;
  color: var(--text-muted);
  border-bottom: 1px solid var(--border-subtle);
}

.suggestion-row {
  display: flex;
  align-items: center;
  justify-content: space-between;
  gap: 12px;
  padding: 7px 10px;
  font-size: 0.75rem;
}

.suggestion-row + .suggestion-row {
  border-top: 1px solid rgba(255, 255, 255, 0.04);
}

.suggestion-row.is-selected {
  background-color: rgba(56, 189, 248, 0.08);
}

.suggestion-token {
  color: var(--text-primary);
  overflow: hidden;
  text-overflow: ellipsis;
  white-space: nowrap;
}

.suggestion-type {
  flex-shrink: 0;
  color: var(--text-muted);
  font-size: 0.6875rem;
}

.tag-inspector {
  display: flex;
  flex-direction: column;
  gap: 12px;
  padding: 4px;
}

.tag-inspector-top {
  display: flex;
  align-items: flex-start;
  justify-content: space-between;
  gap: 12px;
  padding-bottom: 10px;
  border-bottom: 1px solid var(--border-subtle);
}

.tag-category-label {
  font-size: 0.75rem;
  color: var(--text-muted);
  margin-bottom: 2px;
}

.tag-main-name {
  font-size: 1rem;
  font-weight: 600;
  color: var(--text-primary);
}

.tag-usage-badge {
  font-size: 0.75rem;
  color: var(--accent-primary);
  background-color: var(--accent-subtle);
  border: 1px solid var(--accent-border);
  padding: 2px 8px;
  border-radius: var(--radius-xs);
  white-space: nowrap;
}

.tag-meta-rows {
  display: flex;
  flex-direction: column;
  gap: 10px;
}

.tag-meta-row {
  display: flex;
  flex-direction: column;
  gap: 4px;
}

.tag-meta-key {
  font-size: 0.75rem;
  color: var(--text-muted);
}

.tag-meta-val {
  font-size: 0.8125rem;
  color: var(--text-primary);
}

.tag-alias-list {
  display: flex;
  flex-wrap: wrap;
  gap: 6px;
}

.alias-pill {
  font-size: 0.75rem;
  color: var(--text-secondary);
  background-color: var(--bg-surface-elevated);
  border: 1px solid var(--border-subtle);
  padding: 2px 8px;
  border-radius: var(--radius-xs);
}

/* Split-Panel Viewer & Map Showcase */
.split-panel {
  display: grid;
  grid-template-columns: 1fr 1.15fr;
  gap: 40px;
  align-items: center;
}

.viewer-mode-grid {
  display: grid;
  grid-template-columns: repeat(2, minmax(0, 1fr));
  gap: 12px;
  margin-top: 24px;
  margin-bottom: 24px;
}

.viewer-mode-item {
  padding: 14px 16px;
  background-color: var(--bg-surface);
  border: 1px solid var(--border-subtle);
  border-radius: var(--radius-sm);
}

.viewer-mode-top {
  display: flex;
  align-items: center;
  justify-content: space-between;
  gap: 8px;
  margin-bottom: 6px;
}

.viewer-mode-name {
  font-size: 0.875rem;
  font-weight: 600;
  color: var(--text-primary);
}

.viewer-mode-key {
  font-size: 0.75rem;
  color: var(--text-muted);
}

.viewer-mode-summary {
  font-size: 0.8125rem;
  color: var(--text-secondary);
  line-height: 1.5;
}

.check-list {
  list-style: none;
  display: flex;
  flex-direction: column;
  gap: 10px;
}

.check-list li {
  display: flex;
  align-items: flex-start;
  gap: 10px;
  font-size: 0.9375rem;
  color: var(--text-secondary);
}

.check-icon {
  color: var(--accent-primary);
  margin-top: 4px;
  flex-shrink: 0;
}

.map-showcase {
  display: flex;
  flex-direction: column;
  gap: 10px;
}

.frame-caption {
  font-size: 0.8125rem;
  color: var(--text-muted);
  text-align: right;
}

/* Media Specification Grid */
.media-spec-grid {
  display: grid;
  grid-template-columns: repeat(2, minmax(0, 1fr));
  gap: 16px;
}

.media-spec-card {
  display: flex;
  flex-direction: column;
  gap: 12px;
  padding: 24px;
  background-color: var(--bg-surface);
  border: 1px solid var(--border-subtle);
  border-radius: var(--radius-lg);
}

.media-spec-top {
  display: flex;
  align-items: center;
  justify-content: space-between;
  gap: 12px;
  flex-wrap: wrap;
}

.media-spec-title-group {
  display: flex;
  align-items: center;
  gap: 10px;
}

.media-spec-icon {
  width: 32px;
  height: 32px;
  display: inline-flex;
  align-items: center;
  justify-content: center;
  border-radius: var(--radius-sm);
  background-color: var(--bg-surface-elevated);
  border: 1px solid var(--border-subtle);
  color: var(--text-primary);
}

.media-spec-name {
  font-size: 1.0625rem;
  font-weight: 600;
  color: var(--text-primary);
}

.media-spec-note {
  font-size: 0.875rem;
  color: var(--text-secondary);
}

.format-list {
  display: flex;
  flex-wrap: wrap;
  gap: 6px;
  margin-top: auto;
  padding-top: 4px;
}

.format-token {
  font-size: 0.75rem;
  color: var(--text-secondary);
  background-color: var(--bg-surface-elevated);
  border: 1px solid var(--border-subtle);
  padding: 3px 8px;
  border-radius: var(--radius-xs);
}

/* System Requirements Panel */
.specs-panel {
  background-color: var(--bg-surface);
  border: 1px solid var(--border-subtle);
  border-radius: var(--radius-lg);
  padding: 28px;
  display: flex;
  flex-direction: column;
  gap: 24px;
}

.specs-header {
  display: flex;
  align-items: flex-start;
  justify-content: space-between;
  gap: 24px;
  flex-wrap: wrap;
}

.specs-title {
  font-size: 1.375rem;
  font-weight: 700;
  margin-bottom: 6px;
}

.specs-subtitle {
  font-size: 0.9375rem;
  color: var(--text-secondary);
  max-width: 60ch;
}

.specs-cta {
  display: flex;
  flex-wrap: wrap;
  gap: 10px;
}

.system-spec-row {
  display: grid;
  grid-template-columns: repeat(3, minmax(0, 1fr));
  gap: 16px;
  padding-top: 20px;
  border-top: 1px solid var(--border-subtle);
}

.system-spec-cell {
  display: flex;
  flex-direction: column;
  gap: 4px;
}

.spec-label {
  font-size: 0.75rem;
  color: var(--text-muted);
}

.spec-value {
  font-size: 0.875rem;
  font-weight: 500;
  color: var(--text-primary);
}

/* Responsive Breakpoints */
@media (max-width: 960px) {
  .bento-span-7,
  .bento-span-5,
  .bento-span-4 {
    grid-column: span 12;
  }

  .split-panel {
    grid-template-columns: 1fr;
    gap: 28px;
  }
}

@media (max-width: 768px) {
  .hero {
    padding-top: 40px;
    padding-bottom: 36px;
  }

  .hero-meta-tag {
    font-size: 0.75rem;
    padding: 5px 10px;
  }

  .hero-copy {
    text-align: left;
    margin-bottom: 28px;
  }

  .hero-subtitle {
    margin-left: 0;
    margin-right: 0;
  }

  .hero-actions {
    justify-content: flex-start;
  }

  .hero-actions .btn {
    width: 100%;
  }

  .section {
    padding: 40px 0;
  }

  .highlights-strip,
  .media-spec-grid,
  .viewer-mode-grid,
  .system-spec-row {
    grid-template-columns: 1fr;
  }

  .frame-caption {
    text-align: left;
  }

  .specs-panel {
    padding: 20px;
  }

  .specs-cta {
    width: 100%;
  }

  .specs-cta .btn {
    flex: 1 1 200px;
  }
}

@media (max-width: 480px) {
  .hero-title {
    font-size: 1.625rem;
  }
}
</style>
