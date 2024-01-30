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

describe('ecosystem routing test', () => {
    it('Clicks on a topic', () => {
        cy.visit('http://localhost:3000/agriculture')
        cy.fixture('apiCallEcosystem.json').then((json) => {
            cy.intercept('POST', 'http://localhost:5003/ecosystems', json)
        })
        cy.contains('farm').click()
        cy.url().should('include', 'farm')
    })
})

describe('ecosystem remove topic routing test', () => {
    it('Clicks on a topic', () => {
        cy.visit('http://localhost:3000/agriculture')
        cy.fixture('apiCallEcosystem.json').then((json) => {
            cy.intercept('POST', 'http://localhost:5003/ecosystems', json)
        })
        cy.contains('farming').click()
        cy.url().should('include', 'farming')
        cy.contains('X farming').click()
        cy.url().should('not.include', 'farming')
    })
})

describe('full url ecosystem routing test', () => {
    it('Enters a full url and checks if it is routed correctly', () => {
        cy.visit('http://localhost:3000/agriculture?topics=farming')
        cy.fixture('apiCallEcosystem.json').then((json) => {
            cy.intercept('POST', 'http://localhost:5003/ecosystems', json)
        })
        cy.contains('X farming').should('exist');
    });
})


