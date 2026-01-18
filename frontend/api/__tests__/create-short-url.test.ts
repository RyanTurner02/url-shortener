import { createShortUrl } from "@/api/create-short-url";
import { ShortUrlResponseConstants } from "@/constants/short-url-response-constants";
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
            ShortUrlResponseConstants.SUCCESS_MESSAGE,
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
                error: ShortUrlResponseCodes.DuplicateConflict,
                message: ShortUrlResponseConstants.DUPLICATE_CONFLICT_MESSAGE
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
            ShortUrlResponseConstants.DUPLICATE_CONFLICT_MESSAGE
        );
        const actual = await createShortUrl("duplicate");

        expect(actual).toEqual(expected);
    });

    it("returns NullShortUrlResponse on failure", async () => {
        const expected = new ShortUrlResponse(
            ShortUrlResponseCodes.NullShortUrl,
            ShortUrlResponseConstants.NULL_SHORT_URL_MESSAGE);
        const actual = await createShortUrl("error");

        expect(actual).toEqual(expected);
    });
});