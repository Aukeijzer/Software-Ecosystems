import React from "react";
import displayableListItem from "./displayableListItem";

export default class subEcosystemClass implements displayableListItem {
    topic: string;
    projectCount: number;
    
    constructor(topic : string, projectCount : number) {
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
}