import { Button } from "@/components/ui/button";
import { CopyIcon } from "lucide-react";

export default function CopyButton() {
  return (
    <Button className="cursor-pointer" type="button">
      Copy <CopyIcon />
    </Button>
  );
}
