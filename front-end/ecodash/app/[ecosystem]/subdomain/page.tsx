"use client"
import { useSearchParams } from 'next/navigation'
import LayoutEcosystem from '@/components/layoutEcosystemPaged';

export default function SubDomainPage(){
    const SearchParams = useSearchParams();
    const domainsString : string | null = SearchParams.get('subdomain');
    const domains = domainsString?.split(',');

    const url = "/subdomain?subdomain="+ domains +"," 
    return(
        <div>
            {domains? <LayoutEcosystem  ecosystem='agriculture' url={url} subDomains={domains}  /> : <div> helaas </div> }
        </div>
    )
}
