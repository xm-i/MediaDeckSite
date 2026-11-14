<script setup lang="ts">
import { ref, computed, onMounted, onUnmounted, watch } from 'vue'
import { useRoute } from 'vue-router'

interface NavItem {
  path: string
  title: string
}

const route = useRoute()
const isDocsMenuOpen = ref(false)

const navItems: NavItem[] = [
  { path: '/docs/getting-started', title: '1. はじめに' },
  { path: '/docs/basic-operations', title: '2. 基本操作' },
  { path: '/docs/repository', title: '3. リポジトリ管理' },
  { path: '/docs/search-filter', title: '4. 検索・フィルタリング' },
  { path: '/docs/tags', title: '5. タグ管理' },
  { path: '/docs/metadata', title: '6. 詳細情報・メタデータ' },
  { path: '/docs/tools', title: '7. ツール' },
  { path: '/docs/settings', title: '8. 設定' },
  { path: '/docs/map', title: '9. マップビュー' },
]

const currentIndex = computed(() => navItems.findIndex((item) => item.path === route.path))

const currentTitle = computed(() => {
  const idx = currentIndex.value
  const item = idx >= 0 ? navItems[idx] : undefined
  return item ? item.title : 'ドキュメント'
})

const prevItem = computed<NavItem | undefined>(() => {
  const idx = currentIndex.value
  return idx > 0 ? navItems[idx - 1] : undefined
})

const nextItem = computed<NavItem | undefined>(() => {
  const idx = currentIndex.value
  return idx >= 0 && idx < navItems.length - 1 ? navItems[idx + 1] : undefined
})

const handleKeydown = (e: KeyboardEvent) => {
  if (e.key === 'Escape' && isDocsMenuOpen.value) {
    isDocsMenuOpen.value = false
  }
}

watch(
  () => route.path,
  () => {
    isDocsMenuOpen.value = false
  }
)

onMounted(() => {
  window.addEventListener('keydown', handleKeydown)
})

onUnmounted(() => {
  window.removeEventListener('keydown', handleKeydown)
})
</script>

