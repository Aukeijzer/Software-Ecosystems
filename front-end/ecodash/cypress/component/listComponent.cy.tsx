/*
Copyright (C) <2024> <OdinDash>

This file is part of SECODash

SECODash is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published
by the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

SECODash is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License for more details.

You should have received a copy of the GNU Affero General Public License
along with SECODash.  If not, see <https://www.gnu.org/licenses/>.

*/

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
