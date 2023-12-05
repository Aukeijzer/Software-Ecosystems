import { defineConfig } from "cypress";

export default defineConfig({
  component: {
    devServer: {
      framework: "next",
      bundler: "webpack",
    },
  },

  e2e: {
    baseUrl: "http://localhost:3000",
    setupNodeEvents(on, config) {
      // implement node event listeners here
    },
  },

  env: {
    backend_url: "http://localhost:5003",
    frontend_url: "http://localhost:3000",
    urlPreFix: "http://",
    urlPost: ".localhost:3000"
  }
});
