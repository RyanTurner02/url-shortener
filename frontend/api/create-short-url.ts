import { ShortUrlResponseConstants } from "@/constants/short-url-response-constants";
import { ShortUrlResponseCodes } from "@/enums/short-url-response-codes";
import { ShortUrlResponse } from "@/responses/short-url-response";
import axios from "axios";

/**
 * Creates a short URL from a given URL.
 * 
 * @param {string} url - The URL to shorten.
 * @returns {Promise<ShortUrlResponse>}
 */
export const createShortUrl = async (url: string) => {
    try {
        const response = await axios.post("http://localhost:5000/", {
            Url: url
        });

        return new ShortUrlResponse(
            ShortUrlResponseCodes.Success,
            ShortUrlResponseConstants.SUCCESS_MESSAGE,
            response.data.shortUrl);
    } catch (error) {
        if (axios.isAxiosError(error)) {
            if (error.response) {
                if (error.response.status === 400) {
                    return new ShortUrlResponse(
                        error.response.data.error,
                        error.response.data.message);
                } else if (error.response.status === 409) {
                    return new ShortUrlResponse(
                        error.response.data.error,
                        error.response.data.message);
                }
            }
        }

        return new ShortUrlResponse(
            ShortUrlResponseCodes.NullShortUrl,
            ShortUrlResponseConstants.NULL_SHORT_URL_MESSAGE);
    }
}