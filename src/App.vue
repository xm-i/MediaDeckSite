<script setup lang="ts">
import { ref, onMounted, onUnmounted, watch } from 'vue'
import { useRoute } from 'vue-router'
import './index.css'
import logoImage from './images/Square150x150Logo.scale-200.png'

const route = useRoute()
const isScrolled = ref(false)
const isMobileMenuOpen = ref(false)

const handleScroll = () => {
  isScrolled.value = window.scrollY > 16
}

const handleKeydown = (e: KeyboardEvent) => {
  if (e.key === 'Escape' && isMobileMenuOpen.value) {
    isMobileMenuOpen.value = false
  }
}

watch(
  () => route.fullPath,
  () => {
    isMobileMenuOpen.value = false
  }
)

onMounted(() => {
  window.addEventListener('scroll', handleScroll, { passive: true })
  window.addEventListener('keydown', handleKeydown)
})

onUnmounted(() => {
  window.removeEventListener('scroll', handleScroll)
  window.removeEventListener('keydown', handleKeydown)
})
</script>

<template>
  <div class="app-wrapper">
    <div class="grid-bg" aria-hidden="true"></div>

    <header :class="['site-header', { 'site-header-scrolled': isScrolled || isMobileMenuOpen }]">
      <div class="nav-content container">
        <router-link to="/" class="logo" @click="isMobileMenuOpen = false">
          <img :src="logoImage" alt="MediaDeck Logo" class="logo-img" width="24" height="24" />
          <span class="logo-text">MediaDeck</span>
        </router-link>

        <nav class="nav-links" aria-label="メインナビゲーション">
          <router-link to="/#features" class="nav-link">機能</router-link>
          <router-link to="/#media" class="nav-link">対応形式</router-link>
          <router-link
            to="/docs"
            :class="['nav-link', { 'is-active': route.path.startsWith('/docs') }]"
          >
            ドキュメント
          </router-link>
          <a href="https://www.xm-i.net/" target="_blank" rel="noopener noreferrer" class="nav-link">
            xmi
          </a>
          <div class="nav-divider" aria-hidden="true"></div>
          <a
            href="https://github.com/xm-i/MediaDeck"
            target="_blank"
            rel="noopener noreferrer"
            class="btn btn-secondary btn-sm"
          >
            <svg width="15" height="15" viewBox="0 0 24 24" fill="currentColor" aria-hidden="true">
              <path
                d="M12 2C6.477 2 2 6.484 2 12.017c0 4.425 2.865 8.18 6.839 9.504.5.092.682-.217.682-.483 0-.237-.008-.868-.013-1.703-2.782.605-3.369-1.343-3.369-1.343-.454-1.158-1.11-1.466-1.11-1.466-.908-.62.069-.608.069-.608 1.003.07 1.53 1.032 1.53 1.032.892 1.53 2.341 1.088 2.91.832.092-.647.35-1.088.636-1.338-2.22-.253-4.555-1.113-4.555-4.951 0-1.093.39-1.988 1.029-2.688-.103-.253-.446-1.272.098-2.65 0 0 .84-.27 2.75 1.026A9.564 9.564 0 0 1 12 6.844c.85.004 1.705.115 2.504.337 1.909-1.296 2.747-1.027 2.747-1.027.546 1.379.202 2.398.1 2.651.64.7 1.028 1.595 1.028 2.688 0 3.848-2.339 4.695-4.566 4.943.359.309.678.92.678 1.855 0 1.338-.012 2.419-.012 2.747 0 .268.18.58.688.482A10.019 10.019 0 0 0 22 12.017C22 6.484 17.522 2 12 2Z"
              />
            </svg>
            GitHub
          </a>
          <a
            href="https://github.com/xm-i/MediaDeck/releases"
            target="_blank"
            rel="noopener noreferrer"
            class="btn btn-primary btn-sm"
          >
            ダウンロード
          </a>
        </nav>

        <button
          type="button"
          class="mobile-menu-btn"
          @click="isMobileMenuOpen = !isMobileMenuOpen"
          :aria-expanded="isMobileMenuOpen"
          aria-controls="mobile-navigation-drawer"
          aria-label="メニューを開閉"
        >
          <svg
            v-if="!isMobileMenuOpen"
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
            <line x1="4" x2="20" y1="7" y2="7" />
            <line x1="4" x2="20" y1="12" y2="12" />
            <line x1="4" x2="20" y1="17" y2="17" />
          </svg>
          <svg
            v-else
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

      <transition name="drawer-drop">
        <div
          v-if="isMobileMenuOpen"
          id="mobile-navigation-drawer"
          class="mobile-menu-panel"
        >
          <nav class="container mobile-menu-inner" aria-label="モバイルナビゲーション">
            <router-link to="/#features" class="mobile-nav-link" @click="isMobileMenuOpen = false">
              機能
            </router-link>
            <router-link to="/#media" class="mobile-nav-link" @click="isMobileMenuOpen = false">
              対応形式
            </router-link>
            <router-link
              to="/docs"
              :class="['mobile-nav-link', { 'is-active': route.path.startsWith('/docs') }]"
              @click="isMobileMenuOpen = false"
            >
              ドキュメント
            </router-link>
            <a
              href="https://www.xm-i.net/"
              target="_blank"
              rel="noopener noreferrer"
              class="mobile-nav-link"
              @click="isMobileMenuOpen = false"
            >
              xmi
            </a>
            <div class="mobile-menu-actions">
              <a
                href="https://github.com/xm-i/MediaDeck"
                target="_blank"
                rel="noopener noreferrer"
                class="btn btn-secondary btn-lg"
                @click="isMobileMenuOpen = false"
              >
                GitHub
              </a>
              <a
                href="https://github.com/xm-i/MediaDeck/releases"
                target="_blank"
                rel="noopener noreferrer"
                class="btn btn-primary btn-lg"
                @click="isMobileMenuOpen = false"
              >
                ダウンロード
              </a>
            </div>
          </nav>
        </div>
      </transition>
    </header>

    <router-view />

    <footer class="footer">
      <div class="container footer-inner">
        <div class="footer-brand">
          <div class="footer-title-row">
            <img :src="logoImage" alt="" class="footer-logo-img" width="20" height="20" />
            <span class="footer-name">MediaDeck</span>
          </div>
          <p class="footer-desc">
            Windows 10 / 11 向けの高機能メディア管理アプリケーション
          </p>
        </div>

        <div class="footer-meta">
          <nav class="footer-links" aria-label="フッターナビゲーション">
            <router-link to="/docs">ドキュメント</router-link>
            <a href="https://github.com/xm-i/MediaDeck" target="_blank" rel="noopener noreferrer">
              GitHub
            </a>
            <a
              href="https://github.com/xm-i/MediaDeck/releases"
              target="_blank"
              rel="noopener noreferrer"
            >
              リリース
            </a>
            <a href="https://www.xm-i.net/" target="_blank" rel="noopener noreferrer">
              xmi (他のプロダクト)
            </a>
          </nav>
          <p class="copyright">
            Released under the MIT License. &copy; {{ new Date().getFullYear() }} MediaDeck Contributors.
          </p>
        </div>
      </div>
    </footer>
  </div>
