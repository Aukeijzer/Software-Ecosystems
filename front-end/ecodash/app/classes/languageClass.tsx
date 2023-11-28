import React from "react";
import displayable from "./displayableClass";
import { Cell } from "recharts";

export default class languageClass extends displayable{
    language: String;
    percentage: number;
   
    constructor(language: String, percentage: number){
        super()
        this.language = language;
        this.percentage = percentage;
    }

    renderAsListItem(): React.JSX.Element {
        return(
            <div>
                <b>{this.language.valueOf()} </b>: {this.percentage} %
            </div>
        )
    }
    renderAsTableItem(): React.JSX.Element {
        return(
            <div>

            </div>
        )
    }
    renderAsGraph(index: number): React.JSX.Element {
        const COLORS = [ "#4421af", "#1a53ff", "#0d88e6", "#00b7c7", "#5ad45a", "#8be04e", "#ebdc78"]
        return(
            <Cell key={`cell-${index}`} fill={COLORS[index % COLORS.length]} />
        )
    }

    
}