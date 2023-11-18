describe('ecosystem routing test', () => {
    it('Clicks on a topic', () => {
        cy.visit('http://agriculture.localhost:3000')
        cy.fixture('apiCallEcosystem.json').then((json) => {
            cy.intercept('POST', 'http://localhost:5003/ecosystems', json)
        })
        cy.contains('farm').click()
        cy.url().should('include', 'farm')
    })
})

