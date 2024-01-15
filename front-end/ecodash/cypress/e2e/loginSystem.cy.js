describe("Login functionality", () => {
    //This does not work cause session is not set
    
    it("Visit newDashboard not logged in", () => {
        cy.visit("http://localhost:3000/newDashboard")
        cy.get('[data-cy="newDashboard"]').should("be.not.visible")
    })

    it("Login with Google", () => {
        const username = Cypress.env("GOOGLE_USER")
        const password = Cypress.env("GOOGLE_PW")
        const loginUrl = Cypress.env("SITE_NAME")
        const cookieName = Cypress.env("COOKIE_NAME")
        cy.visit('http://localhost:3000')
        //cy.get('[data-cy="loginButton"]').click();
        cy.get('[data-cy=".oauthPopUp"]').should('be.visible');
    
        //ECHT WAAR DIT KAN ALLEMAAL DE TYFUS KRIJGEN 
        //TYF OP MET JE KUT TESTEN VAN LOGIN SYSTEM ECHT KRIJG DE TERING

        const socialLoginOptions = {
            username,
            password,
            loginUrl,
            headless: false,
            logs: true,
            isPopup: true,
            //loginSelector: `a[href="${Cypress.env("SITE_NAME")} /api/auth/signin/google"]`,
            loginSelector: '[data-cy="googleLoginButton"]',
            postLoginSelector: '[data-cy="loggedInSelector"].Click()'
        }

        return cy
        .task("GoogleSocialLogin", socialLoginOptions).then(({ cookies }) => {
             cy.clearCookies()

             const cookie = cookies
             .filter((cookie) => cookie.name === cookieName)
             .pop()

             if (cookie) {
                cy.setCookie(cookie.name, cookie.value, {
                    domain: cookie.domain,
                    expiry: cookie.expires,
                    httpOnly: cookie.httpOnly,
                    path: cookie.path,
                    secure: cookie.secure,
                })

                Cypress.Cookies.defaults({
                    preserve: cookieName,
                })
            
            }
        })
    })
})
        