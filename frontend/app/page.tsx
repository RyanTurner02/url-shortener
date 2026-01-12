import { UrlShortenerForm } from "@/components/forms/url-shortener-form/url-shortener-form";
import Footer from "@/components/footer/footer";

export default function Home() {
  return (
    <div className="flex flex-col items-center justify-between h-screen py-5">
      <main className="w-full max-w-7xl">
        <h1 className="mb-5 sm:mb-0 text-4xl font-bold text-center">URL Shortener</h1>
        <UrlShortenerForm />
      </main>
      <Footer />
    </div>
  );
}
