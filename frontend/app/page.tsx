import { Button } from "@/components/ui/button";
import { Field, FieldGroup, FieldLabel, FieldSet } from "@/components/ui/field";
import { Input } from "@/components/ui/input"

export default function Home() {
  return (
    <div className="h-screen flex justify-center ">
      <main className="w-full max-w-3xl">
        <h1 className="p-10 text-4xl font-bold text-center">URL Shortener</h1>
        <div className="p-10">
          <FieldSet>
            <FieldGroup>
              <Field>
                <FieldLabel htmlFor="url">Paste your long link here</FieldLabel>
                <Input id="url" type="text" placeholder="https://example.com/long-url-here" />
              </Field>
            </FieldGroup>
            <Button className="cursor-pointer" variant="outline">Submit</Button>
            <FieldGroup>
              <Field>
                <FieldLabel htmlFor="shortened-url">Shortened URL</FieldLabel>
                <Input className="text-red-500" id="shortened-url" type="text" placeholder="https://url-shortener-example.com/shortened-url" readOnly />
              </Field>
            </FieldGroup>
          </FieldSet>
        </div>
      </main>
    </div>
  );
}
