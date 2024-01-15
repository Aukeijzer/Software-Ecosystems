"use client"


import { ListGroup } from "flowbite-react"
import displayableListItem from "@/app/classes/displayableListItem";


interface infoCardDataListProps{
    /**
     * An array of displayable items.
     */
    items: displayableListItem[];

    /**
     * A callback function triggered when an item is clicked.
     *
     * @param sub - The sub value associated with the clicked item.
     */
    onClick: (sub: string) => void;
}
/**
 * Renders a list component.
 *
 * @template T - The type of items in the list.
 * @param {infoCardDataListProps} props - The props for the list component.
 * @returns {JSX.Element} The rendered list component.
 */

export default function ListComponent(props : infoCardDataListProps){
    return(
        <div data-cy='list component'className="h-full">
            <ListGroup data-cy='list group' className="border-none bg-amber">
                {props.items.map((item, i) => (
                    //Maybe move onClick to the box instead of the text
                    <ListGroup.Item data-cy='list item' key = {i} >
                       {i + 1}. &nbsp; {item.renderAsListItem(props.onClick)}
                    </ListGroup.Item>
                ))}
            </ListGroup>
            
        </div>
    )
}