import { ref } from "vue";

const loading = ref(false);
const error = ref<string | null>(null);

function startLoading() {
    loading.value = true;
    error.value = null;
}

function stopLoading() {
    loading.value = false;
}

function setError(message: string | null) {
    error.value = message;
}

function resetLoader() {
    loading.value = false;
    error.value = null;
}

export function useLoader() {
    return {
        loading,
        error,
        startLoading,
        stopLoading,
        setError,
        resetLoader
    };
}
