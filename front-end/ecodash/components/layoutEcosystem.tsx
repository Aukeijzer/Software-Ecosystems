"use client"

import subEcosystemClass from "@/app/classes/subEcosystemClass"
import { ecosystemModel, subEcosystem } from "@/app/models/ecosystemModel"
import { useEffect, useState} from "react"
import useSWRMutation from 'swr/mutation'
import ListComponentSingle from "./listComponent"
import InfoCard from "./infoCard"
import languageClass from "@/app/classes/languageClass"
import GraphComponent from "./graphComponent"
import GridLayout from "./gridLayout"
import SpinnerComponent from "./spinner"
import risingClass from "@/app/classes/risingClass"
import { topTopicsGrowing, topTechnologyGrowing, topTechnologies, topicGrowthLine, topTechnology } from "@/mockData/mockAgriculture"
import GraphLine from "./graphLine"
import EcosystemDescription from "./ecosystemDescription"
import { languageModel } from "@/app/models/languageModel"
import { useRouter, useSearchParams } from 'next/navigation'
import { cardWrapper } from "@/app/models/cardWrapper"

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
            //Ugly ass 0. This should maybe not be hardcoded this does not feel safe
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
        const result : ecosystemModel = await fetch(url, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(apiPostBody)
        }).then(res => res.json())
        console.log(result);
        return result;
    }

 

    //Functions that convert a data model into a class and then into a card
    function dataToLanguageGraph(languages: languageModel[], title: string, x : number, y : number) : cardWrapper{
        //Convert to class / count percentage to calculate other
        var total = 0;
        var languageList : languageClass[] = [];
        for(var i = 0; i < languages.length; i++){
            languageList.push(new languageClass(languages[i].language,languages[i].percentage))
            total+= languages[i].percentage;
        }
        var rest = 100 - total;
        //push rest to language
        languageList.push(new languageClass("Other", rest))
        //Putting language data in card
        dataGraphLanguages = <GraphComponent items={languageList}/>
        cardGraph = <InfoCard title={title} data={dataGraphLanguages} />
        //Width / height are still fixed for now

        //this should be auto generated later
        const cardGraphWrapped : cardWrapper = {card: cardGraph, width:2, height:6, x: x, y: y, minH: 3, minW: 2, static:true}
        return cardGraphWrapped;
    }

    function dataToTopicList(topics: subEcosystem[], title: string, x : number, y : number) : cardWrapper {
        var subEcosystemList : subEcosystemClass[] = [];
        for(var i = 0; i < topics.length; i++){
            subEcosystemList.push(new subEcosystemClass(topics[i].topic, topics[i].projectCount));
        }//async (sub: string) => {await trigger({topics: sub, remove:false})}
        dataListTopic = <ListComponentSingle items={subEcosystemList} onClick={(sub : string) => onClickTopic(sub)}></ListComponentSingle>
        cardTopic = <InfoCard title={title} data={dataListTopic} />
        const cardTopicWrapped: cardWrapper = {card: cardTopic, width: 1, height: 4, x: x, y: y, minH: 2, static:true}
        return cardTopicWrapped;
    }

    function dataToTechnologyList(technologies: topTechnology[], title: string, x : number, y : number) : cardWrapper {
        var technology : subEcosystemClass[] = [];
        for(var i = 0; i < technologies.length; i++){
            technology.push(new subEcosystemClass(technologies[i].name, technologies[i].percentage))
        }
        const dataListTechnologies = <ListComponentSingle items={technology} onClick={async (sub: string) => {await trigger({topics: [sub], remove:false})}}/>
        const cardTechnologies = <InfoCard title={title} data={dataListTechnologies} alert="This is mock data" />
        const carrdTechnologiesWrapped : cardWrapper = {card: cardTechnologies, width: 2, height: 4, x: x, y: y, static:true}
        return carrdTechnologiesWrapped;
    }

    function dataToRisingList(risingItems: any, title: string, x : number, y : number, width : number) : cardWrapper {
        var risingClassItems : risingClass[] = [];
        for(var i = 0; i < risingItems.length; i++){
            risingClassItems.push(new risingClass(risingItems[i].name, risingItems[i].percentage, risingItems[i].growth))
        }
        const dataListRising = <ListComponentSingle items={risingClassItems} onClick={async (sub: string) => {await trigger({topics: [sub], remove:false})}} />
        const cardRising = <InfoCard title={title} data={dataListRising} alert="This is mock data" />
        const cardRisingWrapped : cardWrapper = {card: cardRising, width: width, height: 4, x: x, y : y, static:true}
        return cardRisingWrapped;
    }

    function dataToLineGraph(data: any, title: string, x: number, y : number) : cardWrapper{
        //For now the data is already in the correct format
        //TODO: add conversion of real data to correct format once we have real data...
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

    var dataListTopic;
    var cardTopic;
    var dataGraphLanguages;
    var cardGraph;
    var cardWrappedList : cardWrapper[] = []
    if(data){
        //Real data

        //Top 5 topics
        const cardTopicWrapped = dataToTopicList(data.subEcosystems, "Top 5 topics", 0, 2);
        cardWrappedList.push(cardTopicWrapped);

        //Top 5 languages
        const cardGraphWrapped = dataToLanguageGraph(data.topLanguages, "Top 5 languages", 0, 6);
        cardWrappedList.push(cardGraphWrapped);

        //Mock data
        //List of technologies
        const carrdTechnologiesWrapped = dataToTechnologyList(topTechnologies, "Top 5 technologies", 6, 2)
        cardWrappedList.push(carrdTechnologiesWrapped)

        //List of rising technologies
        const cardTechnologiesGrowingWrapped = dataToRisingList(topTechnologyGrowing, "Top 5 rising technologies", 2, 2, 2)
        cardWrappedList.push(cardTechnologiesGrowingWrapped)

        //List of rising topics
        const cardTopicsGrowingWrapped = dataToRisingList(topTopicsGrowing, "Top 5 rising topics", 1, 2, 1);
        cardWrappedList.push(cardTopicsGrowingWrapped)

        //Line graph topicsGrowing
        const cardLineGraphWrapped = dataToLineGraph(topicGrowthLine, "Top 5 topics over time", 2, 6);
        cardWrappedList.push(cardLineGraphWrapped)

        //Ecosystem description
        //Pull this out of the data field?
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