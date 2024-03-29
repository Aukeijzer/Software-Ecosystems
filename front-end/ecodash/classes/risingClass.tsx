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
import displayableListItem from "./displayableListItem";

export default class risingClass implements displayableListItem {
    topic: string;
    percentage: number;
    growth: number;

    constructor(topic: string, percentage: number, growth: number) {
        this.topic = topic;
        this.percentage = percentage;
        this.growth = growth;
    }

    renderAsListItem(onClick: (sub: string) => void): React.JSX.Element {
        return(
            <p className="flex flex-row gap-1"onClick={() => onClick(this.topic)}>
                <b>{this.topic}</b>  {this.percentage}% : {this.growth}  %
                 <svg className="w-6 h-6 text-gray-800 dark:text-white" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 10 14">
                    <path stroke="green" strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M5 13V1m0 0L1 5m4-4 4 4"/>
                </svg>
        </p>
        )
    }

    renderTableHeaders(): React.JSX.Element {
        return(
            <tr className="grid-cols-3">
                <th key={'topic'} className="py-3 px-5">
                    topic
                </th>
                <th key={'percentage'} className="py-3 px-5 text-center">
                    percentage
                </th>
                <th key={'growth'} className="py-3 px-5 text-right">
                    growth
                </th>
            </tr>
        )
    }

    renderAsTableItem(onClick: (sub: string) => void): React.JSX.Element {
        return(
            <tr className="grid-cols-3  bg-white border-b hover:bg-amber" onClick={() => onClick(this.topic)}>
                <th className="py-4 pl-5 font-medium text-gray-900">
                    {this.topic}
                </th>
                <td className="py-4 text-center">
                    {this.percentage}%
                </td>
                <td className="py-4 pr-5 text-right">
                    {this.growth}%
                </td>
            </tr>
        )
    }
}