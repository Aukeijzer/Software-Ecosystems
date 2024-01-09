

describe("Login page", () => {
       
        it("Login with Google", () => {
            //cy.visit("/");
           
            const username = Cypress.env("GOOGLE_USER")
            const password = Cypress.env("GOOGLE_PW")
            const loginUrl = Cypress.env("SITE_NAME")
            const cookieName = Cypress.env("COOKIE_NAME")
            cy.get('[data-cy="loginButton"]').click();

            const socialLoginOptions = {
                username,
                password,
                loginUrl,
                headless: false,
                logs: true,
                isPopup: true,
                loginSelector: `[data-cy="googleLoginButton"]`,
                postLoginSelector: '[data-cy="loggedInSelector"]',
            }
    
            return cy
                .task("GoogleSocialLogin", socialLoginOptions)
                .then((cookies : any) => {
                    cy.clearCookies()
    
                    const cookie = cookies
                        .filter((cookie: any) => cookie.name === cookieName)
                        .pop()
                    if (cookie) {
                       
                        // Add a check to verify if login was successful on the popup window
                        cy.get(socialLoginOptions.postLoginSelector).should('exist').then(() => {
                            // remove the two lines below if you need to stay logged in
                            // for your remaining tests
                            cy.visit("/api/auth/signout")
                            cy.get("form").submit()
                        })
                    }
                })
                
                
        })
    })


