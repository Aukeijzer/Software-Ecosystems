import { programmingLanguage } from "../enums/ProgrammingLanguage";
import displayable from "./displayableClass";
import { Cell } from "recharts";
export default class languageClass extends displayable{
    language: programmingLanguage;
    percentage: number;
   
    constructor(language: programmingLanguage, percentage: number){
        super()
        this.language = language;
        this.percentage = percentage;
    }

    renderAsListItem(): JSX.Element {
        return(
            <div>
                {this.language} : {this.percentage} %
            </div>
        )
    }
    renderAsTableItem(): JSX.Element {
        return(
            <div>

            </div>
        )
    }
    renderAsGraph(index: number): JSX.Element {
        const COLORS = [ "#4421af", "#1a53ff", "#0d88e6", "#00b7c7", "#5ad45a", "#8be04e", "#ebdc78"]
        return(
            <Cell key={`cell-${index}`} fill={COLORS[index % COLORS.length]} />
        )
    }

    
}