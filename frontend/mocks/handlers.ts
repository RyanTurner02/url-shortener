import { ICreateShortUrlRequest } from "@/requests/create-short-url-response";
import { http, HttpResponse } from "msw";

export const handlers = [
    http.post("http://localhost:5000/", async ({ request }) => {
        const body = await request.json() as ICreateShortUrlRequest;

        if (body.Url === "error") {
            return HttpResponse.error();
        }

        return HttpResponse.json({
            ShortUrl: "http://localhost:5000/ShortUrl"
        })
    })
];