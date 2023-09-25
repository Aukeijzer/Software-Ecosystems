//This page should fetch data from the api with the passed props as variable and pass it to its children

import { apiNamedEcosystemModel } from "@/app/models/apiResponseModel"
import EcoInfo from "./ecoInfo";
import EcoItem from "./ecoItem";
import EcoDataList from "./ecoDataList";
import { renderProject } from "@/app/models/ecoDataListModel";
import  { handleApiNamed }  from "./apiHandler";

interface ecoMainProps{
    ecosystem?: string,
}

export default async function ecoMain({ecosystem}: ecoMainProps){
    const result : apiNamedEcosystemModel = await handleApiNamed(`ecosystems/${ecosystem}`);
   
    //Project dataList
    const EcoDataItem : JSX.Element = <EcoDataList items={result.projects ? result.projects : []} renderItem={renderProject} />

    return(
        <div className="flex w-full justify-center flex-col">
            <EcoInfo title={result.name} description={result.description ? result.description : ""}/>
            <EcoItem title="projects" description="lorem ipsum je kent het wel" ecoData={EcoDataItem} />
        </div>
    )
}