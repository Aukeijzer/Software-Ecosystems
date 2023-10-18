import { InfoCardGridLayout } from "@/components/infoCardGridLayout";
import ListComponent from "@/components/listComponent";
import InfoCard from "@/components/infoCard";
import { topTopics, topTechnologies, topEngineers, topProjects, topTopicsGrowing } from "@/mockData/mockAgriculture";
import { renderTechnology, renderEngineer } from "@/components/listComponent";

export interface cardTotal{
    card: JSX.Element,
    height: number,
    width: number
}

export default function testPage(){
    //Top Technologies
    const dataListTechnology = <ListComponent items={topTechnologies} renderFunction={renderTechnology} />
    const cardTechnology = <InfoCard title={"Top 5 technologies"} data={dataListTechnology} alert="This is mock data" />

    //Top Engineers
    const dataListEngineer = <ListComponent items={topEngineers} renderFunction={renderEngineer} />
    const cardEngineer = <InfoCard title={"Top 5 engineers"} data={dataListEngineer}  alert="This is mock data"/>

    var cards : cardTotal[] = []
    cards.push({card: cardTechnology, height: 1, width: 1})
    cards.push({card: cardEngineer, height: 1, width: 1})
    
    return(
        <div className="bg-red-500 ml-10 mr-10 mt-10 w-fill h-screen">
            <InfoCardGridLayout cards={cards}/>
        </div>
      
    )
   
}
