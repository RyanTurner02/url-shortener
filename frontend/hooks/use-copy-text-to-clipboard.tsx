import { toast } from "sonner";

export default function useCopyToClipboard() {
  const copy = async (text: string) => {
    if (!text) return;

    try {
      await navigator.clipboard.writeText(text);
      toast.success("Link copied to clipboard.");
    } catch {
      toast.error("Unable to copy link to clipboard.");
    }
  };

  return { copy };
}
