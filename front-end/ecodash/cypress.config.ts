import { defineConfig } from "cypress";
const { GoogleSocialLogin } = require("cypress-social-logins").plugins

export default defineConfig({
  component: {
    devServer: {
      framework: "next",
      bundler: "webpack",
    },
  },

  e2e: {
    baseUrl: "http://secodash.com:3000",
    experimentalModifyObstructiveThirdPartyCode: true,
    setupNodeEvents(on, config) {
      // implement node event listeners here
      on("task", {
      GoogleSocialLogin: GoogleSocialLogin,
    })
    },
  },

  env: {
    backend_url: "http://localhost:5003",
    frontend_url: "http://localhost:3000",
    urlPreFix: "http://",
    urlPost: ".localhost:3000"
  }
});
