"use client"

import displayable from "@/app/classes/displayableClass";
import { Table } from "flowbite-react";

interface infoCardDataTableProps<T>{
    /**
     * The headers that the table has.
     */
    headers: string[],
    /**
     * A list of items that each should be rendered in the table.
     */
    items: displayable[]
}

/**
 * Component that renders a table with provided data.
 * @param props The props for the table component.
 * @returns A JSX.Element representing the table.
 */
export default function TableComponent<T extends {}>(props: infoCardDataTableProps<T>){
    return(
        <div>
            <Table>
                <Table.Head>
                    {props.headers.map((header) => (
                        <Table.HeadCell key={header}>
                            {header}
                        </Table.HeadCell>
                    ))}
                </Table.Head>

                <Table.Body className="divide-y">
                    {props.items.map((item) => (
                        item.renderAsTableItem()
                    ))}
                </Table.Body>
            </Table>
        </div>
    )
}




