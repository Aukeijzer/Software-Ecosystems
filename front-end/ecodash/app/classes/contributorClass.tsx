import React from "react";
import displayableTableItem from "./displayableTableItem";

export default class contributorClass implements displayableTableItem {
    username: string;
    contributions: number;
    
    constructor(login: string, contributions: number) {
        this.username = login;
        this.contributions = contributions;
    }

    renderTableHeaders(): React.JSX.Element {
        return(
            <tr>
                <th key={'username'} className="px-6 py-3">
                    username
                </th>
                <th key={'contributions'} className="px-6 py-3 text-right">
                    contributions
                </th>
            </tr>
        )
    }

    renderAsTableItem(): React.JSX.Element {
        return(
            <tr className="bg-white border-b hover:bg-amber">
                <th className="px-6 py-4 font-medium text-gray-900">
                    {this.username}
                </th>
                <td className="px-6 py-4 text-right">
                    {this.contributions}
                </td>
            </tr>
        )
    }
}
