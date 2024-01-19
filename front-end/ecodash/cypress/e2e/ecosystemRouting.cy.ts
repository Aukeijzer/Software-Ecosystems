describe('ecosystem routing test', () => {
    it('Clicks on a topic', () => {
        cy.visit('http://localhost:3000/agriculture')
        cy.fixture('apiCallEcosystem.json').then((json) => {
            cy.intercept('POST', 'http://localhost:5003/ecosystems', json)
        })
        cy.contains('farm').click()
        cy.url().should('include', 'farm')
    })
})

describe('ecosystem remove topic routing test', () => {
    it('Clicks on a topic', () => {
        cy.visit('http://localhost:3000/agriculture')
        cy.fixture('apiCallEcosystem.json').then((json) => {
            cy.intercept('POST', 'http://localhost:5003/ecosystems', json)
        })
        cy.contains('farming').click()
        cy.url().should('include', 'farming')
        cy.contains('X farming').click()
        cy.url().should('not.include', 'farming')
    })
})

describe('full url ecosystem routing test', () => {
    it('Enters a full url and checks if it is routed correctly', () => {
        cy.visit('http://localhost:3000/agriculture?topics=farming')
        cy.fixture('apiCallEcosystem.json').then((json) => {
            cy.intercept('POST', 'http://localhost:5003/ecosystems', json)
        })
        cy.contains('X farming').should('exist');
    });
})


