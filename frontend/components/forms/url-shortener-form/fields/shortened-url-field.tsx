import { Field, FieldLabel } from "@/components/ui/field";
import { Input } from "@/components/ui/input";
import { UrlShortenerFormValues } from "@/schemas/url-shortener-form-schema";
import { Control, Controller } from "react-hook-form";
import CopyButton from "@/components/forms/url-shortener-form/buttons/copy-button";
import useCopyToClipboard from "@/hooks/use-copy-text-to-clipboard";

interface ShortenedUrlFieldProps {
  control: Control<UrlShortenerFormValues>;
}

export default function ShortenedUrlField({ control }: ShortenedUrlFieldProps) {
  const { copy } = useCopyToClipboard();

  return (
    <Controller
      name="shortenedUrl"
      control={control}
      render={({ field }) => (
        <Field>
          <FieldLabel htmlFor="shortened-url">Shortened URL</FieldLabel>
          <div className="grid grid-cols-5 gap-4">
            <Input
              {...field}
              className="col-span-4"
              id="shortened-url"
              type="text"
              placeholder="https://url-shortener-example.com/shortened-url"
              readOnly
            />
            <CopyButton text={field.value ?? ""} copy={copy} />
          </div>
        </Field>
      )}
    />
  );
}
