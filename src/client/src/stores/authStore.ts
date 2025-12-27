import { defineStore } from "pinia";
import { ref } from "vue";
import { ApiException, type CreateUserCommand, type LoginUserCommand, type TokenInformationDto, type BooleanServiceResult } from "@/metadata";
import { client } from "@/helpers/client";
import { useLoader } from "@/helpers/loader";

export const authStore = defineStore("auth", () => {
    const userLoginInformation = ref<TokenInformationDto | null>(null);
    const userCreateInformation = ref<boolean | null>(null);

    const { loading, error, success, startLoading, stopLoading, setError, setSuccess } = useLoader();

    function parseServiceResult<T>(payload: unknown): BooleanServiceResult<T> | null {
        if (!payload) return null;

        if (payload instanceof ApiException && payload.response) {
            try {
                return JSON.parse(payload.response) as BooleanServiceResult<T>;
            } catch {
                return null;
            }
        }

        if (typeof payload === "string") {
            try {
                return JSON.parse(payload) as BooleanServiceResult<T>;
            } catch {
                return null;
            }
        }

        const result = payload as BooleanServiceResult<T>;
        if (result && typeof result === "object" && "isSuccess" in result) {
            return result;
        }

        return null;
    }

    async function loginUser(loginInformation: LoginUserCommand) {
        startLoading();
        setError(null);
        try {
            let result: BooleanServiceResult<TokenInformationDto> | null = null;
            let callError: unknown;

            try {
                result = parseServiceResult<TokenInformationDto>(await client.login(loginInformation));
            } catch (err) {
                callError = err;
                result = parseServiceResult<TokenInformationDto>(err);
            }

            if (!result) {
                setError("Giriş başarısız. Lütfen tekrar deneyin.");
                if (callError) throw callError;
                throw new Error("Giriş başarısız. Lütfen tekrar deneyin.");
            }

            if (!result.isSuccess || !result.data?.accessToken) {
                const errorMessage = result.message || "Giriş başarısız. Lütfen tekrar deneyin.";
                setError(errorMessage);
                throw new Error(errorMessage);
            }

            const { data } = result;
            localStorage.setItem("access_token", data.accessToken!);
            if (data.refreshToken) {
                localStorage.setItem("refresh_token", data.refreshToken);
            }

            userLoginInformation.value = data;
            setSuccess("Giriş başarılı.");
            return data;
        } catch (err) {
            if (!error.value) setError("Giriş başarısız. Lütfen tekrar deneyin.");
            throw err as Error;
        } finally {
            stopLoading();
        }
    }

    async function createUser(createInformation: CreateUserCommand) {
        startLoading();
        setError(null);
        try {
            let result: BooleanServiceResult<boolean> | null = null;
            let callError: unknown;

            try {
                result = parseServiceResult<boolean>(await client.register(createInformation));
            } catch (err) {
                callError = err;
                result = parseServiceResult<boolean>(err);
            }

            if (!result) {
                setError("Kayıt başarısız. Lütfen tekrar deneyin.");
                if (callError) throw callError;
                throw new Error("Kayıt başarısız. Lütfen tekrar deneyin.");
            }

            if (!result.isSuccess) {
                const errorMessage = result.message || "Kayıt başarısız. Lütfen tekrar deneyin.";
                setError(errorMessage);
                throw new Error(errorMessage);
            }

            const created = result.data ?? true;
            userCreateInformation.value = created;
            setSuccess(result.message || "Kayıt başarılı.");
            return created;
        } catch (err) {
            if (!error.value) setError("Kayıt başarısız. Lütfen tekrar deneyin.");
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
