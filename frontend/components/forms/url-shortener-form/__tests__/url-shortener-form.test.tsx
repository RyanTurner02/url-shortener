import { render, screen } from "@testing-library/react";
import { UrlShortenerForm } from "../url-shortener-form";
import { QueryProviderWrapper } from "@/test-utils";

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
});
