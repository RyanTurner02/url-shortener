import { createShortUrl } from "@/api/create-short-url";
import { DuplicateConflictResponse } from "@/responses/duplicate-conflict-response";

describe("createShortUrl", () => {
    it("creates a short URL", async () => {
        const response = await createShortUrl("https://example.com/");

        expect(response).toEqual({
            ShortUrl: "http://localhost:5000/ShortUrl"
        });
    });

    it("returns DuplicatedConflictResponse on duplicate", async () => {
        const expected = new DuplicateConflictResponse(
            "DuplicateConflict",
            "Failed to generate a unique short URL. Please try again later.");
        const response = await createShortUrl("duplicate");

        expect(response).toEqual(expected);
    });

    it("returns null on failure", async () => {
        const response = await createShortUrl("error");
        
        expect(response).toBeNull();
    });
});