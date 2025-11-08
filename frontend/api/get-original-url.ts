import axios from "axios";

export const getOriginalUrl = async (shortLink: string) => {
    try {
        const response = await axios.get(shortLink);
        console.log(response.data);
    } catch (error) {
        console.error(error);
    }
}