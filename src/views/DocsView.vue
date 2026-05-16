<script setup lang="ts">
import { useRoute } from 'vue-router'

const route = useRoute()

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
</script>

<template>
  <div class="docs-layout container">
    <aside class="docs-sidebar">
      <nav class="docs-nav">
        <ul>
          <li v-for="item in navItems" :key="item.path">
            <router-link :to="item.path" :class="{ active: route.path === item.path }">
              {{ item.title }}
            </router-link>
          </li>
        </ul>
      </nav>
    </aside>
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
  padding: 0.5rem 1rem;
  color: var(--text-secondary);
  text-decoration: none;
  border-radius: 8px;
  transition: all 0.2s;
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

@media (max-width: 768px) {
  .docs-layout {
    flex-direction: column;
  }
  
  .docs-sidebar {
    width: 100%;
    position: static;
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
