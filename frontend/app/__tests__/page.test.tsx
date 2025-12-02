import { render, screen } from "@testing-library/react";
import Home from "@/app/page";
import { QueryProviderWrapper } from "@/test-utils";

vi.mock("@/components/forms/url-shortener-form/url-shortener-form", () => ({
  UrlShortenerForm: () => <div data-testid="url-shortener-form" />,
}));

describe("Home", () => {
  it("renders the header", () => {
    render(
      <QueryProviderWrapper>
        <Home />
      </QueryProviderWrapper>
    );

    const header = screen.getByRole("heading", { level: 1 });

    expect(header).toBeInTheDocument();
    expect(header).toHaveTextContent("URL Shortener");
  });

  it("renders the url shortener form", () => {
    render(
      <QueryProviderWrapper>
        <Home />
      </QueryProviderWrapper>
    );

    const form = screen.getByTestId("url-shortener-form");

    expect(form).toBeInTheDocument();
  });
});
