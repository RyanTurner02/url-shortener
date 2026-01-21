import { render, screen } from "@testing-library/react";
import NotFound from "@/app/not-found";

describe("NotFound", () => {
  it("renders the not found page", () => {
    render(<NotFound />);

    const notFoundHeader = screen.getByRole("heading", { level: 1 });
    const informationParagraph = screen.getByRole("paragraph");
    const homeLink = screen.getByRole("link");

    expect(notFoundHeader).toBeInTheDocument();
    expect(informationParagraph).toBeInTheDocument();
    expect(homeLink).toBeInTheDocument();
  });
});
