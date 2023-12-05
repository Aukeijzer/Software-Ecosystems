//Asserts that the ecosystem routing works as expected by clicking on a ecosystem and checking that the URL contains the ecosystem name.
describe('ecosystem routing topic click URL test', () => {
    it('Clicks on a topic', () => {
        cy.visit(Cypress.env('urlPreFix') + "agriculture" + Cypress.env('urlPost'))
        cy.fixture('apiCallEcosystem.json').then((json) => {
            cy.intercept('POST', Cypress.env('backend_url') + '/ecosystems', json)
        })
        cy.contains('farm').click()
        
        cy.url().should('include', 'farm')
    })
})

//Asserts that the ecosystem routing works as expected by clicking on a ecosystem 
//and checking that the ecosystem name is displayed.
describe('ecosystem routing topic click data test', () => {
    it('Clicks on a topic', () => {
        cy.visit(Cypress.env('urlPreFix') + "agriculture" + Cypress.env('urlPost'))
        cy.fixture('apiCallEcosystem.json').then((json) => {
            cy.intercept('POST', Cypress.env('backend_url') + '/ecosystems', json)
        })
        cy.contains('farm').click()
        cy.get('button').should('contain', 'X farm')
    })
})

//Asserts that the ecosystem routings works by inserting a full shared URL and checking that the URL contains the topics and that the topics are displayed on the page.
describe('ecosystem full url test', () => {
    it('Inserts a full shared URL', () => {
        cy.visit(Cypress.env('urlPreFix') + "agriculture" + Cypress.env('urlPost') +  '/?topics=machine-learning,remote-sensing')
       
        //Assert that the URL contains the topics
        cy.url().should('include', 'topics=machine-learning,remote-sensing')
        //Assert that the topics are displayed on the page
        cy.should('contain', 'machine-learning')
        cy.should('contain', 'remote-sensing')
    })
})

//Asserts that the ecosystem routing works by removing a topic by clicking on the X and checking that the URL does not contain the topic and that the topic is not displayed on the page.
describe('ecosystem routing topic remove URL test', () => {
    it('Removes a topic by click on the X', () => {
        cy.visit(Cypress.env('urlPreFix') + "agriculture" + Cypress.env('urlPost') +  '/?topics=machine-learning')
        cy.fixture('apiCallEcosystem.json').then((json) => {
            cy.intercept('POST', Cypress.env('backend_url') + '/ecosystems', json)
        })
        cy.contains('X machine-learning').click()
        //Assert that the URL does not contain the topic
        cy.url().should('not.include', 'machine-learning')
        //Assert that the topic is not displayed on the page
        cy.should('not.contain', 'machine-learning')
    })
})



