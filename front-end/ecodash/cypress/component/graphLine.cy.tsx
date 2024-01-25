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
  