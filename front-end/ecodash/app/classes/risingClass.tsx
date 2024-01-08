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
            <tr>
                <th key={'topic'} className="px-6 py-3">
                    topic
                </th>
                <th key={'percentage'} className="px-6 py-3 text-center">
                    percentage
                </th>
                <th key={'growth'} className="px-6 py-3 text-right">
                    growth
                </th>
            </tr>
        )
    }

    renderAsTableItem(onClick: (sub: string) => void): React.JSX.Element {
        return(
            <tr className="bg-white border-b hover:bg-amber" onClick={() => onClick(this.topic)}>
                <th className="px-6 py-4 font-medium text-gray-900">
                    {this.topic}
                </th>
                <td className="px-6 py-4 text-center">
                    {this.percentage}%
                </td>
                <td className="px-6 py-4 text-right">
                    {this.growth}%
                </td>
            </tr>
        )
    }
}