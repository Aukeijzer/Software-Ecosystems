/*
apiHandler exports:

- handleApi returns a list of ecosystems given an api endpoint
    -Input = endpoint : string = endpoint for api on localhost:5003
    -outPut = Promise <EcosstemModel[]> = list of all ecosystems


- handleApiNamed returns a single ecoystem given its name / api endpoint
    -input = endpoint: string = endpoint for api on localhost:5003
    - outPut = Promise <ecosystem> = single ecosystem that belogns to the name

*/


import { ecosystemModel } from "@/app/models/ecosystemModel";

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
//Handles API calls for /ecosystem/[name]/[subEcosystemName[]]
export async function apiCallSubEcosystem(ecosystem : string, domains : string[]) : Promise<ecosystemModel> {
    console.log("Fetching with string:   " + `http://localhost:5003/${ecosystem}/${domains.join(',')}`)
    const response: Response = await fetch(`http://localhost:5003/${ecosystem}/${domains.join(',')}`);
    const result : ecosystemModel = await response.json();

    return result;
}