import displayable from "../classes/displayableClass";
import { cardWrapper } from "../interfaces/cardWrapper";
import GraphComponent from "@/components/graphComponent";
import InfoCard from "@/components/infoCard";
import ListComponent from "@/components/listComponent";
import GraphLine from "@/components/graphLine";

export function buildPieGraphCard(topics: displayable[], title: string, x : number, y : number) : cardWrapper{
    var graphComponent = <GraphComponent items={topics}/>;
    var cardGraph = <InfoCard title={title} data={graphComponent} />
    //TODO: (min)Width / (min)height should be automatically detected here
    var width = 2;
    var height = 6;
    var cardGraphWrapped : cardWrapper = {card : cardGraph, width: width, height: height, x : x, y : y, static: false}
    return cardGraphWrapped;
}


export function buildListCard(topics: displayable[], onClick: any, title: string, x : number, y : number, width: number, height: number, alert?: string){
    //Make list element
    var listComponent = <ListComponent items={topics} onClick={(sub : string) => onClick(sub)}/>
    //Make card element
    var cardList = <InfoCard title={title} data={listComponent} alert={alert}/>
    //Wrap card
    //TODO: (min)Width / (min)height should be automatically detected here
    const cardListWrapped: cardWrapper = {card: cardList, width: width, height: height, x: x, y: y, minH: 2, static:true}
    return cardListWrapped
}


export  function buildLineGraphCard(data: any, title: string, x: number, y : number) : cardWrapper{
    const lineGraphTopicsGrowing = <GraphLine items={data} />
    const cardLineGraph = <InfoCard title={title} data={lineGraphTopicsGrowing} alert="This is mock data"/>
    const cardLineGraphWrapped: cardWrapper = {card: cardLineGraph, x: x, y : y, width: 4, height: 6, static:true}
    return cardLineGraphWrapped;
}



