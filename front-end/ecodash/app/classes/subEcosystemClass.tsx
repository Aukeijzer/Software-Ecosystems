import React from "react";
import displayable from "./displayableClass";

export default class subEcosystemClass extends displayable {
    topic: string;
    projectCount: number;
    
    constructor(topic : string, projectCount : number){
        super()
        this.topic = topic;
        this.projectCount = projectCount;
    }
    
    renderAsListItem(onClick: (sub: string) => void): React.JSX.Element {    
        return(
            <p onClick={() => onClick(this.topic)}>
                <b>{this.topic}</b> with {this.projectCount} projects.
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
        return(
            <div>

            </div>
        )
    }
}