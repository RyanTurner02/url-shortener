import { Button } from "@/components/ui/button";
import { CopyIcon } from "lucide-react";

interface CopyButtonProps {
  text: string;
  copy: (text: string) => void;
}

export default function CopyButton({ text, copy }: CopyButtonProps) {
  return (
    <Button className="cursor-pointer" type="button" onClick={() => copy(text)}>
      Copy <CopyIcon />
    </Button>
  );
}
