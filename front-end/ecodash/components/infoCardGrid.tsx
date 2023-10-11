"use client"

interface infoCardGridProps{
    cards: JSX.Element[]
}
export default function infoCardGrid(props: infoCardGridProps){

    //Stupid way to order them. This should be more customisable
    var firstColCards : JSX.Element[] = [];
    var secondColCards : JSX.Element[] = [];
    var thirdColCards : JSX.Element[] = [];

    props.cards.forEach((card , i) => {
        if(i % 3 === 0)
            firstColCards.push(card);
            
        if(i % 3 === 1)
            secondColCards.push(card);
        if(i % 3 === 2)
            thirdColCards.push(card);
    });

    return(
        <div className="flex flex-row space-x-2 mt-5">
            <div className="w-1/3 flex flex-col  space-y-4 ">
                {firstColCards}
            </div>
            <div className="w-1/3 flex flex-col  space-y-4 ">
                {secondColCards}
            </div>
            <div className="w-1/3 flex-col  space-y-4 ">
                {thirdColCards}
            </div>
        </div>
    )
}