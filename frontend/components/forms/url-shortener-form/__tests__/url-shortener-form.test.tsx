import { render, screen } from "@testing-library/react";
import { UrlShortenerForm } from "@/components/forms/url-shortener-form/url-shortener-form";
import { QueryProviderWrapper } from "@/test-utils";
import userEvent from "@testing-library/user-event";

vi.mock("@/hooks/use-create-shortened-url", () => {
  return {
    default: () => {
      return async (originalUrl: string) => {
        return { shortUrl: "ShortUrl" };
      };
    },
  };
});

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

    const button = screen.getByRole("button");
    const [originalUrlField, shortUrlField] = screen.getAllByRole("textbox");

    expect(button).toBeInTheDocument();
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

    const button = screen.getByRole("button");
    await user.click(button);    
    
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

    const button = screen.getByRole("button");
    await user.click(button);

    const error = screen.getByRole("alert");
    expect(error).toBeInTheDocument();

    const [originalUrlField] = screen.getAllByRole("textbox");
    await user.type(originalUrlField, "https://example.com/");
    expect(error).not.toBeInTheDocument();
  });

  it("displays a short url after submitting a valid original url", async () => {
    const user = userEvent.setup();
    render(
        <QueryProviderWrapper>
            <UrlShortenerForm />
        </QueryProviderWrapper>
    );

    const button = screen.getByRole("button");
    const [originalUrlField, shortUrlField] = screen.getAllByRole("textbox");
    expect(shortUrlField).toHaveValue("");

    await user.type(originalUrlField, "https://example.com/");
    await user.click(button);

    expect(shortUrlField).toHaveValue("ShortUrl");
  });
});
