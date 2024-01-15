/// <reference types="cypress" />
import { JWTPayload } from "jose";

declare global {
  namespace Cypress {
    interface Chainable {
      /**
       * Custom command to set cookie and mock next-auth login during testing
       * @example cy.login(<USER_OBJECT>)
       */
      login(userObj): Chainable<Element>;
    }
  }
}