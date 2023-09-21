interface ListProps<T> {
    items: T[],
    renderItem: (item: T) => JSX.Element,
}


const ecoDataList =<T extends {}> ({items, renderItem} : ListProps<T>) => {
    return(
        <ul>
            {items.map((item, i) => (
                <li key={i}>{renderItem(item)}</li>
            ))}
        </ul>
    )
}

export default ecoDataList;



