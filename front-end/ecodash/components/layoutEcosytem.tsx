import { handleApiNamed } from "@/components/apiHandler";



import InfoCard from "./infoCard";
import { renderProjectList } from "./infoCardDataList";
import { renderProjectTable } from "./infoCardDataTable";
import { apiNamedEcosystemModel } from "@/app/models/apiResponseModel";
import { renderPieGraph } from "./infoCardDataGraph";

interface layoutEcosystemProps {
    ecosystem?: string,
}

export default async function LayoutEcosystem(props: layoutEcosystemProps){
    
    const result : apiNamedEcosystemModel = await handleApiNamed(`ecosystems/${props.ecosystem}`);


    return(
        <div className="flex flex-col p-10 xl:flex-row">
             {/* top 5 programming languages  */}
            <InfoCard type="list" title={"Top 5 - programming languages"} renderFunction={renderProjectList} items={result.projects ? result.projects : []}/>
            {/* project table */}
            <InfoCard type="table"title={"Project table"} headers={["name", "about", "owner"]} renderFunction={renderProjectTable} items={result.projects? result.projects : []}/>
            {/* example graph */}
            <InfoCard type="graph"title={"Example graph"} renderFunction={renderPieGraph} items={result.projects ? result.projects : []} />
        
        </div>
    )
}

