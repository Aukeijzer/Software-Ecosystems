import React from "react";
import displayable from "./displayableClass";

export default class technologyClass extends displayable{
    technology: string;
    projectCount: number;

    constructor(technology: string, projectCount : number){
        super()
        this.technology = technology;
        this.projectCount = projectCount;
    }

    renderAsListItem(onClick: (sub: string) => void): React.JSX.Element {
        return(
            <p onClick={() => onClick(this.technology)}>
                <b>{this.technology}</b> with {this.projectCount} projects.
            </p>
        )
    }

    renderAsTableItem(): React.JSX.Element {
        return(
            <div>

            </div>
        )
    }

    renderAsGraph(index: number): React.JSX.Element {
        return(<div>
            
        </div>)
    }
}