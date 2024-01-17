# Front-end documentation
# Getting started 
For more information to get started with the front-end go to [project readme](../README.md/#front-end). 

# Contents
- [Components](#components)
- [Utils](#util-files)
- [Classes](#classes)
- [Interfaces](#interfaces)
- [Pages](#pages)
# Components
## EcosystemButton

Represents an Ecosystem Button component. When clicked, it goes to the corresponding ecosystem.
### Props

- ecosystem (required): The name of the ecosystem.
- projectCount (required): The number of projects in the ecosystem.
- topics (required): The number of topics in the ecosystem.

### Example
```jsx

<EcosystemButton
   ecosystem="Example Ecosystem"
   projectCount={10}
   topics={5}
/>
```

### Returns

A JSX.Element representing the rendered Ecosystem Button component.

## EcosystemDescription

Renders a card component displaying information about an ecosystem.

- **Component:** `EcosystemDescription`

### Props
- ecosystem (required): The name of the ecosystem.
- description (required): The description of the ecosystem.
- subEcosystems (optional): The list of sub-ecosystems.
- removeTopic (optional): The function to remove a topic.

### Example
```jsx
<EcosystemDescription
   ecosystem="Example Ecosystem"
   description="This is an example ecosystem."
   subEcosystems={['SubEco1', 'SubEco2']}
   removeTopic={(topic) => handleRemoveTopic(topic)}
/>
```

### Returns
A JSX.Element representing the rendered EcosystemDescription component.

## EcosystemInformationCard

Renders the data for an ecosystem information card.

### Props
- ecosystem (required): The ecosystem object containing the information to be displayed. (Type: `ecosystemDTO`)

### Example
```jsx
<EcosystemInformationCard
   ecosystem={exampleEcosystem}
/>
```

### Returns
A JSX.Element representing the rendered component.

## GraphComponent

Renders a graph component that displays data in a pie chart.

### Props
- props (required): The props for the graph component. (Type: `infoCardDataGraphProps<T>`)

### Example
```jsx
<PieChartGraph
   {...graphProps}
/>
```

### Returns
A JSX.Element representing the rendered graph component.

## GraphLine

Renders a line element for a specific topic.

### Props
- index (required): The index of the topic.
- datakey (required): The data key for the line.
- color (required): The color of the line.

### Example
```jsx
<TopicLineElement
   index={0}
   datakey="topicData"
   color="#3498db"
/>
```

### Returns
The JSX element representing the line.

## GridLayout

Renders a grid layout that can fit any number of grid items.

- **Component:** `GridLayout`

### Props
- cards (required): A list of elements wrapped in a cardWrapper. (Type: `cardWrapper[]`)

### Example
```jsx
<GridLayout
   cards={[card1, card2, card3]}
/>
```

### Returns
A JSX.Element representing the rendered grid layout.


## InfoCard 

Represents a card containing a title and a JSX.Element.

- **Component:** `InfoCard`

### Props
- title (required): The title to be displayed at the top of the card.
- data (required): The JSX.Element to be displayed in the card.
- alert (optional): If provided, renders a small alert box with the provided string.
- className (optional): Additional CSS class name for the card.
- onClick (optional): The function to be called when the card is clicked.

### Example 
```jsx
<InfoCard
   title="Example Card"
   data={<List items={exampleData} />}
   alert="This is an example alert"
   className="custom-card"
   onClick={() => handleCardClick()}
/>
```
## EcosystemPageLayout

Renders the layout for the ecosystem page.

- **Component:** `EcosystemPageLayout`

### Props
- ecosystem (required): The name of the ecosystem.

### Example
```jsx
<EcosystemPageLayout
   ecosystem="Example Ecosystem"
/>
```

### Returns
A JSX.Element representing the rendered layout component.

The `EcosystemPageLayout` component accepts a single prop: `ecosystem`, which is a string representing the name of the ecosystem.

The `useRouter` hook is used to get the Router object, which is used to navigate between pages. The `useSearchParams` hook is used to get the `searchParams` object, which is used to access the search parameters in the current URL.

The `useState` hook from React is used to create a state variable `selectedEcosystems` and its setter function `setSelectedEcosystems`. The initial value of `selectedEcosystems` is an array containing the `ecosystem` prop. This state variable is used to keep track of the selected ecosystems.

The `useSWRMutation` hook is used to fetch data from the `/ecosystems` endpoint of the API. The `data`, `trigger`, `error`, and `isMutating` variables are destructured from the hook. The `trigger` function is used to manually fetch the data, `data` holds the fetched data, `error` holds any error that occurred during fetching, and `isMutating` indicates whether a fetch is in progress.

The `useEffect` hook is used to call the `trigger` function when the component mounts, causing the data to be fetched from the API. If the URL has additional parameters, these are added to the `selectedEcosystems` state, and the `trigger` function is called with these parameters. If there are no additional parameters, the `trigger` function is called with the current `selectedEcosystems` state.


## HomePageLayout

Renders the layout for the home page.

### Returns
The rendered layout component.

The `useSWRMutation` hook is used to fetch data from the `/ecosystems` endpoint of the API. The `data`, `trigger`, `error`, and `isMutating` variables are destructured from the hook. The `trigger` function is used to manually fetch the data, `data` holds the fetched data, `error` holds any error that occurred during fetching, and `isMutating` indicates whether a fetch is in progress.

The `useEffect` hook is used to call the `trigger` function when the component mounts, causing the data to be fetched from the API.

If an error occurs during fetching, the component renders a paragraph with an error message.

The `onClickEcosystem` function is a handler for click events. It constructs a URL using the `ecosystem` argument and the `NEXT_PUBLIC_LOCAL_ADDRESS` environment variable, and then navigates to that URL using the `Router.push` method.

The `cardWrappedList` variable is an array that will hold `cardWrapper` objects. If the `data` variable is truthy (i.e., if data has been fetched successfully), the code creates a `div` with some information about "SECODash," wraps it in an `InfoCard` component, and then wraps that in a `cardWrapper` object. The `cardWrapper` object is then pushed into the `cardWrappedList` array.

The `cardWrapper` object has properties for the card component (`card`), its dimensions (`width` and `height`), its position (`x` and `y`), and whether it's static (`static`). The `InfoCard` component likely represents a card in a card-based layout, and the `cardWrapper` object is used to control its layout properties.

## listComponent

Renders a list component.

### Props
- props (required): The props for the list component. (Type: `infoCardDataListProps`)

### Example
```jsx
<InfoCardDataList
   {...listProps}
/>
```

### Returns
A JSX.Element representing the rendered list component.


## NavBar

Renders a NavBar with clickable links to the main ecosystems.

### Returns
A JSX.Element representing the rendered NavBar component.


## Spinner

A component that displays a spinner animation.

### Returns
The `SpinnerComponent` JSX element.

## tableComponent

Component that renders a table with provided data.

### Props
- props (required): The props for the table component.

### Example
```jsx
<DataTable
   {...tableProps}
/>
```

### Returns
A JSX.Element representing the table.


# Util files
## ApiFetcher.ts

### `fetcherEcosystemByTopic`

Fetches ecosystem data by topic from the specified URL.

### Parameters
- `url` (required): The URL to fetch the data from.
- `arg` (required): An object containing the topics array.
  - `arg.topics` (required): An array of topics to filter the ecosystem data.

### Returns
A promise that resolves to the fetched ecosystem data.

### Example
```typescript
const data = await fetcherEcosystemByTopic('example-url', { topics: ['topic1', 'topic2'] });
```

---

### `fetcherHomePage`

Fetches homepage ecosystem data from the specified URL.

### Parameters
- `url` (required): The URL to fetch the data from.

### Returns
A promise that resolves to the fetched homepage ecosystem data.

### Example
```typescript
const data = await fetcherHomePage('example-url');
```

## cardbuilder.tsx

### `buildPieGraphCard`

Builds a pie graph card.

### Parameters
- `topics` (required): The displayable topics for the graph.
- `title` (required): The title of the card.
- `x` (required): The x-coordinate of the card.
- `y` (required): The y-coordinate of the card.

### Returns
The wrapped card object.

### Example
```tsx
const pieGraphCard = buildPieGraphCard(['topic1', 'topic2'], 'Pie Graph', 1, 2);
```

---

### `buildListCard`

Builds a list card.

### Parameters
- `topics` (required): The displayable topics for the list.
- `onClick` (required): The click event handler for the list items.
- `title` (required): The title of the card.
- `x` (required): The x-coordinate of the card.
- `y` (required): The y-coordinate of the card.
- `width` (required): The width of the card.
- `height` (required): The height of the card.
- `alert` (optional): Optional alert message for the card.

### Returns
The wrapped card object.

### Example
```tsx
const listCard = buildListCard(['item1', 'item2'], handleItemClick, 'List Card', 1, 2, 2, 3, 'Alert Message');
```

---

### `buildLineGraphCard`

Builds a line graph card.

### Parameters
- `data` (required): The data for the line graph.
- `title` (required): The title of the card.
- `x` (required): The x-coordinate of the card.
- `y` (required): The y-coordinate of the card.

### Returns
The wrapped card object.

### Example
```tsx
const lineGraphCard = buildLineGraphCard(lineGraphData, 'Line Graph', 1, 2);
```

---

## subdomain.ts

### `getValidSubdomain`

Retrieves the valid subdomain from the given host. If no host is provided, it retrieves the host from the window object in the client-side.

### Parameters
- `host` (optional): The host from which to extract the subdomain.

### Returns
The valid subdomain extracted from the host, or `null` if no valid subdomain is found.

### Example
```typescript
const subdomain = getValidSubdomain('sub.example.com');
// subdomain will be 'sub'
```

## converters/languageConverter.ts

### `listLanguageDTOConverter`

Converts a list of `languageDTO` objects to a list of `languageClass` objects. Calculates the remaining percentage and adds an "Other" language to the list.

### Parameters
- `languages` (required): The list of `languageDTO` objects to be converted.

### Returns
The converted list of `languageClass` objects.

### Example
```typescript
const convertedList = listLanguageDTOConverter([{ name: 'JavaScript', percentage: 50 }, { name: 'Python', percentage: 30 }]);
```

## converters/risingConverter.ts

### `listRisingDTOConverter`

Converts an array of `risingDTO` objects to an array of `risingClass` objects.

### Parameters
- `rising` (required): The array of `risingDTO` objects to be converted.

### Returns
The converted array of `risingClass` objects.

### Example
```typescript
const convertedArray = listRisingDTOConverter([{ name: 'Item1', value: 10 }, { name: 'Item2', value: 20 }]);
```

## converters/subEcosystemRising.ts

### `listSubEcosystemDTOConverter`

Converts an array of `subEcosystemDTO` objects to an array of `subEcosystemClass` objects.

### Parameters
- `subEcosystems` (required): The array of `subEcosystemDTO` objects to be converted.

### Returns
The converted array of `subEcosystemClass` objects.

### Example
```typescript
const convertedArray = listSubEcosystemDTOConverter([{ name: 'SubEco1', value: 10 }, { name: 'SubEco2', value: 20 }]);
```

## converters/technologyConverter.ts

### `TechnologyDTOConverter`

Converts an array of `technologyDTO` objects to an array of `technologyClass` objects.

### Parameters
- `technologies` (required): The array of `technologyDTO` objects to be converted.

### Returns
The converted array of `technologyClass` objects.

### Example
```typescript
const convertedArray = listTechnologyDTOConverter([{ name: 'Tech1', version: '1.0' }, { name: 'Tech2', version: '2.0' }]);
```

# classes
## `contributorClass`

Represents a contributor in the application. Implements the `displayableListItem`, `displayableTableItem`, and `displayableGraphItem` interfaces.

### Constructor

```typescript
/**
 * Creates a new instance of the contributorClass.
 * @param name - The name of the contributor.
 * @param contributions - The number of contributions made by the contributor.
 */
constructor(name: string, contributions: number);
```

### Properties

- `name` (type: `string`): The name of the contributor.
- `contributions` (type: `number`): The number of contributions made by the contributor.

### Methods

#### `renderAsListItem`

Renders the contributor as a list item.

##### Parameters
- `onClick` (type: `(sub: string) => void`): The function to be called when the item is clicked.

##### Returns
The JSX element representing the contributor as a list item.

##### Example
```tsx
const listItem = contributorInstance.renderAsListItem(handleItemClick);
```

---

#### `renderAsTableItem`

Renders the contributor as a table item.

##### Returns
The JSX element representing the contributor as a table item.

##### Example
```tsx
const tableItem = contributorInstance.renderAsTableItem();
```

---

#### `renderAsGraphItem`

Renders the contributor as a graph item.

##### Parameters
- `index` (type: `number`): The index of the contributor in the graph.

##### Returns
The JSX element representing the contributor as a graph item.

##### Example
```tsx
const graphItem = contributorInstance.renderAsGraphItem(1);
```

## `subEcosystemClass`

Represents a sub ecosystem class that implements the `displayableListItem` interface.

### Constructor

```typescript
/**
 * Creates a new instance of the subEcosystemClass.
 * @param topic - The topic of the sub ecosystem.
 * @param projectCount - The number of projects in the sub ecosystem.
 */
constructor(topic: string, projectCount: number);
```

### Properties

- `topic` (type: `string`): The topic of the sub ecosystem.
- `projectCount` (type: `number`): The number of projects in the sub ecosystem.

### Methods

#### `renderAsListItem`

Renders the sub ecosystem as a list item.

##### Parameters
- `onClick` (type: `(sub: string) => void`): The function to be called when the list item is clicked.

##### Returns
The JSX element representing the sub ecosystem as a list item.

##### Example
```tsx
const listItem = subEcosystemInstance.renderAsListItem(handleItemClick);
```
## `technologyClass`

Represents a technology class that implements the `displayableListItem` interface.

### Constructor

```typescript
/**
 * Creates a new instance of the technologyClass.
 * @param technology - The technology name.
 * @param projectCount - The number of projects associated with the technology.
 */
constructor(technology: string, projectCount: number);
```

### Properties

- `technology` (type: `string`): The technology name.
- `projectCount` (type: `number`): The number of projects associated with the technology.

### Methods

#### `renderAsListItem`

Renders the technology as a list item.

##### Parameters
- `onClick` (type: `(sub: string) => void`): The click event handler for the list item.

##### Returns
The JSX element representing the technology as a list item.

##### Example
```tsx
const listItem = technologyInstance.renderAsListItem(handleItemClick);
```

# interfaces
## `cardWrapper` Interface

```typescript
export interface cardWrapper {
    card: JSX.Element;
    width: number;
    height: number;
    x: number;
    y: number;
    minH?: number;
    minW?: number;
    static: boolean;
}
```

### Properties

- `card` (type: `JSX.Element`): The JSX element representing the card.
- `width` (type: `number`): The width of the card.
- `height` (type: `number`): The height of the card.
- `x` (type: `number`): The x-coordinate of the card.
- `y` (type: `number`): The y-coordinate of the card.
- `minH` (optional, type: `number`): The minimum height of the card.
- `minW` (optional, type: `number`): The minimum width of the card.
- `static` (type: `boolean`): Indicates whether the card is static.

### Example
```typescript
const cardWrapperInstance: cardWrapper = {
    card: <div>Card Content</div>,
    width: 300,
    height: 200,
    x: 10,
    y: 20,
    minH: 150,
    minW: 250,
    static: false
};
```

## `ecosystemDTO` Interface

```typescript
export interface ecosystemDTO {
    displayName?: string;
    description?: string;
    numberOfStars?: number;
    topLanguages: languageDTO[];
    subEcosystems: subEcosystemDTO[];
}
```

### Properties

- `displayName` (optional, type: `string`): The display name of the ecosystem.
- `description` (optional, type: `string`): The description of the ecosystem.
- `numberOfStars` (optional, type: `number`): The number of stars associated with the ecosystem.
- `topLanguages` (type: `languageDTO[]`): An array of `languageDTO` objects representing the top languages in the ecosystem.
- `subEcosystems` (type: `subEcosystemDTO[]`): An array of `subEcosystemDTO` objects representing the sub ecosystems.

### Example
```typescript
const ecosystemData: ecosystemDTO = {
    displayName: 'EcoSystemName',
    description: 'EcoSystemDescription',
    numberOfStars: 100,
    topLanguages: [{ name: 'JavaScript', percentage: 50 }, { name: 'Python', percentage: 30 }],
    subEcosystems: [{ topic: 'SubEco1', projectCount: 10 }, { topic: 'SubEco2', projectCount: 20 }]
};
```

---

## `subEcosystemDTO` Interface

```typescript
export interface subEcosystemDTO {
    topic: string;
    projectCount: number;
}
```

### Properties

- `topic` (type: `string`): The topic of the sub ecosystem.
- `projectCount` (type: `number`): The number of projects in the sub ecosystem.

### Example
```typescript
const subEcosystemData: subEcosystemDTO = {
    topic: 'SubEcoName',
    projectCount: 15
};
```


## `languageDTO` Interface

```typescript
export interface languageDTO {
    language: string;
    percentage: number;
}
```

### Properties

- `language` (type: `string`): The name of the language.
- `percentage` (type: `number`): The percentage representation of the language.

### Example
```typescript
const languageData: languageDTO = {
    language: 'JavaScript',
    percentage: 50
};
```
## `projectDTO` Interface

```typescript
export interface projectDTO {
    id: string;
    name: string;
    createdAt: string;
    ecosystem: string[];
    owner: string;
    description?: string;
    topics: string[];
    languages: languageDTO[];
    totalSize?: number;
    readMe?: string;
    numberOfStars: number;
}
```

### Properties

- `id` (type: `string`): The ID of the project.
- `name` (type: `string`): The name of the project.
- `createdAt` (type: `string`): The creation date of the project.
- `ecosystem` (type: `string[]`): An array of ecosystems associated with the project.
- `owner` (type: `string`): The owner of the project.
- `description` (optional, type: `string`): The description of the project.
- `topics` (type: `string[]`): An array of topics associated with the project.
- `languages` (type: `languageDTO[]`): An array of `languageDTO` objects representing the languages used in the project.
- `totalSize` (optional, type: `number`): The total size of the project.
- `readMe` (optional, type: `string`): The README content of the project.
- `numberOfStars` (type: `number`): The number of stars associated with the project.

### Example
```typescript
const projectData: projectDTO = {
    id: '12345',
    name: 'ProjectName',
    createdAt: '2022-01-01',
    ecosystem: ['Eco1', 'Eco2'],
    owner: 'OwnerName',
    description: 'ProjectDescription',
    topics: ['Topic1', 'Topic2'],
    languages: [{ language: 'JavaScript', percentage: 50 }, { language: 'Python', percentage: 30 }],
    totalSize: 1024,
    readMe: 'ReadMeContent',
    numberOfStars: 100
};
```
## `technologyDTO` Interface

```typescript
export interface technologyDTO {
    technology: string;
    projectCount: number;
}
```

### Properties

- `technology` (type: `string`): The name of the technology.
- `projectCount` (type: `number`): The number of projects associated with the technology.

### Example
```typescript
const technologyData: technologyDTO = {
    technology: 'TechName',
    projectCount: 10
};
```

# Pages
## Homepage
## `Home` Page

```tsx
import React from 'react';
import LayoutHomePage from '@/components/layoutHomePage';

/**
 * Home page component.
 * @returns The JSX element representing the Home page.
 */
export default async function Home(): React.JSX.Element {
    return (
        <div>
            <LayoutHomePage />
        </div>
    );
}
```

### Description

The `Home` page component renders the Home page and includes the `LayoutHomePage` component.

## [Ecosystem] page

## `ecosystemPage` Page


### Description

The `EcosystemPage` component represents a page with a dynamic path. The dynamic parameters from the URL are passed as props to the page. It includes the `LayoutEcosystem` component.

