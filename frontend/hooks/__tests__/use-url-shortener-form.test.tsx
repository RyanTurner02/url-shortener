import { act, renderHook } from "@testing-library/react";
import useUrlShortenerForm from "@/hooks/use-url-shortener-form";
import { QueryProviderWrapper } from "@/test-utils";

const mockCreateShortUrl = vi.fn();

vi.mock("@/hooks/use-create-shortened-url", () => ({
  default: () => mockCreateShortUrl,
}));

describe("useUrlShortenerForm", () => {
  beforeEach(() => {
    mockCreateShortUrl.mockReset();
  });

  it("initializes form with default values", () => {
    const { result } = renderHook(() => useUrlShortenerForm(), {
      wrapper: QueryProviderWrapper,
    });
    const { form } = result.current;

    const formValues = form.getValues();

    expect(formValues.originalUrl).toBe("");
    expect(formValues.shortenedUrl).toBe("");
  });

  it("updates short url on submit", async () => {
    mockCreateShortUrl.mockResolvedValue({ shortUrl: "ShortUrl" });

    const { result } = renderHook(() => useUrlShortenerForm(), {
      wrapper: QueryProviderWrapper,
    });
    const { form, onSubmit } = result.current;

    await act(async () => {
      await onSubmit({
        originalUrl: "https://example.com",
        shortenedUrl: "",
      });
    });

    expect(mockCreateShortUrl).toHaveBeenCalledWith("https://example.com");
    expect(mockCreateShortUrl).toHaveBeenCalledOnce();

    expect(form.getValues().shortenedUrl).toBe("ShortUrl");
  });

  it("does not resubmit the same original url", async () => {
    mockCreateShortUrl.mockResolvedValue({ shortUrl: "ShortUrl" });

    const exampleUrl = "https://example.com";
    const { result } = renderHook(() => useUrlShortenerForm(), {
      wrapper: QueryProviderWrapper,
    });
    const { form, onSubmit } = result.current;

    await act(async () => {
      await onSubmit({ originalUrl: exampleUrl });
    });

    await act(async () => {
      await onSubmit({ originalUrl: exampleUrl })
    })

    expect(mockCreateShortUrl).toHaveBeenCalledWith(exampleUrl);
    expect(mockCreateShortUrl).toHaveBeenCalledOnce();

    expect(form.getValues().shortenedUrl).toBe("ShortUrl");
  });
});
