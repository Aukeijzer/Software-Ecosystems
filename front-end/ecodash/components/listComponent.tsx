/*
Copyright (C) <2024> <OdinDash>

This file is part of SECODash

SECODash is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published
by the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

SECODash is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License for more details.

You should have received a copy of the GNU Affero General Public License
along with SECODash.  If not, see <https://www.gnu.org/licenses/>.

*/

"use client"

import { ListGroup } from "flowbite-react"
import displayableListItem from "@/classes/displayableListItem";

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