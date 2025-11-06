import { Button } from "@/components/ui/button";
import { Field, FieldGroup, FieldLabel, FieldSet } from "@/components/ui/field";
import { Input } from "@/components/ui/input"

export default function Home() {
  return (
    <div className="">
      <main className="">
        <h1 className="text-4xl font-bold text-center">URL Shortener</h1>
        <FieldSet>
          <FieldGroup>
            <Field>
              <FieldLabel htmlFor="url">Paste your long link here</FieldLabel>
              <Input id="url" type="text" placeholder="https://example.com/long-url-here" />
            </Field>
          </FieldGroup>
        </FieldSet>
        <Button className="cursor-pointer" variant="outline">Submit</Button>
      </main>
    </div>
  );
}
