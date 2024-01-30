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

import React from "react";
import { Cell } from "recharts";
import displayableListItem from "./displayableListItem";
import displayableGraphItem from "./displayableGraphItem";
import { COLORS } from "../interfaces/colors";
export default class languageClass implements displayableListItem, displayableGraphItem {
    language: string;
    percentage: number;
   
    constructor(language: string, percentage: number) {
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
                <th className="px-6 py-4 font-medium text-gray-900">
                    {this.language.valueOf()}
                </th>
                <td className="px-6 py-4">
                    {this.percentage}%
                </td>
            </tr>
        )
    }

    renderAsGraphItem(index: number, onClick: (sub: string) => void): React.JSX.Element {
        return(
            <Cell key={`cell-${index}`} fill={COLORS[index % COLORS.length]} onClick={() => onClick(this.language)}/>
        )
    }

    
}