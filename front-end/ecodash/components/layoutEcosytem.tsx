/*
layoutEcosystem exports:
- LayoutEcosystem:
    - input:
    - output: 
*/

import { handleApiNamed } from "@/components/apiHandler";
import InfoCard from "./infoCard";
import ListComponent, { renderLanguageList, renderProjectList, renderTechnology, renderEngineer, renderTopic, renderTopicGrowing } from "./listComponent";
import TableComponent, { renderProjectTable } from "./tableComponent";
import GraphComponent, { renderPieGraph } from "./graphComponent";
import { ecosystemModel } from "@/app/models/ecosystemModel";
import EcosystemDescription from "./ecosystemDescription";

//Mock data
import { topTopics, topTechnologies, topEngineers, topProjects, topTopicsGrowing } from "@/mockData/mockAgriculture";
import  GridLayout  from "./gridLayout";
export interface cardWrapper{
    card: JSX.Element,
    width: number,
    height: number,
    x: number,
    y: number,
    minW?: number,
    minH?: number,
    static?: boolean,
}

interface layoutEcosystemProps {
    ecosystem: string,
}

export default async function LayoutEcosystem(props: layoutEcosystemProps){
    const result : ecosystemModel = await handleApiNamed(`ecosystems/name/${props.ecosystem}`);

    //List
    const dataListLanguages = <ListComponent items={result.topLanguages} renderFunction={renderLanguageList}/>
    const cardLanguages = <InfoCard title={"Top 5 languages"} data={dataListLanguages} />
    const cardLanguagesWrapped : cardWrapper= {card: cardLanguages, width:1, height:2, x: 0, y: 1, minH: 2}
    //Table
    const dataTableProjects = <TableComponent items={result.projects} headers={["name", "description", "owner", "languages"]} renderFunction={renderProjectTable} />
    const cardProjects = <InfoCard title={"Test table"} data={dataTableProjects} />
    const cardProjectsWrapped : cardWrapper= {card: cardProjects, width:2, height:14, x: 0, y: 1, minH: 10  , minW: 2}

    //Graph functions
    const dataGraphLanguages = <GraphComponent items={result.topLanguages} renderFunction={renderPieGraph} />
    const cardGraph = <InfoCard title={"Graph: Top 5 languages"} data={dataGraphLanguages} />
    const cardGraphWrapped : cardWrapper = {card: cardGraph, width:2, height:3, x: 1, y: 1, minH: 3, minW: 2}

    //Top Technologies
    const dataListTechnology = <ListComponent items={topTechnologies} renderFunction={renderTechnology} />
    const cardTechnology = <InfoCard title={"Top 5 technologies"} data={dataListTechnology} alert="This is mock data" />
    const cardTechnologyWrapped : cardWrapper = {card: cardTechnology, width: 2, height: 2, x:3 , y:1, minH: 2}
    
    //Top Engineers
    const dataListEngineer = <ListComponent items={topEngineers} renderFunction={renderEngineer} />
    const cardEngineer = <InfoCard title={"Top 5 contributers"} data={dataListEngineer}  alert="This is mock data"/>
    const cardEngineerWrapped: cardWrapper = {card: cardEngineer, width: 1, height: 2, x:3, y:2, minH: 2}
    
    //Top Topics
    const dataListTopic = <ListComponent items={topTopics} renderFunction={renderTopic}  />
    const cardTopic = <InfoCard title={"Top 5 topics"} data={dataListTopic}  alert="This is mock data"/>
    const cardTopicWrapped: cardWrapper = {card: cardTopic, width: 1, height: 2, x: 5, y: 1, minH: 2}

    //Top growing languages
    const dataListLanguageGrowing = <ListComponent items={result.topLanguages} renderFunction={renderLanguageList} />
    const cardLanguageGrowing = <InfoCard title={"Top 5 fastest growing languages"} data={dataListLanguageGrowing}  alert="This is mock data"/>
    const cardLanguageGrowingWrapped: cardWrapper = {card: cardLanguageGrowing, width: 1, height: 3, x: 0, y: 3, minH: 3}

    //Top growing topics
    const dataListTopicsGrowing = <ListComponent items={topTopicsGrowing} renderFunction={renderTopicGrowing} />
    const cardTopicGrowing = <InfoCard title={"Top 5 fastest growing topics"} data={dataListTopicsGrowing} alert="This is mock data" />
    const cardTopicGrowingWrapped: cardWrapper = {card: cardTopicGrowing, width: 1, height: 2, x: 1, y:3, minH:2}

    //Description 
    const ecosystemDescription =  <EcosystemDescription ecosystem={result.displayName? result.displayName : props.ecosystem} description={result.description? result.description : "No description provided" } />
    const ecosystemDescriptionWrapped : cardWrapper = {card: ecosystemDescription, width: 6, height: 1, x: 0, y: 0, static:true}

    var cards : cardWrapper[] = [];
    //Add cards to cards
    cards.push(cardLanguagesWrapped);
    //cards.push(cardProjectsWrapped);
    cards.push(cardGraphWrapped);
    cards.push(cardTechnologyWrapped);
    cards.push(cardEngineerWrapped);
    cards.push(cardTopicWrapped);
    cards.push(cardLanguageGrowingWrapped);
    cards.push(cardTopicGrowingWrapped);
    cards.push(ecosystemDescriptionWrapped);

   

    return(
        <div className="mt-5 ml-10 mr-10">
            <GridLayout cards={cards} />
        </div>  
    )
}
