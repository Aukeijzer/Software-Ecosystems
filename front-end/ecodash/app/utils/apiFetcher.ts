import { ecosystemDTO } from "../interfaces/DTOs/ecosystemDTO";

/**
 * Fetches ecosystem data by topic from the specified URL.
 * @param url - The URL to fetch the data from.
 * @param arg - An object containing the topics array.
 * @param arg.topics - An array of topics to filter the ecosystem data.
 * @returns A promise that resolves to the fetched ecosystem data.
 */
export async function fetcherEcosystemByTopic(url: string, {arg }:{arg: {topics: string[], technologies: string[]}}){
    //Variables for post body
    const numberOfTopContributors = 5;
    const numberOfTopLanguages = 5;
    const numberOfTopSubEcosystems = 5;

   var apiPostBody = {
           topics: arg.topics,
           technologies: arg.technologies,
           numberOfTopLanguages: numberOfTopLanguages,
           numberOfTopSubEcosystems: numberOfTopSubEcosystems,
           numberOfTopContributors: numberOfTopContributors,
           numberOfTopTechnologies: 5,
           numberOfTopProjects: 5,
   }
    //Make fetch call to url that returns promise
    //Resolve promise by awaiting 
    //Then convert result to JSON
    const response : Response = await fetch(url, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(apiPostBody)
    })

    if (response.status === 500) {
        throw new Error(response.statusText)
       
    }
    const ecosystemData  = await response.json();


    console.log(ecosystemData);

     
    return ecosystemData;
}

/**
 * Fetches homepage ecosystem data from the specified URL.
 * @param url - The URL to fetch the data from.
 * @returns A promise that resolves to the fetched homepage ecosystem data.
 */
export async function fetcherHomePage(url: string){
    const result : ecosystemDTO[] = await fetch(url, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
        }
    }).then(res => res.json())
    return result;
}
