import Link from "next/link";

export default function NotFound() {
  return (
    <div className="flex flex-col items-center px-2 sm:p-0">
      <section className="p-4 mb-4 text-center">
        <h1 className="mb-2 text-4xl font-bold text-center">404 Not Found</h1>
        <p>The page you are looking for does not exist.</p>
        <Link className="text-link" href="/">
          Return Home
        </Link>
      </section>
    </div>
  );
}
