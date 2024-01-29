import { risingDTO } from "@/interfaces/DTOs/risingDTO"
import { technologyDTO } from "@/interfaces/DTOs/technologyDTO"

export interface topTopic {
    name: string,
    percentage: number
}

export interface topTopicGrowing {
    name: string,
    percentage: number,
    growth: number
}

export interface topTechnology {
    technology: string,
    projectCount: number
}

export interface topProject {
    name: string,
    devs: number
}

export interface topEngineer {
    name: string
}

export interface topTechnologyGrowing {
    name: string,
    percentage: number,
    growth: number
}
export const topTopics : topTopic[] = [
    {name: "DAO", percentage: 31},
    {name: "protocols", percentage: 22},
    {name: "Wallets", percentage: 11},
    {name: "DApps", percentage: 9},
    {name: "Finance", percentage: 5},
]

export const topTechnologies : technologyDTO[] = [
    {technology: "Ethereum", projectCount: 31},
    {technology: "Blockchain", projectCount: 22},
    {technology: "Hyperledger", projectCount: 11},
    {technology: "Solana", projectCount: 9},
    {technology: "Cardano", projectCount: 5},
]

export const topProjects : topProject[]= [
    {name: "Ethereum", devs: 120},
    {name: "Blockhain", devs: 110},
    {name: "Hyperledger", devs: 50},
    {name: "Solana", devs: 32},
    {name: "Cardano", devs: 19}

]

export const topEngineers : topEngineer[]= [
    {name: "Peter pan"},
    {name: "Siamak Farshidi"},
    {name: "Vivian X"},
    {name: "Daan Hillebrand"}
]

export const topTopicsGrowing : risingDTO[] = [
    {item: "DApps", percentage: 9, growth: 6},
    {item: "Plant protection", percentage: 7, growth: 5},
    {item: "Water weight", percentage: 5, growth: 3},
    {item: "PPO", percentage: 4, growth: 2},
    {item: "Flamingo", percentage: 3, growth: 2},

]


export const topTechnologyGrowing: risingDTO[] = [
    {item: "Ethereum", percentage: 31, growth:5},
    {item: "Blockchain", percentage: 22, growth:3},
    {item: "Hyperledger", percentage: 11, growth:3},
    {item: "Solana", percentage: 9, growth:2},
    {item: "Cardano", percentage: 5, growth:1}
]

export interface lineData{
    date: string,
    topic1: number,
    topic1Name: string,
    topic2: number,
    topic2Name: string,
    topic3: number,
    topic3Name: string,
    topic4: number,
    topic4Name: string,
    topic5: number,
    topic5Name: string,
}

export const topicGrowthLine: lineData[] = [
    {date: "1-1-2023", topic1: 30, topic1Name: "Ehtereum", topic2: 28, topic2Name: "BlockChain", topic3: 130, topic3Name: "Wallets", topic4: 90, topic4Name: "DApps", topic5: 55, topic5Name: "Finance"},
    {date: "10-1-2023", topic1: 40, topic1Name: "Ethereum", topic2: 38, topic2Name: "BlockChain", topic3: 150, topic3Name: "Wallets", topic4: 95, topic4Name: "DApps", topic5: 61, topic5Name: "Finance"},
    {date: "20-1-2023", topic1: 42, topic1Name: "Ethereum", topic2: 39, topic2Name: "BlockChain", topic3: 180, topic3Name: "Wallets", topic4: 99, topic4Name: "DApps", topic5: 99, topic5Name: "Finance"},
    {date: "30-1-2023", topic1: 56, topic1Name: "Ethereum", topic2: 40, topic2Name: "BlockChain", topic3: 210, topic3Name: "Wallets", topic4: 112, topic4Name: "DApps", topic5: 122, topic5Name: "Finance"},
    {date: "10-2-2023", topic1: 91, topic1Name: "Ethereum", topic2: 42, topic2Name: "BlockChain", topic3: 210, topic3Name: "Wallets", topic4: 145, topic4Name: "DApps", topic5: 155, topic5Name: "Finance"},
    {date: "20-2-2023", topic1: 116, topic1Name: "Ethereum", topic2: 44, topic2Name: "BlockChain", topic3: 215, topic3Name: "Wallets", topic4: 166, topic4Name: "DApps", topic5: 177, topic5Name: "Finance"}
]
    