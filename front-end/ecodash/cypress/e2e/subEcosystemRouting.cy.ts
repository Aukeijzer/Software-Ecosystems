describe('Ecosystem routing test', () => {
  it('Clicks a ecosystem' , () => {
    cy.visit('http://localhost:3000')
    cy.contains('agriculture').click()
    cy.url()
  })
})