</template>

<style scoped>
.app-wrapper {
  position: relative;
  min-height: 100vh;
  display: flex;
  flex-direction: column;
}

.site-header {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  height: 60px;
  z-index: 1000;
  background-color: rgba(9, 10, 13, 0.72);
  backdrop-filter: blur(12px);
  -webkit-backdrop-filter: blur(12px);
  border-bottom: 1px solid var(--border-subtle);
  transition:
    background-color 160ms cubic-bezier(0.23, 1, 0.32, 1),
    border-color 160ms cubic-bezier(0.23, 1, 0.32, 1);
}

.site-header-scrolled {
  background-color: var(--bg-glass);
  border-bottom-color: var(--border-hover);
}

.nav-content {
  height: 100%;
  display: flex;
  align-items: center;
  justify-content: space-between;
  gap: 16px;
}

.logo {
  display: inline-flex;
  align-items: center;
  gap: 10px;
  font-weight: 700;
  font-size: 1rem;
  letter-spacing: -0.02em;
  color: var(--text-primary);
  text-decoration: none;
  border-radius: var(--radius-xs);
}

.logo-img {
  width: 24px;
  height: 24px;
  object-fit: contain;
  display: block;
}

.nav-links {
  display: flex;
  align-items: center;
  gap: 8px;
}

.nav-link {
  display: inline-flex;
  align-items: center;
  padding: 6px 10px;
  font-size: 0.875rem;
  font-weight: 500;
  color: var(--text-secondary);
  border-radius: var(--radius-sm);
  transition:
    color 140ms cubic-bezier(0.23, 1, 0.32, 1),
    background-color 140ms cubic-bezier(0.23, 1, 0.32, 1);
}

