import React from "react";

export default interface displayableGraphItem {
    renderAsGraphItem(index: number) : React.JSX.Element
}

