import React from "react";
import { Cell } from "recharts";
import displayableListItem from "./displayableListItem";
import displayableGraphItem from "./displayableGraphItem";

export default class languageClass implements displayableListItem, displayableGraphItem {
    language: String;
    percentage: number;
   
    constructor(language: String, percentage: number) {
        this.language = language;
        this.percentage = percentage;
    }

    renderAsListItem(): React.JSX.Element {
        return(
            <div>
                <b>{this.language.valueOf()} </b>: {this.percentage} %
            </div>
        )
    }
    
    renderAsTableItem(): React.JSX.Element {
        return(
            <tr className="bg-white border-b hover:bg-amber">
                <th className="px-6 py-4 font-medium ">
                    {this.language.valueOf()}
                </th>
                <td className="px-6 py-4">
                    {this.percentage}%
                </td>
            </tr>
        )
    }

    renderAsGraphItem(index: number): React.JSX.Element {
        const COLORS = [ "#4421af", "#1a53ff", "#0d88e6", "#00b7c7", "#5ad45a", "#8be04e", "#ebdc78"]
        return(
            <Cell key={`cell-${index}`} fill={COLORS[index % COLORS.length]} />
        )
    }

    
}