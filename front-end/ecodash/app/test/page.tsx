import FeaturedBox from "@/components/featuredBox";

export default function testPage(){
    return(
        <div>
            <FeaturedBox  options={["DAO", "Protocols", "Wallets", "DApps", "Finance"]}/>
        </div>
    )
}