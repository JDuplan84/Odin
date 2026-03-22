<template>
  <div class="app-shell">
    <AppSidebar />
    <div class="app-body">
      <AppHeader />
      <main class="app-content">
        <RouterView v-slot="{ Component }">
          <Transition name="fade" mode="out-in">
            <component :is="Component" :key="route.path" />
          </Transition>
        </RouterView>
      </main>
    </div>
  </div>
</template>

<script setup>
import { useRoute }  from 'vue-router'
import { RouterView } from 'vue-router'
import AppSidebar    from '@/components/AppSidebar.vue'
import AppHeader     from '@/components/AppHeader.vue'

const route = useRoute()
</script>

<style scoped>
.app-shell {
  display: grid;
  grid-template-columns: var(--sidebar-width) 1fr;
  height: 100vh;
  overflow: hidden;
}

.app-body {
  display: grid;
  grid-template-rows: var(--header-height) 1fr;
  overflow: hidden;
  background: var(--bg-primary);
}

.app-content {
  overflow-y: auto;
  padding: 1.5rem;
}

/* Route transition */
.fade-enter-active,
.fade-leave-active { transition: opacity 0.15s ease; }
.fade-enter-from,
.fade-leave-to     { opacity: 0; }
</style>
