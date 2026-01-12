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
import { NullShortUrlResponse } from "@/responses/null-short-url-response";
import { toast } from "sonner";

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
      toast.info(`Short URL for "${data.originalUrl}" has already been created.`);
      return;
    }

    previousOriginalUrl.current = data.originalUrl;

    const result:
      | ShortUrlResponse
      | DuplicateConflictResponse
      | NullShortUrlResponse = await createShortUrlMutation(data.originalUrl);

    if (result instanceof ShortUrlResponse) {
      toast.success("Short URL has been created.");
      form.setValue("shortenedUrl", result.shortUrl);
      canResubmit.current = false;
    } else if (result instanceof DuplicateConflictResponse) {
      toast.error(result.message);
      canResubmit.current = true;
    } else if (result instanceof NullShortUrlResponse) {
      toast.error(result.message);
      canResubmit.current = true;
    }
  };

  return { form, onSubmit };
}
