/**
 * Odin API client — thin wrapper over fetch().
 * All API calls go through these helpers so the base URL is configured in one place.
 */

const BASE_URL = import.meta?.env?.VITE_API_URL ?? 'http://localhost:5000';

async function request(method, path, body) {
  const options = {
    method,
    headers: { 'Content-Type': 'application/json' }
  };

  if (body !== undefined) {
    options.body = JSON.stringify(body);
  }

  const response = await fetch(`${BASE_URL}${path}`, options);

  if (!response.ok) {
    const text = await response.text();
    throw new Error(`API ${method} ${path} → ${response.status}: ${text}`);
  }

  const contentType = response.headers.get('content-type') ?? '';
  return contentType.includes('application/json') ? response.json() : null;
}

// ── Plugin API ────────────────────────────────────────────────────────────────

export const pluginApi = {
  getAvailable: ()             => request('GET',  '/api/plugin/available'),
  load:         (path)         => request('POST', '/api/plugin/load',       { path }),
  create:       (name, masters) => request('POST', '/api/plugin/create',    { name, masters }),
  addMaster:    (masterPath)   => request('POST', '/api/plugin/add-master', { masterPath }),
  save:         ()             => request('POST', '/api/plugin/save')
};

// ── Dialogue API ──────────────────────────────────────────────────────────────

export const dialogueApi = {
  getTopics:    ()                              => request('GET',  '/api/dialogue/topics'),
  createTopic:  (editorId, type, topicText)     => request('POST', '/api/dialogue/topics', { editorId, type, topicText }),
  addResponse:  (topicFormId, responseText, n)  => request('POST', `/api/dialogue/topics/${topicFormId}/responses`, { responseText, responseNumber: n }),
  addCondition: (responseFormId, condition)     => request('POST', `/api/dialogue/responses/${responseFormId}/conditions`, condition)
};

// ── Script API ────────────────────────────────────────────────────────────────

export const scriptApi = {
  decompile: (pexPath) => request('POST', '/api/script/decompile', { pexPath }),
  compile:   (pscPath) => request('POST', '/api/script/compile',   { pscPath })
};
