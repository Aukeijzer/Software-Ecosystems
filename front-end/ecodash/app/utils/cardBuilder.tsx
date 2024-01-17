// import displayable from "../classes/displayableClass";
import { cardWrapper } from "../interfaces/cardWrapper";
import GraphComponent from "@/components/graphComponent";
import InfoCard from "@/components/infoCard";
import ListComponent from "@/components/listComponent";
import GraphLine from "@/components/graphLine";
import displayableGraphItem from "../classes/displayableGraphItem";
import displayableListItem from "../classes/displayableListItem";
import TableComponent from "@/components/tableComponent";
import displayableTableItem from "../classes/displayableTableItem";

/**
 * Builds a pie graph card.
 * @param topics - The displayable topics for the graph.
 * @param title - The title of the card.
 * @param x - The x-coordinate of the card.
 * @param y - The y-coordinate of the card.
 * @param staticProp - Whether the card is static or not.
 * @returns The wrapped card object.
 */
export function buildPieGraphCard(topics: displayableGraphItem[], title: string, x : number, y : number, color?: string) : cardWrapper{
    var graphComponent = <GraphComponent onClick={() => (console.log("Graph clicked"))} items={topics}/>;
    var cardGraph = <InfoCard title={title} data={graphComponent} Color={color}/>
    var width = 2;
    var height = 6;
    var cardGraphWrapped : cardWrapper = {card : cardGraph, width: width, height: height, x : x, y : y, static: true}
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
 * @param staticProp - Whether the card is static or not.
 * @param alert - Optional alert message for the card.
 * @returns The wrapped card object.
 */
export function buildListCard(topics: displayableListItem[], onClick: any, title: string, x : number, y : number, width: number, height: number, staticProp: boolean, alert?: string, color?: string){
    //Make list element
    var listComponent = <ListComponent items={topics} onClick={(sub : string) => onClick(sub)}/>
    //Make card element
    var cardList = <InfoCard title={title} data={listComponent} alert={alert} Color={color}/>
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
 * @param staticProp - Whether the card is static or not.
 * @returns The wrapped card object.
 */
export  function buildLineGraphCard(data: any, title: string, x: number, y : number, staticProp: boolean, color?: string) : cardWrapper{
    const lineGraphTopicsGrowing = <GraphLine items={data} />
    const cardLineGraph = <InfoCard title={title} data={lineGraphTopicsGrowing} Color={color}/>
    const cardLineGraphWrapped: cardWrapper = {card: cardLineGraph, x: x, y : y, width: 4, height: 6, static:true}
    return cardLineGraphWrapped;
}

/**
 * Builds a table card.
 */
export function buildTableCard(items: displayableTableItem[], title: string, x : number, y : number, width: number, height: number, onClick: any, filterType: string, alert?: string, color?: string){
    //Make table element
    var tableComponent = <TableComponent items={items} onClick={(sub : string) => onClick(sub, filterType)}/>
    //Make card element
    var cardTable = <InfoCard title={title} data={tableComponent} alert={alert} Color={color}/>
    //Wrap card
    const cardTableWrapped: cardWrapper = {card: cardTable, width: width, height: height, x: x, y: y, minH: 2, static:true}
    return cardTableWrapped
}


