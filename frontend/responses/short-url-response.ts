interface IShortUrlResponse {
  shortUrl: string;
}

export class ShortUrlResponse implements IShortUrlResponse {
  shortUrl: string;

  constructor(shortUrl: string) {
    this.shortUrl = shortUrl;
  }
};