
describe("Login page", () => {
    before(() => {
      cy.log(`Visiting https://company.tld`)
      cy.visit("/")
    })
    it("Login with Google", () => {
      const username = Cypress.env("GOOGLE_USER")
      const password = Cypress.env("GOOGLE_PW")
      const loginUrl = Cypress.env("SITE_NAME")
      const cookieName = Cypress.env("COOKIE_NAME")
      const socialLoginOptions = {
        username,
        password,
        loginUrl,
        headless: true,
        logs: false,
        isPopup: true,
        loginSelector: `a[href="${Cypress.env(
          "SITE_NAME"
        )}/api/auth/signin/google"]`,
        postLoginSelector: "adminCreateBoard",
      }
  
      return cy
        .task("GoogleSocialLogin", socialLoginOptions)
        .then((cookies : any) => {
          cy.clearCookies()
  
          const cookie = cookies
            .filter((cookie: any) => cookie.name === cookieName)
            .pop()
          if (cookie) {
          
          //  Cypress.Cookies.defaults({
           //     preserve: cookieName,
          //  });
  
            // remove the two lines below if you need to stay logged in
            // for your remaining tests
           // cy.visit("/api/auth/signout")
           //cy.get("form").submit()
          }
        })
    })
  })