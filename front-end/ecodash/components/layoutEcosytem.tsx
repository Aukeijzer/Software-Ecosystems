import { handleApiNamed } from "@/components/apiHandler";



import InfoCard from "./infoCard";
import InfoCardDataList, { renderLanguageList, renderProjectList } from "./infoCardDataList";
import InfoCardDataTable, { renderProjectTable } from "./infoCardDataTable";
import InfoCardDataGraph, { renderPieGraph } from "./infoCardDataGraph";
import { ecosystemModel } from "@/app/models/apiResponseModel";
import EcosystemDescription from "./ecosystemDescription";

interface layoutEcosystemProps {
    ecosystem: string,
}

export default async function LayoutEcosystem(props: layoutEcosystemProps){
    
    const result : ecosystemModel = await handleApiNamed(`ecosystems/name/${props.ecosystem}`);
    //console.log(result)

    //Set up data items
    const dataListLanguages = <InfoCardDataList items={result.topLanguages} renderFunction={renderLanguageList}/>
    const dataTableProjects = <InfoCardDataTable items={result.projects} headers={["name", "description", "owner", "languages"]} renderFunction={renderProjectTable} />
    
    //Graph functions
    
    const dataGraphLanguages = <InfoCardDataGraph items={result.topLanguages} renderFunction={renderPieGraph} />

    return(
        <div>
            <EcosystemDescription ecosystem={result.displayName? result.displayName : props.ecosystem} description={result.description? result.description : "No description provided :(" } />
            <div className="flex flex-col p-10  xl:flex-row">
                {/* top 5 programming languages  */}
                <InfoCard title={"Top 5 - programming languages list "} data={dataListLanguages}/>
                {/* project table */}
                <InfoCard title={"Project table"} data={dataTableProjects}/>
                {/* */}
                <InfoCard title={"Top 5 languages graph"} data={dataGraphLanguages} />
            </div>
        </div>
        

       
    )
}


