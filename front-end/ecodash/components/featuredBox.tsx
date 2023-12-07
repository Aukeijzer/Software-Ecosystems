"use client"
import { ChangeEvent, useState } from "react";


interface featuredBoxProps{
    options: string[]
}
export default function FeaturedBox(props: featuredBoxProps){
   
    const [editMode, setEditMode] = useState(false);
    const [hasData, setHasData] = useState(false);
    
    const[inputFields, setInputFields] = useState([
        {topic: '', description: ''}
    ])

    function handleFormChangeSelect(index : number, event: ChangeEvent<HTMLSelectElement>){
        let data = [...inputFields];
        data[index]= { ...data[index], [event.target.name]: event.target.value }
        setInputFields(data);

    }
    function handleFormChangeText(index: number, event: ChangeEvent<HTMLInputElement>){
        let data = [...inputFields];
        data[index]= { ...data[index], [event.target.name]: event.target.value }
        setInputFields(data);
    }
    function addField(){
        let newField = {topic: '', description: ''};
        setInputFields([...inputFields, newField]);
    }
    function submit(e: any){
        e.preventDefault();
        console.log(inputFields)
        setEditMode(false);
        setHasData(true);
    }

    if(editMode){
        return(
            <div className="w-fit p-10 border-2 border-black ml-10 mt-10 bg-gray-300">
                <form onSubmit={submit}>
                    {inputFields.map((input, index) => {
                       return(
                        <div key={index + "blub"}>
                             <select name="topic"  required={true} value={input.topic} onChange={(event) => handleFormChangeSelect(index, event) } >
                                <option disabled selected> -- select an option -- </option>
                                {props.options.map(item => <option key={item} value={item} >
                                    {item}
                                </option>)}
                            </select>

                            <input name="description" type="text" value={input.description} placeholder="Optional extra infromation about topic" onChange={(event) => handleFormChangeText(index, event)} /> 
                        </div>

                       )
                    })}
                    
                </form>
                <div className="bg-gray-500 flex flex-col">
                    <button className="bg-blue-500 hover:bg-blue-700 border-2 border-black text-white font-bold py-2 px-4 rounded" onClick={() => addField()}> + </button>
                    <button className="bg-blue-500 hover:bg-blue-700 border-2 border-black text-white font-bold py-2 px-4 rounded" onClick={submit}> Submit </button>
                </div>
              
            </div>
        )
    } else {
        
        //Get rows from DB
        //Get options from DB
        //FOr now just use current rows

        let rows : JSX.Element[] = [];
        
        {inputFields.map((input, index) => {
            rows.push(
                <li key={index*2}> 
                    <ItemField index={index} item={input.topic} description={input.description}/>
                </li>
            )
        })}  
  
        if(hasData){
            return(
                <div className="w-fit p-10 border-2 border-black ml-10 mt-10 bg-gray-300" >
                    <ul>
                        {rows}
                    </ul>
                    <button className="bg-blue-500 hover:bg-blue-700 border-2 border-black text-white font-bold py-2 px-4 rounded" onClick={() => setEditMode(true)}> edit mode </button>
                </div>
            )   
        } else {
            return(
                <div className="w-fit p-10 border-2 border-black ml-10 mt-10 bg-gray-300" >
                    <h1 className="text-xl"> <b>Add featured topics. </b></h1>
                    <h1> These will be displayed to all users visiting this dashboard </h1>
                    <button className="bg-blue-500 hover:bg-blue-700 border-2 border-black text-white font-bold py-2 px-4 rounded" onClick={() => setEditMode(true)}> edit mode </button>
                </div>
            )
        }

    }
    
}

interface itemFieldProps{
    index: number,
    item: string,
    description?: string
}

function ItemField(props: itemFieldProps){
    return(
        <div className="flex flex-row gap-2  ">
            <span> {props.index + 1} :  </span>
            <div className="flex flex-col">
                <h1 className="text-3xl"> {props.item}  </h1>
                <p> {props.description} </p>
            </div>
           
        </div>
    )
}