<template>
    <div class="main-column">
        <div class="loader-container" v-if="loading">
            <loading class="loader" :active="loading"
            :is-full-page="true" :color="color"></loading>
        </div>

        <h1>All gists:</h1>
        <h2 v-if="show">No public gists. 
            <router-link style="color: var(--text-color);" to="/">Create?</router-link>
        </h2>
        <div class="gists" v-for="gist in gists">
            <GistPreview 
                :gist="gist"
            >
            </GistPreview>
        </div>
    </div>
</template>

<script>
import axios from 'axios';
import GistPreview from '../components/GistPreview'
import Loading from 'vue3-loading-overlay'
import url from '@/config'

export default {
    data() {
        return {
            gists: [],
            show: false,
            loading: true,
            color: 'var(--text-color)'
        }
    },
    components: {
        GistPreview,
        Loading,
    },
    methods: {
        async getAllGists() {
            const response = await axios.get(url + 'api/Gists')
            
            this.loading = false
            if(response.data.length === 0) {
                this.show = true
            }
            this.gists = response.data.reverse() 
        }
    },
    mounted() {
        document.title = "All gists • Gist copy"
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
    position: relative;

    margin-top: 16px;
    margin-bottom: 16px;
}

.loader-container {
    position: absolute;
    display: flex;
    width: 100%;
    height: 100%;
    align-items: center;
    justify-content: center;
}
</style>