import { http, HttpResponse } from "msw";

export const handlers = [
    http.post("http://localhost:5000/", () => {
        return HttpResponse.json({
            ShortUrl: "http://localhost:5000/ShortUrl"
        })
    })
];