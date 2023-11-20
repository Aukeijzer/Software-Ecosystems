/*
apiHandler exports:

- handleApi returns a list of ecosystems given an api endpoint
    -Input = endpoint : string = endpoint for api on localhost:5003
    -outPut = Promise <EcosstemModel[]> = list of all ecosystems


- handleApiNamed returns a single ecoystem given its name / api endpoint
    -input = endpoint: string = endpoint for api on localhost:5003
    - outPut = Promise <ecosystem> = single ecosystem that belogns to the name

*/


import { ecosystemModel } from "@/app/interfaces/DTOs/ecosystemDTO";

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

//Handles API calls for /ecosystem/[name]/[subEcosystemName[]]
// post on /ecosystems
export async function apiCallSubEcosystem(ecosystem : string, domains : string[], numberOfTopLanguages: number, numberOfTopSubEcosystems: number, numberOfTopContributors: number ) : Promise<ecosystemModel> { 
    interface apiBodyInterface {
        topics: string[],
        numberOfTopLanguages: number,
        numberOfTopSubEcosystems: number,
        numberOfTopContributors: number
    }
    //Add ecosystem to the start of the array
    var topics : string[] = [ecosystem , ...domains]

    //Prepare post body
    const apiBody : apiBodyInterface = {topics : topics, 
                                        numberOfTopLanguages: numberOfTopLanguages,
                                        numberOfTopSubEcosystems: numberOfTopSubEcosystems,
                                        numberOfTopContributors: numberOfTopContributors,
                                        }
    //Convert body to JSON
    const apiBodyJson = JSON.stringify(apiBody);
    //Send POST request to /ecosystems
    const response: Response = await fetch(`http://localhost:5003/ecosystems`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: apiBodyJson
    })
    
    const result : ecosystemModel = await response.json();
    return result;
}