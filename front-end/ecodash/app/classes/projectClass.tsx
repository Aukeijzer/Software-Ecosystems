import { JSX } from "react";
import displayableTableItem from "./displayableTableItem";

export default class projectClass implements displayableTableItem {
    name: string;
    owner: string;
    numberOfStars: number;
    
    constructor(name: string, owner: string, numberOfStars: number) {
        this.name = name;
        this.owner = owner;
        this.numberOfStars = numberOfStars;
    }

    renderTableHeaders(): JSX.Element {
        return(
            <tr className="grid-cols-3">
                <th key={'name'} className="py-3 px-5">
                    name
                </th>
                <th key={'owner'} className=" text-center py-3 px-5">
                    owner
                </th>
                <th key={'stars'} className="text-right py-3 px-5">
                    stars
                </th>
            </tr>
        )
    }

    renderAsTableItem(): React.JSX.Element {
        return(
            <tr className="grid-cols-3  bg-white border-b hover:bg-amber" onClick={() => window.open(`http://www.github.com/${this.owner}/${this.name}`)}>
                <th className="pl-5 py-4 font-medium text-gray-900">
                    {this.name}
                </th>
                <td className="py-4 text-center">
                    {this.owner}
                </td>
                <td className="pr-5 py-4 text-right">
                    {this.numberOfStars}
                </td>
            </tr>
        )   
    }
}