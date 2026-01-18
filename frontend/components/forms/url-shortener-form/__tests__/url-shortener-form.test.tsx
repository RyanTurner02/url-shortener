import { render, screen } from "@testing-library/react";
import { UrlShortenerForm } from "@/components/forms/url-shortener-form/url-shortener-form";
import { QueryProviderWrapper } from "@/test-utils";
import userEvent from "@testing-library/user-event";
import { ShortUrlResponse } from "@/responses/short-url-response";
import { ShortUrlResponseCodes } from "@/enums/short-url-response-codes";

const mockCreateShortUrl = vi.fn();

vi.mock("@/hooks/use-create-shortened-url", () => ({
  default: () => mockCreateShortUrl,
}));

describe("UrlShortenerForm", () => {
  afterEach(() => {
    vi.clearAllMocks();
  });

  it("renders the fields and submit button", () => {
    render(
      <QueryProviderWrapper>
        <UrlShortenerForm />
      </QueryProviderWrapper>
    );

    const [submitButton] = screen.getAllByRole("button");
    const [originalUrlField, shortUrlField] = screen.getAllByRole("textbox");

    expect(submitButton).toBeInTheDocument();
    expect(originalUrlField).toBeInTheDocument();
    expect(shortUrlField).toBeInTheDocument();
  });

  it("displays a field error when submitting an invalid original url", async () => {
    const user = userEvent.setup();
    render(
        <QueryProviderWrapper>
            <UrlShortenerForm />
        </QueryProviderWrapper>
    );

    const [submitButton] = screen.getAllByRole("button");
    await user.click(submitButton);    
    
    const error = screen.getByRole("alert");
    expect(error).toBeInTheDocument();
    expect(error).toHaveTextContent("Invalid URL");
  });

  it("removes the field error after typing a valid original url", async () => {
    const user = userEvent.setup();
    render(
        <QueryProviderWrapper>
            <UrlShortenerForm />
        </QueryProviderWrapper>
    );

    const [submitButton] = screen.getAllByRole("button");
    await user.click(submitButton);

    const error = screen.getByRole("alert");
    expect(error).toBeInTheDocument();

    const [originalUrlField] = screen.getAllByRole("textbox");
    await user.type(originalUrlField, "https://example.com/");
    expect(error).not.toBeInTheDocument();
  });

  it("displays a short url after submitting a valid original url", async () => {
    mockCreateShortUrl.mockResolvedValue(
      new ShortUrlResponse(
        ShortUrlResponseCodes.Success,
        "Short URL has been successfully created.",
        "ShortUrl"));

    const user = userEvent.setup();
    render(
        <QueryProviderWrapper>
            <UrlShortenerForm />
        </QueryProviderWrapper>
    );

    const [submitButton] = screen.getAllByRole("button");
    const [originalUrlField, shortUrlField] = screen.getAllByRole("textbox");
    expect(shortUrlField).toHaveValue("");

    await user.type(originalUrlField, "https://example.com/");
    await user.click(submitButton);

    expect(shortUrlField).toHaveValue("ShortUrl");
  });
});
