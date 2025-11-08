import axios from "axios";

/**
 * Creates a short URL from a given URL.
 * 
 * @param url - The URL to shorten.
 */
export const createShortUrl = async (url: string) => {
    try {
        const response = await axios({
            method: "post",
            url: "http://localhost:5000/",
            data: {
                Url: url
            }
        });
        console.log(response);
    } catch (error) {
        console.log(error);
    }
}