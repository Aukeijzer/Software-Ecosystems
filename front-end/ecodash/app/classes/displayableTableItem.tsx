import React from "react";

export default interface displayableTableItem {
    renderTableHeaders() : React.JSX.Element
    renderAsTableItem() : React.JSX.Element
}

