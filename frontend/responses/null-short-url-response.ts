interface INullShortUrlResponse {
    error: string;
    message: string;
};

export class NullShortUrlResponse implements INullShortUrlResponse {
    error: string;
    message: string;

    constructor() {
        this.error = "NullShortUrl";
        this.message = "Unable to create a short URL. Please try again later.";
    }
}