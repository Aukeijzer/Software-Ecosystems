export default function newDashboardPage(){
    //Check if isAdmin
    return(
        <div className="bg-gray-400 m-10 p-3 border-2 border-black">
            <h1 className="text-3xl"> Create a new dashboard</h1>
            <p> Text here explaining the steps to create a new dashboard</p>
            <form className="flex flex-col mt-5 gap-2">
                <label> <b> Ecosystem name </b></label>
                <input type="text" name="ecosystem"/> 
            
                <label> <b> Description </b></label>
                <input type="text" name="description"/> 
                
                <label > <b> Topic taxonomy list </b> </label>
                <input type="file" name="taxonomy"/>
           
                <label> <b> Technology taxonomy list </b> </label>
                <input type="file" name="technology" />
            
                <label> <b> Excluded topic list </b> </label>
                <input type="file" name="excluded" />
            
                <button className="text-center w-52 bg-blue-500 hover:bg-blue-700 mt-5 border-2 border-black text-white font-bold py-2 px-4 rounded"> Submit </button>
            </form>
        </div>
    )
}