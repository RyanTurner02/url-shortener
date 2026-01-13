import { UrlShortenerForm } from "@/components/forms/url-shortener-form/url-shortener-form";
import Footer from "@/components/footer/footer";

export default function Home() {
  return (
    <div className="flex flex-col items-center justify-between h-screen">
      <main className="w-full mt-10 max-w-7xl">
        <h1 className="mb-5 text-4xl font-bold text-center sm:mb-0">
          URL Shortener
        </h1>
        <UrlShortenerForm />
      </main>
      <Footer />
    </div>
  );
}
