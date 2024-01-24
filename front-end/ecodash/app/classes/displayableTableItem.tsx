import React from "react";

export default interface displayableTableItem {
    renderTableHeaders() : React.JSX.Element
    renderAsTableItem(onClick: (sub: string) => void) : React.JSX.Element
}

