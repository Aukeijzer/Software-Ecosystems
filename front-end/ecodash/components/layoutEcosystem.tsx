"use client"

import { ecosystemDTO} from "@/app/interfaces/DTOs/ecosystemDTO"
import { useEffect, useState} from "react"
import useSWRMutation from 'swr/mutation'
import ListComponentSingle from "./listComponent"
import InfoCard from "./infoCard"
import GraphComponent from "./graphComponent"
import GridLayout from "./gridLayout"
import SpinnerComponent from "./spinner"
import { topTopicsGrowing, topTechnologyGrowing, topTechnologies, topicGrowthLine } from "@/mockData/mockAgriculture"
import GraphLine from "./graphLine"
import EcosystemDescription from "./ecosystemDescription"
import { listLanguageDTOConverter } from "@/app/interfaces/DTOs/Converters/languageConverter"
import { useRouter, useSearchParams } from 'next/navigation'
import { cardWrapper } from "@/app/interfaces/cardWrapper"
import displayable from "@/app/classes/displayableClass"
import listTechnologyDTOConverter from "@/app/interfaces/DTOs/Converters/technologyConverter"
import { listRisingDTOConverter } from "@/app/interfaces/DTOs/Converters/risingConverter"
import listSubEcosystemDTOConverter from "@/app/interfaces/DTOs/Converters/subEcosystemConverter"

interface layoutEcosystemProps{
    ecosystem: string
}


