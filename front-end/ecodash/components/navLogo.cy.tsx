import NavLogo from "./navLogo";

describe('NavLogo', () => {
    it("contains a link component ", () => {
        cy.mount(<NavLogo />)
        cy.get('[data-cy="navLogo').children().get('[data-cy="navLogoLink"]').should('exist')
    })
}); 