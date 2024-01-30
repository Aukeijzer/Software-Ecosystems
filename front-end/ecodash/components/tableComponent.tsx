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

import displayableTableItem from "@/classes/displayableTableItem";

interface infoCardDataTableProps<T>{
    /**
     * A list of items that each should be rendered in the table.
     */
    items: displayableTableItem[]

    onClick: (sub: string) => void;
}

/**
 * Component that renders a table with provided data.
 * @param props The props for the table component.
 * @returns A JSX.Element representing the table.
 */
export default function TableComponent<T extends {}>(props: infoCardDataTableProps<T>){
    if(props.items.length > 0){
        return(
            <div>
                <table className="cursor-pointer w-full text-left text-gray-500">
                    <thead className=" uppercase bg-gray-200">
                        {props.items[0].renderTableHeaders()}
                    </thead>
                    <tbody className="w-full">
                        {props.items.map((item) => (
                            item.renderAsTableItem(props.onClick)
                        ))}
                    </tbody>
                </table>
            </div>
        )
    }
    else{
        return(
            <div className="flex justify-center">
                No more data to show.
            </div>
        )
    }
    
}




