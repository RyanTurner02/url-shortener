import { UrlShortenerFormValues } from "@/schemas/url-shortener-form-schema";
import { render, screen } from "@testing-library/react";
import { useForm } from "react-hook-form";
import ShortenedUrlField from "@/components/forms/url-shortener-form/fields/shortened-url-field";

describe("ShortenedUrlField", () => {
  const Wrapper = () => {
    const { control } = useForm<UrlShortenerFormValues>();
    return <ShortenedUrlField control={control} />;
  };

  it("renders the shortened URL field", async () => {
    const labelRegex = /shortened url/i;
    const placeholderText = "https://url-shortener-example.com/shortened-url";
    render(<Wrapper />);

    const label = screen.getByLabelText(labelRegex);
    expect(label).toBeInTheDocument();

    const input = screen.getByRole("textbox", { name: labelRegex });
    expect(input).toBeInTheDocument();
    expect(input).toHaveAttribute("placeholder", placeholderText);
    expect(input).toHaveAttribute("readonly");
    expect(input).toHaveValue("");
  });
});
