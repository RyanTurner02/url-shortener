import axios from "axios";

/**
 * Retrieves an original URL from a short URL.
 * 
 * @param shortLink - The short URL.
 */
export const getOriginalUrl = async (shortLink: string) => {
    try {
        const response = await axios.get(shortLink);
        console.log(response.data);
    } catch (error) {
        console.error(error);
    }
}