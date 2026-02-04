import { defineStore } from "pinia";
import { ref } from "vue";
import { type ResumeCreateCommand, type ResumeUpdateCommand, type ResumeRemoveCommand, type SaveResumeDto } from "@/metadata";
import { parseServiceResultResponse } from "@/helpers/errorHandler";
import { useLoader } from "@/helpers/loader";

type ResumeSummaryDto = {
    id?: string;
    name?: string;
    surname?: string;
    email?: string;
};

export const resumeStore = defineStore("resume", () => {
    const resumes = ref<ResumeSummaryDto[]>([]);
    const activeResume = ref<SaveResumeDto | null>(null);

    const { loading, error, success, startLoading, stopLoading, setError, setSuccess } = useLoader();
    const baseUrl = import.meta.env.VITE_BASE_URL ?? "";

    function authHeaders() {
        const token = localStorage.getItem("access_token");
        return token ? { Authorization: `Bearer ${token}` } : {};
    }

    async function createResume(command: ResumeCreateCommand) {
        startLoading();
        setError(null);
        try {
            const response = await fetch(`${baseUrl}/api/Resume/CreateResume`, {
                method: "POST",
                headers: { "Content-Type": "application/json", ...authHeaders() },
                body: JSON.stringify(command)
            });

            const result = await parseServiceResultResponse<unknown>(response);
            if (result.error) {
                setError(result.error);
                throw new Error(result.error);
            }
            if (!response.ok) {
                throw new Error();
            }

            setSuccess(null);
            return true;
        } finally {
            stopLoading();
        }
    }

    async function updateResume(command: ResumeUpdateCommand) {
        startLoading();
        setError(null);
        try {
            const response = await fetch(`${baseUrl}/api/Resume/UpdateResume`, {
                method: "PUT",
                headers: { "Content-Type": "application/json", ...authHeaders() },
                body: JSON.stringify(command)
            });

            const result = await parseServiceResultResponse<SaveResumeDto>(response);
            if (result.error) {
                setError(result.error);
                throw new Error(result.error);
            }
            if (!response.ok) {
                throw new Error();
            }

            if (result.data) activeResume.value = result.data;
            setSuccess(null);
            return result.data ?? null;
        } finally {
            stopLoading();
        }
    }

    async function removeResume(command: ResumeRemoveCommand) {
        startLoading();
        setError(null);
        try {
            const response = await fetch(`${baseUrl}/api/Resume/RemoveResume`, {
                method: "DELETE",
                headers: { "Content-Type": "application/json", ...authHeaders() },
                body: JSON.stringify(command)
            });

            const result = await parseServiceResultResponse<unknown>(response);
            if (result.error) {
                setError(result.error);
                throw new Error(result.error);
            }
            if (!response.ok) {
                throw new Error();
            }

            setSuccess(null);
            return true;
        } finally {
            stopLoading();
        }
    }

    async function getAllResumeByUserId(userId: string) {
        startLoading();
        setError(null);
        try {
            const response = await fetch(`${baseUrl}/api/Resume/GetAllResumeByUserId?userId=${encodeURIComponent(userId)}`, {
                method: "GET",
                headers: { ...authHeaders() }
            });

            const result = await parseServiceResultResponse<ResumeSummaryDto[]>(response);
            if (result.error) {
                setError(result.error);
                throw new Error(result.error);
            }
            if (!response.ok) {
                throw new Error();
            }

            resumes.value = result.data ?? [];
            return resumes.value;
        } finally {
            stopLoading();
        }
    }

    async function getResumeByResumeId(resumeId: string) {
        startLoading();
        setError(null);
        try {
            const response = await fetch(`${baseUrl}/api/Resume/GetResumeByResumeId?resumeId=${encodeURIComponent(resumeId)}`, {
                method: "GET",
                headers: { ...authHeaders() }
            });

            const result = await parseServiceResultResponse<SaveResumeDto>(response);
            if (result.error) {
                setError(result.error);
                throw new Error(result.error);
            }
            if (!response.ok) {
                throw new Error();
            }

            activeResume.value = result.data ?? null;
            return activeResume.value;
        } finally {
            stopLoading();
        }
    }

    return {
        resumes,
        activeResume,
        createResume,
        updateResume,
        removeResume,
        getAllResumeByUserId,
        getResumeByResumeId,
        loading,
        error,
        success
    };
});
