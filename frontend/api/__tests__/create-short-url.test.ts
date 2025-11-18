import { createShortUrl } from "@/api/create-short-url";

describe("createShortUrl", () => {
    it("creates a short URL", async () => {
        const response = await createShortUrl("https://example.com/");

        expect(response).toEqual({
            ShortUrl: "http://localhost:5000/ShortUrl"
        });
    });

    it("returns null on failure", async () => {
        const response = await createShortUrl("error");
        
        expect(response).toBeNull();
    });
});