//This page should fetch data from the api with the passed props as variable and pass it to its children

import { ecoMainResponse } from "@/app/models/ecoMainModel"
import EcoInfo from "./ecoInfo";
import EcoItem from "./ecoItem";
import EcoDataList from "./ecoProjectList";

interface ecoMainProps{
    ecosystem: string,
}



export default async function ecoMain({ecosystem}: ecoMainProps){
    //Make a call to the api
    
    //This should eventually be a seperate component
    const response : Response = await fetch(`http://localhost:5003/ecosystems/${ecosystem}`)
    const result : ecoMainResponse = await response.json();

    //Project dataList
    const EcoDataItem : JSX.Element = <EcoDataList projects={result.projects} />

    return(
        <div className="flex w-full justify-center flex-col">
            <EcoInfo title={result.name} description={result.description}/>
            <EcoItem title="projects" description="lorem ipsum je kent het wel" ecoData={EcoDataItem} />
        </div>
    )

}