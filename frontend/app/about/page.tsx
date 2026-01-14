import Link from "next/link";

export default function About() {
  return (
    <div className="flex flex-col items-center px-3 md:p-0">
      <section className="w-full max-w-3xl mb-8">
        <h1 className="mb-2 text-4xl font-bold text-center">About</h1>
        <p>URL Shortener is a web app implementation of a URL shortener.</p>
      </section>

      <section className="w-full max-w-3xl">
        <h2 className="mb-2 text-3xl font-bold text-center">
          What is a URL Shortener?
        </h2>
        <p className="mb-4 text-pretty">
          A URL shortener is a tool that takes a long web address and converts
          it into a shorter link that is easy to share. When someone clicks a
          shortened URL, they are redirected to the original, long URL.
        </p>

        <p>Popular websites that use URL shortening:</p>
        <ul className="pl-8 mb-4 list-disc">
          <li>
            Twitter/X has{" "}
            <Link className="text-link" href="https://t.co/" target="_blank">
              t.co
            </Link>{" "}
            for sharing links
          </li>
          <li>
            LinkedIn has{" "}
            <Link className="text-link" href="https://lnkd.in/" target="_blank">
              lnkd.in
            </Link>{" "}
            for sharing links
          </li>
          <li>
            YouTube has{" "}
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
