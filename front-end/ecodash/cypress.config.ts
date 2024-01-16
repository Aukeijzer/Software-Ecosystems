import { defineConfig } from "cypress";
const { GoogleSocialLogin } = require('cypress-social-logins').plugins;

export default defineConfig({
  component: {
    devServer: {
      framework: "next",
      bundler: "webpack",
    },
  },

  e2e: {
    setupNodeEvents(on, config) {
      // implement node event listeners here
      on('before:browser:launch', (browser, launchOptions) => {
        console.log(launchOptions.args);
        let removeFlags = [
          '--enable-automation',
        ];
        launchOptions.args = launchOptions.args.filter(value => !removeFlags.includes(value));
        return launchOptions
      });
      
      on('task', {
            GoogleSocialLogin: GoogleSocialLogin,
      });
    },
    baseUrl: "http://localhost:3000",
    chromeWebSecurity: true
  },

});
