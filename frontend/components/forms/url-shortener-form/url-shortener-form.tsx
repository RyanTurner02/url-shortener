"use client";

import { FieldSet } from "@/components/ui/field";
import useUrlShortenerForm from "@/hooks/use-url-shortener-form";
import OriginalUrlField from "@/components/forms/url-shortener-form/fields/original-url-field";
import ShortenedUrlField from "@/components/forms/url-shortener-form/fields/shortened-url-field";
import SubmitButton from "@/components/forms/url-shortener-form/buttons/submit-button";

export const UrlShortenerForm = () => {
  const { form, onSubmit } = useUrlShortenerForm();

  return (
    <form
      className="p-10"
      id="url-shortener-form"
      onSubmit={form.handleSubmit(onSubmit)}
    >
      <FieldSet>
        <OriginalUrlField control={form.control} />
        <SubmitButton />
        <ShortenedUrlField control={form.control} />
      </FieldSet>
    </form>
  );
};
