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

import EcosystemDescription from "@/components/ecosystemDescription";
import { mockEcosystemDescription1, mockEcosystemDescription2, mockEcosystemDescription3 } from "../fixtures/mockData";

function MockChangeDescription(desc: string) {
    return;
}
   
var editMode = false;

describe('Ecosystem Description',() => {
    it('render ecosystem description', () =>{
        // Render component
  
        cy.mount(<EcosystemDescription {...mockEcosystemDescription1} editMode={editMode} changeDescription={MockChangeDescription}/>)

        // Check whether the welcome message is rendered correctly
        cy.get('[data-cy="ecosystem description"]')
            .children()
            .get('[data-cy="welcome ecosystem"]')
            .then($value => {
                const textValue = $value.text()
                expect(textValue).to.contain('Welcome to the ' + mockEcosystemDescription1.ecosystem + ' ecosystem page')
            })     
            
         // Check whether the description is rendered correctly
         cy.get('[data-cy="ecosystem description"]')
            .children()
            .get('[data-cy="description ecosystem"]')
            .then($value => {
                const textValue = $value.text()
                expect(textValue).to.contain(mockEcosystemDescription1.description)
            })   
    })

    it('render selected subecosystems', () =>{
        // Render component
        cy.mount(<EcosystemDescription {...mockEcosystemDescription2} editMode={editMode} changeDescription={MockChangeDescription} />)

        // Check whether the subecosystems are rendered correctly
        cy.get('[data-cy="ecosystem description"]')
            .children()
            .get('[data-cy="subecosystems"]')
            .then($value => {
                const textValue = $value.text()
                mockEcosystemDescription2.subEcosystems.forEach(element => {
                    expect(textValue).to.contain(element)
                });
            })     
    })
    
    // Test to check when remove is clicked, subecosystem list is updated
    it('remove selected sub-ecosystems', () => {
        // Render component
        cy.mount(<EcosystemDescription {...mockEcosystemDescription3} editMode={editMode} changeDescription={MockChangeDescription}/>);
    
        // Simulate clicking on the remove button for 'farming'
        cy.get('[data-cy=subecosystems]')
            .contains('X farming')
            .click();
    
        // Wait for removal and rerender component
        cy.wait(100).then(() => {
            cy.mount(<EcosystemDescription {...mockEcosystemDescription3} editMode={editMode} changeDescription={MockChangeDescription} />);
    
        // Check that 'farming' is removed from the subecosystems list
        cy.get('[data-cy=subecosystems]')
            .should('not.contain', 'X farming');
        });
    });
})