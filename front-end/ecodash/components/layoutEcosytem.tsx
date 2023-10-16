import { handleApiNamed } from "@/components/apiHandler";



import InfoCard from "./infoCard";
import InfoCardDataList, { renderLanguageList, renderProjectList, renderTechnology, renderEngineer, renderTopic, renderTopicGrowing } from "./infoCardDataList";
import InfoCardDataTable, { renderProjectTable } from "./infoCardDataTable";
import InfoCardDataGraph, { renderPieGraph } from "./infoCardDataGraph";
import InfoCardGrid from "./infoCardGrid";
import { ecosystemModel } from "@/app/models/apiResponseModel";
import EcosystemDescription from "./ecosystemDescription";

//Mock data
import { topTopics, topTechnologies, topEngineers, topProjects, topTopicsGrowing } from "@/mockData/mockAgriculture";

interface layoutEcosystemProps {
    ecosystem: string,
}

export default async function LayoutEcosystem(props: layoutEcosystemProps){
    
    const result : ecosystemModel = await handleApiNamed(`ecosystems/name/${props.ecosystem}`);
    //console.log(result)

    var cardList : JSX.Element[] = [];
    //List
    const dataListLanguages = <InfoCardDataList items={result.topLanguages} renderFunction={renderLanguageList}/>
    const cardLanguages = <InfoCard title={"Top 5 languages"} data={dataListLanguages} />

    //Table
    const dataTableProjects = <InfoCardDataTable items={result.projects} headers={["name", "description", "owner", "languages"]} renderFunction={renderProjectTable} />
    const cardProjects = <InfoCard title={"Test table"} data={dataTableProjects} />
  
    //Graph functions
    const dataGraphLanguages = <InfoCardDataGraph items={result.topLanguages} renderFunction={renderPieGraph} />
    const cardGraph = <InfoCard title={"Test graph"} data={dataGraphLanguages} />
   
    //Top Technologies
    const dataListTechnology = <InfoCardDataList items={topTechnologies} renderFunction={renderTechnology} />
    const cardTechnology = <InfoCard title={"Top 5 technologies"} data={dataListTechnology} alert="This is mock data" />

    //Top Engineers
    const dataListEngineer = <InfoCardDataList items={topEngineers} renderFunction={renderEngineer} />
    const cardEngineer = <InfoCard title={"Top 5 engineers"} data={dataListEngineer}  alert="This is mock data"/>

    //Top Topics
    const dataListTopic = <InfoCardDataList items={topTopics} renderFunction={renderTopic}  />
    const cardTopic = <InfoCard title={"Top 5 topics"} data={dataListTopic}  alert="This is mock data"/>

    //Top growing languages
    const dataListLanguageGrowing = <InfoCardDataList items={result.topLanguages} renderFunction={renderLanguageList} />
    const cardLangageGrowing = <InfoCard title={"Top 5 fastest growing languages"} data={dataListLanguageGrowing}  alert="This is mock data"/>

    //Top growing topics
    const dataListTopicsGrowing = <InfoCardDataList items={topTopicsGrowing} renderFunction={renderTopicGrowing} />
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
