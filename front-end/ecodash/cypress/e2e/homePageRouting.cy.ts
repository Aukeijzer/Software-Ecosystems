describe('Ecosystem routing test', () => {
  it('Clicks the agriculture ecosystem' , () => {
    cy.visit('http://localhost:3000')
    cy.contains('agriculture').click()
    cy.url().should('include', 'agriculture')
  })

  it('Clicks the quantum ecosystem' , () => {
    cy.visit('http://localhost:3000')
    cy.contains('quantum').click()
    cy.url().should('include', 'quantum')
  })
})


