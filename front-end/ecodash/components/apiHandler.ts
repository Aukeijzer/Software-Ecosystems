import { apiGetAllEcosystems, apiNamedEcosystemModel } from "@/app/models/apiResponseModel";

export async function handleApi(endpoint : string) : Promise<apiGetAllEcosystems> {
    console.log(`http://localhost:5003/${endpoint}`)
    const https = require('https');
    
    const httpsAgent = new https.Agent({
        rejectUnauthorized: false,
      });

    const response : Response = await fetch(`http://localhost:5003/${endpoint}`, httpsAgent);
    const result : apiGetAllEcosystems = await response.json();
   
    return result;
}

export async function handleApiNamed(endpoint : string) : Promise<apiNamedEcosystemModel> {
    console.log(`http://localhost:5003/${endpoint}`)
    const response : Response = await fetch(`http://localhost:5003/${endpoint}`)
    const result : apiNamedEcosystemModel = await response.json();
   
    return result;
}