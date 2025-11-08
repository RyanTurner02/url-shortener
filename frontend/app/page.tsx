"use client";

import { getOriginalUrl } from "@/api/get-original-url";
import { UrlShortenerForm } from "@/components/forms/url-shortener-form";
import { useEffect } from "react";

export default function Home() {
  useEffect(() => {
    const geturl = async () => {
      await getOriginalUrl("http://localhost:5000/xC29EF4");
    }

    geturl();
  }, []);

  return (
    <div className="h-screen flex justify-center ">
      <main className="w-full max-w-3xl">
        <h1 className="p-10 text-4xl font-bold text-center">URL Shortener</h1>
        <UrlShortenerForm />
      </main>
    </div>
  );
}
