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
            setSuccess("Giriş başarılı.");
            return userLoginInformation.value = res;
        } catch (err: any) {
            setError(err?.message || "Giriş başarısız. Lütfen tekrar deneyin.");
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
        } catch (err: any) {
            setError(err?.message || "Kayıt başarısız. Lütfen tekrar deneyin.");
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
