import useCreateShortenedUrl from "@/hooks/use-create-shortened-url";
import {
  defaultUrlShortenerFormValues,
  urlShortenerFormSchema,
  UrlShortenerFormValues,
} from "@/schemas/url-shortener-form-schema";
import { ShortUrlResponse } from "@/responses/short-url-response";
import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";
import { useRef } from "react";
import { toast } from "sonner";
import { ShortUrlResponseCodes } from "@/enums/short-url-response-codes";

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

    const result: ShortUrlResponse = await createShortUrlMutation(data.originalUrl);

    if (result.code === ShortUrlResponseCodes.Success) {
      toast.success(result.message);
      form.setValue("shortenedUrl", result.shortUrl);
      canResubmit.current = false;
    } else if (result.code === ShortUrlResponseCodes.InvalidUrl) {
      toast.error(result.message);
      canResubmit.current = false;
    } else if (result.code === ShortUrlResponseCodes.DuplicateConflict) {
      toast.error(result.message);
      canResubmit.current = true;
    } else if (result.code === ShortUrlResponseCodes.NullShortUrl) {
      toast.error(result.message);
      canResubmit.current = true;
    }
  };

  return { form, onSubmit };
}
