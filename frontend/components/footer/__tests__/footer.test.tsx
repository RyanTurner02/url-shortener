import { render, screen } from "@testing-library/react";
import Footer from "@/components/footer/footer";

describe("Footer", () => {
  it("renders the footer", () => {
    render(<Footer />);

    const footer = screen.getByRole("contentinfo");

    expect(footer).toBeInTheDocument();
  });

  it("renders the correct links", () => {
    render(<Footer />);

    const [homeLink, aboutLink, githubLink] = screen.getAllByRole("link");

    expect(homeLink).toBeInTheDocument();
    expect(aboutLink).toBeInTheDocument();
    expect(githubLink).toBeInTheDocument();

    expect(homeLink).toHaveAttribute("href", "/");
    expect(aboutLink).toHaveAttribute("href", "/about");
    expect(githubLink).toHaveAttribute(
      "href",
      "https://github.com/RyanTurner02/url-shortener",
    );
    expect(githubLink).toHaveAttribute("target", "_blank");
  });
});
