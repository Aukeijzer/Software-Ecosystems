"use client"

import { ListGroup } from "flowbite-react"
import { Project } from "@/app/models/apiResponseModel";

interface infoCardDataListProps<T>{
    items: T[],
    renderItem: (item: T) => JSX.Element
}

export default function InfoCardDataList<T extends {}>(props : infoCardDataListProps<T>){
    return(
        <div>
            <ListGroup>
                {props.items.map((item, i) => (
                    <ListGroup.Item>
                        {props.renderItem(item)}
                    </ListGroup.Item>
                ))}
            </ListGroup>
            
        </div>
    )
}


export function renderProjectList(project : Project){
    return(
        <p>
            {project.name}
        </p>
    )
}


