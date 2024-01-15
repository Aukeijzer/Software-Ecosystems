import NavBarTop from "@/components/NavbarTop"

describe('NavBar', () => {
    it("contains the correct NavLogo component", () => {
        cy.mount(<NavBarTop />)
        
         // Check if the Navbar, logo and title are rendered
         cy.get('[data-cy=navBar]').should('exist');
         cy.get('[data-cy=navLogo]').should('exist');
         cy.contains('SECODash').should('exist');
    })

    it('navigates to the specified URL on clicking the Navbar link', () => {
        const expectedURL = 'http://localhost:3000';

        cy.mount(<NavBarTop />);

        // Click the Navbar link and check the URL
        cy.get('[data-cy=navBar]').find('a').should('have.attr', 'href', expectedURL).click()
    });
})