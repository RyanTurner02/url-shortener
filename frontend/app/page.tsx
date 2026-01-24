import { UrlShortenerForm } from "@/components/forms/url-shortener-form/url-shortener-form";

export default function Home() {
  return (
    <>
      <h1 className="mb-5 text-4xl font-bold text-center sm:mb-0">
        URL Shortener
      </h1>
      <UrlShortenerForm />
    </>
  );
}
