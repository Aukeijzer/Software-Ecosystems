import NavBarTop from "./navbarTop"


describe('NavBar', () => {
    it("contains the correct NavLogo component", () => {
        cy.mount(<NavBarTop />)
        cy.get('[data-cy="navBar"]').children().get('[data-cy="navLogo"]').should('exist')
    })
})