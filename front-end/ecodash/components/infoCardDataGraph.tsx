"use client"

interface infoCardDataGraphProps<T>{
    items: T[],
    renderItem: (items: T[]) => JSX.Element
}

export default function infoCardDataGraph<T extends {}>(props: infoCardDataGraphProps<T>){
    <div>
        {props.renderItem(props.items)}
    </div>
}

export function renderPieGraph(){
    
}