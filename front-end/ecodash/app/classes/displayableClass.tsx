export default abstract class displayable{
    constructor(){

    }
    
    abstract renderAsListItem() : JSX.Element
       
    abstract renderAsTableItem() : JSX.Element
}

