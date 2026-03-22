<template>
  <div class="home">
    <div class="home__hero">
      <h2 class="home__heading">Welcome to <span class="accent">Odin</span></h2>
      <p class="home__sub">
        A modern, open-source alternative to the Skyrim Creation Kit.
        Load a plugin to begin editing.
      </p>
    </div>

    <div class="home__cards">
      <RouterLink
        v-for="card in quickLinks"
        :key="card.name"
        :to="{ name: card.name }"
        class="card"
      >
        <span class="card__icon">{{ card.icon }}</span>
        <div>
          <div class="card__title">{{ card.label }}</div>
          <div class="card__desc">{{ card.desc }}</div>
        </div>
      </RouterLink>
    </div>
  </div>
</template>

<script setup>
import { RouterLink } from 'vue-router'

const quickLinks = [
  { name: 'plugins',  label: 'Plugins',  icon: '⬡', desc: 'Load and manage .esm / .esp / .esl files' },
  { name: 'dialogue', label: 'Dialogue', icon: '💬', desc: 'Edit DIAL / INFO records' },
  { name: 'scripts',  label: 'Scripts',  icon: '📜', desc: 'Compile and decompile Papyrus scripts' }
]
</script>

<style scoped>
.home {
  display: flex;
  flex-direction: column;
  gap: 2rem;
  max-width: 720px;
}

.home__hero    { display: flex; flex-direction: column; gap: 0.5rem; }
.home__heading { font-size: 1.75rem; font-weight: 700; color: var(--text-primary); }
.home__sub     { color: var(--text-muted); line-height: 1.6; }
.accent        { color: var(--accent-amber); }

.home__cards {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(200px, 1fr));
  gap: 1rem;
}

.card {
  display: flex;
  align-items: flex-start;
  gap: 0.75rem;
  padding: 1rem;
  background: var(--bg-secondary);
  border: 1px solid var(--border);
  border-radius: 6px;
  text-decoration: none;
  color: inherit;
  transition: border-color 0.15s, background 0.15s;
}
.card:hover {
  border-color: var(--accent-amber);
  background: color-mix(in srgb, var(--accent-amber) 5%, var(--bg-secondary));
}

.card__icon  { font-size: 1.5rem; flex-shrink: 0; }
.card__title { font-weight: 600; font-size: 0.9rem; color: var(--text-primary); }
.card__desc  { font-size: 0.8rem; color: var(--text-muted); margin-top: 0.2rem; line-height: 1.4; }
</style>
