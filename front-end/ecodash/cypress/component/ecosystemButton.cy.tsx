import EcosystemButton from "@/components/ecosystemButton";
import { mockEcosystem } from "../fixtures/mockData";

describe('Ecosystem Button',() => {
    it('render ecosystem button', () =>{
        cy.wrap(mockEcosystem.ecosystem).should('be.a', 'string')
        cy.wrap(mockEcosystem.projectCount).should('be.a', 'number')
        cy.wrap(mockEcosystem.topics).should('be.a', 'number')

        cy.mount(<EcosystemButton {...mockEcosystem}> </EcosystemButton>)

        cy.get('[data-cy="ecosystem-projects"]')
            .then($value => {
                const textValue = $value.text()
                expect(textValue).to.equal('projects: ' + mockEcosystem.projectCount)
            })  

        cy.get('[data-cy="ecosystem-topics"]')
        .then($value => {
            const textValue = $value.text()
            expect(textValue).to.equal('topics: ' + mockEcosystem.topics)
            })     
    })
})