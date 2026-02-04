import { defineStore } from "pinia";
import { ref } from "vue";
import { type CreateUserCommand, type LoginUserCommand } from "@/metadata";
import { parseServiceResultResponse } from "@/helpers/errorHandler";
import { useLoader } from "@/helpers/loader";
import { setTokens } from "@/helpers/auth";

type TokenInformationDto = {
    accessToken?: string;
    refreshToken?: string;
    expirationDatetime?: string;
};

export const authStore = defineStore("auth", () => {
    const userLoginInformation = ref<TokenInformationDto | null>(null);
    const userCreateInformation = ref<boolean | null>(null);

    const { loading, error, success, startLoading, stopLoading, setError, setSuccess } = useLoader();
    const baseUrl = import.meta.env.VITE_BASE_URL ?? "";

    async function loginUser(loginInformation: LoginUserCommand) {
        startLoading();
        setError(null);
        try {
            const response = await fetch(`${baseUrl}/api/Auth/Login`, {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(loginInformation)
            });

            const result = await parseServiceResultResponse<TokenInformationDto>(response);
            if (result.error) {
                setError(result.error);
                throw new Error(result.error);
            }
            if (!response.ok) {
                throw new Error();
            }

            const data = result.data;
            if (!data?.accessToken) {
                throw new Error();
            }

            setTokens({ accessToken: data.accessToken, refreshToken: data.refreshToken ?? null });
            userLoginInformation.value = data;
            setSuccess("Giriş başarılı.");
            return data;
        } catch (err) {
            throw err as Error;
        } finally {
            stopLoading();
        }
    }

    async function createUser(createInformation: CreateUserCommand) {
        startLoading();
        setError(null);
        try {
            const response = await fetch(`${baseUrl}/api/Auth/Register`, {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(createInformation)
            });

            const result = await parseServiceResultResponse<unknown>(response);
            if (result.error) {
                setError(result.error);
                throw new Error(result.error);
            }
            if (!response.ok) {
                throw new Error();
            }

            userCreateInformation.value = true;
            setSuccess("Kayıt başarılı.");
            return true;
        } catch (err) {
            throw err as Error;
        } finally {
            stopLoading();
        }
    }

    return {
        userLoginInformation,
        userCreateInformation,
        loginUser,
        createUser,
        loading,
        error,
        success
    }
})
