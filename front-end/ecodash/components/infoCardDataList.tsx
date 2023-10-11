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
import {topTopic, topTechnology, topProject, topEngineer, topTopicGrowing} from "@/mockData/mockAgriculture";
import {HiArrowLongUp} from "react-icons/hi2";

interface infoCardDataListProps<T>{
    items: T[],
    renderFunction: (item: T) => JSX.Element
}

export default function InfoCardDataList<T extends {}>(props : infoCardDataListProps<T>){
    return(
        <div>
            <ListGroup>
                {props.items.map((item, i) => (
                    <ListGroup.Item key = {i} >
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
            {language.language} : {language.percentage} %
        </p>
    )
}

export function renderTechnology(technology: topTechnology){
    return(
        <p>
            {technology.name} ({technology.percentage}%)
        </p>
    )
}

export function renderEngineer(engineer : topEngineer){
    return(
        <p>
            {engineer.name}
        </p>
    )
}

export function renderTopic(topic: topTopic){
    return(
        <p>
            {topic.name} ({topic.percentage}%)
        </p>
    )
}

export function renderTopicGrowing(topic: topTopicGrowing){
    

    return(
        <p className="flex flex-row">
            {topic.name} ({topic.percentage}%) : {topic.growth} <HiArrowLongUp />
        </p>
    )
}




