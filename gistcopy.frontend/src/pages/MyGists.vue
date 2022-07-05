<template>
    <div class="main-column">
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

export default {
    data() {
        return {
            gists: [],
            show: false
        }
    },
    components: {
        GistPreview
    },
    methods: {
        async getMyGists() {
            const response = await axios.get('https://localhost:7005/api/Gists/my',
            {
                headers: {
                    'Authorization': `Bearer ${localStorage.getItem('jwtToken')}` 
                }
            })
            this.gists = response.data.reverse() 
            if(this.gists.length === 0) {
                this.show = true
            }
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

    margin-top: 8px;
    margin-bottom: 8px;
}
</style>