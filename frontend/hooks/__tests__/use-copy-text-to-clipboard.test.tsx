import { act, renderHook } from "@testing-library/react";
import useCopyToClipboard from "@/hooks/use-copy-text-to-clipboard";
import { toast } from "sonner";

describe("useCopyTextToClipboard", () => {
  beforeEach(() => {
    vi.clearAllMocks();

    Object.defineProperty(navigator, "clipboard", {
      value: { writeText: vi.fn() },
      configurable: true,
    });
  });

  it("copies text to clipboard and shows success toast", async () => {
    const clipboardSpy = vi
      .spyOn(navigator.clipboard, "writeText")
      .mockResolvedValue();
    const toastSpy = vi.spyOn(toast, "success");
    const exampleUrl = "https://example.com/";

    const { result } = renderHook(() => useCopyToClipboard());
    const { copy } = result.current;

    await act(async () => {
      await copy(exampleUrl);
    });

    expect(clipboardSpy).toBeCalledWith(exampleUrl);
    expect(clipboardSpy).toBeCalledTimes(1);

    expect(toastSpy).toBeCalledWith("Link copied to clipboard.");
    expect(toastSpy).toBeCalledTimes(1);
  });

  it("fails to copy text to clipboard and shows error toast", async () => {
    const clipboardSpy = vi
      .spyOn(navigator.clipboard, "writeText")
      .mockRejectedValue(new Error());
    const toastSpy = vi.spyOn(toast, "error");
    const exampleUrl = "https://example.com/";

    const { result } = renderHook(() => useCopyToClipboard());
    const { copy } = result.current;

    await act(async () => {
      await copy(exampleUrl);
    });

    expect(clipboardSpy).toBeCalledWith(exampleUrl);
    expect(clipboardSpy).toBeCalledTimes(1);

    expect(toastSpy).toBeCalledWith("Unable to copy link to clipboard.");
    expect(toastSpy).toBeCalledTimes(1);
  });

  it("does nothing when there is no text to copy", async () => {
    const spy = vi.spyOn(navigator.clipboard, "writeText").mockResolvedValue();

    const { result } = renderHook(() => useCopyToClipboard());
    const { copy } = result.current;

    await act(async () => {
      await copy("");
    });

    expect(spy).toBeCalledTimes(0);
  });
});