<template>
  <div class="docs-layout container">
    <!-- Mobile Sticky Docs Header -->
    <div class="docs-mobile-header">
      <button
        type="button"
        class="docs-mobile-menu-btn"
        @click="isDocsMenuOpen = !isDocsMenuOpen"
        :aria-expanded="isDocsMenuOpen"
        aria-controls="docs-sidebar-nav"
      >
        <span class="docs-mobile-label-group">
          <span class="docs-mobile-prefix">目次</span>
          <span class="active-title">{{ currentTitle }}</span>
        </span>
        <svg
          width="18"
          height="18"
          viewBox="0 0 24 24"
          fill="none"
          stroke="currentColor"
          stroke-width="2"
          stroke-linecap="round"
          stroke-linejoin="round"
          aria-hidden="true"
        >
          <polyline points="6 9 12 15 18 9" />
        </svg>
      </button>
    </div>

    <!-- Sidebar Menu Drawer -->
    <aside id="docs-sidebar-nav" :class="['docs-sidebar', { 'mobile-open': isDocsMenuOpen }]">
      <div class="mobile-sidebar-header">
        <span class="mobile-sidebar-title">ドキュメント目次</span>
        <button
          type="button"
          class="close-btn"
          @click="isDocsMenuOpen = false"
          aria-label="閉じる"
        >
          <svg
            width="20"
            height="20"
            viewBox="0 0 24 24"
            fill="none"
            stroke="currentColor"
            stroke-width="2"
            stroke-linecap="round"
            stroke-linejoin="round"
            aria-hidden="true"
          >
            <line x1="18" y1="6" x2="6" y2="18" />
            <line x1="6" y1="6" x2="18" y2="18" />
          </svg>
        </button>
      </div>

      <div class="sidebar-section-label">ドキュメント目次</div>
      <nav class="docs-nav" aria-label="ドキュメント目次">
        <ul>
          <li v-for="item in navItems" :key="item.path">
            <router-link
              :to="item.path"
              :class="{ active: route.path === item.path }"
              @click="isDocsMenuOpen = false"
            >
              {{ item.title }}
            </router-link>
          </li>
        </ul>
      </nav>
    </aside>

    <!-- Overlay backdrop for mobile drawer -->
    <transition name="fade">
      <div
        v-if="isDocsMenuOpen"
        class="docs-mobile-overlay"
        @click="isDocsMenuOpen = false"
        aria-hidden="true"
      ></div>
    </transition>

    <!-- Main Content Area -->
    <main class="docs-main">
      <div class="docs-article-column">
        <nav class="docs-breadcrumb" aria-label="パンくずリスト">
          <router-link to="/docs" class="breadcrumb-root mono">Docs</router-link>
          <span class="breadcrumb-sep" aria-hidden="true">/</span>
          <span class="breadcrumb-current">{{ currentTitle }}</span>
        </nav>

        <article class="docs-content">
          <router-view />
        </article>

        <nav
          v-if="prevItem || nextItem"
          class="docs-pager"
          aria-label="前後のページ"
        >
          <router-link
            v-if="prevItem"
            :to="prevItem.path"
            class="pager-card pager-prev"
          >
            <span class="pager-dir">
              <svg
                width="14"
                height="14"
                viewBox="0 0 24 24"
                fill="none"
                stroke="currentColor"
                stroke-width="2"
                stroke-linecap="round"
                stroke-linejoin="round"
                aria-hidden="true"
              >
                <path d="m15 18-6-6 6-6" />
              </svg>
              前のページ
            </span>
            <span class="pager-title">{{ prevItem.title }}</span>
          </router-link>
          <div v-else class="pager-spacer" aria-hidden="true"></div>

          <router-link
            v-if="nextItem"
            :to="nextItem.path"
            class="pager-card pager-next"
          >
            <span class="pager-dir">
              次のページ
              <svg
                width="14"
                height="14"
                viewBox="0 0 24 24"
                fill="none"
                stroke="currentColor"
                stroke-width="2"
                stroke-linecap="round"
                stroke-linejoin="round"
                aria-hidden="true"
              >
                <path d="m9 18 6-6-6-6" />
              </svg>
            </span>
            <span class="pager-title">{{ nextItem.title }}</span>
          </router-link>
        </nav>
      </div>
    </main>
  </div>
</template>

<style scoped>
.docs-layout {
  display: flex;
  gap: 48px;
  padding-top: 96px;
  padding-bottom: 64px;
  align-items: flex-start;
  width: 100%;
}

.docs-sidebar {
  width: 240px;
  flex-shrink: 0;
  position: sticky;
  top: 84px;
}

.sidebar-section-label {
  font-size: 0.75rem;
  font-weight: 600;
  color: var(--text-muted);
  padding: 0 12px 10px;
  margin-bottom: 6px;
  border-bottom: 1px solid var(--border-subtle);
}

.mobile-sidebar-header,
.docs-mobile-header {
  display: none;
}

.docs-nav ul {
  list-style: none;
  padding: 0;
  margin: 0;
  display: flex;
  flex-direction: column;
  gap: 2px;
}

.docs-nav a {
  display: flex;
  align-items: center;
  padding: 8px 12px;
  color: var(--text-secondary);
  text-decoration: none;
  border-radius: 0 var(--radius-sm) var(--radius-sm) 0;
  border-left: 2px solid transparent;
  font-size: 0.875rem;
  font-weight: 500;
  transition:
    color 140ms cubic-bezier(0.23, 1, 0.32, 1),
    background-color 140ms cubic-bezier(0.23, 1, 0.32, 1),
    border-color 140ms cubic-bezier(0.23, 1, 0.32, 1);
}

.docs-nav a:hover {
  background-color: rgba(255, 255, 255, 0.04);
  color: var(--text-primary);
}

.docs-nav a.active {
  background-color: var(--accent-subtle);
  border-left-color: var(--accent-primary);
  color: var(--accent-primary);
  font-weight: 600;
}

.docs-main {
  flex: 1 1 auto;
  min-width: 0;
}

.docs-article-column {
  max-width: 760px;
}

.docs-breadcrumb {
  display: flex;
  align-items: center;
  gap: 8px;
  font-size: 0.8125rem;
  color: var(--text-muted);
  margin-bottom: 16px;
}

