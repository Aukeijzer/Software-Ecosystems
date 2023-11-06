"use client"
import {useEffect, useState} from "react"
import languageClass from "@/app/classes/languageClass"
import subEcosystemClass from "@/app/classes/subEcosystemClass"
import { apiCallSubEcosystem } from "./apiHandler"
import { ecosystemModel } from "@/app/models/ecosystemModel"
import { cardWrapper } from "./layoutEcosystemPaged"
import GraphComponent from "./graphComponent"
import InfoCard from "./infoCard"
import EcosystemDescription from "./ecosystemDescription"
import GridLayout from "./gridLayout"
import ListComponentSingle from "./listComponent"
import { topTopicsGrowing, topTechnologyGrowing, topTechnologies, topicGrowthLine } from "@/mockData/mockAgriculture"
import risingClass from "@/app/classes/risingClass"
import GraphLine from "./graphLine"

interface layoutEcosystemSingleProps{
    ecosystem: string,
}

export default function LayoutEcosystem(props: layoutEcosystemSingleProps){
    //Ecosystem data
    const [subEcosystems, setSubEcosystems] = useState<subEcosystemClass[] | null>(null)
    const [languages, setLanguages] = useState<languageClass[] | null>(null)
    const [ecosystem, setEcosystem] = useState<string | null>(null)
    const [description, setDescription] = useState<string | null>(null)
    const [dataLoaded, setDataLoaded] = useState<boolean>(false)
    const [technologies, setTechnologies] = useState<subEcosystemClass[] | null>(null)
    const [subEcosystemsGrowing, setSubEcosystemsGrowing] = useState<risingClass[] | null>(null)
    const [technologiesGrowing, setTechnologiesGrowing] = useState<risingClass[] | null>(null)

    //Subecosystems
    const [selectedSubs, setSelectedSubs] = useState<string[]>([])

    useEffect(() => {
        console.log("use effect triggered");
        apiCallSubEcosystem(props.ecosystem, selectedSubs, 5,  5 ,5).then(data => transferData(data))
    }, [selectedSubs])

    function transferData(data : ecosystemModel){
        console.log(data);
        var subEcosystemList : subEcosystemClass[] = [];

        //Instantiate subEcosystem class for each subEcosystem object
        for(var i = 0; i < data.subEcosystems.length; i++){
            subEcosystemList.push(new subEcosystemClass(data.subEcosystems[i].topic, data.subEcosystems[i].projectCount));
        }

        //Instantiate language class for each language object
        var languageList : languageClass[] = [];
        for(var i = 0; i < data.topLanguages.length; i++){
            languageList.push(new languageClass(data.topLanguages[i].language, data.topLanguages[i].percentage))
        }

        //Instantiate rising class for each topic rising

        var topicGrowing : risingClass[] = [];
        for(var i = 0; i < topTopicsGrowing.length; i++){
            topicGrowing.push(new risingClass(topTopicsGrowing[i].name, topTopicsGrowing[i].percentage, topTopicsGrowing[i].growth))
        }
      
        setSubEcosystemsGrowing(topicGrowing);
        //Instantie rising class for each technology rising

        var technologyGrowing : risingClass[] = [];
        for(var i = 0; i < topTechnologyGrowing.length; i++){
            technologyGrowing.push(new risingClass(topTechnologyGrowing[i].name, topTechnologyGrowing[i].percentage, topTechnologyGrowing[i].growth))
        }
        setTechnologiesGrowing(technologyGrowing);

        //Instantiate technology class for each technology
        var technology : subEcosystemClass[] = [];
        for(var i = 0; i < topTechnologies.length; i++){
            technology.push(new subEcosystemClass(topTechnologies[i].name, topTechnologies[i].percentage))
        }
        setTechnologies(technology);

        if(data.displayName){
            setEcosystem(data.displayName)
        }

        if(data.description){
            setDescription(data.description)
        }

        setSubEcosystems(subEcosystemList);
        setLanguages(languageList);

        //Set data loaded as true
        setDataLoaded(true);
    }

    function onClickTopic(topic: string){
        //Set everything to null?
        setDescription(null);
        setSelectedSubs([...selectedSubs, topic]) 
        setDataLoaded(false)
        //This should cause a refresh causing the api call to go again with updated sub data
    }

    function onRemoveTopic(topic : string){
        //Set everything to null?
        //Remove topic from selectedSubs
        setSelectedSubs(selectedSubs.filter(n => n != topic))
        setDataLoaded(false)
    }

    if(dataLoaded) {
        //List of topics
        const dataListTopic = <ListComponentSingle items={subEcosystems!} onClick={onClickTopic}/>
        const cardTopic = <InfoCard title={"Top 5 topics"} data={dataListTopic} />
        const cardTopicWrapped: cardWrapper = {card: cardTopic, width: 1, height: 2, x: 2, y: 1, minH: 2}

        //List of languages
        const dataListLanguages = <ListComponentSingle items={languages!} onClick={onClickTopic}/>
        const cardLanguages = <InfoCard title={"Top 5 languages"} data={dataListLanguages} />
        const cardLanguagesWrapped : cardWrapper= {card: cardLanguages, width:1, height:2, x: 3, y: 1, minH: 2}

        //Graph of languages
        const dataGraphLanguages = <GraphComponent items={languages!}/>
        const cardGraph = <InfoCard title={"Graph: Top 5 languages"} data={dataGraphLanguages} />
        const cardGraphWrapped : cardWrapper = {card: cardGraph, width:2, height:3, x: 4, y: 1, minH: 3, minW: 2}

        //List of growing topics
        const dataListTopicsGrowing = <ListComponentSingle items={subEcosystemsGrowing!} onClick={onClickTopic}/>
        const cardTopicsGrowing = <InfoCard title={"Top 5 rising topics"} data={dataListTopicsGrowing} alert="This is mock data"/>
        const cardTopicsGrowingWrapped : cardWrapper = {card: cardTopicsGrowing, width: 2, height:2 , x: 0, y: 3}

        //List of technologies
        const dataListTechnologies = <ListComponentSingle items={technologies!} onClick={onClickTopic}/>
        const cardTechnologies = <InfoCard title={"Top 5 technologies"} data={dataListTechnologies} alert="This is mock data" />
        const carrdTechnologiesWrapped : cardWrapper = {card: cardTechnologies, width: 2, height: 2, x: 0, y: 0}

        //List of rising technologies
        const dataListTechnologiesGrowing = <ListComponentSingle items={technologiesGrowing!} onClick={onClickTopic} />
        const cardTechnologiesGrowing = <InfoCard title={"Top 5 rising technologies"} data={dataListTechnologiesGrowing} alert="This is mock data" />
        const cardTechnologiesGrowingWrapped: cardWrapper = {card: cardTechnologiesGrowing, width: 2, height: 2, x: 2, y : 2}
 
        //Description ecosystem information
        const ecosystemDescription =  <EcosystemDescription ecosystem={ecosystem? ecosystem : props.ecosystem}   removeTopic={onRemoveTopic} description={description? description : "No description provided" }  subEcosystems={selectedSubs} />
        const ecosystemDescriptionWrapped : cardWrapper = {card: ecosystemDescription, width: 6, height: 1, x: 0, y: 0, static:true}

        //Line graph topicsGrowing
        const lineGraphTopicsGrowing = <GraphLine items={topicGrowthLine} />
        const cardLineGraph = <InfoCard title={"Top 5 Topics over time"} data={lineGraphTopicsGrowing} alert="This is mock data"/>
        const cardLineGraphWrapped: cardWrapper = {card: cardLineGraph, x: 0, y : 5, width: 6, height: 3}

        //Add to card list
        var cards : cardWrapper[] = [];

        cards.push(cardTopicWrapped);
        cards.push(cardLanguagesWrapped);
        cards.push(ecosystemDescriptionWrapped);
        cards.push(cardGraphWrapped);
        cards.push(cardTopicsGrowingWrapped);
        cards.push(carrdTechnologiesWrapped);
        cards.push(cardTechnologiesGrowingWrapped);
        cards.push(cardLineGraphWrapped);

        return(
            <div>
                <GridLayout cards={cards} />
            </div>
        )
    } else {
        return(
            <div>
                loading...
            </div>
        )
    }
}