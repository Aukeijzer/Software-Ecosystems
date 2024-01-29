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