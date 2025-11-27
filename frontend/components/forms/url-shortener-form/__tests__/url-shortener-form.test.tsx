import { render, screen } from "@testing-library/react";
import { UrlShortenerForm } from "@/components/forms/url-shortener-form/url-shortener-form";
import { QueryProviderWrapper } from "@/test-utils";
import userEvent from "@testing-library/user-event";

describe("UrlShortenerForm", () => {
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
});
