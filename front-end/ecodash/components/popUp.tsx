"use client"
import { Children, useState } from "react";
import LoginBox from "./loginBox";

interface PopUpBoxProps{
    buttonText: string,
    children: React.ReactNode[]
}
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
         <div className="flex">
                <button data-cy="loginButton" onClick={() => setShow(true)} className="flex ml-3 bg-gray-300 pl-3 pr-3 pt-2 pb-2 rounded-md"> <b>{props.buttonText} </b></button>
         </div>)
    }
}