export default abstract class displayablePaged{
    constructor(){

    }
    
    abstract renderAsListItem(url: string) : JSX.Element
       
    abstract renderAsTableItem() : JSX.Element

    abstract renderAsGraph(index: number) : JSX.Element
}

