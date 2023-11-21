"use client"

import { useEffect, useState} from "react"
import useSWRMutation from 'swr/mutation'
import GridLayout from "./gridLayout"
import SpinnerComponent from "./spinner"
import { buildLineGraphCard, buildListCard, buildPieGraphCard } from "@/app/utils/cardBuilder"
import { topTopicsGrowing, topTechnologyGrowing, topTechnologies, topicGrowthLine } from "@/mockData/mockAgriculture"
import EcosystemDescription from "./ecosystemDescription"
import { listLanguageDTOConverter } from "@/app/utils/Converters/languageConverter"
import { useRouter, useSearchParams } from 'next/navigation'
import { cardWrapper } from "@/app/interfaces/cardWrapper"
import listTechnologyDTOConverter from "@/app/utils/Converters/technologyConverter"
import { listRisingDTOConverter } from "@/app/utils/Converters/risingConverter"
import listSubEcosystemDTOConverter from "@/app/utils/Converters/subEcosystemConverter"
import { fetcherEcosystemByTopic } from "@/app/utils/apiFetcher"
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

    //Trigger = function to manually trigger fetcher function in SWR mutation. 
    //Data = data received from API. updates when trigger is called. causes update
    const { data, trigger, error, isMutating } = useSWRMutation(process.env.NEXT_PUBLIC_BACKEND_ADRESS + '/ecosystems', fetcherEcosystemByTopic)

    //Triggers upon page load once. Calls trigger function with no argument that calls api backend with selected ecosystem
    //Triggers twice in dev mode. Not once build tho / and npm run start. 
    
    useEffect(() => {
        //Check if URL has additional parameters
        const params = searchParams.get('topics');
    
        if(params){
            //Convert params to string list
            const topics = params.split(',')
            trigger({topics: [...selectedEcosystems, ...topics ]})
            setSelectedEcosystems([...selectedEcosystems, ...topics])
        } else {
            trigger({topics: selectedEcosystems})
        }

    },[]) 
    //[] means that it calls useEffect again if values inside [] are updated.
    //But since [] is empty it only is called upon page load

    //Function that gets trigger on Clicking on topic
    async function onClickTopic(topic: string){
        //Append topic to selected ecosystems
        await trigger({topics: [...selectedEcosystems, topic]});
        //Use shallow routing
        //Dont display the props.ecosystem 
        Router.push(`/?topics=${[...selectedEcosystems, topic].filter(n => n != props.ecosystem).toString()}`)
        //Update selected topics?
        setSelectedEcosystems([...selectedEcosystems, topic]);
        
    }

    //Function to remove sub-ecosystem from sub-ecosytem list
    async function removeSubEcosystem(subEcosystem: string){
        var updatedList = selectedEcosystems.filter(n => n != subEcosystem)
        await trigger({topics: updatedList})
        //Use shallow routing to update URL
        //Remove props.ecosystem
        setSelectedEcosystems(selectedEcosystems.filter(n => n != subEcosystem));
        Router.push(`/?topics=${updatedList.filter(n => n != props.ecosystem)}`)
        //Update selected topics?
        
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
   
        const subEcosystemCard = buildListCard(subEcosystems, onClickTopic, "Top 5 topics", 0, 2, 1, 4);
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
        const technologyCard = buildListCard(technologies, onClickTopic, "Top 5 technologies", 6, 2, 1, 4, "This is mock data");
        cardWrappedList.push(technologyCard)

        //List of rising technologies
        const risingTechnologies = listRisingDTOConverter(topTechnologyGrowing); 
        const risingTechnologiesCard = buildListCard(risingTechnologies, onClickTopic, "Top 5 rising technologies", 3, 2, 2, 4, "This is mock data");
        cardWrappedList.push(risingTechnologiesCard)

        //List of rising topics
        const risingTopics = listRisingDTOConverter(topTopicsGrowing);
        const risingTopicsCard = buildListCard(risingTopics, onClickTopic, "Top 5 rising topics", 1, 2, 2, 4, "This is mock data");
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