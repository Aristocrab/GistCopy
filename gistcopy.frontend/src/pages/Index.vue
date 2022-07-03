<template>
    <div class="main-column">
        <h1>Create new gist:</h1>
        <NewGistForm></NewGistForm>
    </div>
</template>

<script>
import NewGistForm from '@/components/NewGistForm.vue';

export default { 
    components: {
        NewGistForm
    }
}
</script>

<style scoped>
.main-column {
    grid-column-start: 2;
    grid-column-end: 3;
    display: flex;
    flex-direction: column;
    justify-content: center;
}
</style>