"use client"

import displayable from "@/app/classes/displayableClass";
/*
infoCardDataTable exports:
- InfoCardDataTable: JSX.Element filled with a table containing provided data 
    - input:
        - headers: string[]: the headers that the table has
        - items: T[]: a list of items that each should be rendered in the table
        - renderFunction: a function that takes T item and renders a table row for it
    - output: 
        - JSX.Element
*/

import { Table } from "flowbite-react";

interface infoCardDataTableProps<T>{
    headers: string[],
    items: displayable[]
}

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




