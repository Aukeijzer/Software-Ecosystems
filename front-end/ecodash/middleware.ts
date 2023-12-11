import { NextRequest, NextResponse } from 'next/server';
import { getValidSubdomain } from './app/utils/subdomain';
import next from 'next';

export async function middleware(req: NextRequest) {
    //Clone the url
    const url = req.nextUrl.clone();

    // RegExp for public files
    const PUBLIC_FILE = /\.(.*)$/; // Files

    // Skip public files
    if (PUBLIC_FILE.test(url.pathname) || url.pathname.includes('_next') || url.pathname.includes('api')) return;

    //Host is full adress
    const host = req.headers.get('host');
    //Adress of server is: (secodash.science.uu.nl)
    //console.log(host);
    const subdomain = getValidSubdomain(host);

    if (subdomain) {
        //subdomain available
        //rewriting to 
        //console.log(`>>> Rewriting: ${url.pathname} to ${subdomain}${url.pathname}`)
        url.pathname = `${subdomain}${url.pathname}`
    }
    //const cookie = req.headers.get('__Secure-next-auth.session-token')?.valueOf;
   
    const nextResponse: NextResponse = NextResponse.rewrite(url);
    const next2Response: NextResponse = NextResponse.redirect(url);
    return nextResponse;
}


/*
 const nextResponse: NextResponse = NextResponse.rewrite(url);
    if (req.cookies.get(`next-auth.session-token`) == undefined) {
        console.log("cookie is undefined");
        nextResponse.cookies.set('test', 'stront');
    } else {
        const cookie = req.cookies.get(`next-auth.session-token`);
        nextResponse.cookies.set('foo', 'bar');
        nextResponse.cookies.set(`next-auth.session-token`, cookie!.value)
        //req.headers.set('Set-Cookie', ["stront=${cookie}",  "Path=/", "HttpOnly", "SameSite=Lax"].join('; '));
        //req.headers.set("test", "testCoookieInhoud");
        // req.headers.set("Set-Cookie", "test=testCookie; Path=/; HttpOnly; SameSite=Lax");
        console.log(cookie);
        
    }
*/