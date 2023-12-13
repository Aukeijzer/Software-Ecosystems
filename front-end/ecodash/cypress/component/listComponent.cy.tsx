import React from 'react';
import ListComponent from '@/components/listComponent';
import { mockSubecosystems } from '../fixtures/mockData';

describe('ListComponent', () => {
  it('renders a list component and checks if data is correct', () => {
    // Stub the onClick function
    const onClickStub = cy.stub().as('onClickStub');

    // Mount component with mocked data and onClick handler
    cy.mount(<ListComponent items={mockSubecosystems} onClick={onClickStub} />);

    // Assertions on the rendered component
    cy.get('[data-cy="list component"]').should('exist');
    cy.get('[data-cy="list group"]').should('exist')

    // Click the items and check if all data is rendered and values are correct
    cy.get('[data-cy="list component"]')
        .children()
        .get('[data-cy="list item"]')
        .should('have.length', mockSubecosystems.length)
        .then($value => {
            const textValue = $value.text()
            expect(textValue).to.contain('with');
        });
  });
  it('checks whether clicking functionality is called', () => {
    // Stub the onClick function
    const onClickStub = cy.stub().as('onClickStub');

    // Render component with mocked data and onClick handler
    cy.mount(<ListComponent items={mockSubecosystems} onClick={onClickStub} />);

    // Click the list items 
    cy.get('[data-cy="list component"]')
        .children()
        .get('[data-cy="list item"]')
        .click({multiple: true});

    // Assert that onClick is called at least once
    cy.get('@onClickStub').should('be.called');
  });
});
