import React from "react";

export default class contributorClass {
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
    renderAsGraph(index: number): React.JSX.Element {
        return(
            <div>

            </div>
        )
    }
}
