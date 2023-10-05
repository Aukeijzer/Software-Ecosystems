import { handleApiNamed } from "@/components/apiHandler";



import InfoCard from "./infoCard";
import { renderProjectList } from "./infoCardDataList";
import { renderProjectTable } from "./infoCardDataTable";
import { ecosystemModel } from "@/app/models/apiResponseModel";
import { renderPieGraph } from "./infoCardDataGraph";
import EcosystemDescription from "./ecosystemDescription";

interface layoutEcosystemProps {
    ecosystem: string,
}

export default async function LayoutEcosystem(props: layoutEcosystemProps){
    
    const result : ecosystemModel = await handleApiNamed(`ecosystems/name/${props.ecosystem}`);
    console.log(result.name);

    return(
        <div>
            <EcosystemDescription ecosystem={result.displayName? result.displayName : props.ecosystem} description={result.description? result.description : "No description provided :(" } />
            <div className="flex flex-col p-10  xl:flex-row">

                {/* top 5 programming languages  */}
                <InfoCard type="list" title={"Top 5 - programming languages"} renderFunction={renderProjectList} items={result.projects ? result.projects : []}/>
                {/* project table */}
                <InfoCard type="table"title={"Project table"} headers={["name", "description", "owner", "languages"]} renderFunction={renderProjectTable} items={result.projects? result.projects : []}/>
                {/* example graph */}
                <InfoCard type="graph"title={"Example graph"} renderGraph={renderPieGraph} items={result.projects ? result.projects : []} />
            </div>
        </div>
        

       
    )
}


