import { handleApi } from "./apiHandler";
import { apiGetAllEcosystems } from "@/app/models/apiResponseModel";
import EcoDataList from "./ecoDataList";
import EcoItem from "./ecoItem";
import { renderEcosystem } from "@/app/models/ecoDataListModel";

export default async function ecoHome(){
    const result : apiGetAllEcosystems = await handleApi(`ecosystems`);
    //Project dataList
    return(
        <div className="flex w-full justify-center flex-col">
                <EcoItem 
                title="Available ecosystems" 
                description="lorem ipsum je kent het wel" 
                ecoData={<EcoDataList 
                            items={result} 
                            renderItem={renderEcosystem}
                            />
                        }
                />
        </div>
    )
}