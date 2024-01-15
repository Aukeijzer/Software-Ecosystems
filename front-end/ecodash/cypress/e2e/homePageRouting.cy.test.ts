describe('Ecosystem routing test', () => {
  it('Clicks an ecosystem', () => {
    cy.visit('http://localhost:3000');
    cy.get('agriculture').click();
    cy.url().should('include', 'agriculture');
  });
});

describe('Ecosystem routing test', () => {
  it('Clicks an ecosystem and checks the URL', () => {
    cy.visit('http://localhost:3000');
    cy.get('agriculture').click();
    cy.url().should('include', 'agriculture');
  });
});