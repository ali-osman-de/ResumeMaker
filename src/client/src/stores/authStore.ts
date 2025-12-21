import { defineStore } from "pinia";
import { ref } from "vue";
import { type CreateUserCommand, type LoginUserCommand, type TokenInformationDto } from "@/metadata";
import { client } from "@/helpers/client";
import { useLoader } from "@/helpers/loader";

export const authStore = defineStore("auth", () => {
    const userLoginInformation = ref<TokenInformationDto | null>(null);
    const userCreateInformation = ref<boolean | null>(null);

    const { loading, error, success, startLoading, stopLoading, setError, setSuccess } = useLoader();

    async function loginUser(loginInformation: LoginUserCommand) {
        startLoading();
        setError(null);
        try {
            const res = await client.login(loginInformation);
            const { isSuccess, data, message } = res ?? {};

            if (!isSuccess || !data?.accessToken) {
                throw new Error(message || "Giriş başarısız. Lütfen tekrar deneyin.");
            }

            localStorage.setItem("access_token", data.accessToken);
            if (data.refreshToken) {
                localStorage.setItem("refresh_token", data.refreshToken);
            }

            userLoginInformation.value = data as TokenInformationDto;
            setSuccess("Giriş başarılı.");
            return data as TokenInformationDto;
        } catch (err) {
            setError("Giriş başarısız. Lütfen tekrar deneyin.");
            throw err;
        } finally {
            stopLoading();
        }
    }

    async function createUser(createInformation: CreateUserCommand) {
        startLoading();
        setError(null);
        try {
            const res = await client.register(createInformation);
            setSuccess("Kayıt başarılı.");
            return res;
        } catch (err) {
            setError("Kayıt başarısız. Lütfen tekrar deneyin.");
            throw err;
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
