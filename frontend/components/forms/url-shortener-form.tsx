import { Button } from "@/components/ui/button";
import { Field, FieldGroup, FieldLabel, FieldSet } from "@/components/ui/field";
import { Input } from "@/components/ui/input";

export const UrlShortenerForm = () => {
  return (
    <div className="p-10">
      <FieldSet>
        <FieldGroup>
          <Field>
            <FieldLabel htmlFor="url">Paste your long link here</FieldLabel>
            <Input
              id="url"
              type="text"
              placeholder="https://example.com/long-url-here"
            />
          </Field>
        </FieldGroup>
        <Button className="cursor-pointer" variant="outline">
          Submit
        </Button>
        <FieldGroup>
          <Field>
            <FieldLabel htmlFor="shortened-url">Shortened URL</FieldLabel>
            <Input
              className="text-red-500"
              id="shortened-url"
              type="text"
              placeholder="https://url-shortener-example.com/shortened-url"
              readOnly
            />
          </Field>
        </FieldGroup>
      </FieldSet>
    </div>
  );
};
