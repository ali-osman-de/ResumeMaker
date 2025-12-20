import { ref } from "vue";

const loading = ref(false);
const error = ref<string | null>(null);
const success = ref<string | null>(null);

function startLoading() {
    loading.value = true;
    error.value = null;
    success.value = null;
}

function stopLoading() {
    loading.value = false;
}

function setError(message: string | null) {
    error.value = message;
    if (message) success.value = null;
}

function setSuccess(message: string | null) {
    success.value = message;
    if (message) error.value = null;
}

function resetLoader() {
    loading.value = false;
    error.value = null;
    success.value = null;
}

export function useLoader() {
    return {
        loading,
        error,
        success,
        startLoading,
        stopLoading,
        setError,
        setSuccess,
        resetLoader
    };
}
