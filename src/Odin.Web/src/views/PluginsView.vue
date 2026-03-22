<template>
  <div class="view">
    <div class="view__header">
      <h2 class="view__title">Plugin Manager</h2>
      <p class="view__desc">Browse, load and create Bethesda plugin files (.esm / .esp / .esl).</p>
    </div>
    <div class="placeholder-panel">
      <span>🚧</span>
      <p>Plugin panel — to be implemented in the Mutagen milestone.</p>
    </div>
  </div>
</template>

<style scoped>
.view { display: flex; flex-direction: column; gap: 1.5rem; }
.view__title { font-size: 1.2rem; font-weight: 600; color: var(--text-primary); }
.view__desc  { color: var(--text-muted); font-size: 0.85rem; }
.placeholder-panel {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  gap: 0.75rem;
  padding: 3rem;
  border: 1px dashed var(--border);
  border-radius: 6px;
  color: var(--text-muted);
  font-size: 0.875rem;
}
.placeholder-panel span { font-size: 2rem; }
</style>
