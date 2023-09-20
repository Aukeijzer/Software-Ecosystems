import Image from 'next/image';
import Link from 'next/link';
import logo from '../public/logo.png';

export default function navLogo(): JSX.Element{
    return (
        <div className='flex justify-between w-full'>
            <Link href="/" className="flex">
                <Image
                    src={logo}
                    width={80}
                    height={80}
                    alt="SECODash Logo"
                    className='flex'
                />
                <span className='flex self-center text-lg'>
                    EcoDash
                </span>
            </Link>
        </div>
    );
}