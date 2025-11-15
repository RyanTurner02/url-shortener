import { UrlShortenerForm } from "@/components/forms/url-shortener-form/url-shortener-form";

export default function Home() {
  return (
    <div className="h-screen flex justify-center ">
      <main className="w-full max-w-3xl">
        <h1 className="p-10 text-4xl font-bold text-center">URL Shortener</h1>
        <UrlShortenerForm />
      </main>
    </div>
  );
}
