import { ICreateShortUrlRequest } from "@/requests/create-short-url-request";
import { http, HttpResponse } from "msw";

export const handlers = [
    http.post("http://localhost:5000/", async ({ request }) => {
        const body = await request.json() as ICreateShortUrlRequest;

        if (body.Url === "OriginalUrl") {
            return HttpResponse.json({
                shortUrl: "ShortUrl"
            });
        }

        if (body.Url === "error") {
            return HttpResponse.error();
        }

        if (body.Url === "duplicate") {
            return HttpResponse.json({
                error: "DuplicateConflict",
                message: "Failed to generate a unique short URL. Please try again later."
            });
        }

        return HttpResponse.json({
            ShortUrl: "http://localhost:5000/ShortUrl"
        })
    })
];