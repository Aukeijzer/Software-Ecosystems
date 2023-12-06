import type { Config } from 'tailwindcss'

const config: Config = {
  content: [
    './pages/**/*.{js,ts,jsx,tsx,mdx}',
    './components/**/*.{js,ts,jsx,tsx,mdx}',
    './app/**/*.{js,ts,jsx,tsx,mdx}',
    "./node_modules/flowbite/**/*.js",
    './app/*.css'
  ],
  theme: {
    colors: {
      'logoColor': '#0f766e',
      'odin': '#015B6D',
      'odinAccent': '#143240',
      'amber': '#F5F8FA',

    }
  },
  plugins: [
    require('flowbite/plugin')
  ],
}
export default config
