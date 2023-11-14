"use client"

/*
infoCardDataList exports:
- InfoCardDataList: JSX.Element containing a div that contains a rendered list
    - input: 
        - items: T[]: a list containing items of Type T
        - renderFunction: (T) => JSX.Elemnent: a function that renders T items. (Must be same as list type)
    - output: JSX.Element
*/

import { ListGroup } from "flowbite-react"
import displayable from "@/app/classes/displayableClass";

interface infoCardDataListProps<T>{
    items: displayable[],
    onClick: (sub: string) => void
}

export default function ListComponentSingle<T extends {}>(props : infoCardDataListProps<T>){
    return(
        <div className="h-full">
            <ListGroup>
                {props.items.map((item, i) => (
                    <ListGroup.Item key = {i} >
                       {item.renderAsListItem(props.onClick)}
                    </ListGroup.Item>
                ))}
            </ListGroup>
            
        </div>
    )
}