"use client"
/*
gridLayout exports:
- GridLayout: JSX.Element containing a grid that can fit any number of grid items
- input:
    - cards: cardWrapper[] a list containing elements that have a cardWrapper around them
- output: JSX.Element

*/

import 'react-grid-layout/css/styles.css' 
import 'react-resizable/css/styles.css' 

import { Responsive, WidthProvider } from "react-grid-layout";
import {cardWrapper} from "@/app/interfaces/cardWrapper";

const ResponsiveGridLayout = WidthProvider(Responsive);
interface InfoCardGridProps{
    cards: cardWrapper[]
}
export default function GridLayout(props: InfoCardGridProps){
    function createElement(card: cardWrapper, i : number) : JSX.Element{
        return(
            <div key={i}  
            data-grid={{i: i, x: card.x, y:card.y, w: card.width, h:card.height, minH: card.minH, minW: card.minW, static: card.static }}
            className='cursor-pointer'
            >
                {card.card}
            </div>
        )
    }
    
    return(
        <ResponsiveGridLayout 
            rowHeight={80}
            breakpoints ={{lg: 3, md:2}}
            cols ={{lg: 6, md:10}}
            autoSize
        > 
           {props.cards.map((card, i) => (
                createElement(card, i)
            ))}
        </ResponsiveGridLayout>
    )
}