import { act, renderHook } from "@testing-library/react";
import useUrlShortenerForm from "@/hooks/use-url-shortener-form";
import { QueryProviderWrapper } from "@/test-utils";
import { ShortUrlResponse } from "@/responses/short-url-response";
import { DuplicateConflictResponse } from "@/responses/duplicate-conflict-response";

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
    mockCreateShortUrl.mockResolvedValue(new ShortUrlResponse("ShortUrl"));

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
    mockCreateShortUrl.mockResolvedValue(new ShortUrlResponse("ShortUrl"));

    const exampleUrl = "https://example.com";
    const { result } = renderHook(() => useUrlShortenerForm(), {
      wrapper: QueryProviderWrapper,
    });
    const { form, onSubmit } = result.current;

    await act(async () => {
      await onSubmit({ originalUrl: exampleUrl });
    });

    await act(async () => {
      await onSubmit({ originalUrl: exampleUrl });
    });

    expect(mockCreateShortUrl).toHaveBeenCalledWith(exampleUrl);
    expect(mockCreateShortUrl).toHaveBeenCalledOnce();

    expect(form.getValues().shortenedUrl).toBe("ShortUrl");
  });

  it("allows resubmitting when there is an error", async () => {
    const exampleUrl = "https://example.com";
    const { result } = renderHook(() => useUrlShortenerForm(), {
      wrapper: QueryProviderWrapper,
    });
    const { form, onSubmit } = result.current;

    mockCreateShortUrl.mockResolvedValue(
      new DuplicateConflictResponse(
        "DuplicateConflict",
        "Failed to generate a unique short URL. Please try again later."
      )
    );
    await act(async () => {
      await onSubmit({ originalUrl: exampleUrl });
    });

    mockCreateShortUrl.mockResolvedValue(new ShortUrlResponse("ShortUrl"));
    await act(async () => {
      await onSubmit({ originalUrl: exampleUrl });
    });

    expect(mockCreateShortUrl).toHaveBeenCalledWith(exampleUrl);
    expect(mockCreateShortUrl).toHaveBeenCalledTimes(2);

    expect(form.getValues().shortenedUrl).toBe("ShortUrl");
  });
});
