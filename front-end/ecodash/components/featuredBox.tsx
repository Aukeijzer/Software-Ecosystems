"use client"
import { useState } from "react";

interface inputFieldProps{
    index: number,
    options: string[]
}

interface itemFieldProps{
    index: number,
    item: string,
    description?: string
}

export default function FeaturedBox(){
   
    const [editMode, setEditMode] = useState(false);
    const [numberOfItems, setNumberOfItems] = useState(1);
    const [items, setItems] = useState([""]);
    const [descriptions, setDescriptions] = useState([""]);
    
    function onChangeSelect(index : number, selected : string){
        let tempArr = items;
        console.log(selected);
        tempArr[index] = selected;
        setItems(tempArr);
    }
    
    function onChangeDescription(value: string, index: number){
        let tempArr = descriptions;
        tempArr[index] = value;
        setDescriptions(tempArr);
        console.log(descriptions)
    }

    function onSubmit(){
        //Gather all data
        //Display confirmation?
        //Send post to backend
        let data = {items, descriptions};
        console.log("submitting to backend");
        console.log(items);
        console.log(descriptions);
        setEditMode(false);
    }
    function InputField(props: inputFieldProps){
       console.log(descriptions[props.index]);
        return(
           <div>
               <span> {props.index + 1} : </span>
               <select  onChange={(val) => onChangeSelect(props.index, val.target.value )} >
                    {props.options.map(item => <option value={item} >
                        {item}
                    </option>)}
               </select>
               <input type="text"  placeholder="Optional extra infromation about topic" onChange={(ev) => onChangeDescription(ev.target.value, props.index)} /> 
           </div>
        )
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

    if(editMode){
        const rows = [];

        for(let i = 0; i < numberOfItems; i++){
            rows.push(
                <li key={i}> 
                    <InputField index={i} options={["test1", "test2", "test3"]}/>
                </li>)
        }
        return(
            <div className="w-fit p-10 border-2 border-black ml-10 mt-10 bg-gray-300">
                <form>
                    <ul>
                        {rows}
                    </ul>
                    
                </form>
                <div className="bg-gray-500 flex flex-col">
                    <button className="bg-blue-500 hover:bg-blue-700 border-2 border-black text-white font-bold py-2 px-4 rounded" onClick={() => setNumberOfItems(numberOfItems + 1)}> + </button>
                    <button className="bg-blue-500 hover:bg-blue-700 border-2 border-black text-white font-bold py-2 px-4 rounded" onClick={() => onSubmit()}> Submit </button>
                </div>
              
            </div>
        )
    } else {
        
        //Get rows from DB
        //Get options from DB
        //FOr now just use current rows
        const finalRows = [];
        for(let i = 0; i < numberOfItems; i++){
            if(numberOfItems == 1){
                break;
            }
            finalRows.push( 
                <li key={i*2}> 
                    <ItemField index={i} item={items[i]} description={descriptions[i]}/>
                </li>)
        }
        return(
            <div className="w-fit p-10 border-2 border-black ml-10 mt-10 bg-gray-300" >
                <button className="bg-blue-500 hover:bg-blue-700 border-2 border-black text-white font-bold py-2 px-4 rounded" onClick={() => setEditMode(true)}> edit mode </button>
                <ul>
                    {finalRows}
                </ul>
            </div>
        )
    }
    
}

