import SpinnerComponent from "@/components/spinner";

describe('Spinner component', () =>{
    it('render spinner component', () => {
        cy.mount(<SpinnerComponent/>)
        cy.get('[data-cy="spinner"]').should('exist');
    })
})