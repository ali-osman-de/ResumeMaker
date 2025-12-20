import { defineStore } from "pinia";
import { ref } from "vue";
import { type CreateUserCommand, type LoginUserCommand } from "@/metadata";
import { client } from "@/helpers/client";

export const authStore = defineStore("auth", () => {
    const userLoginInformation = ref<LoginUserCommand | null>(null); 
    const userCreateInformation = ref<CreateUserCommand | null>(null);

    const loading = ref(false);
    const error = ref<string | null>(null);

    async function loginUser(userLoginInformation : LoginUserCommand) {
        try {
            const res = await client.login(userLoginInformation);
            return res;
        } catch (error) {
            return error;
        }
    }

    async function createUser(userCreateInformation : CreateUserCommand) {
        try {
            const res = await client.register(userCreateInformation);
            return res;
        } catch (error) {
            return error;
        }
    }

    return {
        userLoginInformation,
        userCreateInformation,
        loginUser,
        createUser
    }
})