/*
import { NextRequest, NextResponse } from 'next/server';
import { getValidSubdomain } from './app/utils/subdomain';


This had been commented out because it does not work with certificates.
This can be renabled if we get a wildcard certificate.


export async function middleware(req: NextRequest) {
    //Clone the url
    const url = req.nextUrl.clone();

    // RegExp for public files
    const PUBLIC_FILE = /\.(.*)$/; // Files

    // Skip public files
    if (PUBLIC_FILE.test(url.pathname) || url.pathname.includes('_next') || url.pathname.includes('api')) return;

    //Host is full adress
    const host = req.headers.get('host');
    const subdomain = getValidSubdomain(host);

    if(subdomain){
        //subdomain available
        //rewriting to 
        //console.log(`>>> Rewriting: ${url.pathname} to ${subdomain}${url.pathname}`)
        url.pathname = `${subdomain}${url.pathname}`
    }
   return NextResponse.rewrite(url)
}

*/