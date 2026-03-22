import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'
import { resolve } from 'path'

export default defineConfig({
  plugins: [vue()],

  resolve: {
    alias: { '@': resolve(__dirname, 'src') }
  },

  // Production build → output to wwwroot/ for the .NET static file server
  build: {
    outDir:    'wwwroot',
    emptyOutDir: true
  },

  server: {
    port: 5173,
    // Proxy API calls to Odin.API during development
    proxy: {
      '/api': {
        target:      'http://localhost:5000',
        changeOrigin: true
      }
    }
  }
})
