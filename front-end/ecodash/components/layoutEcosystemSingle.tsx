"use client"
import {useEffect, useState} from "react"
import languageClass from "@/app/classes/languageClass"
import subEcosystem from "@/app/classes/subEcosystemClass"
import subEcosystemSingle from "@/app/classes/subEcosystemClassSingle"
import { apiCallSubEcosystem } from "./apiHandler"
import { ecosystemModel } from "@/app/models/ecosystemModel"
import { cardWrapper } from "./layoutEcosystem"
import ListComponent from "./listComponent"
import GraphComponent from "./graphComponent"
import InfoCard from "./infoCard"
import EcosystemDescription from "./ecosystemDescription"
import GridLayout from "./gridLayout"
import ListComponentSingle from "./listComponentSingle"

interface layoutEcosystemSingleProps{
    ecosystem: string,
}

export default function layoutEcosystemSingle(props: layoutEcosystemSingleProps){
    //Ecosystem data
    const [subEcosystems, setSubEcosystems] = useState<subEcosystemSingle[] | null>(null)
    const [languages, setLanguages] = useState<languageClass[] | null>(null)
    const [ecosystem, setEcosystem] = useState<string | null>(null)
    const [description, setDescription] = useState<string | null>(null)
    const [dataLoaded, setDataLoaded] = useState<boolean>(false)


    //Subecosystems
    const [selectedSubs, setSelectedSubs] = useState<string[]>([])

    useEffect(() => {
        console.log("use effect triggered");
        apiCallSubEcosystem(props.ecosystem, selectedSubs, 5,  5 ,5).then(data => transferData(data))
    }, [selectedSubs])

    function transferData(data : ecosystemModel){
        console.log(data);
        var subEcosystemList : subEcosystemSingle[] = [];

        //Instantiate subEcosystem class for each subEcosystem object
        for(var i = 0; i < data.subEcosystems.length; i++){
            subEcosystemList.push(new subEcosystemSingle(data.subEcosystems[i].topic, data.subEcosystems[i].projectCount));
        }

        //Instantiate language class for each language object
        var languageList : languageClass[] = [];
        for(var i = 0; i < data.topLanguages.length; i++){
            languageList.push(new languageClass(data.topLanguages[i].language, data.topLanguages[i].percentage))
        }


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
        console.log("This onclick is working with sub = " + topic)
        //Append instead of hard setting if u want to be able to go deeper!

        //Set everything to null?
        setDescription(null);
        setSelectedSubs([...selectedSubs, topic]) 
        setDataLoaded(false)
        //This should cause a refresh causing the api call to go again with updated sub data
    }

    function onRemoveTopic(topic : string){
        console.log("this onclick is working with " + topic)
        
        //Set everything to null?
        //Remove topic from selectedSubs
        setSelectedSubs(selectedSubs.filter(n => n != topic))
        setDataLoaded(false)
    }


    if(dataLoaded) {
        //List of topics
        const dataListTopic = <ListComponentSingle items={subEcosystems!} onClick={onClickTopic}/>
        const cardTopic = <InfoCard title={"Top 5 topics"} data={dataListTopic} />
        const cardTopicWrapped: cardWrapper = {card: cardTopic, width: 1, height: 2, x: 1, y: 3, minH: 2}

        //List of languages
        const dataListLanguages = <ListComponentSingle items={languages!} onClick={onClickTopic}/>
        const cardLanguages = <InfoCard title={"Top 5 languages"} data={dataListLanguages} />
        const cardLanguagesWrapped : cardWrapper= {card: cardLanguages, width:1, height:2, x: 0, y: 1, minH: 2}

        //Graph of languages
        const dataGraphLanguages = <GraphComponent items={languages!}/>
        const cardGraph = <InfoCard title={"Graph: Top 5 languages"} data={dataGraphLanguages} />
        const cardGraphWrapped : cardWrapper = {card: cardGraph, width:2, height:3, x: 4, y: 1, minH: 3, minW: 2}

        //Description ecosystem information
        const ecosystemDescription =  <EcosystemDescription ecosystem={ecosystem? ecosystem : props.ecosystem}   removeTopic={onRemoveTopic} description={description? description : "No description provided" }  subEcosystems={selectedSubs} />
        const ecosystemDescriptionWrapped : cardWrapper = {card: ecosystemDescription, width: 6, height: 1, x: 0, y: 0, static:true}

        //Add to card list
        var cards : cardWrapper[] = [];

        cards.push(cardTopicWrapped);
        cards.push(cardLanguagesWrapped);
        cards.push(ecosystemDescriptionWrapped);
        cards.push(cardGraphWrapped);

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