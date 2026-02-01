import * as z from "zod";
import { isWebUri } from "valid-url";

export const urlShortenerFormSchema = z.object({
  originalUrl:
    z.string()
      .trim()
      .max(128, "URL must not exceed 128 characters.")
      .refine(x => isWebUri(x), { error: "Invalid URL." }),
  shortenedUrl: z.string().optional(),
});

export type UrlShortenerFormValues = z.infer<typeof urlShortenerFormSchema>;

export const defaultUrlShortenerFormValues: UrlShortenerFormValues = {
  originalUrl: "",
  shortenedUrl: "",
};