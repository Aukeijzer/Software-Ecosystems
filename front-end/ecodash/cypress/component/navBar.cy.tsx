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

import NavBarTop from "@/components/navbarTop"

describe('NavBar', () => {
    /*
    This test is not working because of the useSession hook.
    Disabled until we can figure out how to mock the hook.
    link to github issue:  https://github.com/lirantal/cypress-social-logins/issues/43

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
    */
})