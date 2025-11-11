"use client";

import { Button } from "@/components/ui/button";
import {
  Field,
  FieldError,
  FieldGroup,
  FieldLabel,
  FieldSet,
} from "@/components/ui/field";
import { Input } from "@/components/ui/input";
import useCreateShortenedUrl from "@/hooks/use-create-shortened-url";
import { zodResolver } from "@hookform/resolvers/zod";
import { Controller, useForm } from "react-hook-form";
import * as z from "zod";

interface ShortUrlResponse {
  shortUrl: string;
}

const formSchema = z.object({
  originalUrl: z.url(),
  shortenedUrl: z.string().optional(),
});

export const UrlShortenerForm = () => {
  const form = useForm<z.infer<typeof formSchema>>({
    resolver: zodResolver(formSchema),
    defaultValues: {
      originalUrl: "",
      shortenedUrl: "",
    },
  });

  const createShortUrlMutation = useCreateShortenedUrl();

  const onSubmit = async (data: z.infer<typeof formSchema>) => {
    const result: ShortUrlResponse = await createShortUrlMutation(data.originalUrl);

    if (result) {
      form.setValue("shortenedUrl", result.shortUrl);
    }
  };

  return (
    <form
      className="p-10"
      id="url-shortener-form"
      onSubmit={form.handleSubmit(onSubmit)}
    >
      <FieldSet>
        <FieldGroup>
          <Controller
            name="originalUrl"
            control={form.control}
            render={({ field, fieldState }) => (
              <Field data-invalid={fieldState.invalid}>
                <FieldLabel htmlFor="original-url">
                  Paste your long link here
                </FieldLabel>
                <Input
                  {...field}
                  id="original-url"
                  aria-invalid={fieldState.invalid}
                  placeholder="https://example.com/long-url-here"
                />
                {fieldState.invalid && (
                  <FieldError errors={[fieldState.error]} />
                )}
              </Field>
            )}
          />
        </FieldGroup>
        <Button
          className="cursor-pointer"
          type="submit"
          form="url-shortener-form"
        >
          Submit
        </Button>
        <FieldGroup>
          <Controller
            name="shortenedUrl"
            control={form.control}
            render={({ field }) => (
              <Field>
                <FieldLabel htmlFor="shortened-url">Shortened URL</FieldLabel>
                <Input
                  {...field}
                  id="shortened-url"
                  type="text"
                  placeholder="https://url-shortener-example.com/shortened-url"
                  readOnly
                />
              </Field>
            )}
          />
        </FieldGroup>
      </FieldSet>
    </form>
  );
};
