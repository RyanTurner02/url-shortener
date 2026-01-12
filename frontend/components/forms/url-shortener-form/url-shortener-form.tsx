"use client";

import { FieldSet } from "@/components/ui/field";
import useUrlShortenerForm from "@/hooks/use-url-shortener-form";
import OriginalUrlField from "@/components/forms/url-shortener-form/fields/original-url-field";
import ShortenedUrlField from "@/components/forms/url-shortener-form/fields/shortened-url-field";
import SubmitButton from "@/components/forms/url-shortener-form/buttons/submit-button";
import { Toaster } from "sonner";

export const UrlShortenerForm = () => {
  const { form, onSubmit } = useUrlShortenerForm();

  return (
    <form
      className="px-3 sm:px-16 md:px-32 lg:px-48 xl:px-60 2xl:px-72"
      id="url-shortener-form"
      onSubmit={form.handleSubmit(onSubmit)}
    >
      <FieldSet>
        <OriginalUrlField control={form.control} />
        <SubmitButton />
        <ShortenedUrlField control={form.control} />
      </FieldSet>
      <Toaster richColors />
    </form>
  );
};
