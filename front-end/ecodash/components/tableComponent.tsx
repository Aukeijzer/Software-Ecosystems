"use client"

/*
infoCardDataTable exports:
- InfoCardDataTable: JSX.Element filled with a table containing provided data 
    - input:
        - headers: string[]: the headers that the table has
        - items: T[]: a list of items that each should be rendered in the table
        - renderFunction: a function that takes T item and renders a table row for it
    - output: 
        - JSX.Element
- renderProjectTable: renders a single table row given a project
    - input: Project
    - output: JSX.Element: Table.Row
- renderEcosystemTable: renders a single table row given an ecosystem
    - input: Ecosystem
    - output: JSX.Element: Table.Row
*/

import { Table } from "flowbite-react";
import { TableCell } from "flowbite-react/lib/esm/components/Table/TableCell";
import { ecosystemModel } from "@/app/models/ecosystemModel";
import { projectModel } from "@/app/models/projectModel";
import { languageModel } from "@/app/models/languageModel";


interface infoCardDataTableProps<T>{
    headers: string[],
    items: T[],
    renderFunction: (item: T) => JSX.Element
}

//Todo: Make a check if headers.count === cells in table?

export default function TableComponent<T extends {}>(props: infoCardDataTableProps<T>){
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
                         props.renderFunction(item)
                    ))}
                </Table.Body>
            </Table>
        </div>
    )
}

export function renderLanguage(language: languageModel){
    return(
        <span> {language.language} : {language.percentage} % </span>
    )
}

//Table render functions all have the same architecture
/*
    <Table.Row>
        <Table.Cell> (Same number as cells as headers provided)
            {passedObject.data}
        </Table.Cell>
    </Table.Row>
*/
export function renderProjectTable(project : projectModel){
    return(
        <Table.Row id={project.id} >
            <Table.Cell >
                {project.name}
            </Table.Cell>
            <Table.Cell className="max-w-sm">
                {project.description}
            </Table.Cell>
            <Table.Cell>
                {project.owner}
            </Table.Cell>
            <Table.Cell>
                <ul>
                    {project.languages.map((language) => (
                        <li> {renderLanguage(language)}</li>
                    ))}
                </ul>
               
            </Table.Cell>
        
        </Table.Row>
    )
}

export function renderEcosystemTable(ecosystem: ecosystemModel){
    return(
        <Table.Row >
            <Table.Cell>
                {ecosystem.name}
            </Table.Cell>
            <Table.Cell>
                {ecosystem.description}
            </Table.Cell>
            <Table.Cell>
                {ecosystem.projects.length}
            </Table.Cell>
            <Table.Cell>
                {ecosystem.numberOfStars}
            </Table.Cell>
        </Table.Row>
    )
}



