"use client"

/*
infoCardDataList exports:
- InfoCardDataList: JSX.Element containing a div that contains a rendered list
    - input: 
        - items: T[]: a list containing items of Type T
        - renderFunction: (T) => JSX.Elemnent: a function that renders T items. (Must be same as list type)
    - output: JSX.Element
- renderProjectList: renders a single project as a list item
    - input:
        -item: Project
    -Output: 
        - JSX.Element 
- renderLanguageList: renders a single language as a list item
    - input:
        -item: Language
    - ouput:
        - JSX.Element
- renderTechnology: renders a single Technology as a list item
    - input:
        -item: technology
    - ouput:
        - JSX.Element
- renderEngineer: renders a single Engineer as a list item
    - input:
        -item: topEngineer
    - ouput:
        - JSX.Element
- renderTopic:  renders a single Topic as a list item
    - input:
        -item: topTopic
    - ouput:
        - JSX.Element
- renderTopicGrowing renders a growing topic along with growth direction
    - input:
        -item: topTopicGrowing
    - ouput:
        - JSX.Element

*/

import { ListGroup } from "flowbite-react"
import { languageModel } from "@/app/models/languageModel";
import { projectModel } from "@/app/models/projectModel";
import {topTopic, topTechnology, topProject, topEngineer, topTopicGrowing, topTechnologyGrowing} from "@/mockData/mockAgriculture";
import {HiArrowLongUp} from "react-icons/hi2";
import { ogranization } from "@/mockData/mockEcosystems";
import Link from "next/link";

interface infoCardDataListProps<T>{
    items: T[],
    renderFunction: (item: T) => JSX.Element
}

export default function ListComponent<T extends {}>(props : infoCardDataListProps<T>){
    return(
        <div className="h-full">
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
        <Link href={`/subdomain?subdomains=${topic.name}`}>
            {topic.name} ({topic.percentage}%)
        </Link>
    )
}

export function renderTopicGrowing(topic: topTopicGrowing){
    return(
        <p className="flex flex-row">
            {topic.name} ({topic.percentage}%) : {topic.growth} <HiArrowLongUp />
        </p>
    )
}

export function renderOrganization(org: ogranization){
    return(
        <div>
            {org.name}
        </div>
    )
}

export function renderTechnologyGrowing(technology: topTechnologyGrowing){
    return(
        <div className="flex flex-row">
            {technology.name} ({technology.percentage}%) : {technology.growth} <HiArrowLongUp />
        </div>    
    )
}
