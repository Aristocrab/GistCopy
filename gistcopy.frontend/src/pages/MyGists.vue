<template>
    <div class="main-column">
        <div class="loader-container" v-if="loading">
            <loading class="loader" :active="loading"
            :is-full-page="true" :color="color"></loading>
        </div>

        <h1>My gists:</h1>
        <h2 v-if="show">No gists. 
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
        async getMyGists() {
            const response = await axios.get('https://localhost:7005/api/Gists/my',
            {
                headers: {
                    'Authorization': `Bearer ${localStorage.getItem('jwtToken')}` 
                }
            })
            
            this.loading = false
            if(response.data.reverse === 0) {
                this.show = true
            }
            this.gists = response.data.reverse() 
        }
    },
    mounted() {
        document.title = "My gists • Gist copy"
        this.getMyGists()
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

    margin-top: 8px;
    margin-bottom: 8px;
}

.loader-container {
    position: absolute;
    display: flex;
    width: 100%;
    height: calc(100vh - 60px);
    align-items: center;
    justify-content: center;
}
</style>