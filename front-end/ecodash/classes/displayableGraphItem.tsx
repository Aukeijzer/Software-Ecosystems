import React from "react";

export default interface displayableGraphItem {
    renderAsGraphItem(index: number, onClick: (sub: string) => void) : React.JSX.Element
}

