import languageClass from '@/app/classes/languageClass';
import displayableGraphItem from "@/app/classes/displayableGraphItem";
import { lineData } from "@/mockData/mockAgriculture";
import displayableListItem from '@/app/classes/displayableListItem';
import subEcosystemClass from '@/app/classes/subEcosystemClass';

//Mock test data 
export const mockEcosystem = {ecosystem: 'quantum', projectCount:1000, topics:16};

export const mockEcosystemDescription1 = {
    ecosystem: 'quantum', 
    description:'Ecosystem about quantum software'
};

export const mockEcosystemDescription2 = {
    ecosystem: 'agriculture', 
    description:'Ecosystem about agriculture software', 
    subEcosystems: ['farming', 'machine-learning']
};

export const mockEcosystemDescription3 = {
    ecosystem: 'agriculture',
    description: 'Ecosystem about agriculture software',
    subEcosystems: ['farming', 'machine-learning'],
    removeTopic: (topic : string) => {
        mockEcosystemDescription3.subEcosystems = mockEcosystemDescription3.subEcosystems.filter(item => item != topic);
    }
};

export const mockLanguages : displayableGraphItem[] = [
    new languageClass('Javascript', 60),
    new languageClass('Python', 40)
]

export const languages : string[] = ['Javascript', 'Python'];

export const mockDataOverTime : lineData[] = [
    {date: "1-1-2023", topic1: 30, topic1Name: "Ehtereum", topic2: 28, topic2Name: "BlockChain", topic3: 130, topic3Name: "Wallets", topic4: 90, topic4Name: "DApps", topic5: 55, topic5Name: "Finance"},
    {date: "10-1-2023", topic1: 40, topic1Name: "Ethereum", topic2: 38, topic2Name: "BlockChain", topic3: 150, topic3Name: "Wallets", topic4: 95, topic4Name: "DApps", topic5: 61, topic5Name: "Finance"},
    {date: "20-1-2023", topic1: 42, topic1Name: "Ethereum", topic2: 39, topic2Name: "BlockChain", topic3: 180, topic3Name: "Wallets", topic4: 99, topic4Name: "DApps", topic5: 99, topic5Name: "Finance"},
    {date: "30-1-2023", topic1: 56, topic1Name: "Ethereum", topic2: 40, topic2Name: "BlockChain", topic3: 210, topic3Name: "Wallets", topic4: 112, topic4Name: "DApps", topic5: 122, topic5Name: "Finance"},
    {date: "10-2-2023", topic1: 91, topic1Name: "Ethereum", topic2: 42, topic2Name: "BlockChain", topic3: 210, topic3Name: "Wallets", topic4: 145, topic4Name: "DApps", topic5: 155, topic5Name: "Finance"},
    {date: "20-2-2023", topic1: 116, topic1Name: "Ethereum", topic2: 44, topic2Name: "BlockChain", topic3: 215, topic3Name: "Wallets", topic4: 166, topic4Name: "DApps", topic5: 177, topic5Name: "Finance"}
] 

export const mockCardsStatic = [
    {
      x: 0,
      y: 0,
      width: 2,
      height: 1,
      minH: 1,
      minW: 1,
      static: true,
      card: <div title="test" data-testid="card1">Card 1 Content</div>,
    },
    {
      x: 2,
      y: 0,
      width: 2,
      height: 2,
      minH: 1,
      minW: 1,
      static: true,
      card: <div title="test" data-testid="card2">Card 2 Content</div>,
  }
]

export const mockCards = [
    {
        x: 0,
        y: 0,
        width: 2,
        height: 1,
        minH: 1,
        minW: 1,
        static: false,
        card: <div title="test" data-testid="card1">Card 1 Content</div>,
    },
    {
        x: 2,
        y: 0,
        width: 2,
        height: 2,
        minH: 1,
        minW: 1,
        static: false,
        card: <div title="test" data-testid="card2">Card 2 Content</div>,
    },
    {
        x: 0,
        y: 1,
        width: 1,
        height: 1,
        minH: 1,
        minW: 1,
        static: false,
        card: <div title="test" data-testid="card3">Card 3 Content</div>,
    },
    {
        x: 1,
        y: 1,
        width: 1,
        height: 2,
        minH: 1,
        minW: 1,
        static: false,
        card: <div title="test" data-testid="card4">Card 4 Content</div>,
    },
    {
        x: 2,
        y: 2,
        width: 1,
        height: 1,
        minH: 1,
        minW: 1,
        static: false,
        card: <div title="test" data-testid="card5">Card 5 Content</div>,
    },
    {
        x: 3,
        y: 2,
        width: 1,
        height: 1,
        minH: 1,
        minW: 1,
        static: false,
        card: <div title="test" data-testid="card6">Card 6 Content</div>,
    },
];

export const mockSubecosystems : displayableListItem[] = [
    new subEcosystemClass('qiskit', 120),
    new subEcosystemClass('quantum-mechanics', 99)
];
