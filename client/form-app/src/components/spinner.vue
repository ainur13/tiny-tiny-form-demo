<template>
    <div class="spinner-overlay">
        <div class="spinner"></div>
    </div>
</template>

<style scoped>
.spinner {
    margin-top: 1rem;
    width: 32px;
    height: 32px;
    border: 3px solid #ccc;
    border-top-color: transparent;
    border-radius: 50%;
    animation: spin 0.8s linear infinite;
}

.spinner-overlay {
    position: absolute;       
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    display: flex;         
    align-items: center;
    justify-content: center;
    background: rgba(255,255,255,0.4); 
    z-index: 9999;
}

@keyframes spin {
    to {
        transform: rotate(360deg);
    }
}
</style>