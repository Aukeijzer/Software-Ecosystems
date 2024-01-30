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

import { ReactNode } from "react";

interface cardProps{
    children: ReactNode,
    className?: string,
    onClick?: any
}
/**
 * 
 * @param props - children: (ReactNode) - children that the card should render
 *  - optionalclassname: (string) classname that gets applied to the div
 *  - optional onClick: (() => void) function that is called when card is clicked 
 * @returns 
 */
export default function Card(props: cardProps){
    return(
        <div className={"relative bg-white w-full h-full rounded-sm pt-3 px-1" + props.className}
        onClick={props.onClick}
        >
            {props.children}
        </div>
    )

}