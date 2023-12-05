"use client"
import { useState } from "react";
import LoginBox from "./loginBox";

interface PopUpBoxProps{
    children: React.JSX.Element,
    buttonText: string,
}
export default function PopUpBox(props: PopUpBoxProps){
    const [show, setShow] = useState(false);
    
    function onClickFunc(event: React.MouseEvent<HTMLButtonElement, MouseEvent>){
        
        setShow(false);
    }

    if(show){
        return(
            <div className="absolute w-screen h-screen bg-opacity-80  bg-gray-500 z-10 top-0 left-0" onClick={() => setShow(false)}>
                <div className="relative top-20 left-1/3 z-10 w-min border-4 border-black shadow-2xl p-3 bg-opacity-100 bg-gray-500 rounded-md"  >
                    <div>
                        <button onClick={(event ) => onClickFunc(event)} className="p-3 bg-red-400 border-2 rounded-3xl  border-gray-900">X</button>
                        {props.children}
                    </div>
                </div>
            </div>
        )
            
            
    } else {
        return(
         <div>
                <button onClick={() => setShow(true)} className="bg-gray-500 border-2 p-2 border-gray-900">{props.buttonText}</button>
         </div>)
    }
}