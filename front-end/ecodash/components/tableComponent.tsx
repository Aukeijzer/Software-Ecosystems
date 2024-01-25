"use client"

import displayableTableItem from "@/app/classes/displayableTableItem";
import { Table } from "flowbite-react";

interface infoCardDataTableProps<T>{
    /**
     * A list of items that each should be rendered in the table.
     */
    items: displayableTableItem[]

    onClick: (sub: string) => void;
}

/**
 * Component that renders a table with provided data.
 * @param props The props for the table component.
 * @returns A JSX.Element representing the table.
 */
export default function TableComponent<T extends {}>(props: infoCardDataTableProps<T>){
    if(props.items.length > 0){
        return(
            <div>
                <table className="cursor-pointer w-full text-left text-gray-500">
                    <thead className=" uppercase bg-gray-200">
                        {props.items[0].renderTableHeaders()}
                    </thead>
                    <tbody className="w-full">
                        {props.items.map((item) => (
                            item.renderAsTableItem(props.onClick)
                        ))}
                    </tbody>
                </table>
            </div>
        )
    }
    else{
        return(
            <div className="flex justify-center">
                No more data to show.
            </div>
        )
    }
    
}




