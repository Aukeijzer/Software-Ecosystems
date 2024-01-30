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
import { useState } from "react";
import Button from "./button";

interface PopUpBoxProps{
    buttonText: string,
    children: React.ReactNode[]
}

/**
 * Represents a pop-up box component.
 * @param {string} buttonText - The text to display on the button.
 * @param {React.ReactNode[]} children - The content to display inside the pop-up box.
 * @returns {JSX.Element} The rendered pop-up box component.
 */

export default function PopUpBox(props: PopUpBoxProps){
    const [show, setShow] = useState(false);
    
    function onClickFunc(event: React.MouseEvent<HTMLButtonElement, MouseEvent>){
        setShow(false);
    }

    if(show){
        return(
            <div className="absolute w-screen h-screen bg-opacity-80  bg-gray-500 z-10 top-0 left-0" onClick={() => setShow(false)}>
                <div className="relative top-20 left-1/3 z-10 w-min border-4 border-black shadow-2xl p-3 rounded-xl bg-opacity-100 bg-gray-900 "  >
                    <div>
                        <button onClick={(event ) => onClickFunc(event)} className="pt-1 pl-2 pr-2 pb-1 text-white border-2 rounded-md  border-gray-900 hover:bg-red-500">X</button>
                        {props.children}
                    </div>
                </div>
            </div>
        )    
    } else {
        return(
         <div>
                <Button data-cy="loginButton" text={props.buttonText} onClick={() => setShow(true)} />
         </div>)
    }
}