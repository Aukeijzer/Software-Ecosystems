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
            <tr className="flex flex-row justify-between">
                <th key={'username'} className="px-5 py-3">
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
            <tr className="bg-white border-b px-5 flex flex-row justify-between hover:bg-amber" onClick={() => window.open(`https://www.github.com/${this.username}`)}>
                <th className="py-4 font-medium text-gray-900">
                    {this.username}
                </th>
                <td className="py-4 text-right">
                    {this.contributions}
                </td>
            </tr>
        )
    }
}
