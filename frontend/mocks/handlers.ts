import { ShortUrlResponseConstants } from "@/constants/short-url-response-constants";
import { ShortUrlResponseCodes } from "@/enums/short-url-response-codes";
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

        if (body.Url === "duplicate") {
            return HttpResponse.json({
                error: ShortUrlResponseCodes.DuplicateConflict,
                message: ShortUrlResponseConstants.DUPLICATE_CONFLICT_MESSAGE
            });
        }

        if (body.Url === "invalid") {
            return HttpResponse.json({
                error: ShortUrlResponseCodes.InvalidUrl,
                message: ShortUrlResponseConstants.INVALID_URL_MESSAGE
            })
        }

        if (body.Url === "error") {
            return HttpResponse.error();
        }

        return HttpResponse.json({
            ShortUrl: "http://localhost:5000/ShortUrl"
        })
    })
];