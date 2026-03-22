/**
 * Odin — Application entry point.
 *
 * Architecture: vanilla JS component model using ES modules.
 * Each file in /components/ exports a single async mount(container) function.
 * Components communicate via a lightweight EventBus (see ./eventBus.js).
 *
 * Replace with Vue / React if the component surface grows beyond a manageable size.
 */

import { mountShell } from './components/shell.js';

document.addEventListener('DOMContentLoaded', () => {
  mountShell(document.getElementById('app'));
});
