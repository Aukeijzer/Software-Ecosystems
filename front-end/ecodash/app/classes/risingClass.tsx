import displayable from "./displayableClass";


export default class risingClass extends displayable {
    topic: string;
    percentage: number;
    growth: number;

    constructor(topic: string, percentage: number, growth: number) {
        super()
        this.topic = topic;
        this.percentage = percentage;
        this.growth = growth;
    }


    renderAsListItem(onClick: (sub: string) => void): JSX.Element {
        return(
            <p className="flex flex-row gap-1"onClick={() => onClick(this.topic)}>
                <b>{this.topic}</b>  {this.percentage}% : {this.growth}  %
                 <svg className="w-6 h-6 text-gray-800 dark:text-white" aria-hidden="true" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 10 14">
                    <path stroke="green" strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M5 13V1m0 0L1 5m4-4 4 4"/>
                </svg>
        </p>
        )
    }

    renderAsTableItem(): JSX.Element {
        return(
            <div>

            </div>
        )
    }
    renderAsGraph(index: number): JSX.Element {
        return(<div>

        </div>)
    }
}