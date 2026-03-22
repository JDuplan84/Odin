/**
 * Shell component — top-level layout: header + sidebar + main content area.
 * Sub-components are mounted lazily when their panel becomes active.
 */

export function mountShell(container) {
  container.innerHTML = `
    <header class="odin-header">
      <span class="odin-header__logo">⚡ Odin</span>
      <nav class="odin-header__nav">
        <button class="nav-btn active" data-panel="plugins">Plugins</button>
        <button class="nav-btn" data-panel="dialogue">Dialogue</button>
        <button class="nav-btn" data-panel="scripts">Scripts</button>
      </nav>
    </header>
    <main class="odin-workspace">
      <aside class="odin-sidebar" id="sidebar"></aside>
      <section class="odin-content" id="content">
        <p class="placeholder">Select a plugin to begin.</p>
      </section>
    </main>
  `;

  const buttons = container.querySelectorAll('.nav-btn');
  buttons.forEach(btn => {
    btn.addEventListener('click', () => {
      buttons.forEach(b => b.classList.remove('active'));
      btn.classList.add('active');
      loadPanel(btn.dataset.panel);
    });
  });

  // Load default panel
  loadPanel('plugins');
}

async function loadPanel(name) {
  const { mount } = await import(`./${name}.js`);
  mount(document.getElementById('sidebar'), document.getElementById('content'));
}
