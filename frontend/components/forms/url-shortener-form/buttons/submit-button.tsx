import { Button } from "@/components/ui/button";

export default function SubmitButton() {
  return (
    <Button className="cursor-pointer" type="submit" form="url-shortener-form">
      Submit
    </Button>
  );
}
