import { ref } from "vue";

const loading = ref(false);
const error = ref<string | null>(null);
const success = ref<string | null>(null);
let hideTimeout: ReturnType<typeof setTimeout> | null = null;

function clearHideTimeout() {
    if (hideTimeout) {
        clearTimeout(hideTimeout);
        hideTimeout = null;
    }
}

function scheduleHide(callback: () => void) {
    clearHideTimeout();
    hideTimeout = setTimeout(() => {
        callback();
        hideTimeout = null;
    }, 5000);
}

function startLoading() {
    loading.value = true;
    error.value = null;
    success.value = null;
    clearHideTimeout();
}

function stopLoading() {
    loading.value = false;
}

function setError(message: string | null) {
    error.value = message;
    if (message) {
        success.value = null;
        scheduleHide(() => error.value = null);
    } else {
        clearHideTimeout();
    }
}

function setSuccess(message: string | null) {
    success.value = message;
    if (message) {
        error.value = null;
        scheduleHide(() => success.value = null);
    } else {
        clearHideTimeout();
    }
}

function resetLoader() {
    loading.value = false;
    error.value = null;
    success.value = null;
    clearHideTimeout();
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
