export default abstract class displayableSingle{
    constructor(){

    }
    
    abstract renderAsListItem(onClick: (sub: string) => void) : JSX.Element
       
    abstract renderAsTableItem() : JSX.Element

    abstract renderAsGraph(index: number) : JSX.Element
}

