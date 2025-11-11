import { createShortUrl } from "@/api/create-short-url";
import { useMutation } from "@tanstack/react-query";

export default function useCreateShortenedUrl() {
  const { mutateAsync: createShortUrlMutation } = useMutation({
    mutationFn: createShortUrl,
  });

  return createShortUrlMutation;
}
