/**
 * Plugins panel — lists available plugins and handles load/create actions.
 */

import { pluginApi } from '../js/api.js';

export async function mount(sidebar, content) {
  sidebar.innerHTML = `
    <div class="panel">
      <h2 class="panel__title">Plugin Files</h2>
      <ul class="plugin-list" id="plugin-list">
        <li class="plugin-list__item--loading">Loading…</li>
      </ul>
    </div>
  `;

  content.innerHTML = `
    <div class="panel">
      <h2 class="panel__title">Active Plugin</h2>
      <p class="text-muted" id="active-plugin-info">No plugin loaded.</p>
    </div>
  `;

  try {
    const plugins = await pluginApi.getAvailable();
    const list = document.getElementById('plugin-list');
    list.innerHTML = '';

    if (plugins.length === 0) {
      list.innerHTML = '<li class="text-muted">No plugins found in Data folder.</li>';
      return;
    }

    plugins.forEach(path => {
      const li = document.createElement('li');
      li.className = 'plugin-list__item';
      li.textContent = path.split(/[\\/]/).pop();
      li.title = path;
      li.addEventListener('click', () => loadPlugin(path));
      list.appendChild(li);
    });
  } catch (err) {
    document.getElementById('plugin-list').innerHTML =
      `<li class="text-error">Error: ${err.message}</li>`;
  }
}

async function loadPlugin(path) {
  const info = document.getElementById('active-plugin-info');
  info.textContent = `Loading ${path.split(/[\\/]/).pop()}…`;

  try {
    const plugin = await pluginApi.load(path);
    info.innerHTML = `
      <strong>${plugin.name}</strong><br>
      <span class="text-muted">Type: ${plugin.type} | Masters: ${plugin.masters?.join(', ') || 'none'}</span>
    `;
  } catch (err) {
    info.textContent = `Error: ${err.message}`;
  }
}
