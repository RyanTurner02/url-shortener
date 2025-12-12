interface IDuplicateConflictResponse {
    error: string;
    message: string;
};

export class DuplicateConflictResponse implements IDuplicateConflictResponse {
    error: string;
    message: string;

    constructor(error: string, message: string) {
        this.error = error;
        this.message = message;
    }
}