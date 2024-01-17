
"use client"

import { useEffect, useState} from "react"
import useSWRMutation from 'swr/mutation'
import GridLayout from "./gridLayout"
import SpinnerComponent from "./spinner"
import { buildLineGraphCard, buildListCard, buildPieGraphCard, buildTableCard } from "@/app/utils/cardBuilder"
import { topTopicsGrowing, topTechnologyGrowing, topTechnologies, topicGrowthLine } from "@/mockData/mockAgriculture"
import EcosystemDescription from "./ecosystemDescription"
import  listLanguageDTOConverter  from "@/app/utils/Converters/languageConverter"
import { useRouter, useSearchParams } from 'next/navigation'
import { cardWrapper } from "@/app/interfaces/cardWrapper"
import listTechnologyDTOConverter from "@/app/utils/Converters/technologyConverter"
import  listRisingDTOConverter  from "@/app/utils/Converters/risingConverter"
import listSubEcosystemDTOConverter from "@/app/utils/Converters/subEcosystemConverter"
import { fetcherEcosystemByTopic } from "@/app/utils/apiFetcher"
import listContributorDTOConverter from "@/app/utils/Converters/contributorConverter"
import Filters from "./filters"
import test from "node:test"
import SmallDataBox from "./smallDataBox"
interface layoutEcosystemProps{
    ecosystem: string
}

/**
 * 
 * Renders the layout for the ecosystem page.
 * 
 * @component
 * @param {string} props.ecosystem - The name of the ecosystem.
 * @returns {JSX.Element} The rendered layout component.
 *  The LayoutEcosystem component accepts a single prop: ecosystem, 
 *  which is a string representing the name of the ecosystem.
 *
 *   The useRouter hook is used to get the Router object, 
 *   which is used to navigate between pages. 
 *   The useSearchParams hook is used to get the searchParams object, 
 *   which is used to access the search parameters in the current URL.
 *
 *   The useState hook from React is used to create a state variable selectedEcosystems and its setter function setSelectedEcosystems. 
 *   The initial value of selectedEcosystems is an array containing the ecosystem prop. 
 *   This state variable is used to keep track of the selected ecosystems.
 *
 *   The useSWRMutation hook is used to fetch data from the /ecosystems endpoint of the API. 
 *   The data, trigger, error, and isMutating variables are destructured from the hook. 
 *   The trigger function is used to manually fetch the data, 
 *   data holds the fetched data, error holds any error that occurred during fetching, 
 *   and isMutating indicates whether a fetch is in progress.
 *
 *   The useEffect hook is used to call the trigger function when the component mounts, 
 *   causing the data to be fetched from the API. 
 *   If the URL has additional parameters, these are added to the selectedEcosystems state 
 *   and the trigger function is called with these parameters. If there are no additional parameters, 
 *   the trigger function is called with the current selectedEcosystems state.
 */

