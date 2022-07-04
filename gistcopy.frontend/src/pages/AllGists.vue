<template>
    <div class="main-column">
        <h1>All gists:</h1>
        <div class="gists" v-for="gist in gists">
            <GistPreview 
                v-bind:id="gist.id"
                v-bind:description="gist.description"
                v-bind:filename="gist.filename"
                v-bind:text="gist.text"
            >
            </GistPreview>
        </div>
    </div>
</template>

<script>
import axios from 'axios';
import GistPreview from '../components/GistPreview'

export default {
    data() {
        return {
            gists: []
        }
    },
    components: {
        GistPreview
    },
    methods: {
        async getAllGists() {
            const response = await axios.get('https://localhost:7005/api/Gists')
            this.gists = response.data.reverse() 
        }
    },
    mounted() {
        this.getAllGists()
    }
}
</script>

<style scoped>
.main-column {
    grid-column-start: 2;
    grid-column-end: 3;
    display: flex;
    flex-direction: column;

    margin-top: 8px;
    margin-bottom: 8px;
}
</style>