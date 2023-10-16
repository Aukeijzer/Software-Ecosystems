import { handleApiNamed } from "@/components/apiHandler";



import InfoCard from "./infoCard";
import ListComponent, { renderLanguageList, renderProjectList, renderTechnology, renderEngineer, renderTopic, renderTopicGrowing } from "./listComponent";
import TableComponent, { renderProjectTable } from "./tableComponent";
import GraphComponent, { renderPieGraph } from "./graphComponent";
import InfoCardGrid from "./infoCardGrid";
import { ecosystemModel } from "@/app/models/ecosystemModel";
import EcosystemDescription from "./ecosystemDescription";

//Mock data
import { topTopics, topTechnologies, topEngineers, topProjects, topTopicsGrowing } from "@/mockData/mockAgriculture";

interface layoutEcosystemProps {
    ecosystem: string,
}

export default async function LayoutEcosystem(props: layoutEcosystemProps){
    const result : ecosystemModel = await handleApiNamed(`ecosystems/name/${props.ecosystem}`);
    var cardList : JSX.Element[] = [];
    //List
    const dataListLanguages = <ListComponent items={result.topLanguages} renderFunction={renderLanguageList}/>
    const cardLanguages = <InfoCard title={"Top 5 languages"} data={dataListLanguages} />

    //Table
    const dataTableProjects = <TableComponent items={result.projects} headers={["name", "description", "owner", "languages"]} renderFunction={renderProjectTable} />
    const cardProjects = <InfoCard title={"Test table"} data={dataTableProjects} />
  
    //Graph functions
    const dataGraphLanguages = <GraphComponent items={result.topLanguages} renderFunction={renderPieGraph} />
    const cardGraph = <InfoCard title={"Test graph"} data={dataGraphLanguages} />
   
    //Top Technologies
    const dataListTechnology = <ListComponent items={topTechnologies} renderFunction={renderTechnology} />
    const cardTechnology = <InfoCard title={"Top 5 technologies"} data={dataListTechnology} alert="This is mock data" />

    //Top Engineers
    const dataListEngineer = <ListComponent items={topEngineers} renderFunction={renderEngineer} />
    const cardEngineer = <InfoCard title={"Top 5 engineers"} data={dataListEngineer}  alert="This is mock data"/>

    //Top Topics
    const dataListTopic = <ListComponent items={topTopics} renderFunction={renderTopic}  />
    const cardTopic = <InfoCard title={"Top 5 topics"} data={dataListTopic}  alert="This is mock data"/>

    //Top growing languages
    const dataListLanguageGrowing = <ListComponent items={result.topLanguages} renderFunction={renderLanguageList} />
    const cardLangageGrowing = <InfoCard title={"Top 5 fastest growing languages"} data={dataListLanguageGrowing}  alert="This is mock data"/>

    //Top growing topics
    const dataListTopicsGrowing = <ListComponent items={topTopicsGrowing} renderFunction={renderTopicGrowing} />
    const cardTopicGrowing = <InfoCard title={"Top 5 fastest growing topics"} data={dataListTopicsGrowing} alert="This is mock data" />

    //Add to cardList
    cardList.push(cardLanguages);
    cardList.push(cardTechnology);
    cardList.push(cardEngineer);
    cardList.push(cardTopic);
    cardList.push(cardGraph);
    cardList.push(cardProjects);
    cardList.push(cardLangageGrowing);
    cardList.push(cardTopicGrowing);
    
    return(
        <div className="mt-5 ml-10 mr-10">
            <EcosystemDescription ecosystem={result.displayName? result.displayName : props.ecosystem} description={result.description? result.description : "No description provided :(" } />
            <InfoCardGrid cards={cardList}/>
        </div>  
    )
}
