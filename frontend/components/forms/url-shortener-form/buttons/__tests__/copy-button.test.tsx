import CopyButton from "@/components/forms/url-shortener-form/buttons/copy-button";
import { render, screen } from "@testing-library/react";
import userEvent from "@testing-library/user-event";

describe("CopyButton", () => {
  it("renders the copy button", () => {
    render(<CopyButton text="" copy={vi.fn()} />);

    const copyButton = screen.getByRole("button", { name: /copy/i });

    expect(copyButton).toBeInTheDocument();
    expect(copyButton).toHaveAttribute("type", "button");
    expect(copyButton).toHaveClass("cursor-pointer");
  });

  it("calls copy when the copy button is clicked", async () => {
    const text = "text";
    const copyMock = vi.fn();
    const user = userEvent.setup();

    render(<CopyButton text={text} copy={copyMock} />);

    const button = screen.getByRole("button", { name: /copy/i });
    await user.click(button);

    expect(copyMock).toBeCalledWith(text);
    expect(copyMock).toBeCalledTimes(1);
  });
});
