import displayable from "./displayableClass";
import Link from "next/link";

export default class subEcosystem extends displayable {
    topic: string;
    projectCount: number;
    
    constructor(topic : string, projectCount : number){
        super()
        this.topic = topic;
        this.projectCount = projectCount;
    }
    
    renderAsListItem(url: string): JSX.Element {
        
        var teest = () => {
            window.location.href = url + this.topic;
        }
        
        return(
            <Link href={url + this.topic} onClick={teest}>
                {this.topic} with {this.projectCount} projects.
            </Link>

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