.breadcrumb-root {
  color: var(--text-secondary);
}

.breadcrumb-root:hover {
  color: var(--accent-primary);
}

.breadcrumb-current {
  color: var(--text-secondary);
}

/* Previous / Next Chapter Navigation */
.docs-pager {
  display: grid;
  grid-template-columns: repeat(2, minmax(0, 1fr));
  gap: 16px;
  margin-top: 48px;
  padding-top: 24px;
  border-top: 1px solid var(--border-subtle);
}

.pager-card {
  display: flex;
  flex-direction: column;
  gap: 6px;
  padding: 16px;
  background-color: var(--bg-surface);
  border: 1px solid var(--border-subtle);
  border-radius: var(--radius-md);
  text-decoration: none;
  transition:
    border-color 140ms cubic-bezier(0.23, 1, 0.32, 1),
    background-color 140ms cubic-bezier(0.23, 1, 0.32, 1),
    transform 80ms cubic-bezier(0.23, 1, 0.32, 1);
}

.pager-card:hover {
  border-color: var(--border-hover);
  background-color: var(--bg-surface-elevated);
}

.pager-card:active {
  transform: scale(0.985);
}

.pager-next {
  align-items: flex-end;
  text-align: right;
}

.pager-dir {
  display: inline-flex;
  align-items: center;
  gap: 4px;
  font-size: 0.75rem;
  color: var(--text-muted);
}

.pager-title {
  font-size: 0.9375rem;
  font-weight: 600;
  color: var(--text-primary);
}

.fade-enter-active,
.fade-leave-active {
  transition: opacity 180ms cubic-bezier(0.23, 1, 0.32, 1);
}

.fade-enter-from,
.fade-leave-to {
  opacity: 0;
}

@media (max-width: 860px) {
  .docs-layout {
    flex-direction: column;
    padding-top: 76px;
    gap: 16px;
  }

  .sidebar-section-label {
    display: none;
  }

  .docs-mobile-header {
    display: block;
    position: sticky;
    top: 68px;
    z-index: 99;
    width: 100%;
  }

  .docs-mobile-menu-btn {
    display: flex;
    align-items: center;
    justify-content: space-between;
    width: 100%;
    min-height: 44px;
    padding: 10px 14px;
    background-color: var(--bg-surface);
    border: 1px solid var(--border-subtle);
    border-radius: var(--radius-sm);
    color: var(--text-primary);
    font-family: var(--font-sans);
    font-size: 0.875rem;
    font-weight: 600;
    cursor: pointer;
    box-shadow: 0 8px 24px rgba(0, 0, 0, 0.4);
    transition:
      border-color 140ms cubic-bezier(0.23, 1, 0.32, 1),
      background-color 140ms cubic-bezier(0.23, 1, 0.32, 1),
      transform 80ms cubic-bezier(0.23, 1, 0.32, 1);
  }

  .docs-mobile-menu-btn:hover {
    border-color: var(--border-hover);
    background-color: var(--bg-surface-elevated);
  }

  .docs-mobile-menu-btn:active {
    transform: scale(0.99);
  }

  .docs-mobile-label-group {
    display: flex;
    align-items: center;
    gap: 10px;
    min-width: 0;
  }

  .docs-mobile-prefix {
    font-size: 0.75rem;
    font-weight: 500;
    color: var(--text-muted);
    background-color: var(--bg-surface-elevated);
    border: 1px solid var(--border-subtle);
    padding: 2px 6px;
    border-radius: var(--radius-xs);
    flex-shrink: 0;
  }

  .active-title {
    overflow: hidden;
    text-overflow: ellipsis;
    white-space: nowrap;
  }

  .docs-sidebar {
    position: fixed;
    top: 0;
    left: 0;
    width: 280px;
    max-width: 85vw;
    height: 100vh;
    background-color: var(--bg-surface);
    border-right: 1px solid var(--border-subtle);
    z-index: 2000;
    padding: 16px;
    transform: translateX(-100%);
    transition: transform 200ms cubic-bezier(0.23, 1, 0.32, 1);
    display: flex;
    flex-direction: column;
    gap: 12px;
    overflow-y: auto;
    box-shadow: 16px 0 40px rgba(0, 0, 0, 0.65);
  }

  .docs-sidebar.mobile-open {
    transform: translateX(0);
  }

  .mobile-sidebar-header {
    display: flex;
    justify-content: space-between;
    align-items: center;
    padding-bottom: 12px;
    border-bottom: 1px solid var(--border-subtle);
  }

  .mobile-sidebar-title {
    font-size: 0.9375rem;
    font-weight: 600;
    color: var(--text-primary);
  }

  .close-btn {
    min-width: 44px;
    min-height: 44px;
    background: transparent;
    border: 1px solid transparent;
    border-radius: var(--radius-sm);
    color: var(--text-secondary);
    cursor: pointer;
    display: inline-flex;
    align-items: center;
    justify-content: center;
    transition:
      color 140ms cubic-bezier(0.23, 1, 0.32, 1),
      background-color 140ms cubic-bezier(0.23, 1, 0.32, 1);
  }

  .close-btn:hover {
    color: var(--text-primary);
    background-color: rgba(255, 255, 255, 0.05);
  }

  .docs-nav a {
    min-height: 44px;
  }

  .docs-mobile-overlay {
    position: fixed;
    inset: 0;
    background-color: rgba(0, 0, 0, 0.6);
    backdrop-filter: blur(4px);
    -webkit-backdrop-filter: blur(4px);
    z-index: 1999;
  }
}

