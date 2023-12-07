/**
 * Retrieves the valid subdomain from the given host.
 * If no host is provided, it retrieves the host from the window object in the client-side.
 * 
 * @param host - The host from which to extract the subdomain.
 * @returns The valid subdomain extracted from the host, or null if no valid subdomain is found.
 */
export const getValidSubdomain = (host?: string | null) => {
    let subdomain: string | null = null;
    if(!host && typeof window !== 'undefined'){
        //On client get the host from window
        host = window.location.host;
    }

    //secodash.science.uu.nl
    //agriculture.secodash.science.uu.nl
    
    if(host && host.includes('.')) {
        const candidate = host.split('.')[0];
        if(candidate){
            subdomain = candidate
        }
    }
    return subdomain
};