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
    name: string,
    percentage: number
}

export interface topProject {
    name: string,
    devs: number
}

export interface topEngineer {
    name: string
}
export const topTopics : topTopic[] = [
    {name: "DAO", percentage: 31},
    {name: "protocols", percentage: 22},
    {name: "Wallets", percentage: 11},
    {name: "DApps", percentage: 9},
    {name: "Finance", percentage: 5},
]

export const topTechnologies : topTechnology[] = [
    {name: "Etereum", percentage: 31},
    {name: "Blockchain", percentage: 22},
    {name: "Hyperledger", percentage: 11},
    {name: "Solana", percentage: 9},
    {name: "Cardano", percentage: 5},
]

export const topProjects : topProject[]= [
    {name: "Etereum", devs: 120},
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

export const topTopicsGrowing : topTopicGrowing[] = [
    {name: "DApps", percentage: 9, growth: 6},
    {name: "Plant protection", percentage: 7, growth: 5},
    {name: "Water weight", percentage: 5, growth: 3},
    {name: "PPO", percentage: 4, growth: 2},
    {name: "Flamingo", percentage: 3, growth: 2},

]