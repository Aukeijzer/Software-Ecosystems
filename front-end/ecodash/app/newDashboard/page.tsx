"use client";
import { useSession } from "next-auth/react";
import { ExtendedUser } from "../utils/authOptions";

export default function NewDashboardPage(){
    //Check if isAdmin

    const handleFormSubmit = async (event : any) => {
        event.preventDefault();

        const fileInput : HTMLInputElement | null = document.querySelector('input[name="taxonomy"]');
        if(fileInput === null || fileInput.files === null){
            return;
        }

        const file = fileInput.files[0];

        if (file) {
            console.log(`File name: ${file.name}`);
            const formData = new FormData();
            formData.append('taxonomy', file);

            try {
                const response = await fetch('/api/newEcosystem', {
                    method: 'POST',
                    
                    body: formData
                });

                if (response.ok) {
                    // Handle success
                } else {
                    // Handle error
                }
            } catch (error) {
                // Handle error
            }
        }
    }

    return(
        <div className="bg-gray-400 m-10 p-3 border-2 border-black">
            <h1 className="text-3xl"> Create a new dashboard</h1>
            <p> Text here explaining the steps to create a new dashboard</p>
            <form className="flex flex-col mt-5 gap-2" onSubmit={handleFormSubmit}>
                <label> <b> Ecosystem name </b></label>
                <input type="text" name="ecosystem"/> 
            
                <label> <b> Description </b></label>
                <input type="text" name="description"/> 
                
                <label> <b> Topic taxonomy list </b> </label>
                <input type="file" name="taxonomy" accept=".json" multiple={false} />
           
                <label> <b> Technology taxonomy list </b> </label>
                <input type="file" name="technology" />
            
                <label> <b> Excluded topic list </b> </label>
                <input type="file" name="excluded" />
            
                <button className="text-center w-52 bg-blue-500 hover:bg-blue-700 mt-5 border-2 border-black text-white font-bold py-2 px-4 rounded" type="submit"> Submit </button>
            </form>
        </div>
    )
}