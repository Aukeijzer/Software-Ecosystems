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
import { usePathname, useSearchParams } from "next/navigation";
import { useRouter } from "next/router";
import Link from "next/link";
import { subEcosystem } from "@/app/models/ecosystemModel";
import displayable from "@/app/classes/displayableClass";

interface infoCardDataListProps<T>{
    items: displayable[],
    url: string
}

export default function ListComponent<T extends {}>(props : infoCardDataListProps<T>){
    return(
        <div className="h-full">
            <ListGroup>
                {props.items.map((item, i) => (
                    <ListGroup.Item key = {i} >
                       {item.renderAsListItem(props.url)}
                    </ListGroup.Item>
                ))}
            </ListGroup>
            
        </div>
    )
}