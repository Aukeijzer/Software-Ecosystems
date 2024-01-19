import React from "react";
import displayableListItem from "./displayableListItem";

/**
 * Represents a sub ecosystem class that implements the displayableListItem interface.
 */
export default class subEcosystemClass implements displayableListItem {
    topic: string;
    projectCount: number;
    
    /**
     * Creates a new instance of the subEcosystemClass.
     * @param topic - The topic of the sub ecosystem.
     * @param projectCount - The number of projects in the sub ecosystem.
     */
    constructor(topic : string, projectCount : number) {
        this.topic = topic;
        this.projectCount = projectCount;
    }
    
    /**
     * Renders the sub ecosystem as a list item.
     * @param onClick - The function to be called when the list item is clicked.
     * @returns The JSX element representing the sub ecosystem as a list item.
     */
    renderAsListItem(onClick: (sub: string) => void): React.JSX.Element {    
        return(
            <p onClick={() => onClick(this.topic)}>
                <b>{this.topic}</b> with {this.projectCount} projects.
            </p>
        )
    }
}