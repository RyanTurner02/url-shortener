import { render, screen } from "@testing-library/react";
import About from "@/app/about/page";

describe("About", () => {
  it("renders the page heading", () => {
    render(<About />);

    const heading = screen.getByRole("heading", { level: 1 });

    expect(heading).toBeInTheDocument();
  });

  it("renders the paragraphs", () => {
    render(<About />);

    const paragraphs = screen.getAllByRole("paragraph");

    paragraphs.forEach((paragraph) => {
      expect(paragraph).toBeInTheDocument();
    });
  });

  it("renders the list of popular URL shortener services", () => {
    render(<About />);

    const [twitterBullet, linkedinBullet, youtubeBullet] =
      screen.getAllByRole("listitem");

    expect(twitterBullet).toBeInTheDocument();
    expect(linkedinBullet).toBeInTheDocument();
    expect(youtubeBullet).toBeInTheDocument();
  });

  it("renders the correct external links", () => {
    render(<About />);

    const [twitterLink, linkedinLink, youtubeLink] =
      screen.getAllByRole("link");

    expect(twitterLink).toBeInTheDocument();
    expect(linkedinLink).toBeInTheDocument();
    expect(youtubeLink).toBeInTheDocument();

    expect(twitterLink).toHaveAttribute("href", "https://t.co/");
    expect(twitterLink).toHaveAttribute("target", "_blank");

    expect(linkedinLink).toHaveAttribute("href", "https://lnkd.in/");
    expect(linkedinLink).toHaveAttribute("target", "_blank");

    expect(youtubeLink).toHaveAttribute("href", "https://youtu.be/");
    expect(linkedinLink).toHaveAttribute("target", "_blank");
  });
});
