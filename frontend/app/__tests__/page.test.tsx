import { render, screen } from "@testing-library/react";
import Home from "@/app/page";
import { QueryProviderWrapper } from "@/test-utils";

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

    const label = screen.getByText(/paste your long link here/i);
    const form = label.closest("form");

    expect(form).toBeInTheDocument();
  });
});
