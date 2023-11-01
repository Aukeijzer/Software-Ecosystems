"use client"

import {useEffect, useState} from "react"
import { apiCallSubEcosystem } from "./apiHandler"
import { ecosystemModel } from "@/app/models/ecosystemModel"
import subEcosystem from "@/app/classes/subEcosystemClass"
import languageClass from "@/app/classes/languageClass"
import ListComponent from "./listComponent"
import InfoCard from "./infoCard"
import GridLayout from "./gridLayout"
import EcosystemDescription from "./ecosystemDescription"
import GraphComponent from "./graphComponent"

interface layoutEcosystemProps{
    ecosystem: string,
    url: string,
    subDomains?: string[],
}
export interface cardWrapper{
    card: JSX.Element,
    width: number,
    height: number,
    x: number,
    y: number,
    minW?: number,
    minH?: number,
    static?: boolean,
}

export default function LayoutEcosystem(props: layoutEcosystemProps) {
    const [subEcosystems, setSubEcosystems] = useState<subEcosystem[] | null>(null)
    const [languages, setLanguages] = useState<languageClass[] | null>(null)
    const [ecosystem, setEcosystem] = useState<string | null>(null)
    const [description, setDescription] = useState<string | null>(null)
    const [dataLoaded, setDataLoaded] = useState<boolean>(false)
    
    useEffect(() => {
        if(props.subDomains){
            apiCallSubEcosystem(props.ecosystem, props.subDomains, 5, 5, 5).then(data => transferData(data));
        } else {
            apiCallSubEcosystem(props.ecosystem, [], 5, 5, 5).then(data => transferData(data));
        }
    }, [])

    function transferData(data : ecosystemModel){
        var subEcosystemList : subEcosystem[] = [];

        //Instantiate subEcosystem class for each subEcosystem object
        for(var i = 0; i < data.subEcosystems.length; i++){
            subEcosystemList.push(new subEcosystem(data.subEcosystems[i].topic, data.subEcosystems[i].projectCount));
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


    if(dataLoaded) {
        //List of topics
        const dataListTopic = <ListComponent items={subEcosystems!} url={props.url}/>
        const cardTopic = <InfoCard title={"Top 5 topics"} data={dataListTopic} />
        const cardTopicWrapped: cardWrapper = {card: cardTopic, width: 1, height: 2, x: 1, y: 3, minH: 2}
    
        //List of languages
        const dataListLanguages = <ListComponent items={languages!} url={props.url}/>
        const cardLanguages = <InfoCard title={"Top 5 languages"} data={dataListLanguages} />
        const cardLanguagesWrapped : cardWrapper= {card: cardLanguages, width:1, height:2, x: 0, y: 1, minH: 2}
        
        //Graph of languages
        const dataGraphLanguages = <GraphComponent items={languages!}/>
        const cardGraph = <InfoCard title={"Graph: Top 5 languages"} data={dataGraphLanguages} />
        const cardGraphWrapped : cardWrapper = {card: cardGraph, width:2, height:3, x: 4, y: 1, minH: 3, minW: 2}

        //Description ecosystem information
        const ecosystemDescription =  <EcosystemDescription ecosystem={ecosystem? ecosystem : props.ecosystem}  description={description? description : "No description provided" }  subEcosystems={props.subDomains}/>
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
