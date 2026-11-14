import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '../views/HomeView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView
    },
    {
      path: '/docs',
      name: 'docs',
      component: () => import('../views/DocsView.vue'),
      children: [
        { path: '', redirect: '/docs/getting-started' },
        { path: 'getting-started', component: () => import('../views/docs/GettingStarted.vue') },
        { path: 'basic-operations', component: () => import('../views/docs/BasicOperations.vue') },
        { path: 'repository', component: () => import('../views/docs/Repository.vue') },
        { path: 'search-filter', component: () => import('../views/docs/SearchFilter.vue') },
        { path: 'tags', component: () => import('../views/docs/Tags.vue') },
        { path: 'metadata', component: () => import('../views/docs/Metadata.vue') },
        { path: 'tools', component: () => import('../views/docs/Tools.vue') },
        { path: 'settings', component: () => import('../views/docs/Settings.vue') },
        { path: 'map', component: () => import('../views/docs/Map.vue') },
      ]
    }
  ],
  scrollBehavior(to, from, savedPosition) {
    if (to.hash) {
      return { el: to.hash, behavior: 'smooth' }
    }
    return savedPosition || { top: 0 }
  }
})

export default router
