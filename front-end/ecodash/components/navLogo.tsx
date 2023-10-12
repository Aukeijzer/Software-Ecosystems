import Image from 'next/image';
import Link from 'next/link';
import logo from '../public/logo.png';

export default function navLogo(): JSX.Element{
    return (
        <div className='flex justify-between w-full' data-cy="navLogo">
            <Link href="/" className="flex" data-cy="navLogoLink">
                <Image
                    src={logo}
                    width={80}
                    height={80}
                    alt="SECODash Logo"
                    className='flex'
                    data-cy="navLogoImage"
                />
                <span className='flex self-center text-lg' data-cy="navLogoText">
                    SECODash
                </span>
            </Link>
        </div>
    );
}