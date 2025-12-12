import { DuplicateConflictResponse } from "@/responses/duplicate-conflict-response";
import axios from "axios";

/**
 * Creates a short URL from a given URL.
 * 
 * @param url - The URL to shorten.
 */
export const createShortUrl = async (url: string) => {
    try {
        const response = await axios.post("http://localhost:5000/", {
            Url: url
        });
        return response.data;
    } catch (error) {
        if (axios.isAxiosError(error)) {
            if (error.response && error.response.status === 409) {
                return new DuplicateConflictResponse(
                    error.response.data.error,
                    error.response.data.message);
            }
        }

        return null;
    }
}