@media (max-width: 640px) {
  .docs-pager {
    grid-template-columns: 1fr;
  }

  .pager-spacer {
    display: none;
  }
}
</style>

<style>
/* Global styles for documentation page templates */
.docs-content h1 {
  font-size: clamp(1.75rem, 3vw, 2.25rem);
  font-weight: 700;
  margin-bottom: 20px;
  letter-spacing: -0.02em;
  color: var(--text-primary);
}

.docs-content h2 {
  font-size: 1.375rem;
  font-weight: 600;
  margin-top: 40px;
  margin-bottom: 14px;
  padding-bottom: 8px;
  border-bottom: 1px solid var(--border-subtle);
  color: var(--text-primary);
}

.docs-content h3 {
  font-size: 1.0625rem;
  font-weight: 600;
  margin-top: 28px;
  margin-bottom: 10px;
  color: var(--text-primary);
}

.docs-content p {
  color: var(--text-secondary);
  line-height: 1.7;
  margin-bottom: 16px;
  font-size: 0.9375rem;
}

.docs-content ul {
  margin-bottom: 20px;
  padding-left: 20px;
  color: var(--text-secondary);
  font-size: 0.9375rem;
}

.docs-content li {
  margin-bottom: 8px;
  line-height: 1.65;
}

.docs-content li strong {
  color: var(--text-primary);
  font-weight: 600;
}

.docs-content code {
  font-family: var(--font-mono);
  background-color: var(--bg-surface-elevated);
  border: 1px solid var(--border-subtle);
  color: var(--accent-primary);
  padding: 2px 6px;
  border-radius: var(--radius-xs);
  font-size: 0.85em;
}

.docs-content table {
  width: 100%;
  border-collapse: separate;
  border-spacing: 0;
  margin: 24px 0;
  background-color: var(--bg-surface);
  border: 1px solid var(--border-subtle);
  border-radius: var(--radius-sm);
  overflow: hidden;
  font-size: 0.875rem;
  font-variant-numeric: tabular-nums;
}

.docs-content th,
.docs-content td {
  padding: 12px 16px;
  text-align: left;
  vertical-align: top;
  border-bottom: 1px solid var(--border-subtle);
}

.docs-content tr:last-child th,
.docs-content tr:last-child td {
  border-bottom: none;
}

.docs-content th + th,
.docs-content th + td,
.docs-content td + td {
  border-left: 1px solid var(--border-subtle);
}

.docs-content th {
  background-color: var(--bg-surface-elevated);
  font-weight: 600;
  color: var(--text-primary);
}

.docs-content td {
  color: var(--text-secondary);
}

.docs-content td strong {
  color: var(--text-primary);
  font-weight: 600;
}

@media (max-width: 640px) {
  .docs-content table {
    display: block;
    overflow-x: auto;
    -webkit-overflow-scrolling: touch;
  }
}
</style>
