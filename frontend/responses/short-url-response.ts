interface IShortUrlResponse {
  code: string;
  message: string;
  shortUrl: string;
}

export class ShortUrlResponse implements IShortUrlResponse {
  code: string;
  message: string;
  shortUrl: string;

  constructor(code: string, message: string, shortUrl: string = "") {
    this.code = code;
    this.message = message;
    this.shortUrl = shortUrl;
  }
};