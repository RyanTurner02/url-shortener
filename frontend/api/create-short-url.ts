import { DuplicateConflictResponse } from "@/responses/duplicate-conflict-response";
import { NullShortUrlResponse } from "@/responses/null-short-url-response";
import { ShortUrlResponse } from "@/responses/short-url-response";
import axios from "axios";

/**
 * Creates a short URL from a given URL.
 * 
 * @param {string} url - The URL to shorten.
 * @returns {Promise<ShortUrlResponse | DuplicateConflictResponse | NullShortUrlResponse>}
 */
export const createShortUrl = async (url: string) => {
    try {
        const response = await axios.post("http://localhost:5000/", {
            Url: url
        });

        return new ShortUrlResponse(response.data.shortUrl);
    } catch (error) {
        if (axios.isAxiosError(error)) {
            if (error.response && error.response.status === 409) {
                return new DuplicateConflictResponse(
                    error.response.data.error,
                    error.response.data.message);
            }
        }

        return new NullShortUrlResponse();
    }
}