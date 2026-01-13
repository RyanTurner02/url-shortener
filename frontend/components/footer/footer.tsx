import Link from "next/link";

export default function Footer() {
  return (
    <footer className="flex flex-wrap justify-center w-full gap-6 py-5 border-t-2 bg-gray-50">
      <Link className="text-link" href="/">
        URL Shortener
      </Link>
      <Link className="text-link" href="/about">
        About
      </Link>
      <Link
        className="text-link"
        href="https://github.com/RyanTurner02/url-shortener"
        target="_blank"
      >
        GitHub
      </Link>
    </footer>
  );
}
