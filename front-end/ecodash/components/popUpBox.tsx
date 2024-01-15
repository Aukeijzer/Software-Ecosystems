"use client"
import { useState } from "react";

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

    if(!show){
        return(
            <div className="absolute w-screen h-screen bg-opacity-80  bg-gray-500 z-10 top-0 left-0" onClick={() => setShow(false)} data-cy={".oauthPopUp"}>
                <div className="relative top-20 left-1/3 z-10 w-min border-4 border-black shadow-2xl p-3 rounded-xl bg-opacity-100 bg-gray-900 "  >
                    <div>
                        <button data-cy={".popUpClose"} onClick={(event ) => onClickFunc(event)} className="pt-1 pl-2 pr-2 pb-1 text-white border-2 rounded-md  border-gray-900 hover:bg-red-500">X</button>
                        {props.children}
                    </div>
                </div>
            </div>
        )    
    } else {
        return(
         <div>
                <button data-cy="loginButton" onClick={() => setShow(true)} className="bg-gray-500 border-2 p-2 border-gray-900">{props.buttonText}</button>
         </div>)
    }
}