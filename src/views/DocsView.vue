<script setup lang="ts">
import { ref, computed } from 'vue'
import { useRoute } from 'vue-router'

const route = useRoute()
const isDocsMenuOpen = ref(false)

const navItems = [
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

const currentTitle = computed(() => {
  const activeItem = navItems.find(item => item.path === route.path)
  return activeItem ? activeItem.title : 'ドキュメント'
})
</script>

<template>
  <div class="docs-layout container">
    <!-- Mobile Sticky Docs Header -->
    <div class="docs-mobile-header">
      <button class="docs-mobile-menu-btn" @click="isDocsMenuOpen = !isDocsMenuOpen">
        <span class="active-title">{{ currentTitle }}</span>
        <svg width="18" height="18" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2.5" stroke-linecap="round" stroke-linejoin="round">
          <polyline points="6 9 12 15 18 9"></polyline>
        </svg>
      </button>
    </div>

    <!-- Sidebar Menu Drawer -->
    <aside :class="['docs-sidebar', { 'mobile-open': isDocsMenuOpen }]">
      <div class="mobile-sidebar-header">
        <span class="mobile-sidebar-title">ドキュメント目次</span>
        <button class="close-btn" @click="isDocsMenuOpen = false" aria-label="閉じる">
          <svg width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2.5" stroke-linecap="round" stroke-linejoin="round">
            <line x1="18" y1="6" x2="6" y2="18"></line>
            <line x1="6" y1="6" x2="18" y2="18"></line>
          </svg>
        </button>
      </div>
      <nav class="docs-nav">
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
      ></div>
    </transition>

    <!-- Main Content Area -->
    <main class="docs-content">
      <router-view />
    </main>
  </div>
</template>

<style scoped>
.docs-layout {
  display: flex;
  gap: 3rem;
  padding-top: 8rem;
  padding-bottom: 4rem;
  align-items: flex-start;
}

.docs-sidebar {
  width: 250px;
  flex-shrink: 0;
  position: sticky;
  top: 6rem;
}

.mobile-sidebar-header {
  display: none;
}

.docs-mobile-header {
  display: none;
}

.docs-nav ul {
  list-style: none;
  padding: 0;
  margin: 0;
  display: flex;
  flex-direction: column;
  gap: 0.5rem;
}

.docs-nav a {
  display: block;
  padding: 0.75rem 1rem;
  color: var(--text-secondary);
  text-decoration: none;
  border-radius: 8px;
  transition: all 0.2s;
  font-size: 0.9375rem;
}

.docs-nav a:hover {
  background: rgba(255, 255, 255, 0.05);
  color: var(--text-primary);
}

.docs-nav a.active {
  background: rgba(0, 163, 255, 0.1);
  color: #00a3ff;
  font-weight: 600;
}

.docs-content {
  flex-grow: 1;
  min-width: 0;
}

/* Transitions */
.fade-enter-active,
.fade-leave-active {
  transition: opacity 0.25s ease;
}
.fade-enter-from,
.fade-leave-to {
  opacity: 0;
}

@media (max-width: 768px) {
  .docs-layout {
    flex-direction: column;
    padding-top: 6.5rem;
    gap: 1rem;
  }
  
  .docs-mobile-header {
    display: block;
    position: sticky;
    top: 5rem;
    z-index: 99;
    width: 100%;
    margin-bottom: 0.5rem;
  }

  .docs-mobile-menu-btn {
    display: flex;
    align-items: center;
    justify-content: space-between;
    width: 100%;
    padding: 0.75rem 1.25rem;
    background: rgba(18, 21, 26, 0.8);
    backdrop-filter: blur(16px);
    -webkit-backdrop-filter: blur(16px);
    border: 1px solid rgba(255, 255, 255, 0.08);
    border-radius: 12px;
    color: var(--text-primary);
    font-family: var(--font-display);
    font-size: 0.9375rem;
    font-weight: 600;
    cursor: pointer;
    box-shadow: 0 4px 20px rgba(0, 0, 0, 0.3);
    transition: all 0.2s;
  }

  .docs-mobile-menu-btn:hover {
    border-color: rgba(255, 255, 255, 0.15);
    background: rgba(18, 21, 26, 0.9);
  }

  .docs-sidebar {
    position: fixed;
    top: 0;
    left: -280px;
    width: 280px;
    height: 100vh;
    background: rgba(12, 14, 18, 0.95);
    backdrop-filter: blur(20px);
    -webkit-backdrop-filter: blur(20px);
    border-right: 1px solid rgba(255, 255, 255, 0.08);
    z-index: 2000;
    padding: 2rem 1.5rem;
    transition: transform 0.3s cubic-bezier(0.16, 1, 0.3, 1);
    display: flex;
    flex-direction: column;
    gap: 1.5rem;
    box-shadow: 10px 0 30px rgba(0, 0, 0, 0.5);
  }

  .docs-sidebar.mobile-open {
    transform: translateX(280px);
  }

  .mobile-sidebar-header {
    display: flex;
    justify-content: space-between;
    align-items: center;
    padding-bottom: 1rem;
    border-bottom: 1px solid rgba(255, 255, 255, 0.08);
  }

  .mobile-sidebar-title {
    font-family: var(--font-display);
    font-size: 1.125rem;
    font-weight: 700;
    color: var(--text-primary);
  }

  .close-btn {
    background: none;
    border: none;
    color: var(--text-secondary);
    cursor: pointer;
    padding: 4px;
    display: flex;
    align-items: center;
    justify-content: center;
    transition: color 0.2s;
  }

  .close-btn:hover {
    color: var(--text-primary);
  }

  .docs-mobile-overlay {
    position: fixed;
    top: 0;
    left: 0;
    width: 100vw;
    height: 100vh;
    background: rgba(0, 0, 0, 0.5);
    backdrop-filter: blur(4px);
    -webkit-backdrop-filter: blur(4px);
    z-index: 1999;
  }
}
</style>
<style>
/* Global styles for markdown-like content in docs */
.docs-content h1 {
  font-size: 2.5rem;
  margin-bottom: 1.5rem;
}

.docs-content h2 {
  font-size: 1.75rem;
  margin-top: 3rem;
  margin-bottom: 1rem;
  padding-bottom: 0.5rem;
  border-bottom: 1px solid rgba(255, 255, 255, 0.1);
}

.docs-content h3 {
  font-size: 1.25rem;
  margin-top: 2rem;
  margin-bottom: 0.75rem;
}

.docs-content p {
  color: var(--text-secondary);
  line-height: 1.6;
  margin-bottom: 1rem;
}

.docs-content ul {
  margin-bottom: 1.5rem;
  padding-left: 1.5rem;
  color: var(--text-secondary);
}

.docs-content li {
  margin-bottom: 0.5rem;
}

.docs-content table {
  width: 100%;
  border-collapse: collapse;
  margin: 2rem 0;
}

.docs-content th,
.docs-content td {
  padding: 0.75rem;
  border: 1px solid rgba(255, 255, 255, 0.1);
  text-align: left;
}

.docs-content th {
  background: rgba(255, 255, 255, 0.05);
  font-weight: 600;
}
</style>
