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

import GraphLine from "@/components/graphLine";
import { mockDataOverTime } from "../fixtures/mockData";

describe('GraphComponent', () => {
    it('renders a line chart with the provided data', () => {
        // Render component
        cy.mount(<GraphLine items={mockDataOverTime}  labels={["test1", "test2", "test3", "test4", "test5"]} />);
        
        // Assertions on the rendered component
        cy.get('[data-cy="line-graph"]').should('exist');
        cy.get('[class="recharts-responsive-container"]').should('exist');
        cy.get('[class= "recharts-legend-wrapper"]').should('exist');

        // Count the number of topics
        let topicCount = -1;
        mockDataOverTime.forEach(_ => {
            topicCount++;
        });

        // Check if all topics have a rendered line
        cy.get('[class="recharts-layer recharts-line"]').should('have.length', topicCount); 
    });
});
  