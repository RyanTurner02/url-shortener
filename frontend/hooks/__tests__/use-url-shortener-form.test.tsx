import { renderHook } from "@testing-library/react";
import useUrlShortenerForm from "@/hooks/use-url-shortener-form";
import { QueryProviderWrapper } from "@/test-utils";

describe("useCreateShortenedUrl", () => {
  it("initializes form with default values", () => {
    const { result } = renderHook(() => useUrlShortenerForm(), {
      wrapper: QueryProviderWrapper,
    });
    const { form } = result.current;

    const formValues = form.getValues();

    expect(formValues.originalUrl).toBe("");
    expect(formValues.shortenedUrl).toBe("");
  });
});
