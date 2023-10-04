"use client"

import { Table } from "flowbite-react";
import { Project } from "@/app/models/apiResponseModel";
import { TableCell } from "flowbite-react/lib/esm/components/Table/TableCell";

interface infoCardDataTableProps<T>{
    headers: string[],
    items: T[],
    renderItem: (item: T) => JSX.Element
}



export default function InfoCardDataTable<T extends {}>(props: infoCardDataTableProps<T>){
    return(
        <div>
            <Table>
                <Table.Head>
                    {props.headers.map((header) => (
                        <Table.HeadCell>
                            {header}
                        </Table.HeadCell>
                    ))}
                </Table.Head>

                <Table.Body className="divide-y">
                    {props.items.map((item) => (
                         props.renderItem(item)
                    ))}
                </Table.Body>
            </Table>
        </div>
    )
}

export function renderProjectTable(project : Project){
    return(
        <Table.Row >
            <Table.Cell >
                {project.name}
            </Table.Cell>
            <Table.Cell>
                {project.about}
            </Table.Cell>
            <Table.Cell>
                {project.owner}
            </Table.Cell>
        </Table.Row>
    )
}

