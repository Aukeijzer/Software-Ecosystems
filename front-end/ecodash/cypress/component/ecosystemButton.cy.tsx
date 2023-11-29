import EcosystemButton from "@/components/ecosystemButton";

describe('Ecosystem Button',() => {
    it('render ecosystem button', () =>{
        //Mock test data 
        const props1 = {ecosystem: 'quantum', projectCount:1000, topics:16};
        
        cy.wrap(props1.ecosystem).should('be.a', 'string')
        cy.wrap(props1.projectCount).should('be.a', 'number')
        cy.wrap(props1.topics).should('be.a', 'number')

        cy.mount(<EcosystemButton {...props1}> </EcosystemButton>)

        cy.get('[data-cy="ecosystem-projects"]')
            .then($value => {
                const textValue = $value.text()
                expect(textValue).to.equal('projects: ' + props1.projectCount)
            })  

        cy.get('[data-cy="ecosystem-topics"]')
        .then($value => {
            const textValue = $value.text()
            expect(textValue).to.equal('topics: ' + props1.topics)
            })     
    })
})