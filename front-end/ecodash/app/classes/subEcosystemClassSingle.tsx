import displayable from "./displayableClass";
import displayableSingle from "./displayableClassSingle";
import Link from "next/link";


export default class subEcosystemSingle extends displayableSingle {
    topic: string;
    projectCount: number;
    
    constructor(topic : string, projectCount : number){
        super()
        this.topic = topic;
        this.projectCount = projectCount;
    }
    
    renderAsListItem(onClick: (sub: string) => void): JSX.Element {    
        return(
            <p onClick={() => onClick(this.topic)}>
                {this.topic} with {this.projectCount} projects.
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
        return(
            <div>

            </div>
        )
    }
}