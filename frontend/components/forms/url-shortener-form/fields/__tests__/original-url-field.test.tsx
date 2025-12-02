import { useForm } from "react-hook-form";
import { render, screen } from "@testing-library/react";
import { UrlShortenerFormValues } from "@/schemas/url-shortener-form-schema";
import OriginalUrlField from "@/components/forms/url-shortener-form/fields/original-url-field";
import userEvent from "@testing-library/user-event";

describe("OriginalUrlField", () => {
  const Wrapper = () => {
    const { control } = useForm<UrlShortenerFormValues>();
    return <OriginalUrlField control={control} />;
  }

  it("renders the original URL field", async () => {
    const labelRegex = /paste your long link here/i;
    const placeholderText = "https://example.com/long-url-here";
    const userInput = "https://example.com/";
    const user = userEvent.setup();
    render(<Wrapper />);

    const label = screen.getByText(labelRegex);
    expect(label).toBeInTheDocument();

    const input = screen.getByRole("textbox", { name: labelRegex });
    expect(input).toBeInTheDocument();
    expect(input).toHaveAttribute("placeholder", placeholderText);
    expect(input).toHaveValue("");

    await user.type(input, userInput);
    expect(input).toHaveValue(userInput);
  });
});