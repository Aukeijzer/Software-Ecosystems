import { ecosystemModel } from "@/app/models/apiResponseModel";

//Handles API call for /ecosystems
export async function handleApi(endpoint : string) : Promise<ecosystemModel[]> {
    console.log(`http://localhost:5003/${endpoint}`)
    const https = require('https');
    
    const httpsAgent = new https.Agent({
        rejectUnauthorized: false,
      });

    const response : Response = await fetch(`http://localhost:5003/${endpoint}`, httpsAgent);
    const result : ecosystemModel[] = await response.json();
   
    return result;
}

//Handles API calls for /ecosystem/[name]
export async function handleApiNamed(endpoint : string) : Promise<ecosystemModel> {

    const response : Response = await fetch(`http://localhost:5003/${endpoint}`)
    const result : ecosystemModel = await response.json();
   
    return result;
}