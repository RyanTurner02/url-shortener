import useCreateShortenedUrl from "@/hooks/use-create-shortened-url";
import {
  urlShortenerFormSchema,
  UrlShortenerFormValues,
} from "@/schemas/url-shortener-form-schema";
import { IShortUrlResponse } from "@/types/url-shortener-types";
import { zodResolver } from "@hookform/resolvers/zod";
import { useForm } from "react-hook-form";

export default function useUrlShortenerForm() {
  const form = useForm<UrlShortenerFormValues>({
    resolver: zodResolver(urlShortenerFormSchema),
    defaultValues: {
      originalUrl: "",
      shortenedUrl: "",
    },
  });

  const createShortUrlMutation = useCreateShortenedUrl();

  const onSubmit = async (data: UrlShortenerFormValues) => {
    const result: IShortUrlResponse = await createShortUrlMutation(
      data.originalUrl
    );

    if (result) {
      form.setValue("shortenedUrl", result.shortUrl);
    }
  };

  return { form, onSubmit };
}
