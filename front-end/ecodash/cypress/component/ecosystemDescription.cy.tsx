import EcosystemDescription from "@/components/ecosystemDescription";

describe('Ecosystem Description',() => {
    it('render ecosystem description', () =>{
        //Mock test data 
        const props1 = {
            ecosystem: 'quantum', 
            description:'Ecosystem about quantum software'
        };
        
        // Render component
        cy.mount(<EcosystemDescription {...props1} />)

        // Check whether the welcome message is rendered correctly
        cy.get('[data-cy="ecosystem description"]')
            .children()
            .get('[data-cy="welcome ecosystem"]')
            .then($value => {
                const textValue = $value.text()
                expect(textValue).to.contain('Welcome to the ' + props1.ecosystem + ' ecosystem page')
            })     
            
         // Check whether the description is rendered correctly
         cy.get('[data-cy="ecosystem description"]')
         .children()
         .get('[data-cy="description ecosystem"]')
         .then($value => {
             const textValue = $value.text()
             expect(textValue).to.contain(props1.description)
         })   
    })

    it('render selected subecosystems', () =>{
        //Mock test data 
        const props1 = {
            ecosystem: 'agriculture', 
            description:'Ecosystem about agriculture software', 
            subEcosystems: ['farming', 'machine-learning']
        };
        
        // Render component
        cy.mount(<EcosystemDescription {...props1} />)

        // Check whether the subecosystems are rendered correctly
        cy.get('[data-cy="ecosystem description"]')
            .children()
            .get('[data-cy="subecosystems"]')
            .then($value => {
                const textValue = $value.text()
                props1.subEcosystems.forEach(element => {
                    expect(textValue).to.contain(element)
                });
            })     
    })
    
    // Test to check when remove is clicked, subecosystem list is updates
    it('remove selected sub-ecosystems', () => {
        // Mock test data 
        const props1 = {
            ecosystem: 'agriculture',
            description: 'Ecosystem about agriculture software',
            subEcosystems: ['farming', 'machine-learning'],
            removeTopic: (topic : string) => {
                props1.subEcosystems = props1.subEcosystems.filter(item => item != topic);
            }
        };
    
        // Render component
        cy.mount(<EcosystemDescription {...props1} />);
    
        // Simulate clicking on the remove button for 'farming'
        cy.get('[data-cy=subecosystems]')
            .contains('X farming')
            .click();
    
        // Wait for the removal and remount component
        cy.wait(100).then(() => {
            cy.mount(<EcosystemDescription {...props1} />);
    
        // Check that 'farming' is removed from the subecosystems list
        cy.get('[data-cy=subecosystems]')
            .should('not.contain', 'X farming');
        });
    });
})