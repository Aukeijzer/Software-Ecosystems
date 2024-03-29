/*
Copyright (C) <2024> <OdinDash>

This file is part of SECODash

SECODash is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published
by the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

SECODash is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License for more details.

You should have received a copy of the GNU Affero General Public License
along with SECODash.  If not, see <https://www.gnu.org/licenses/>.

*/

"use client"

import { useEffect, useState} from "react"
import useSWRMutation from 'swr/mutation'
import SpinnerComponent from "./spinner"
import EcosystemDescription from "./ecosystemDescription"
import  listLanguageDTOConverter  from "@/utils/Converters/languageConverter"
import { useRouter, useSearchParams } from 'next/navigation'
import listSubEcosystemDTOConverter from "@/utils/Converters/subEcosystemConverter"
import { fetcherEcosystemByTopic } from "@/utils/apiFetcher"
import listContributorDTOConverter from "@/utils/Converters/contributorConverter"
import Filters from "./filters"
import SmallDataBox from "./smallDataBox"
import TopicSearch from "./topicSearch"
import InfoCard from "./infoCard"
import TableComponent from "./tableComponent"
import GraphComponent from "./graphComponent"
import GraphLine from "./graphLine"
import { colors } from "@/enums/filterColor"
import { useSession} from "next-auth/react"
import { ExtendedUser } from "@/utils/authOptions"
import Button from "./button"
import { lineData } from "@/interfaces/lineData"
import listprojectDTOConverter from "@/utils/Converters/projectConverter"
import { convertTimedData, getLabels, timedDataDTO } from "@/utils/Converters/timedDataConverter"

