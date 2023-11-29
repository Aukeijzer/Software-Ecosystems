import GridLayout from "@/components/gridLayout";

describe('GridLayout Component', () => {
    // Mock data for the test
    const mockCards = [
      {
        x: 0,
        y: 0,
        width: 2,
        height: 1,
        minH: 1,
        minW: 1,
        static: false,
        card: <div title="test" data-testid="card1">Card 1 Content</div>,
      },
      {
        x: 2,
        y: 0,
        width: 2,
        height: 2,
        minH: 1,
        minW: 1,
        static: false,
        card: <div title="test" data-testid="card2">Card 2 Content</div>,
    },
    ];
  
    it('renders GridLayout with provided cards', () => {
        cy.viewport(1200, 800); 
    
        cy.window().then(() => {
            cy.mount(
            <GridLayout cards={mockCards} />
            );
        });
    
        cy.get('.react-grid-layout').children().should('have.length', mockCards.length);
    
        cy.wait(1000);
        
        mockCards.forEach((_card, index) => {
            cy.get('.react-grid-layout')
            .find('.cursor-pointer') 
            .eq(index) 
            .should('exist')
            .find(`[data-testid="card${index + 1}"]`)
            .should('contain', `Card ${index + 1} Content`);
        });
    });
  });
  