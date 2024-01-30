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
  