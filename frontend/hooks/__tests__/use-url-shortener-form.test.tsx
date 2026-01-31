import { act, renderHook } from "@testing-library/react";
import useUrlShortenerForm from "@/hooks/use-url-shortener-form";
import { QueryProviderWrapper } from "@/test-utils";
import { ShortUrlResponse } from "@/responses/short-url-response";
import { ShortUrlResponseCodes } from "@/enums/short-url-response-codes";
import { ShortUrlResponseConstants } from "@/constants/short-url-response-constants";
import { toast } from "sonner";

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
    const toastSpy = vi.spyOn(toast, "success");

    mockCreateShortUrl.mockResolvedValue(
      new ShortUrlResponse(
        ShortUrlResponseCodes.Success,
        ShortUrlResponseConstants.SUCCESS_MESSAGE,
        "ShortUrl"));

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

    expect(toastSpy).toHaveBeenCalledWith(ShortUrlResponseConstants.SUCCESS_MESSAGE);
    expect(toastSpy).toHaveBeenCalledOnce();

    expect(mockCreateShortUrl).toHaveBeenCalledWith("https://example.com");
    expect(mockCreateShortUrl).toHaveBeenCalledOnce();

    expect(form.getValues().shortenedUrl).toBe("ShortUrl");
  });

  it("does not resubmit the same original url", async () => {
    const successToastSpy = vi.spyOn(toast, "success");
    const infoToastSpy = vi.spyOn(toast, "info");

    mockCreateShortUrl.mockResolvedValue(new ShortUrlResponse(
      ShortUrlResponseCodes.Success,
      ShortUrlResponseConstants.SUCCESS_MESSAGE,
      "ShortUrl"));

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

    expect(successToastSpy).toHaveBeenCalledWith(ShortUrlResponseConstants.SUCCESS_MESSAGE);
    expect(successToastSpy).toHaveBeenCalledOnce();

    expect(infoToastSpy).toHaveBeenCalledWith(`Short URL for "${exampleUrl}" has already been created.`);
    expect(infoToastSpy).toHaveBeenCalledOnce();

    expect(mockCreateShortUrl).toHaveBeenCalledWith(exampleUrl);
    expect(mockCreateShortUrl).toHaveBeenCalledOnce();

    expect(form.getValues().shortenedUrl).toBe("ShortUrl");
  });

    it("displays an error toast with the invalid original url message", async () => {
    const errorToastSpy = vi.spyOn(toast, "error");

    mockCreateShortUrl.mockResolvedValue(new ShortUrlResponse(
      ShortUrlResponseCodes.InvalidUrl,
      ShortUrlResponseConstants.INVALID_URL_MESSAGE));

    const invalidUrl = "InvalidUrl";
    const { result } = renderHook(() => useUrlShortenerForm(), {
      wrapper: QueryProviderWrapper,
    });
    const { onSubmit } = result.current;

    await act(async () => {
      await onSubmit({ originalUrl: invalidUrl });
    });

    expect(errorToastSpy).toHaveBeenCalledWith(ShortUrlResponseConstants.INVALID_URL_MESSAGE);
    expect(errorToastSpy).toHaveBeenCalledOnce();

    expect(mockCreateShortUrl).toHaveBeenCalledWith(invalidUrl);
    expect(mockCreateShortUrl).toHaveBeenCalledOnce();
  });

  it("allows resubmitting when there is a duplicate error", async () => {
    const successToastSpy = vi.spyOn(toast, "success");
    const errorToastSpy = vi.spyOn(toast, "error");

    const exampleUrl = "https://example.com";
    const { result } = renderHook(() => useUrlShortenerForm(), {
      wrapper: QueryProviderWrapper,
    });
    const { form, onSubmit } = result.current;

    mockCreateShortUrl.mockResolvedValue(
      new ShortUrlResponse(
        ShortUrlResponseCodes.DuplicateConflict,
        ShortUrlResponseConstants.DUPLICATE_CONFLICT_MESSAGE
      )
    );
    await act(async () => {
      await onSubmit({ originalUrl: exampleUrl });
    });

    mockCreateShortUrl.mockResolvedValue(
      new ShortUrlResponse(
        ShortUrlResponseCodes.Success,
        ShortUrlResponseConstants.SUCCESS_MESSAGE,
        "ShortUrl"));
    await act(async () => {
      await onSubmit({ originalUrl: exampleUrl });
    });

    expect(successToastSpy).toHaveBeenCalledWith(ShortUrlResponseConstants.SUCCESS_MESSAGE);
    expect(successToastSpy).toHaveBeenCalledOnce();

    expect(errorToastSpy).toHaveBeenCalledWith(ShortUrlResponseConstants.DUPLICATE_CONFLICT_MESSAGE);
    expect(errorToastSpy).toHaveBeenCalledOnce();

    expect(mockCreateShortUrl).toHaveBeenCalledWith(exampleUrl);
    expect(mockCreateShortUrl).toHaveBeenCalledTimes(2);

    expect(form.getValues().shortenedUrl).toBe("ShortUrl");
  });

  it("allows resubmitting when there is a null error", async () => {
    const successToastSpy = vi.spyOn(toast, "success");
    const errorToastSpy = vi.spyOn(toast, "error");

    const exampleUrl = "https://example.com";
    const { result } = renderHook(() => useUrlShortenerForm(), {
      wrapper: QueryProviderWrapper,
    });
    const { form, onSubmit } = result.current;

    mockCreateShortUrl.mockResolvedValue(
      new ShortUrlResponse(
        ShortUrlResponseCodes.NullShortUrl,
        ShortUrlResponseConstants.NULL_SHORT_URL_MESSAGE
      )
    );
    await act(async () => {
      await onSubmit({ originalUrl: exampleUrl });
    });

    mockCreateShortUrl.mockResolvedValue(
      new ShortUrlResponse(
        ShortUrlResponseCodes.Success,
        ShortUrlResponseConstants.SUCCESS_MESSAGE,
        "ShortUrl"));
    await act(async () => {
      await onSubmit({ originalUrl: exampleUrl });
    });

    expect(successToastSpy).toHaveBeenCalledWith(ShortUrlResponseConstants.SUCCESS_MESSAGE);
    expect(successToastSpy).toHaveBeenCalledOnce();

    expect(errorToastSpy).toHaveBeenCalledWith(ShortUrlResponseConstants.NULL_SHORT_URL_MESSAGE);
    expect(errorToastSpy).toHaveBeenCalledOnce();

    expect(mockCreateShortUrl).toHaveBeenCalledWith(exampleUrl);
    expect(mockCreateShortUrl).toHaveBeenCalledTimes(2);

    expect(form.getValues().shortenedUrl).toBe("ShortUrl");
  });
});
