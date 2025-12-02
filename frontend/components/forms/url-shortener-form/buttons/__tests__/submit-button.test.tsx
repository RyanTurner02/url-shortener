import { render, screen } from "@testing-library/react";
import SubmitButton from "@/components/forms/url-shortener-form/buttons/submit-button";

describe("SubmitButton", () => {
    it("renders the submit button", () => {
        render(<SubmitButton />);

        const submitButton = screen.getByRole("button", { name: /submit/i });

        expect(submitButton).toBeInTheDocument();
        expect(submitButton).toHaveAttribute("type", "submit");
        expect(submitButton).toHaveAttribute("form", "url-shortener-form");
        expect(submitButton).toHaveClass("cursor-pointer");
    });
});