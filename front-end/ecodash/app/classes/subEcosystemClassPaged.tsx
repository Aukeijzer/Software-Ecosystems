import displayablePaged from "./displayableClassPaged";
import Link from "next/link";

export default class subEcosystem extends displayablePaged {
    topic: string;
    projectCount: number;
    
    constructor(topic : string, projectCount : number){
        super()
        this.topic = topic;
        this.projectCount = projectCount;
    }
    
    renderAsListItem(url: string): JSX.Element {
        
        var onClick = () => {
            window.location.href = url + this.topic;
        }
        
        return(
            <Link href={url + this.topic} onClick={onClick}>
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