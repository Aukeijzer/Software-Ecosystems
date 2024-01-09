import React from "react";
import displayableListItem from "./displayableListItem";
import displayableTableItem from "./displayableTableItem";
import displayableGraphItem from "./displayableGraphItem";

export default class contributorClass implements displayableListItem, displayableTableItem, displayableGraphItem {
    name: string;
    contributions: number;
    constructor(name: string, contributions: number) {
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
    renderAsGraphItem(index: number): React.JSX.Element {
        return(
            <div>

            </div>
        )
    }
}
