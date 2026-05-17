<script setup lang="ts">
import { ref, onMounted, onUnmounted } from 'vue'
import './index.css'
import logoImage from './images/Square150x150Logo.scale-200.png'

const isScrolled = ref(false)

const handleScroll = () => {
  isScrolled.value = window.scrollY > 50
}

onMounted(() => {
  window.addEventListener('scroll', handleScroll)
})

onUnmounted(() => {
  window.removeEventListener('scroll', handleScroll)
})
</script>

<template>
  <div class="app-wrapper">
    <div class="grid-bg"></div>
    <div class="glow-bg"></div>

    <nav :class="['navbar', { 'navbar-scrolled': isScrolled }]">
      <div class="nav-content container">
        <router-link to="/" class="logo">
          <img :src="logoImage" alt="MediaDeck Logo" class="logo-img" />
          <span class="logo-text">MediaDeck</span>
        </router-link>
        <div class="nav-links">
          <router-link to="/#features">機能</router-link>
          <router-link to="/docs">ドキュメント</router-link>
          <a href="https://www.xm-i.net/" target="_blank" rel="noopener noreferrer">xmi</a>
          <a href="https://github.com/xm-i/MediaDeck" target="_blank" class="btn btn-primary btn-sm">
            GitHub
          </a>
        </div>
      </div>
    </nav>

    <router-view />

    <footer class="footer">
      <div class="container text-center">
        <div class="footer-logo">
          <img :src="logoImage" alt="MediaDeck Logo" class="logo-img grayscale" />
          <span class="logo-text text-muted">MediaDeck</span>
        </div>
        <div class="footer-links">
          <a href="https://www.xm-i.net/" target="_blank" rel="noopener noreferrer">xmi (他のプロダクト)</a>
          <a href="https://github.com/xm-i/MediaDeck" target="_blank" rel="noopener noreferrer">GitHub</a>
        </div>
        <p class="copyright">Released under the MIT License. &copy; {{ new Date().getFullYear() }} MediaDeck Contributors.</p>
      </div>
    </footer>
  </div>
</template>

<style scoped>
/* Scoped styles specific to the layout */
.logo-img {
  width: 28px;
  height: 28px;
  object-fit: contain;
}

.grayscale {
  filter: grayscale(1) opacity(0.5);
}

.app-wrapper {
  min-height: 100vh;
  display: flex;
  flex-direction: column;
}

.text-muted {
  color: var(--text-muted);
}

.navbar {
  position: fixed;
  top: 1.5rem;
  left: 50%;
  transform: translateX(-50%);
  width: calc(100% - 3rem);
  max-width: 800px;
  z-index: 1000;
  padding: 0.5rem 1.5rem;
  border-radius: 9999px;
  transition: all 0.4s cubic-bezier(0.16, 1, 0.3, 1);
}

.navbar-scrolled {
  background: rgba(10, 10, 10, 0.8);
  backdrop-filter: blur(16px);
  border: 1px solid rgba(255, 255, 255, 0.1);
  box-shadow: 0 10px 30px rgba(0, 0, 0, 0.5);
  top: 1rem;
}

.nav-content {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 0;
}

.logo {
  display: flex;
  align-items: center;
  gap: 0.75rem;
  font-weight: 800;
  font-size: 1.25rem;
  letter-spacing: -0.03em;
  font-family: var(--font-display);
  color: inherit;
  text-decoration: none;
}

.nav-links {
  display: flex;
  align-items: center;
  gap: 1.5rem;
  font-size: 0.875rem;
  font-weight: 500;
}

.nav-links a:not(.btn) {
  opacity: 0.7;
  transition: opacity 0.2s ease;
  color: inherit;
  text-decoration: none;
}

.nav-links a:not(.btn):hover,
.nav-links .router-link-active:not(.btn) {
  opacity: 1;
}

.btn {
  display: inline-flex;
  align-items: center;
  justify-content: center;
  gap: 0.5rem;
  border-radius: 9999px;
  font-weight: 600;
  cursor: pointer;
  transition: all 0.3s cubic-bezier(0.16, 1, 0.3, 1);
  border: none;
  font-family: var(--font-display);
  text-decoration: none;
}

.btn-sm {
  padding: 0.5rem 1.25rem;
  font-size: 0.8125rem;
}

.btn-primary {
  background: #fff;
  color: #000;
  box-shadow: 0 0 0 rgba(255, 255, 255, 0);
}

.btn-primary:hover {
  transform: translateY(-2px) scale(1.02);
  box-shadow: 0 10px 25px rgba(255, 255, 255, 0.2);
}

/* Footer */
.footer {
  padding: 4rem 0;
  border-top: 1px solid var(--border-color);
  margin-top: auto;
}

.footer-logo {
  display: flex;
  align-items: center;
  justify-content: center;
  gap: 0.5rem;
  margin-bottom: 1rem;
}

.footer-links {
  display: flex;
  justify-content: center;
  gap: 2rem;
  margin-bottom: 1.5rem;
  font-size: 0.875rem;
}

.footer-links a {
  color: var(--text-secondary);
  transition: color 0.2s ease;
}

.footer-links a:hover {
  color: var(--accent-color);
}

.copyright {
  color: var(--text-muted);
  font-size: 0.875rem;
}

@media (max-width: 768px) {
  .nav-links { display: none; }
}
</style>
