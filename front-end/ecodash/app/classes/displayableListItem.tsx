import React from "react";

export default interface displayableListItem {
    renderAsListItem(onClick: (sub: string) => void) : React.JSX.Element
}

