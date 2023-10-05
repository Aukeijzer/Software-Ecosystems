"use client"

import { ListGroup } from "flowbite-react"
import { projectModel } from "@/app/models/apiResponseModel";

interface infoCardDataListProps<T>{
    items: T[],
    renderItem: (item: T) => JSX.Element
}

export default function InfoCardDataList<T extends {}>(props : infoCardDataListProps<T>){
    return(
        <div>
            <ListGroup>
                {props.items.map((item, i) => (
                    <ListGroup.Item key = {i}>
                        {props.renderItem(item)}
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