.nav-link:hover {
  color: var(--text-primary);
  background-color: rgba(255, 255, 255, 0.04);
}

.nav-link.is-active {
  color: var(--text-primary);
  background-color: rgba(255, 255, 255, 0.06);
}

.nav-divider {
  width: 1px;
  height: 18px;
  background-color: var(--border-subtle);
  margin: 0 4px;
}

.mobile-menu-btn {
  display: none;
  min-width: 44px;
  min-height: 44px;
  align-items: center;
  justify-content: center;
  background: transparent;
  border: 1px solid transparent;
  border-radius: var(--radius-sm);
  color: var(--text-primary);
  cursor: pointer;
  transition:
    background-color 140ms cubic-bezier(0.23, 1, 0.32, 1),
    transform 80ms cubic-bezier(0.23, 1, 0.32, 1);
}

.mobile-menu-btn:hover {
  background-color: rgba(255, 255, 255, 0.06);
}

.mobile-menu-btn:active {
  transform: scale(0.97);
}

.mobile-menu-panel {
  position: fixed;
  top: 60px;
  left: 0;
  right: 0;
  background-color: var(--bg-surface);
  border-bottom: 1px solid var(--border-subtle);
  box-shadow: 0 16px 40px rgba(0, 0, 0, 0.6);
  transform-origin: top center;
}

.mobile-menu-inner {
  display: flex;
  flex-direction: column;
  padding-top: 12px;
  padding-bottom: 20px;
  gap: 4px;
}

.mobile-nav-link {
  display: flex;
  align-items: center;
  min-height: 44px;
  padding: 0 12px;
  font-size: 0.9375rem;
  font-weight: 500;
  color: var(--text-secondary);
  border-radius: var(--radius-sm);
  transition:
    color 140ms cubic-bezier(0.23, 1, 0.32, 1),
    background-color 140ms cubic-bezier(0.23, 1, 0.32, 1);
}

.mobile-nav-link:hover,
.mobile-nav-link.is-active {
  color: var(--text-primary);
  background-color: rgba(255, 255, 255, 0.05);
}

.mobile-menu-actions {
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap: 12px;
  margin-top: 12px;
  padding-top: 12px;
  border-top: 1px solid var(--border-subtle);
}

.drawer-drop-enter-active,
.drawer-drop-leave-active {
  transition:
    opacity 180ms cubic-bezier(0.23, 1, 0.32, 1),
    transform 180ms cubic-bezier(0.23, 1, 0.32, 1);
}

.drawer-drop-enter-from,
.drawer-drop-leave-to {
  opacity: 0;
  transform: translateY(-6px) scaleY(0.98);
}

/* Footer */
.footer {
  margin-top: auto;
  padding: 40px 0;
  border-top: 1px solid var(--border-subtle);
  background-color: var(--bg-canvas);
}

.footer-inner {
  display: flex;
  align-items: flex-start;
  justify-content: space-between;
  gap: 24px;
}

.footer-title-row {
  display: flex;
  align-items: center;
  gap: 8px;
  margin-bottom: 6px;
}

.footer-logo-img {
  width: 20px;
  height: 20px;
  object-fit: contain;
  opacity: 0.75;
}

.footer-name {
  font-weight: 600;
  font-size: 0.9375rem;
  color: var(--text-primary);
}

.footer-desc {
  font-size: 0.8125rem;
  color: var(--text-muted);
}

.footer-meta {
  display: flex;
  flex-direction: column;
  align-items: flex-end;
  gap: 8px;
}

.footer-links {
  display: flex;
  flex-wrap: wrap;
  gap: 20px;
  font-size: 0.875rem;
}

.footer-links a {
  color: var(--text-secondary);
}

.footer-links a:hover {
  color: var(--text-primary);
}

.copyright {
  color: var(--text-muted);
  font-size: 0.8125rem;
}

@media (max-width: 768px) {
  .nav-links {
    display: none;
  }

  .mobile-menu-btn {
    display: inline-flex;
  }

  .footer-inner {
    flex-direction: column;
    align-items: flex-start;
  }

  .footer-meta {
    align-items: flex-start;
  }

  .footer-links {
    gap: 16px;
  }
}

@media (min-width: 769px) {
  .mobile-menu-panel {
    display: none;
  }
}
</style>
