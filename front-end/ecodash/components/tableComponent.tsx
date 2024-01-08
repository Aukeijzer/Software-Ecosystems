"use client"

import displayableTableItem from "@/app/classes/displayableTableItem";
import { Table } from "flowbite-react";

interface infoCardDataTableProps<T>{
    /**
     * A list of items that each should be rendered in the table.
     */
    items: displayableTableItem[]
}

/**
 * Component that renders a table with provided data.
 * @param props The props for the table component.
 * @returns A JSX.Element representing the table.
 */
export default function TableComponent<T extends {}>(props: infoCardDataTableProps<T>){
    return(
        <div>
            <table className="w-full text-left text-gray-500">
                <thead className="text-gray-700 uppercase bg-gray-200">
                    {props.items[0].renderTableHeaders()}
                </thead>
                <tbody>
                    {props.items.map((item) => (
                        item.renderAsTableItem()
                    ))}
                </tbody>
            </table>
        </div>
    )
}




