import { createShortUrl } from "@/api/create-short-url";
import { ShortUrlResponseCodes } from "@/enums/short-url-response-codes";
import { ShortUrlResponse } from "@/responses/short-url-response";
import { AxiosError, AxiosHeaders } from "axios";
import axios from "axios";

describe("createShortUrl", () => {
    beforeEach(() => {
        vi.restoreAllMocks();
    });

    it("creates a short URL", async () => {
        const expected = new ShortUrlResponse(
            ShortUrlResponseCodes.Success,
            "Short URL has been successfully created.",
            "ShortUrl");
        const actual = await createShortUrl("OriginalUrl");

        expect(actual).toEqual(expected);
    });

    it("returns DuplicatedConflictResponse on duplicate", async () => {
        const mockRequest = {};
        const mockConfig = { headers: new AxiosHeaders() };
        const mockResponse = {
            status: 409,
            statusText: "Conflict",
            data: {
                error: "DuplicateConflict",
                message: "Failed to generate a unique short URL. Please try again later."
            },
            headers: new AxiosHeaders(),
            config: mockConfig,
            request: mockRequest
        };
        const axiosError = new AxiosError(
            "Request failed with status code 404",
            "ERR_BAD_REQUEST",
            mockConfig,
            mockRequest,
            mockResponse
        );
        vi.spyOn(axios, "post")
            .mockRejectedValue(axiosError);

        const expected = new ShortUrlResponse(
            ShortUrlResponseCodes.DuplicateConflict,
            "Failed to generate a unique short URL. Please try again later."
        );
        const actual = await createShortUrl("duplicate");

        expect(actual).toEqual(expected);
    });

    it("returns NullShortUrlResponse on failure", async () => {
        const expected = new ShortUrlResponse(
            ShortUrlResponseCodes.NullShortUrl,
            "Unable to create a short URL. Please try again later.");
        const actual = await createShortUrl("error");

        expect(actual).toEqual(expected);
    });
});