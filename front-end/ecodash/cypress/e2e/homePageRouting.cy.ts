describe('Homepage to ecosystem page routing', () => {
  it('Clicks a ecosystem' , () => {
    cy.visit(Cypress.env('frontend_url'))
    cy.contains('agriculture').click()
    let currentURL
      cy.url().then(url => {
        currentURL = url
        console.log(currentURL)
      });
   
    cy.url().should('include', 'agriculture')
  })
})

/*
describe('Ecosystem page to homepage routing', () => {
  it('Clicks the logo' , () => {
    cy.visit('http://agriculture.localhost:3000')
    cy.contains('SECODash').click()
    //Asserts that the agriculture has been removed from the URL
    cy.url().should('not.include', 'agriculture')
    //Asserts that we are on the homepage
    cy.url().should('eq', 'http://localhost:3000/')
    //Asserts that the homepage is displayed
    cy.should('contain', 'Information about SECODash')
  })
})

*/
