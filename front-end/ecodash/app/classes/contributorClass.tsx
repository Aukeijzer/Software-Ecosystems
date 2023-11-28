import React from "react";
import displayable from "./displayableClass";

export default class contributorClass extends displayable {
    name: string;
    contributions: number;
    constructor(name: string, contributions: number){
        super()
        this.name = name;
        this.contributions = contributions;
    }

    renderAsListItem(onClick: (sub: string) => void): React.JSX.Element {
        return(
            <div>

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
        return(
            <div>

            </div>
        )
    }
}
