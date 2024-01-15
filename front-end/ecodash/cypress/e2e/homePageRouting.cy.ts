describe('PopUp box functionality', () => {
  it('should open the popup box', () => {
    cy.visit('http://localhost:3000');
    cy.contains('Login').click();
    cy.get('[data-cy=".oauthPopUp"]').should('be.visible');
  });
  it('should close the popup box', () => {
    cy.visit('http://localhost:3000');
    cy.contains('Login').click();
    cy.get('[data-cy=".oauthPopUp"]').should('be.visible');
    cy.get('[data-cy=".popUpClose"]').click();
    cy.get('[data-cy=".oauthPopUp"]').should('not.exist');
  });
});

describe('Home Page Routing', () => {
  it('should navigate to the agriculture page', () => {
    cy.visit('http://localhost:3000');
    cy.contains('div', 'agriculture').click();
    cy.url().should('include', 'agriculture');
  });

  it('should navigate to the quantum page', () => {
    cy.visit('http://localhost:3000');
    cy.contains('div', 'quantum').click();
    cy.url().should('include', 'quantum');
  });

  it('should navigate to the artificial-intelligence page', () => {
    cy.visit('http://localhost:3000');
    cy.contains('div', 'artificial-intelligence').click();
    cy.url().should('include', 'artificial-intelligence');
  });
});

