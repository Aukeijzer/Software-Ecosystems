import { apiGetAllEcosystems, apiNamedEcosystemModel, apiResponse } from "@/app/models/apiResponseModel";

export async function handleApi(endpoint : string) : Promise<apiGetAllEcosystems> {
    const response : Response = await fetch(`http://localhost:5003/${endpoint}`)
    const result : apiGetAllEcosystems = await response.json();
   
    return result;
    
}

export async function handleApiNamed(endpoint : string) : Promise<apiNamedEcosystemModel> {
    console.log(`http://localhost:5003/${endpoint}`)
    const response : Response = await fetch(`http://localhost:5003/${endpoint}`)
    const result : apiNamedEcosystemModel = await response.json();
   
    return result;
}