var abbreviate = require('number-abbreviate');

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
    //Set up session
    const { data: session } = useSession()
    const user = session?.user as ExtendedUser;

    //Set up search params
    const searchParams = useSearchParams();

    //Keep track of selected filters / (sub)Ecosystems. start with ecosystem provided
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

    //Keeps track of edit mode
    const [editMode, setEditMode] = useState<boolean>(false);
    const [description, setDescription] = useState<string>('');


    
     
    //Trigger = function to manually trigger fetcher function in SWR mutation. 
    //Data = data received from API. updates when trigger is called. causes update
    const { data, trigger, error, isMutating } = useSWRMutation('/api/ecosystemPagePost', fetcherEcosystemByTopic)

    //Triggers upon page load once. Calls trigger function with no argument that calls api backend with selected ecosystem
    //Triggers twice in dev mode.
    
    useEffect(() => {
        //Check if URL has additional parameters
        const topicParams = searchParams.get('topics');
        const techParams = searchParams.get('technologies');
        const languageParams = searchParams.get('languages');

        
        if(topicParams || techParams || languageParams){
            //Convert params to string list
            var topics : string[] = [] ;
            if(topicParams){
                topics = topicParams!.split(',');

            }
            var technologies : string[] = [];
            if(techParams){
                technologies = techParams!.split(',');
            }

            var languages : string[] = [];
            if(languageParams){
                languages = languageParams!.split(',');
            }

            trigger({topics: [...selectedItems.ecosystems, ...topics ], 
                technologies:  [...selectedItems.technologies, ...technologies ],
                languages: [...selectedItems.languages, ...languages]}
            )
            setSelectedItems({ecosystems: [...selectedItems.ecosystems, ...topics ] , 
                technologies: [...selectedItems.technologies, ...technologies], 
                languages: [...selectedItems.languages, ...languages]}
            )
        } else {
            trigger({topics: selectedItems.ecosystems, technologies: selectedItems.technologies, languages: selectedItems.languages},)
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
        await trigger({topics: [...selectedItems.ecosystems, filter], technologies: selectedItems.technologies, languages: selectedItems.languages});
        setSelectedItems(prevState => ({
            ...prevState,
            [filterType]: [...prevState[filterType as keyof typeof selectedItems], filter]
        }));
        //Prepare technology url
        var techUrl = "";
        if(filterType == "technologies" ){
            if(selectedItems.technologies.length > 1){
                techUrl ="&technologies=" + selectedItems.technologies.join(",")  + ',' + filter ;
            } else {
                techUrl ="&technologies=" + filter ;
            }
        } else if(selectedItems.technologies.length > 0) {
            techUrl ="&technologies=" + selectedItems.technologies.join(",")
        }
    
        //Prepare topic url
        var topicUrl = "";
        if(filterType == "ecosystems"){
            if(selectedItems.ecosystems.length > 1){
                topicUrl ="&topics=" + selectedItems.ecosystems.filter(n => n != props.ecosystem).join(",") + ',' + filter
            } else {
                topicUrl ="&topics=" + filter
            }
        } else if(selectedItems.ecosystems.length > 1) {
            topicUrl ="&topics=" + selectedItems.ecosystems.filter(n => n != props.ecosystem).join(",")
        }
        //Prepare language url
        var languageUrl = "";
        if(filterType == "languages"){
            if(selectedItems.languages.length > 1){
                languageUrl ="&languages=" + selectedItems.languages.join(",") + ',' + filter
            } else {
                languageUrl ="&languages=" + filter
            }
        } else if(selectedItems.languages.length > 0) {
            languageUrl ="&languages=" + selectedItems.languages.join(",")
        }

        console.log(topicUrl);
    
        if(techUrl != "" || topicUrl != "" || languageUrl != "") {
            Router.push(`?${topicUrl}${techUrl}${languageUrl}`, { scroll: false})
        }
    }

    /**
     * Removes a filter from the selected items.
     * @param filter - The filter to be removed.
     * @param filterType - The type of filter to be removed. Indexes the selectedItems object.
     * @returns A Promise that resolves when the filter has been removed.
     */
    async function removeFilter(filter: string, filterType: string){
        //Solve trigger function
        await trigger({topics: selectedItems.ecosystems.filter(n => n != filter), technologies: selectedItems.technologies, languages: selectedItems.languages});
        setSelectedItems(prevState => ({
            ...prevState,
            [filterType]: [...prevState[filterType as keyof typeof selectedItems].filter(n => n != filter)]
        }));   

        //Prepare technology url
        var techUrl = "";
        if(filterType == "technologies" && selectedItems.technologies.length > 1){
            techUrl ="&technologies=" + selectedItems.technologies.filter(n => n != filter).join(",")
        }  else if(filterType != "technologies" && selectedItems.technologies.length > 0) {
            techUrl ="&technologies=" + selectedItems.technologies.join(",")
        }
        //Prepare topic url
        var topicUrl = "";
        if(filterType == "ecosystems" && selectedItems.ecosystems.length > 2){
            topicUrl ="&topics=" + selectedItems.ecosystems.filter(n => n != props.ecosystem).filter(n => n != filter).join(",")
            topicUrl = "&topics=" + selectedItems.ecosystems.filter(n => n != props.ecosystem).join(",")
        } 
        //Prepare language url
        var languageUrl = "";
        if(filterType == "languages" && selectedItems.languages.length > 1){
            languageUrl ="&languages=" + selectedItems.languages.filter(n => n != filter).join(",") 
        } else if(filterType != "languages" && selectedItems.languages.length > 0) {
            techUrl ="&languages=" + selectedItems.languages.join(",")
        }

        Router.push(`?${topicUrl}${techUrl}${languageUrl}`, { scroll: false})
    }

    async function saveDescription(){
        //Prepare post body with description and selected ecosystems
        var apiPostBody = {
            description: description,
            ecosystem: props.ecosystem
        }

        //Send to node backend,
        const response : Response = await fetch("/api/saveEdit", {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(apiPostBody)
        })

        //Check if response is ok, if not throw error 500
        if(response.ok){
            if(response.status === 200){
                alert("Description updated");
            } else {
                alert("Error updating description");
            }
        } else {
            throw new Error("Error in response");
        }
    }

    function changeDescription(description: string){
        console.log(description);
        setDescription(description);
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
   
    //Prepare variables before we have data so we can render before data is gathered
    var cardList  = []
    if(data){
        if(user !== undefined && user !== null && (user.userType === "Admin" || user.userType === "RootAdmin") && user.ecosystems.includes(props.ecosystem)) {
            const ecosystemEdit = (
                <div className="rounded-sm  p-3 text-yellow-700 bg-red-200 col-span-full">
                    <form className="flex flex-col">
                        <div className="flex flex-row gap-3">
                            <label> edit mode:</label>
                            <input type="checkbox" name="editMode" className="mt-1" onChange={() => setEditMode(!editMode)}/> 
                        </div>
                    </form>
                    {editMode && 
                        <Button text='Save changes' onClick={() => saveDescription()} />
                    }

                </div>) 
            cardList.push(ecosystemEdit)
        }
        
         //Topic search box
        const topicSearch = <div className="col-span-1 lg:col-start-3">
            <TopicSearch selectTopic={onClickFilter} />
        </div>
        
        const ecosystemDescription =  <div className="col-span-full">
            <EcosystemDescription ecosystem={props.ecosystem}  
            description={description ? description : data.description}
            editMode={editMode}
            changeDescription={changeDescription} 
            >
                {topicSearch}
            </EcosystemDescription>
        </div>
        cardList.push(ecosystemDescription)
        
        //Small data boxes  
        const smallBoxes = (
                 <div className="grid w-full grid-cols-2 md:grid-cols-2 lg:grid-cols-4">
                    <SmallDataBox item={"Sub-ecosystems"} count={abbreviate(data.numberOfTopics)} increase={5}  />
                    <SmallDataBox item={"Projects"} count={abbreviate(data.numberOfProjects)} increase={5} />
                    <SmallDataBox item={"Contributors"} count={abbreviate(data.numberOfContributors)} increase={5} />
                    <SmallDataBox item={"Contributions"} count={abbreviate(data.numberOfContributions)} increase={5} />
                </div>
           )
        const smallBoxesCard = <div className="col-span-full">
            {smallBoxes}
        </div>
        cardList.push(smallBoxesCard)
        
        //Filters
        const filters = <div className="col-span-full">
            <Filters technologies={selectedItems.technologies} 
                     subEcosystems={selectedItems.ecosystems.filter(n => n != props.ecosystem)} 
                     languages={selectedItems.languages} 
                     removeFilter={removeFilter}
            />
        </div>
        cardList.push(filters)
        

        //Check if there is enough data to display
        if(data.topSubEcosystems.length > 0 || data.topContributors.length > 0){
            //Top 5 topics
        //First Convert DTO's to Classes
        const subEcosystems = listSubEcosystemDTOConverter(data.topSubEcosystems);
        //Make table element
        var subEcosystemComponent = <TableComponent items={subEcosystems} onClick={(sub : string) => onClickFilter(sub, "ecosystems")}/>
        //Make card element
        var subEcosystemCard = <div>
                <InfoCard title={""} data={subEcosystemComponent} Color={colors.topic}/>
        </div>
        cardList.push(subEcosystemCard)
        
        //Top 5 contributors
        const contributors = listContributorDTOConverter(data.topContributors);
        var contributorTable = <TableComponent items={contributors} onClick={(contributor : string) => (console.log(contributor))}/>
        var contributorCard = <div>
                <InfoCard title={""} data={contributorTable} Color={colors.contributor}/>
        </div>
        cardList.push(contributorCard);
      
        //Top 5 languages
        const languages = listLanguageDTOConverter(data.topLanguages);
        //Make graph card
        const languageGraph = <GraphComponent items={languages} onClick={(language : string) => onClickFilter(language, "languages")}/>
        var languageCard = <div>
                <InfoCard title={""} data={languageGraph} Color={colors.language}/>
        </div>
        //Add card to list
        cardList.push(languageCard);
        
        //Line graph topicsGrowing 
        var topicsGrowing = convertTimedData(data.topicsActivityTimeSeries);
        console.log(topicsGrowing);
        var topicLabels = getLabels(data.topicsActivityTimeSeries);

        //Timed data graph sub-topics
        const lineGraphTopicsGrowing = <GraphLine items={topicsGrowing} labels={topicLabels}/>
        const cardLineGraph = <div className="col-span-full h-[600px] flex flex-col">
            <InfoCard title={"Sub-ecosystem activity"} data={lineGraphTopicsGrowing} Color={colors.topic} >
                <div className="bg-white justify-center flex text-sm text-gray-900">
                  Number of active projects of the most popular sub-topics over time
                </div>
            </InfoCard>
           
        </div>
        cardList.push(cardLineGraph)

      

        //List of projects
        const projects = listprojectDTOConverter(data.topProjects);
        const projectTable = <TableComponent items={projects} onClick={(project : string) => (console.log(project))}/>
        const projectCard = <div className="col-span-full">
            <InfoCard title={""} data={projectTable} Color={colors.project}/>
        </div>
        cardList.push(projectCard)
       
        //Timed data graph ecosystem
        var ecosystemGrowing = convertTimedData(data.ecosystemActivityTimeSeries);
        var labelsEcosystem = getLabels(data.ecosystemActivityTimeSeries);
        const lineGraphEcosystemGrowing = <GraphLine items={ecosystemGrowing} labels={labelsEcosystem}/>
        const cardLineGraphEcosystem = <div className="col-span-full h-[600px] flex flex-col">
            <InfoCard title={"Ecosystem activity"} data={lineGraphEcosystemGrowing} Color={colors.topic}>
                <div className="flex justify-center bg-white text-sm">
                  Number of active projects in the ecosystem over time
                </div>
            </InfoCard>
           
        </div>
        cardList.push(cardLineGraphEcosystem)

        } else {
             cardList.push(<div className="col-span-full bg-white py-10 justify-center flex"> No data available with selected filters.</div>)
        }
          
        
    } else {
        return(
            <div>
                <SpinnerComponent />
            </div>
        )
    } 

    //Normal render (No error)
    return(
        <div className="lg:ml-44 lg:mr-44 md:ml-32 md:mr-32 sm:ml-0 sm:mr-0 mb-16">
           <div className="grid grid-cols-1 gap-3 md:grid-cols-2 lg:grid-cols-3" >
                {cardList.map((card, i) => (
                    card
                ))}
           </div>
        </div>      
    )
}

