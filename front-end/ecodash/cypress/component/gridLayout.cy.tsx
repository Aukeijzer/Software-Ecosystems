import GridLayout from "@/components/gridLayout";
import { mockCards, mockCardsStatic } from "../fixtures/mockData";

describe('GridLayout Component', () => {
  it('renders GridLayout with provided cards and validates content', () => {
    cy.viewport(1200, 800); 
        
    cy.mount(<GridLayout cards={mockCardsStatic} />);
    
    cy.get('.react-grid-layout').children().should('have.length', mockCardsStatic.length);
    
    cy.wait(1000);
        
    mockCardsStatic.forEach((_, index) => {
      cy.get('.react-grid-layout')
        .find('.cursor-pointer') 
        .eq(index) 
        .should('exist')
        .find(`[data-testid="card${index + 1}"]`)
        .should('contain', `Card ${index + 1} Content`);
    });
  });

  it('allows dragging grid items', () => {
    cy.viewport(1200, 800);

    cy.mount(<GridLayout cards={mockCards} />);

    cy.get('.react-grid-layout')
      .find('.cursor-pointer')
      .first()
      .should('be.visible')
      .then(($item) => {
        const rect = $item[0].getBoundingClientRect();
        const originalPositionX = rect.left;
        const originalPositionY = rect.bottom;

        cy.get('.react-grid-layout')
          .find('.cursor-pointer')
          .first()
          .trigger('mousedown', { which: 1, pageX: 100, pageY: 100 })
          .trigger('mousemove', { clientX: 200, clientY: 200 })
          .trigger('mouseup');

        cy.get('.react-grid-layout')
          .find('.cursor-pointer')
          .first()
          .should('be.visible')
          .should((draggedItem) => {
            const rect = draggedItem[0].getBoundingClientRect();
            const newPositionX = rect.left;
            const newPositionY = rect.bottom;

            expect(newPositionX).to.not.equal(originalPositionX); 
            expect(newPositionY).to.not.equal(originalPositionY); 
          });
      });
  });

  it('allows resizing grid items', () => {
    cy.viewport(1200, 800);

    cy.mount(<GridLayout cards={mockCards} />);

    cy.get('.react-grid-layout')
      .find('.cursor-pointer')
      .first()
      .then(($item) => {
        const rect = $item[0].getBoundingClientRect();
        const originalWidth = rect.width || 0;
        const originalHeight = rect.height || 0;
        const originalX = rect.left + rect.width / 2;
        const originalY = rect.top + rect.height / 2;

        cy.get('.react-grid-layout')
          .find('.cursor-pointer')
          .first()
          .find('.react-resizable-handle-se')
          .trigger('mousedown', { which: 1, pageX: originalX, pageY: originalY });

        const newX = originalX + 100; 
        const newY = originalY + 100; 
        cy.get('body').trigger('mousemove', { clientX: newX, clientY: newY });

        cy.get('body').trigger('mouseup');

        cy.get('.react-grid-layout')
          .find('.cursor-pointer')
          .first()
          .then(($updatedItem) => {
            const updatedRect = $updatedItem[0].getBoundingClientRect();
            const newWidth = updatedRect.width || 0;
            const newHeight = updatedRect.height || 0;

            expect(newWidth).not.equal(originalWidth);
            expect(newHeight).not.equal(originalHeight); 
          });
      });
  });
});
