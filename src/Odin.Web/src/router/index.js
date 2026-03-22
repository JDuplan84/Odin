import { createRouter, createWebHistory } from 'vue-router'

/**
 * Each route is lazy-loaded so the initial bundle stays small.
 * Add new tool panels here as new views — no changes needed in the layout.
 */
const routes = [
  {
    path:      '/',
    component: () => import('@/layouts/MainLayout.vue'),
    children: [
      {
        path:      '',
        name:      'home',
        component: () => import('@/views/HomeView.vue'),
        meta:      { label: 'Home' }
      },
      {
        path:      'plugins',
        name:      'plugins',
        component: () => import('@/views/PluginsView.vue'),
        meta:      { label: 'Plugins' }
      },
      {
        path:      'dialogue',
        name:      'dialogue',
        component: () => import('@/views/DialogueView.vue'),
        meta:      { label: 'Dialogue' }
      },
      {
        path:      'scripts',
        name:      'scripts',
        component: () => import('@/views/ScriptsView.vue'),
        meta:      { label: 'Scripts' }
      }
    ]
  },
  // Catch-all — redirect unknown paths to home
  { path: '/:pathMatch(.*)*', redirect: '/' }
]

export const router = createRouter({
  history: createWebHistory(),
  routes
})
