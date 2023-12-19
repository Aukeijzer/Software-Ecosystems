// import displayable from "../classes/displayableClass";
import { cardWrapper } from "../interfaces/cardWrapper";
import GraphComponent from "@/components/graphComponent";
import InfoCard from "@/components/infoCard";
import ListComponent from "@/components/listComponent";
import GraphLine from "@/components/graphLine";
import displayableGraphItem from "../classes/displayableGraphItem";
import displayableListItem from "../classes/displayableListItem";


/**
 * Builds a pie graph card.
 * @param topics - The displayable topics for the graph.
 * @param title - The title of the card.
 * @param x - The x-coordinate of the card.
 * @param y - The y-coordinate of the card.
 * @returns The wrapped card object.
 */
export function buildPieGraphCard(topics: displayableGraphItem[], title: string, x : number, y : number, staticProp: boolean) : cardWrapper{
    var graphComponent = <GraphComponent items={topics}/>;
    var cardGraph = <InfoCard title={title} data={graphComponent} />
    //TODO: (min)Width / (min)height should be automatically detected here
    var width = 2;
    var height = 6;
    var cardGraphWrapped : cardWrapper = {card : cardGraph, width: width, height: height, x : x, y : y, static: staticProp}
    return cardGraphWrapped;
}

/**
 * Builds a list card.
 * @param topics - The displayable topics for the list.
 * @param onClick - The click event handler for the list items.
 * @param title - The title of the card.
 * @param x - The x-coordinate of the card.
 * @param y - The y-coordinate of the card.
 * @param width - The width of the card.
 * @param height - The height of the card.
 * @param alert - Optional alert message for the card.
 * @returns The wrapped card object.
 */
export function buildListCard(topics: displayableListItem[], onClick: any, title: string, x : number, y : number, width: number, height: number, staticProp: boolean, alert?: string) : cardWrapper{
    //Make list element
    var listComponent = <ListComponent items={topics} onClick={(sub : string) => onClick(sub)}/>
    //Make card element
    var cardList = <InfoCard title={title} data={listComponent} alert={alert}/>
    //Wrap card
    //TODO: (min)Width / (min)height should be automatically detected here
    const cardListWrapped: cardWrapper = {card: cardList, width: width, height: height, x: x, y: y, minH: 2, static:staticProp}
    return cardListWrapped
}

/**
 * Builds a line graph card.
 * @param data - The data for the line graph.
 * @param title - The title of the card.
 * @param x - The x-coordinate of the card.
 * @param y - The y-coordinate of the card.
 * @returns The wrapped card object.
 */
export  function buildLineGraphCard(data: any, title: string, x: number, y : number, staticProp: boolean) : cardWrapper{
    const lineGraphTopicsGrowing = <GraphLine items={data} />
    const cardLineGraph = <InfoCard title={title} data={lineGraphTopicsGrowing} alert="This is mock data"/>
    const cardLineGraphWrapped: cardWrapper = {card: cardLineGraph, x: x, y : y, width: 4, height: 6, static:staticProp}
    return cardLineGraphWrapped;
}



