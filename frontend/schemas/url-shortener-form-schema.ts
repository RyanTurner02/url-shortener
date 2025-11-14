import * as z from "zod";

export const urlShortenerFormSchema = z.object({
  originalUrl: z.url(),
  shortenedUrl: z.string().optional(),
});

export type UrlShortenerFormValues = z.infer<typeof urlShortenerFormSchema>;