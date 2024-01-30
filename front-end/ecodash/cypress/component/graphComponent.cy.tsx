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

import GraphComponent from "@/components/graphComponent";
import React from 'react';
import { mockLanguages, languages } from "../fixtures/mockData";

function mockOnClick(){
  return;
}
describe('GraphComponent', () => {
  beforeEach(() => {
    // Mount the component before each test
    cy.mount(<GraphComponent items={mockLanguages} onClick={() => mockOnClick}/>);
  });

  it('renders the pie chart with the provided data', () => {
    // Assertions on the rendered component structure
    cy.get('[data-cy="pie-chart"]').should('exist'); 
    cy.get('[class="recharts-layer recharts-pie"]').should('exist'); 
    cy.get('[class="recharts-legend-wrapper"]').should('exist'); 

    // Check if the correct number of pie chart slices are rendered
    cy.get('[class="recharts-layer recharts-pie-sector"]').should('have.length', mockLanguages.length);
  });

  it('displays the legend with correct items', () => {
    // Check if the legend items exist and match the provided data
    languages.forEach((language, index) => {
      cy.get(`[class="recharts-legend-item-text"]:eq(${index})`).should('exist'); 
      cy.contains(`[class="recharts-legend-item-text"]:eq(${index})`, language); 
    });
  });
});
