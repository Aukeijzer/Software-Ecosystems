"use client";
import { useSession } from "next-auth/react";
import { ExtendedUser } from "../utils/authOptions";
import { error } from "console";
import Button from "@/components/button";
import SpinnerComponent from "@/components/spinner";


export default function NewDashboardPage(){
    //Check if isAdmin
    const { data: session, status } = useSession()
    const user = session?.user as ExtendedUser;
    if(status === "loading"){
        return(
            <div>
                <SpinnerComponent/>
            </div>
        )
    }
    if(!user || (user.userType !== "Admin" && user.userType !== "RootAdmin")){
        return(
            <div className="lg:ml-44 lg:mr-44 md:ml-32 md:mr-32 sm:ml-0 sm:mr-0 bg-white p-10">
                <h1 className="text-2xl font-bold mb-4">Create a new dashboard</h1>
                <p className="text-gray-600 mb-4">You do not have permission to access this page</p>
            </div>
        )
    }

    const handleFormSubmit = async (event : any) => {
        event.preventDefault();

        //Get taxonomy.json file
        const fileTaxonomyInput : HTMLInputElement | null = document.querySelector('input[name="taxonomy"]');
        if(fileTaxonomyInput === null || fileTaxonomyInput.files === null){
            throw new Error("File taxonomy input not found");
        }
        const fileTaxonomy = fileTaxonomyInput.files[0];

        //Get technology.json file
        const fileTechnologyInput : HTMLInputElement | null = document.querySelector('input[name="technology"]');
        if(fileTechnologyInput === null || fileTechnologyInput.files === null){
            throw new Error("File technology input not found");
        }
        const fileTechnology = fileTechnologyInput.files[0];

        //Get excluded topic.json file
        const fileExcludedInput : HTMLInputElement | null = document.querySelector('input[name="excluded"]');
        if(fileExcludedInput === null || fileExcludedInput.files === null){
            throw new Error("File excluded input not found");
        }
        const fileExcluded = fileExcludedInput.files[0];


        if (fileTaxonomy && fileTechnology && fileExcluded) {
            const formData = new FormData();
            formData.append('taxonomy', fileTaxonomy);
            formData.append('technology', fileTechnology);
            formData.append('excluded', fileExcluded);
            formData.append('ecosystem', event.target.ecosystem.value);
            formData.append('description', event.target.description.value);
            
            console.log(formData);
            try {
                const response = await fetch('/api/newEcosystem', {
                    method: 'POST',
                    body: formData
                });

                if (response.ok) {
                    // Handle success
                    const data = await response.json();
                    console.log(data);
                    //Alert user that ecosystem was created
                    alert("Ecosystem created");
                } else {
                    // Handle error
                    throw new Error("Error in response");
                }
            } catch (error ) {
                throw new Error("Error in fetch");
            }
        } else {
            // Handle no file chosen
            throw new Error("Not all files were selected");
        }
    }

    return(
        <div className="lg:ml-44 lg:mr-44 md:ml-32 md:mr-32 sm:ml-0 sm:mr-0 bg-white p-10">
            <h1 className="text-2xl font-bold mb-4">Create a new dashboard</h1>
            <p className="text-gray-600 mb-4">Text here explaining the steps to create a new dashboard</p>
            <form className="space-y-4" onSubmit={handleFormSubmit}>
                <div>
                    <label className="block text-sm font-medium text-gray-700">Ecosystem name</label>
                    <input className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-500 focus:ring-indigo-500" type="text" name="ecosystem" />
                </div>

                <div>
                    <label className="block text-sm font-medium text-gray-700">Description</label>
                    <input className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-500 focus:ring-indigo-500" type="text" name="description" />
                </div>

                <div>
                    <label className="block text-sm font-medium text-gray-700">Topic taxonomy list</label>
                    <input className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-500 focus:ring-indigo-500" type="file" name="taxonomy" accept=".json" multiple={false} />
                </div>

                <div>
                    <label className="block text-sm font-medium text-gray-700">Technology taxonomy list</label>
                    <input className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-500 focus:ring-indigo-500" type="file" name="technology" accept=".json" multiple={false} />
                </div>

                <div>
                    <label className="block text-sm font-medium text-gray-700">Excluded topic list</label>
                    <input className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-indigo-500 focus:ring-indigo-500" type="file" name="excluded" accept=".json" multiple={false} />
                </div>

                <Button text={"Submit"} onClick={() => handleFormSubmit} />
            </form>
        </div>
    )
}