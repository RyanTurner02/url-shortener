import { Metadata } from "next";
import Link from "next/link";

export const metadata: Metadata = {
  title: "About"
}

export default function About() {
  return (
    <div className="flex flex-col items-center px-3 md:p-0">
      <section className="w-full max-w-3xl mb-8">
        <h1 className="mb-2 text-4xl font-bold text-center">About</h1>
        <p className="mb-4 text-pretty">
          URL Shortener is a web app implementation of a URL shortener.
        </p>

        <p className="mb-4 text-pretty">
          A URL shortener is a tool that takes a long web address and converts
          it into a shorter, easier-to-share link. When someone clicks a
          shortened URL, they are automatically redirected to the original, long
          URL.
        </p>

        <p className="mb-4 text-pretty">
          URL Shorteners are commonly used to make links more readable, save
          space, and track analytics.
        </p>

        <p>Popular websites that use URL shortening include:</p>
        <ul className="pl-8 mb-4 list-disc">
          <li>
            Twitter/X, which uses{" "}
            <Link className="text-link" href="https://t.co/" target="_blank">
              t.co
            </Link>{" "}
            for sharing links
          </li>
          <li>
            LinkedIn, which uses{" "}
            <Link className="text-link" href="https://lnkd.in/" target="_blank">
              lnkd.in
            </Link>{" "}
            for sharing links
          </li>
          <li>
            YouTube, which uses{" "}
            <Link
              className="text-link"
              href="https://youtu.be/"
              target="_blank"
            >
              youtu.be
            </Link>{" "}
            for sharing videos
          </li>
        </ul>
      </section>
    </div>
  );
}
