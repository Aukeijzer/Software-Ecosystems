/*
Copyright (C) <2024> <OdinDash>

This file is part of SECODash

SECODash is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published
by the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

SECODash is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License for more details.

You should have received a copy of the GNU Affero General Public License
along with SECODash.  If not, see <https://www.gnu.org/licenses/>.

*/

escribe("Login functionality", () => {
    //Sadly this is not implemented fuly. OAuth2 is hard to test with cypress.
    //Link to github issue:https://github.com/lirantal/cypress-social-logins/issues/43
    /*
    it("Login with Google", () => {
        const username = Cypress.env("GOOGLE_USER")
        const password = Cypress.env("GOOGLE_PW")
        const loginUrl = Cypress.env("SITE_NAME")
        const cookieName = Cypress.env("COOKIE_NAME")
        cy.visit('http://localhost:3000')
        //cy.get('[data-cy="loginButton"]').click();
        cy.get('[data-cy=".oauthPopUp"]').should('be.visible');

        const socialLoginOptions = {
            username,
            password,
            loginUrl,
            headless: false,
            logs: true,
            isPopup: false,
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
    */
})
        