export default function LayoutEcosystem(props: layoutEcosystemProps){
    //Set up router
    const Router = useRouter();

    //Set up search params
    const searchParams = useSearchParams();

    //Keep track of selected (sub)Ecosystems. start with ecosystem provided
    interface SelectedItems {
        ecosystems: string[];
        technologies: string[];
        languages: string[];
        [key: string]: string[]; // Add index signature
    }

    class filters {
        ecosystems: string[];
        technologies: string[];
        languages: string[];
        constructor(ecosystems: string[], technologies: string[], languages: string[]){
            this.ecosystems = ecosystems;
            this.technologies = technologies;
            this.languages = languages;
        }
    }

    const [selectedItems, setSelectedItems] = useState<filters>({
        ecosystems: [props.ecosystem],
        technologies: [],
        languages: [],
    });


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
            trigger({topics: [...selectedItems.ecosystems, ...topics ]})
            setSelectedItems({ecosystems: [...selectedItems.ecosystems, ...topics ] , technologies: selectedItems.technologies, languages: selectedItems.languages})
        } else {
            trigger({topics: selectedItems.ecosystems})
        }

    },[]) 
    //[] means that it calls useEffect again if values inside [] are updated.
    //But since [] is empty it only is called upon page load

   
    /**
     * Handles the click event for applying a filter. Everything in a table is clickable
     * @param filter - The filter to be applied.
     * @param filterType - The type of filter to be applied. Indexes the selectedItems object.
     * @returns A Promise that resolves when the filter is applied.
     */
    async function onClickFilter(filter: string, filterType: string){
        await trigger({topics: [...selectedItems.ecosystems, filter]});
        setSelectedItems(prevState => ({
            ...prevState,
            [filterType]: [...prevState[filterType as keyof typeof selectedItems], filter]
        }));
    }

    /**
     * Removes a filter from the selected items.
     * @param filter - The filter to be removed.
     * @param filterType - The type of filter to be removed. Indexes the selectedItems object.
     * @returns A Promise that resolves when the filter has been removed.
     */
    async function removeFilter(filter: string, filterType: string){
        await trigger({topics: selectedItems.ecosystems.filter(n => n != filter)});
        setSelectedItems(prevState => ({
            ...prevState,
            [filterType]: [...prevState[filterType as keyof typeof selectedItems].filter(n => n != filter)]
        }));
    }
    //If error we display error message
    if(error){
        return(
            <>
                
                <h2 className='text-3xl m-5'>Something went wrong!</h2>
                <div className='flex flex-col gap-3 p-5 bg-gray-300 border-2 m-5 border-gray-900 rounded-sm'>
                    <p>
                        {error.message}
                    </p>
                    <p>
                        {error.stack}
                    </p>
                   
                </div>
            </>
        )
    }
  

    //Prepare variables before we have data so we can render before data
    var cardWrappedList : cardWrapper[] = []
    if(data){
        //Real data
        const COLORS = ["#f2c4d8", "#f9d4bb", "#f8e3a1", "#c9e4ca", "#a1d9e8", "#c6c8e7", "#f0c4de", "#d8d8d8"];

        //Top 5 topics
        //First Convert DTO's to Classes
        const subEcosystems = listSubEcosystemDTOConverter(data.subEcosystems);
        //Make list element
        const subEcosystemCard = buildTableCard(subEcosystems, "", 0, 4, 2, 5, onClickFilter, "ecosystems", "", COLORS[0]);
        cardWrappedList.push(subEcosystemCard);

        //Top 5 contributors
        const contributors = listContributorDTOConverter(data.topContributors);
        const contributorCard = buildTableCard(contributors, "", 0, 15, 2, 5, onClickFilter, "contributor" , "", COLORS[1]);
        cardWrappedList.push(contributorCard);

        //Top 5 languages
        const languages = listLanguageDTOConverter(data.topLanguages);
        //Make graph card
        const languageCard = buildPieGraphCard(languages, "", 0, 9, COLORS[1]);
        //Add card to list
        cardWrappedList.push(languageCard);

        //Mock data
        //List of technologies
        const technologies = listTechnologyDTOConverter(topTechnologies)
        const technologyCard = buildTableCard(technologies, "", 4, 4, 2, 5, onClickFilter, "languages", "", COLORS[2]);
        cardWrappedList.push(technologyCard)

        //List of rising technologies
        const risingTechnologies = listRisingDTOConverter(topTechnologyGrowing); 
        const risingTechnologiesCard = buildTableCard(risingTechnologies, "", 3, 15, 3, 5, onClickFilter, "technologies", "", COLORS[3]);
        cardWrappedList.push(risingTechnologiesCard)

        //List of rising topics
        const risingTopics = listRisingDTOConverter(topTopicsGrowing);
        const risingTopicsCard = buildTableCard(risingTopics, "", 2, 4, 2, 5, onClickFilter, "ecosystems", "", COLORS[4]);
        cardWrappedList.push(risingTopicsCard)

        //Line graph topicsGrowing 
        //For now no data conversion needed as Mock data is already in correct format 
        //When working with real data there should be a conversion from DTO to dataLineGraphModel
        const cardLineGraphWrapped = buildLineGraphCard(topicGrowthLine, "", 6, 9, false, COLORS[5]);
        cardWrappedList.push(cardLineGraphWrapped)

        //Ecosystem description
        //Remove main ecosystem from selected sub-ecosystem list to display
        const ecosystemDescription =  <EcosystemDescription ecosystem={props.ecosystem}  description={data.description ? data.description : "no description provided"} />
        const ecosystemDescriptionWrapped : cardWrapper = {card: ecosystemDescription, width: 6, height: 2, x: 0, y: 0, static:true}
        cardWrappedList.push(ecosystemDescriptionWrapped)

        //Filters
        const filters = <Filters technologies={selectedItems.technologies} subEcosystems={selectedItems.ecosystems.filter(n => n != props.ecosystem)} languages={selectedItems.languages} removeFilter={removeFilter}/>
        const filtersWrapped : cardWrapper = {card: filters, width: 10, height: 0.5, x: 4, y: 3.3, static:true}
        cardWrappedList.push(filtersWrapped)

        //Small data boxes  
        const smallBoxes = ( <div className="flex flex-row   mb-5 justify-around">

            <SmallDataBox item={"Topics"} count={data.allTopics} increase={5}  />
            <SmallDataBox item={"Projects"} count={data.allProjects} increase={5} />
            <SmallDataBox item={"Contributors"} count={data.allContributors} increase={5} />
            <SmallDataBox item={"Contributions"} count={data.allContributions} increase={5} />
        </div>)
        const smallBoxesWrapped : cardWrapper = {card: smallBoxes, width: 10, height: 1, x: 4, y: 2, static:true}
        cardWrappedList.push(smallBoxesWrapped)

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
        <div className="ml-44 mr-44">
            <GridLayout cards={cardWrappedList} />
        </div>      
    )
}