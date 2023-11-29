"use client"

import 'react-grid-layout/css/styles.css' 
import 'react-resizable/css/styles.css' 

import { Responsive, WidthProvider } from "react-grid-layout";
import {cardWrapper} from "@/app/interfaces/cardWrapper";

//Get initial width of the window
const ResponsiveGridLayout = WidthProvider(Responsive);

interface InfoCardGridProps{
    cards: cardWrapper[]
}

/**
 * Renders a grid layout that can fit any number of grid items.
 *
 * @component
 * @param {Object} props - The component props.
 * @param {cardWrapper[]} props.cards - A list of elements wrapped in a cardWrapper.
 * @returns {JSX.Element} The rendered grid layout.
 */

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