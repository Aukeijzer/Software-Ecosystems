import { NextAuthOptions, User } from "next-auth";
import GoogleProvider from "next-auth/providers/google";
import GitHubProvider from "next-auth/providers/github"


const useSecureCookies = process.env.NEXTAUTH_URL!.startsWith("https://");
const cookiePrefix = useSecureCookies ? "__Secure-": "";
const hostName = new URL(process.env.NEXTAUTH_URL!).hostname;
console.log(hostName);

interface ExtendedUser extends User {
    isAdmin: boolean;
    userId: string;
}

export const authOptions: NextAuthOptions = {
    
    //Add callback here

    providers: [
        GoogleProvider({
            clientId: process.env.GOOGLE_CLIENT_ID!,
            clientSecret: process.env.GOOGLE_CLIENT_SECRET!,
            checks: ['none']

        }),
        GitHubProvider({
            clientId: process.env.GITHUB_CLIENT_ID!,
            clientSecret: process.env.GITHUB_CLIENT_SECRET!
        })
    ],

    session: {
        strategy: "jwt",
        
    },
    cookies: {
        sessionToken: {
            name: `${cookiePrefix}next-auth.session-token`,
            options: {
              httpOnly: true,
              sameSite: 'lax',
              path: '/',
              domain: "."+ hostName,
              secure: useSecureCookies,
              
             
            }
          }, 
    },
    callbacks: {
        async jwt({token , user }) {
            if(user){
                token.id = user.id;
                console.log("testdsaf");
                token.isAdmin = await fetchIsAdmin(user.id);
            }
            return token;
        },
        async session({ session, token }) {
            if (session.user) {
            
                const user = session.user as ExtendedUser;
                user.isAdmin = token.isAdmin as boolean; // Cast token.isAdmin to boolean
                console.log("Session callback")

            }
            return session;
        }
    }
  
}

async function fetchIsAdmin(userId: string) : Promise<boolean> {
    const response = await fetch(`http://secodash.com:3000/api/isAdmin/${userId}`);
    const data = await response.json();
    console.log("jwt callback")
    console.log(data.isAdmin);
    return data.isAdmin;
}

/*
 cookies: {
        sessionToken: {
            name: `${cookiePrefix}next-auth.session-token`,
            options: {
                httpOnly: true,
                sameSite: "lax",
                path: "/",
                //domain: "."+ hostName,
                secure: useSecureCookies,
            }
        },
       
    }

*/