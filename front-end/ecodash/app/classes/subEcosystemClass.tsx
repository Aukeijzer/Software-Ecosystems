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

    renderTableHeaders(): React.JSX.Element {
        return(
            <tr className="flex flex-row justify-between py-3 px-5">
                <th key={'topic'} className="">
                    topic
                </th>
                <th key={'projects'} className=" text-right">
                    projects
                </th>
            </tr>
        )
    }

    renderAsTableItem(onClick: (sub: string) => void): React.JSX.Element {
        return(
            <tr className="bg-white border-b px-5 flex flex-row justify-between hover:bg-amber " onClick={() => onClick(this.topic)}>
                <th className="py-4 font-medium text-gray-900">
                    {this.topic}
                </th>
                <td className="py-4 text-right ">
                    {this.projectCount}
                </td>
            </tr>
        )
    }
}