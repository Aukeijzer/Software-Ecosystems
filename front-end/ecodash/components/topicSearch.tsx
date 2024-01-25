import React, { useState } from 'react';
import Button from './button';

interface TopicSearchProps {
    selectTopic: (topic: string, filtertype: string) => void;
}


enum SearchType {
    ecosystems = 'ecosystems',
    technologies = 'technologies',
    languages = 'languages',
}
/**
 * Component that renders a input field for manually selecting topics/languages/technologies
 * @param selectTopic: function that updates selected topics in layoutEcosystem
 * @returns A JSX.Element representing a input field.
 */

export default function TopicSearch(props: TopicSearchProps){
    const [searchType, setSearchType] = useState(SearchType.ecosystems);
    const [searchText, setSearchText] = useState('');
    const handleSearchTypeChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
        setSearchType(event.target.value as SearchType);
    };

    const handleSearchTextChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setSearchText(event.target.value);
    };

    const handleSearch = () => {
        props.selectTopic(searchText, searchType);
    };
   
    return(
            <div className="flex items-center">
                <select
                    value={searchType}
                    onChange={handleSearchTypeChange}
                    className="mr-2 p-2 border border-gray-300 rounded-md"
                >
                    <option value={SearchType.ecosystems} className='font-sans'>Topic</option>
                    <option value={SearchType.technologies} className='font-sans'>Technology</option>
                    <option value={SearchType.languages} className='font-sans'>Language</option>
                </select>

                <input
                    type="text"
                    value={searchText}
                    onChange={handleSearchTextChange}
                    className="mr-2 p-2 border border-gray-300 rounded-md"
                />

                <Button text='Search' onClick={handleSearch} />
            </div>
    );
}
        
    