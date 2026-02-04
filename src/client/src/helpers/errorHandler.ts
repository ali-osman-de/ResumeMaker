export type ProblemDetails = {
    title?: string;
    detail?: string;
    status?: number;
    extensions?: Record<string, unknown>;
};

export type ServiceResult<T> = {
    data?: T | null;
    fail?: ProblemDetails | null;
};

async function readJson<T>(response: Response): Promise<T | null> {
    const text = await response.text();
    if (!text) return null;
    try {
        return JSON.parse(text) as T;
    } catch {
        return null;
    }
}

function extractErrorMessage(problem?: ProblemDetails | null): string | null {
    return problem?.detail || problem?.title || null;
}

export async function parseServiceResultResponse<T>(response: Response): Promise<{
    data: T | null;
    error: string | null;
    raw: ServiceResult<T> | ProblemDetails | null;
}> {
    const body = await readJson<unknown>(response);
    if (!body || typeof body !== "object") {
        return { data: null, error: null, raw: null };
    }

    const record = body as Record<string, unknown>;

    if ("fail" in record || "data" in record) {
        const result = body as ServiceResult<T>;
        return {
            data: result.data ?? null,
            error: extractErrorMessage(result.fail),
            raw: result
        };
    }

    if ("detail" in record || "title" in record) {
        const problem = body as ProblemDetails;
        return { data: null, error: extractErrorMessage(problem), raw: problem };
    }

    return { data: body as T, error: null, raw: null };
}
