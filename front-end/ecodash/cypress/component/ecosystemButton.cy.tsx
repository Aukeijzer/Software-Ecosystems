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

import EcosystemButton from "@/components/ecosystemButton";
import { mockEcosystem } from "../fixtures/mockData";

describe('Ecosystem Button',() => {
    it('render ecosystem button', () =>{
        cy.wrap(mockEcosystem.ecosystem).should('be.a', 'string')
        cy.wrap(mockEcosystem.projectCount).should('be.a', 'number')
        cy.wrap(mockEcosystem.topics).should('be.a', 'number')

        cy.mount(<EcosystemButton ecosystem={"test"} projectCount={100} topics={100} contributors={100} stars={100}/> )

        cy.get('[data-cy="ecosystem-projects"]')
            .then($value => {
                const textValue = $value.text()
                expect(textValue).to.equal('projects: ' + mockEcosystem.projectCount)
            })  

        cy.get('[data-cy="ecosystem-topics"]')
        .then($value => {
            const textValue = $value.text()
            expect(textValue).to.equal('topics: ' + mockEcosystem.topics)
            })     
    })
})