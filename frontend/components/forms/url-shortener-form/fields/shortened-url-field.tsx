import { Field, FieldLabel } from "@/components/ui/field";
import { Input } from "@/components/ui/input";
import { UrlShortenerFormValues } from "@/schemas/url-shortener-form-schema";
import { Control, Controller } from "react-hook-form";

interface ShortenedUrlFieldProps {
  control: Control<UrlShortenerFormValues>;
}

export default function ShortenedUrlField({ control }: ShortenedUrlFieldProps) {
  return (
    <Controller
      name="shortenedUrl"
      control={control}
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
  );
}
