import { ReactNode } from "react";

interface cardProps{
    children: ReactNode,
    className?: string,
    onClick?: any
}
export default function Card(props: cardProps){
    return(
        <div className={"relative bg-white w-full h-full rounded-sm pt-3 px-1" + props.className}>
            {props.children}
        </div>
    )

}