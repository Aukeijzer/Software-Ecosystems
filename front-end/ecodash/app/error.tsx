"use client"
//Code from NextJS error page https://nextjs.org/docs/app/building-your-application/routing/error-handling

import { useEffect } from 'react'
 
export default function Error({
  error,
  reset,
}: {
  error: Error & { digest?: string }
  reset: () => void
}) {
  useEffect(() => {
    // Log the error to an error reporting service
    console.error(error)
  }, [error])
 
  return (
    <div>
      <h2>Something went wrong!</h2>
      <h2 className='text-3xl m-5'>Something went wrong!</h2>
      <div className='flex flex-col gap-3 p-5 bg-gray-300 border-2 m-5 border-gray-900 rounded-sm'>
        <p>
          {error.message}
        </p>
        <p className='bg-red-300 border-2 border-red-500 '>
          {error.stack}
        </p>
      </div>
   
      <button
        onClick={
          // Attempt to recover by trying to re-render the segment
          () => reset()
        }
        className='bg-gray-500 border-2 border-gray-900 p-2 ml-5'
      >
        Try again
      </button>
    </div>
  )
}

