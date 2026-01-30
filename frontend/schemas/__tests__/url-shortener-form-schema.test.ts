import { urlShortenerFormSchema } from "@/schemas/url-shortener-form-schema";

describe("urlShortenerFormSchema", () => {
    describe("valid URLs", () => {
        const cases = [
            { url: "http://example.com/" },
            { url: "https://example.com/" },
            { url: " https://example.com/" },
            { url: "https://example.com/ " },
            { url: " https://example.com/ " },
        ];
        it.each(cases)("is a valid URL", ({ url }) => {
            const data = {
                originalUrl: url,
                shortenedUrl: ""
            };

            const parsedData = urlShortenerFormSchema.safeParse(data);

            expect(parsedData.success).toBe(true);
            expect(parsedData.data).toEqual({
                originalUrl: url.trim(),
                shortenedUrl: ""
            });
        })
    });

    describe("invalid URLs", () => {
        const cases = [
            { url: "" },
            { url: " " },
            { url: "example" },
            { url: "example.com" },
            { url: "www.example.com" },
        ];
        it.each(cases)("is an invalid URL", ({ url }) => {
            const data = {
                originalUrl: url,
                shortenedUrl: ""
            };

            const parsedData = urlShortenerFormSchema.safeParse(data);

            expect(parsedData.success).toBe(false);

            if (!parsedData.success) {
                expect(parsedData.error.issues[0].message).toBe("Invalid URL.");
            }
        })

        it("exceeds maximum length", () => {
            const maxLength = 128;
            const baseUrl = "https://example.com/";
            const urlPath = 'a'.repeat(maxLength - baseUrl.length + 1);
            const url = `${baseUrl}${urlPath}`;

            const data = {
                originalUrl: url,
                shortenedUrl: ""
            };

            const parsedData = urlShortenerFormSchema.safeParse(data);

            expect(parsedData.success).toBe(false);

            if (!parsedData.success) {
                expect(parsedData.error.issues[0].message).toBe("URL must not exceed 128 characters.");
            }
        });
    });
});