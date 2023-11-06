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
import { cardWrapper } from './layoutEcosystemPaged';

const ResponsiveGridLayout = WidthProvider(Responsive);
interface InfoCardGridProps{
    cards: cardWrapper[]
}
export default function GridLayout(props: InfoCardGridProps){
    const layout = [
        { i: "0", x: 0, y: 0, w: 2, h: 1},
        { i: "1", x: 2, y: 1, w: 2, h: 1},
    ];

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
            layouts={{lg: layout}}  
            rowHeight={160}
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