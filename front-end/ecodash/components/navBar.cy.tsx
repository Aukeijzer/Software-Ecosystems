import NavBar from "./navBar"

describe('NavBar', () => {
    it("contains the correct NavLogo component", () => {
        cy.mount(<NavBar />)
        cy.get('[data-cy="navBar"]').children().get('[data-cy="navLogo"]').should('exist')
    })
})