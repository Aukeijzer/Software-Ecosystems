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