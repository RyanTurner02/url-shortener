import { UrlShortenerForm } from "@/components/forms/url-shortener-form/url-shortener-form";
import Link from "next/link";

export default function Home() {
  return (
    <div className="flex flex-col items-center justify-between h-screen py-5">
      <main className="w-full max-w-7xl">
        <h1 className="mb-5 sm:mb-0 text-4xl font-bold text-center">URL Shortener</h1>
        <UrlShortenerForm />
      </main>
      <footer className="flex flex-wrap gap-6">
        <Link className="text-link" href="/">URL Shortener</Link>
        <Link className="text-link" href="/about">About</Link>
        <Link className="text-link" href="https://github.com/RyanTurner02/url-shortener" target="_blank">GitHub</Link>
      </footer>
    </div>
  );
}
