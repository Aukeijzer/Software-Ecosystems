import React from "react";
import displayableListItem from "./displayableListItem";

/**
 * Represents a technology class that implements the displayableListItem interface.
 */
export default class technologyClass implements displayableListItem {
    technology: string;
    projectCount: number;

    /**
     * Creates a new instance of the technologyClass.
     * @param technology The technology name.
     * @param projectCount The number of projects associated with the technology.
     */
    constructor(technology: string, projectCount : number) {
        this.technology = technology;
        this.projectCount = projectCount;
    }

    /**
     * Renders the technology as a list item.
     * @param onClick The click event handler for the list item.
     * @returns The JSX element representing the technology as a list item.
     */
    renderAsListItem(onClick: (sub: string) => void): React.JSX.Element {
        return(
            <p onClick={() => onClick(this.technology)}>
                <b>{this.technology}</b> with {this.projectCount} projects.
            </p>
        )
    }

    renderTableHeaders(): React.JSX.Element {
        return(
            <tr>
                <th key={'technology'} className="px-6 py-3">
                    technology
                </th>
                <th key={'projects'} className="px-6 py-3 text-right">
                    projects
                </th>
            </tr>
        )
    }

    renderAsTableItem(onClick: (sub: string) => void): React.JSX.Element {
        return(
            <tr className="bg-white border-b hover:bg-amber" onClick={() => onClick(this.technology)}>
                <th className="px-6 py-4 font-medium text-gray-900">
                    {this.technology}
                </th>
                <td className="px-6 py-4 text-right">
                    {this.projectCount}
                </td>
            </tr>
        )
    }
}