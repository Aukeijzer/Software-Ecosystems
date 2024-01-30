/*
Copyright (C) <2024> <OdinDash>

This file is part of SECODash

SECODash is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published
by the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

SECODash is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License for more details.

You should have received a copy of the GNU Affero General Public License
along with SECODash.  If not, see <https://www.gnu.org/licenses/>.

*/

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
                    Project
                </th>
                <th key={'owner'} className=" text-center py-3 px-5">
                    Owner
                </th>
                <th key={'stars'} className="text-right py-3 px-5">
                    Stars
                </th>
            </tr>
        )
    }

    renderAsTableItem(): React.JSX.Element {
        return(
            <tr className="grid-cols-3  bg-white border-b hover:bg-amber" onClick={() => window.open(`http://www.github.com/${this.owner}/${this.name}`)}>
                <th className="pl-4 py-4 font-medium text-gray-900 ">
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