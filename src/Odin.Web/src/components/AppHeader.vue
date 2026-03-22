<template>
  <header class="app-header">
    <h1 class="app-header__title">{{ pageTitle }}</h1>
    <div class="app-header__actions">
      <!-- Future: plugin status indicator, settings button, etc. -->
      <span class="app-header__status" :class="statusClass">{{ statusLabel }}</span>
    </div>
  </header>
</template>

<script setup>
import { computed } from 'vue'
import { useRoute } from 'vue-router'

const route = useRoute()

const pageTitle = computed(() => route.meta?.label ?? 'Odin')

// Placeholder — will be driven by a store once plugin state is managed globally
const statusLabel = 'No plugin loaded'
const statusClass = 'app-header__status--idle'
</script>

<style scoped>
.app-header {
  display: flex;
  align-items: center;
  justify-content: space-between;
  padding: 0 1.5rem;
  background: var(--bg-secondary);
  border-bottom: 1px solid var(--border);
  height: var(--header-height);
  flex-shrink: 0;
}

.app-header__title {
  font-size: 0.95rem;
  font-weight: 600;
  color: var(--text-primary);
}

.app-header__status {
  font-size: 0.75rem;
  padding: 0.2rem 0.6rem;
  border-radius: 999px;
  background: var(--border);
  color: var(--text-muted);
}
.app-header__status--idle   { background: color-mix(in srgb, var(--text-muted) 15%, transparent); }
.app-header__status--active { background: color-mix(in srgb, var(--accent-blue)  20%, transparent); color: var(--accent-blue); }
</style>
