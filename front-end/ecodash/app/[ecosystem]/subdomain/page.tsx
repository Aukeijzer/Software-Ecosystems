 "use client"
import { useSearchParams } from 'next/navigation'
import { apiCallSubEcosystem } from '@/components/apiHandler';
import { ecosystemModel } from '@/app/models/ecosystemModel';
import LayoutEcosystem from '@/components/layoutEcosytem';

export default function SubDomainPage(){
    const SearchParams = useSearchParams();
    const domainsString : string | null = SearchParams.get('subdomains');
    const domains = domainsString?.split(',');

    

    if(domains){
      //If subdomains selected show ecosystem layout object


    } else {
        //Go to ecosystem page when no subdomains are selected

    }
    
    
    return(
        <div>
            {domains? <LayoutEcosystem  ecosystem='agriculture' subDomains={domains} /> : <div> helaas </div>}
        </div>
    )
}