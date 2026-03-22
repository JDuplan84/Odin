<template>
  <aside class="sidebar">
    <!-- Branding -->
    <div class="sidebar__brand">
      <span class="sidebar__logo">⚡</span>
      <span class="sidebar__title">Odin</span>
    </div>

    <!-- Navigation -->
    <nav class="sidebar__nav">
      <div
        v-for="section in navSections"
        :key="section.label"
        class="sidebar__section"
      >
        <span class="sidebar__section-label">{{ section.label }}</span>
        <RouterLink
          v-for="item in section.items"
          :key="item.name"
          :to="{ name: item.name }"
          class="sidebar__link"
          active-class="sidebar__link--active"
        >
          <span class="sidebar__link-icon" aria-hidden="true">{{ item.icon }}</span>
          <span>{{ item.label }}</span>
        </RouterLink>
      </div>
    </nav>

    <!-- Footer -->
    <div class="sidebar__footer">
      <span class="sidebar__version">v0.1.0</span>
    </div>
  </aside>
</template>

<script setup>
import { RouterLink } from 'vue-router'

const navSections = [
  {
    label: 'Files',
    items: [
      { name: 'home',    label: 'Dashboard', icon: '⊞' },
      { name: 'plugins', label: 'Plugins',   icon: '⬡' }
    ]
  },
  {
    label: 'Editing',
    items: [
      { name: 'dialogue', label: 'Dialogue', icon: '💬' },
      { name: 'scripts',  label: 'Scripts',  icon: '📜' }
    ]
  },
  {
    label: 'Coming soon',
    items: [
      { name: 'home', label: 'Audio',     icon: '🔊' },
      { name: 'home', label: '3D Preview', icon: '🧊' }
    ]
  }
]
</script>

<style scoped>
.sidebar {
  display: flex;
  flex-direction: column;
  background: var(--bg-secondary);
  border-right: 1px solid var(--border);
  overflow-y: auto;
  height: 100vh;
}

/* Brand */
.sidebar__brand {
  display: flex;
  align-items: center;
  gap: 0.6rem;
  padding: 1.25rem 1.25rem 1rem;
  border-bottom: 1px solid var(--border);
  flex-shrink: 0;
}
.sidebar__logo  { font-size: 1.4rem; }
.sidebar__title {
  font-size: 1.2rem;
  font-weight: 700;
  color: var(--accent-amber);
  letter-spacing: 0.05em;
  text-transform: uppercase;
}

/* Nav */
.sidebar__nav {
  flex: 1;
  padding: 0.75rem 0;
  display: flex;
  flex-direction: column;
  gap: 1.25rem;
}

.sidebar__section {
  display: flex;
  flex-direction: column;
  gap: 0.15rem;
}

.sidebar__section-label {
  font-size: 0.65rem;
  font-weight: 600;
  text-transform: uppercase;
  letter-spacing: 0.1em;
  color: var(--text-muted);
  padding: 0 1.25rem 0.25rem;
}

.sidebar__link {
  display: flex;
  align-items: center;
  gap: 0.6rem;
  padding: 0.45rem 1.25rem;
  color: var(--text-muted);
  text-decoration: none;
  font-size: 0.875rem;
  border-left: 3px solid transparent;
  transition: color 0.15s, background 0.15s, border-color 0.15s;
}
.sidebar__link:hover {
  color: var(--text-primary);
  background: color-mix(in srgb, var(--accent-blue) 8%, transparent);
}
.sidebar__link--active {
  color: var(--accent-amber);
  border-left-color: var(--accent-amber);
  background: color-mix(in srgb, var(--accent-amber) 8%, transparent);
}

.sidebar__link-icon { width: 1rem; text-align: center; }

/* Footer */
.sidebar__footer {
  padding: 0.75rem 1.25rem;
  border-top: 1px solid var(--border);
  flex-shrink: 0;
}
.sidebar__version {
  font-size: 0.7rem;
  color: var(--text-muted);
}
</style>
