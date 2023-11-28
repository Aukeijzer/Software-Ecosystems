import React from "react";

export default abstract class displayable{
    constructor(){

    }
    
    abstract renderAsListItem(onClick: (sub: string) => void) : React.JSX.Element
       
    abstract renderAsTableItem() : React.JSX.Element

    abstract renderAsGraph(index: number) : React.JSX.Element
}

