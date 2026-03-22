<template>
  <div class="view">
    <div class="view__header">
      <h2 class="view__title">Dialogue Editor</h2>
      <p class="view__desc">Create and edit DIAL topics and INFO responses.</p>
    </div>
    <div class="placeholder-panel">
      <span>💬</span>
      <p>Dialogue tree panel — to be implemented in the Dialogue milestone.</p>
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
