import { handleApiNamed } from "@/components/apiHandler";



import InfoCard from "./infoCard";
import { renderProjectList } from "./infoCardDataList";
import { renderProjectTable } from "./infoCardDataTable";
import { apiNamedEcosystemModel } from "@/app/models/apiResponseModel";

interface layoutEcosystemProps {
    ecosystem?: string,
}

export default async function LayoutEcosystem(props: layoutEcosystemProps){
    
    const result : apiNamedEcosystemModel = await handleApiNamed(`ecosystems/${props.ecosystem}`);


    return(
        <div className="flex flex-col p-10 xl:flex-row">
             {/* top 5 programming languages  */}
            <InfoCard title={"Top 5 - programming languages"} renderFunction={renderProjectList} items={result.projects ? result.projects : []}/>
            {/* project list */}
            <InfoCard title={"Project table"} headers={["name", "about", "owner"]} renderFunction={renderProjectTable} items={result.projects? result.projects : []}/>
        </div>
    )
}

