/** @type {import('next').NextConfig} */
const nextConfig = {
    output: 'standalone',
    experimental: {
        serverActions: true,
    },
    async headers(){
        return[
            {
                source:"/",
                headers:[
                    { key: "Access-Control-Allow-Credentials", value: "true" },
                    { key: "Access-Control-Allow-Origin", value: "*" },
                    {
                        key: "Access-Control-Allow-Methods",
                        value: "HEAD,GET,OPTIONS,PATCH,DELETE,POST,PUT,FETCH",
                    },
                    {
                        key: "Access-Control-Allow-Headers",
                        value:
                        "X-CSRF-Token, X-Requested-With, Accept, Accept-Version, Content-Length, Content-MD5, Content-Type," +
                            "Date, X-Api-Version, X-ACCESS_TOKEN, Access-Control-Allow-Origin, Authorization, Origin, " +
                            "x-requested-with, Content-Range, Content-Disposition, Content-Description"
                    },
                ]
            }
        ]
    }
}

module.exports = nextConfig
