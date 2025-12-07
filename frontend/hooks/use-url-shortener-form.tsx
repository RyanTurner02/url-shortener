import useCreateShortenedUrl from "@/hooks/use-create-shortened-url";
import {
  defaultUrlShortenerFormValues,
  urlShortenerFormSchema,
  UrlShortenerFormValues,
} from "@/schemas/url-shortener-form-schema";
import { IShortUrlResponse } from "@/responses/short-url-response";
import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import { useRef } from "react";

export default function useUrlShortenerForm() {
  const form = useForm<UrlShortenerFormValues>({
    resolver: zodResolver(urlShortenerFormSchema),
    defaultValues: defaultUrlShortenerFormValues,
  });
  const previousOriginalUrl = useRef("");
  const createShortUrlMutation = useCreateShortenedUrl();

  const onSubmit = async (data: UrlShortenerFormValues) => {
    if (data.originalUrl === previousOriginalUrl.current) {
      return;
    }

    previousOriginalUrl.current = data.originalUrl;
    
    const result: IShortUrlResponse = await createShortUrlMutation(
      data.originalUrl
    );

    if (result) {
      form.setValue("shortenedUrl", result.shortUrl);
    }
  };

  return { form, onSubmit };
}
