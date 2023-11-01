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
// post on /ecosystems
export async function apiCallSubEcosystem(ecosystem : string, domains : string[], numberOfTopLanguages: number, numberOfTopSubEcosystems: number, numberOfTopContributors: number ) : Promise<ecosystemModel> {

    //Make post request with JSON object
        // Eerste in list is ecosystem
        // Verder list alle topics:
        // topics
        // Hoeveelheid top x : numberOfTopLanguages
        // numberOfTopSubEcosystems
        // numberOfTopContributors
    
    

    interface apiBodyInterface {
        topics: string[],
        numberOfTopLanguages: number,
        numberOfTopSubEcosystems: number,
        numberOfTopContributors: number
    }
    //Add ecosystem to the start of the array
    //domains.unshift(ecosystem);
    var topics : string[] = [ecosystem , ...domains]
    
    //console.log(domains);
    console.log("The domains are " + topics + " end")
    const apiBody : apiBodyInterface = {topics : topics, 
                                        numberOfTopLanguages: numberOfTopLanguages,
                                        numberOfTopSubEcosystems: numberOfTopSubEcosystems,
                                        numberOfTopContributors: numberOfTopContributors,
                                        }
    
    const apiBodyJson = JSON.stringify(apiBody);
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