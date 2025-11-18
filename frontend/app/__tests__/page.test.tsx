import { render, screen } from "@testing-library/react";
import Home from "@/app/page";
import QueryProvider from "@/providers/query-provider";

vi.mock("@/components/forms/url-shortener-form/url-shortener-form", () => ({
  UrlShortenerForm: () => <div data-testid="url-shortener-form" />,
}));

const Wrapper = () => {
  return (
    <QueryProvider>
      <Home />
    </QueryProvider>
  );
};

describe("Home", () => {
  it("renders the header", () => {
    render(<Wrapper />);

    const header = screen.getByRole("heading", { level: 1 });

    expect(header).toBeInTheDocument();
    expect(header).toHaveTextContent("URL Shortener");
  });

  it("renders the url shortener form", () => {
    render(<Wrapper />);

    const form = screen.getByTestId("url-shortener-form");

    expect(form).toBeInTheDocument();
  });
});
