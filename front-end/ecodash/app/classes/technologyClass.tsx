import React from "react";
import displayableListItem from "./displayableListItem";

export default class technologyClass implements displayableListItem {
    technology: string;
    projectCount: number;

    constructor(technology: string, projectCount : number) {
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
}