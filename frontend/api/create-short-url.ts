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
    } catch {
        return null;
    }
}