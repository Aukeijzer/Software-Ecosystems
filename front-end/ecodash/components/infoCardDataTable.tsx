"use client"

/*
    This component renders a <T>[] list: a generic type list as a Table
    required props:
        - items <T>[] a list that contains objects that extend generic type
        - renderItem (item: T) => JSX.Element. A function that returns a JSX.Element provided an item from the list
        - headers string[]: a list of strings that the headers of the table will be
    Renderfunctions (per T type):
        - render project table (project: projectModel)
        - render ecosystem table (ecosystem: EcosystemModel)
*/

import { Table } from "flowbite-react";
import { ecosystemModel, languageModel, projectModel } from "@/app/models/apiResponseModel";
import { TableCell } from "flowbite-react/lib/esm/components/Table/TableCell";

interface infoCardDataTableProps<T>{
    headers: string[],
    items: T[],
    renderItem: (item: T) => JSX.Element
}

//Todo: Make a check if headers.count === cells in table?

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
        <Table.Row >
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
        <Table.Row>
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



