import { createShortUrl } from "@/api/create-short-url";
import { DuplicateConflictResponse } from "@/responses/duplicate-conflict-response";
import { NullShortUrlResponse } from "@/responses/null-short-url-response";
import { ShortUrlResponse } from "@/responses/short-url-response";

describe("createShortUrl", () => {
    beforeEach(() => {
        vi.clearAllMocks();
    });

    it("creates a short URL", async () => {
        const expected = new ShortUrlResponse("ShortUrl");
        const response = await createShortUrl("OriginalUrl");

        expect(response).toEqual(expected);
    });

    it("returns DuplicatedConflictResponse on duplicate", async () => {
        const expected = new DuplicateConflictResponse(
            "DuplicateConflict",
            "Failed to generate a unique short URL. Please try again later."
        );
        const response = await createShortUrl("duplicate");

        expect(response).toEqual(expected);
    });

    it("returns NullShortUrlResponse on failure", async () => {
        const expected = new NullShortUrlResponse();
        const response = await createShortUrl("error");
        
        expect(response).toEqual(expected);
    });
});