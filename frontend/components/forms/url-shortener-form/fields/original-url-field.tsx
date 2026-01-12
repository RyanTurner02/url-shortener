import { Field, FieldLabel, FieldError } from "@/components/ui/field";
import { Input } from "@/components/ui/input";
import { UrlShortenerFormValues } from "@/schemas/url-shortener-form-schema";
import { Control, Controller } from "react-hook-form";

interface OriginalUrlFieldProps {
  control: Control<UrlShortenerFormValues>;
}

export default function OriginalUrlField({ control }: OriginalUrlFieldProps) {
  return (
    <Controller
      name="originalUrl"
      control={control}
      render={({ field, fieldState }) => (
        <Field data-invalid={fieldState.invalid}>
          <FieldLabel htmlFor="original-url">
            Paste your long link here
          </FieldLabel>
          <Input
            {...field}
            id="original-url"
            aria-invalid={fieldState.invalid}
            placeholder="https://example.com/"
          />
          {fieldState.invalid && <FieldError errors={[fieldState.error]} />}
        </Field>
      )}
    />
  );
}
