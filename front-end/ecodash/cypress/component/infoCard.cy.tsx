import InfoCard from "@/components/infoCard";

describe('InfoCard Component', () => {
    it('renders InfoCard with title and data', () => {
      const title = 'Test Title';
      const data = <div data-testid="test-data">Test Data</div>;
  
      cy.mount(<InfoCard title={title} data={data} />);
  
      cy.contains(title).should('be.visible');
      cy.get('[data-testid="test-data"]').should('be.visible').contains('Test Data');
    });
  
    it('triggers onClick when clicked', () => {
      const title = 'Clickable Title';
      const onClick = cy.stub().as('clickHandler');
  
      cy.mount(<InfoCard title={title} data={<div />} onClick={onClick} />);

      cy.contains(title).click();
      cy.get('@clickHandler').should('have.been.calledOnce');
    });
  
    it('renders alert when provided', () => {
      const title = 'Alert Title';
      const alertMessage = 'This is an alert';
  
      cy.mount(<InfoCard title={title} data={<div />} alert={alertMessage} />);

      cy.contains(alertMessage).should('be.visible');
    });
  });
  