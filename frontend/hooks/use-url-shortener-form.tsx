import useCreateShortenedUrl from "@/hooks/use-create-shortened-url";
import {
  defaultUrlShortenerFormValues,
  urlShortenerFormSchema,
  UrlShortenerFormValues,
} from "@/schemas/url-shortener-form-schema";
import { ShortUrlResponse } from "@/responses/short-url-response";
import { DuplicateConflictResponse } from "@/responses/duplicate-conflict-response";
import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import { useRef } from "react";

export default function useUrlShortenerForm() {
  const form = useForm<UrlShortenerFormValues>({
    resolver: zodResolver(urlShortenerFormSchema),
    defaultValues: defaultUrlShortenerFormValues,
  });
  const canResubmit = useRef(false);
  const previousOriginalUrl = useRef("");
  const createShortUrlMutation = useCreateShortenedUrl();

  const onSubmit = async (data: UrlShortenerFormValues) => {
    if (!canResubmit.current && data.originalUrl === previousOriginalUrl.current) {
      return;
    }

    previousOriginalUrl.current = data.originalUrl;
    
    const result: ShortUrlResponse | DuplicateConflictResponse = await createShortUrlMutation(
      data.originalUrl
    );

    if ("shortUrl" in result) {
      form.setValue("shortenedUrl", result.shortUrl);
      canResubmit.current = false;
    } else if ("error" in result) {
      // TODO: Display toast with error message.
      canResubmit.current = true;
    }
  };

  return { form, onSubmit };
}
