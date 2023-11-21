import { ecosystemDTO } from "../interfaces/DTOs/ecosystemDTO";
export async function fetcherEcosystemByTopic(url: string, {arg }:{arg: {topics: string[]}}){
    //Variables for post body
    const numberOfTopContributors = 5;
    const numberOfTopLanguages = 5;
    const numberOfTopSubEcosystems = 5;

   var apiPostBody = { topics: arg.topics,
           numberOfTopLanguages: numberOfTopLanguages,
           numberOfTopSubEcosystems: numberOfTopSubEcosystems,
           numberOfTopContributors: numberOfTopContributors
    }
   
    //Make fetch call to url that returns promise
    //Resolve promise by awaiting 
    //Then convert result to JSON
    const result : ecosystemDTO = await fetch(url, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(apiPostBody)
    }).then(res => res.json())
    console.log(result);
    return result;
}

export async function fetcherHomePage(url: string){
    const result : ecosystemDTO[] = await fetch(url, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
        }
    }).then(res => res.json())
    return result;
}
