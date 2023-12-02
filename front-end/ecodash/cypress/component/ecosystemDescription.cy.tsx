import EcosystemDescription from "@/components/ecosystemDescription";
import { mockEcosystemDescription1, mockEcosystemDescription2, mockEcosystemDescription3 } from "../fixtures/mockData";

describe('Ecosystem Description',() => {
    it('render ecosystem description', () =>{
        // Render component
        cy.mount(<EcosystemDescription {...mockEcosystemDescription1} />)

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
        cy.mount(<EcosystemDescription {...mockEcosystemDescription2} />)

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
        cy.mount(<EcosystemDescription {...mockEcosystemDescription3} />);
    
        // Simulate clicking on the remove button for 'farming'
        cy.get('[data-cy=subecosystems]')
            .contains('X farming')
            .click();
    
        // Wait for removal and rerender component
        cy.wait(100).then(() => {
            cy.mount(<EcosystemDescription {...mockEcosystemDescription3} />);
    
        // Check that 'farming' is removed from the subecosystems list
        cy.get('[data-cy=subecosystems]')
            .should('not.contain', 'X farming');
        });
    });
})