export default function LayoutEcosystem(props: layoutEcosystemProps){
    //Set up router
    const Router = useRouter();

    //Set up search params
    const searchParams = useSearchParams();

    //Keep track of selected (sub)Ecosystems. start with ecosystem provided
    const [selectedEcosystems, setSelectedEcosystems] = useState<string[]>([props.ecosystem])

    //Variables for post body
    const numberOfTopContributors = 5;
    const numberOfTopLanguages = 5;
    const numberOfTopSubEcosystems = 5;
    
    //Trigger = function to manually trigger fetcher function in SWR mutation. 
    //Data = data received from API. updates when trigger is called. causes update
    const { data, trigger, error, isMutating } = useSWRMutation('http://localhost:5003/ecosystems', fetcherEcosystems)

    //Triggers upon page load once. Calls trigger function with no argument that calls api backend with selected ecosystem
    //Triggers twice in dev mode. Not once build tho / and npm run start. 
    
    useEffect(() => {
        //Check if URL has additional parameters
        const params = searchParams.get('topics');
       

        if(params){
            //Convert params to string list
            const topics = params.split(',')
            trigger({topics: topics, remove: false})
        } else {
            trigger({remove: false})
        }

    },[])

    //Fetcher function for SWR mutation. Receives URL to API and subEcosystem clicked.
    async function fetcherEcosystems(url: string, {arg }:{arg: {topics?: string[], remove: boolean}}){

        //First check if removing a subEcosystem -> topic is always provided
        //Then if no removal check if topic provided -> new sub ecosystem to subecosystemList
        //If no topic -> inital API call
        var apiPostBody;
        if(arg.remove){
            //1st (0 index) item of topics is the to be removed topic
            //Body for removal of topic
            apiPostBody = { topics: selectedEcosystems.filter(n => n!= arg.topics![0]),
                numberOfTopLanguages: numberOfTopLanguages,
                numberOfTopSubEcosystems: numberOfTopSubEcosystems,
                numberOfTopContributors: numberOfTopContributors
            }
            setSelectedEcosystems(selectedEcosystems.filter(n => n!= arg.topics![0]))

        } else if(arg.topics){
            //Body for adding topic
            apiPostBody = { topics: [...selectedEcosystems, ...arg.topics],
                numberOfTopLanguages: numberOfTopLanguages,
                numberOfTopSubEcosystems: numberOfTopSubEcosystems,
                numberOfTopContributors: numberOfTopContributors
            }
            setSelectedEcosystems([...selectedEcosystems, ...arg.topics]);
        } else { 
            //Body for initial API call
            apiPostBody = { topics: selectedEcosystems,
                numberOfTopLanguages: numberOfTopLanguages,
                numberOfTopSubEcosystems: numberOfTopSubEcosystems,
                numberOfTopContributors: numberOfTopContributors
            }
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

    function buildPieGraphCard(topics: displayable[], title: string, x : number, y : number) : cardWrapper{
        var graphComponent = <GraphComponent items={topics}/>;
        var cardGraph = <InfoCard title={title} data={graphComponent} />
        //TODO: (min)Width / (min)height should be automatically detected here
        var width = 2;
        var height = 6;
        var cardGraphWrapped : cardWrapper = {card : cardGraph, width: width, height: height, x : x, y : y, static: false}
        return cardGraphWrapped;
    }

    function buildListCard(topics: displayable[], title: string, x : number, y : number, width: number, height: number, alert?: string){
        //Make list element
        var listComponent = <ListComponentSingle items={topics} onClick={(sub : string) => onClickTopic(sub)}/>
        //Make card element
        var cardList = <InfoCard title={title} data={listComponent} alert={alert}/>
        //Wrap card
        //TODO: (min)Width / (min)height should be automatically detected here
        const cardListWrapped: cardWrapper = {card: cardList, width: width, height: height, x: x, y: y, minH: 2, static:true}
        return cardListWrapped
    }

    function buildLineGraphCard(data: any, title: string, x: number, y : number) : cardWrapper{
        const lineGraphTopicsGrowing = <GraphLine items={data} />
        const cardLineGraph = <InfoCard title={title} data={lineGraphTopicsGrowing} alert="This is mock data"/>
        const cardLineGraphWrapped: cardWrapper = {card: cardLineGraph, x: x, y : y, width: 4, height: 6, static:true}
        return cardLineGraphWrapped;
    }

    //Function that gets trigger on Clicking on topic
    async function onClickTopic(topic: string){
        await trigger({topics: [topic], remove:false});
        //Use shallow routing
        Router.push(`/?topics=${[...selectedEcosystems, topic].filter(n => n != props.ecosystem).toString()}`)
        
    }

    //Function to remove sub-ecosystem from sub-ecosytem list
    async function removeSubEcosystem(subEcosystem: string){
        await trigger({topics: [subEcosystem], remove: true})
        //Use shallow routing to update URL
        Router.push(`/?topics=${selectedEcosystems.filter(n => n != props.ecosystem).filter(n => n!= subEcosystem)}`)
    }

    //If error we display error message
    if(error){
        return(
            <p>
                Error making API call:
                {error}
            </p>
        )
    }

    //Prepare variables before we have data so we can render before data
    var cardWrappedList : cardWrapper[] = []
    if(data){
        //Real data

        //Top 5 topics
        //First Convert DTO's to Classes
        const subEcosystems = listSubEcosystemDTOConverter(data.subEcosystems);
        //Make list element
   
        const subEcosystemCard = buildListCard(subEcosystems, "Top 5 topics", 0, 2, 1, 4);
        //Add card to list
        cardWrappedList.push(subEcosystemCard);

        //Top 5 languages
        const languages = listLanguageDTOConverter(data.topLanguages);
        //Make graph card
        const languageCard = buildPieGraphCard(languages, "Top 5 languages", 0, 6);
        //Add card to list
        cardWrappedList.push(languageCard);

        //Mock data
        //List of technologies
        const technologies = listTechnologyDTOConverter(topTechnologies)
        const technologyCard = buildListCard(technologies, "Top 5 technologies", 6, 2, 1, 4, "This is mock data");
        cardWrappedList.push(technologyCard)

        //List of rising technologies
        const risingTechnologies = listRisingDTOConverter(topTechnologyGrowing); 
        const risingTechnologiesCard = buildListCard(risingTechnologies, "Top 5 rising technologies", 3, 2, 2, 4, "This is mock data");
        cardWrappedList.push(risingTechnologiesCard)

        //List of rising topics
        const risingTopics = listRisingDTOConverter(topTopicsGrowing);
        const risingTopicsCard = buildListCard(risingTopics, "Top 5 rising topics", 1, 2, 2, 4, "This is mock data");
        cardWrappedList.push(risingTopicsCard)

        //Line graph topicsGrowing 
        //For now no data conversion needed as Mock data is already in correct format 
        //When working with real data there should be a conversion from DTO to dataLineGraphModel
        const cardLineGraphWrapped = buildLineGraphCard(topicGrowthLine, "Top 5 topics over time", 2, 6);
        cardWrappedList.push(cardLineGraphWrapped)

        //Ecosystem description
        //Remove main ecosystem from selected sub-ecosystem list to display
        const ecosystemDescription =  <EcosystemDescription ecosystem={props.ecosystem}   removeTopic={removeSubEcosystem} description={data.description ? data.description : "no description provided"}  subEcosystems={selectedEcosystems.filter(n => n!= props.ecosystem)} />
        const ecosystemDescriptionWrapped : cardWrapper = {card: ecosystemDescription, width: 6, height: 2, x: 0, y: 0, static:true}
        cardWrappedList.push(ecosystemDescriptionWrapped)
    
    } else {
        //When no data display spinner
        return(
            <div>
                <SpinnerComponent />
            </div>
        )
    }

    //Normal render (No error)
    return(
        <div>
            <GridLayout cards={cardWrappedList} />
        </div>      
    )
}