export default abstract class displayable{
    constructor(){

    }
    
    abstract renderAsListItem(url: string) : JSX.Element
       
    abstract renderAsTableItem() : JSX.Element

    abstract renderAsGraph(index: number) : JSX.Element
}

