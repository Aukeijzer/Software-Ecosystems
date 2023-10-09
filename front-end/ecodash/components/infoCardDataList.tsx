"use client"

/*
    This component renders a <T>[] list: a generic type list
    required props:
        - items <T>[] a list that contains objects that extend generic type
        - renderItem (item: T) => JSX.Element. A function that returns a JSX.Element provided an item from the list
    Renderfunctions (per T type):
        - render project list (project: projectModel)
*/

import { ListGroup } from "flowbite-react"
import { languageModel, projectModel } from "@/app/models/apiResponseModel";

interface infoCardDataListProps<T>{
    items: T[],
    renderFunction: (item: T) => JSX.Element
}

export default function InfoCardDataList<T extends {}>(props : infoCardDataListProps<T>){
    return(
        <div>
            <ListGroup>
                {props.items.map((item, i) => (
                    <ListGroup.Item key = {i}>
                        {props.renderFunction(item)}
                    </ListGroup.Item>
                ))}
            </ListGroup>
            
        </div>
    )
}


export function renderProjectList(project : projectModel){
    return(
        <p>
            {project.name}
        </p>
    )
}

export function renderLanguageList(language: languageModel){
    return(
        <p>
            {language.language} + {language.percentage} %
        </p>
    )
}



