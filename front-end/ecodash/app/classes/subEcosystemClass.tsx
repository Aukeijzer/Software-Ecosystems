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

    renderTableHeaders(): React.JSX.Element {
        return(
            <tr>
                <th key={'topic'} className="px-6 py-3">
                    topic
                </th>
                <th key={'projects'} className="px-6 py-3 text-right">
                    projects
                </th>
            </tr>
        )
    }

    renderAsTableItem(onClick: (sub: string) => void): React.JSX.Element {
        return(
            <tr className="bg-white border-b hover:bg-amber" onClick={() => onClick(this.topic)}>
                <th className="px-6 py-4  font-medium">
                    {this.topic}
                </th>
                <td className="px-6 py-4 text-right">
                    {this.projectCount}
                </td>
            </tr>
        )
    }
}