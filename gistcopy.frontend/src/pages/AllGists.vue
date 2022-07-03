<template>
    <div class="main-column">
        <h1>All gists:</h1>
        <div class="gists" v-for="gist in gists">
            <Gist 
                v-bind:id="gist.id"
                v-bind:description="gist.description"
                v-bind:filename="gist.filename"
                v-bind:text="gist.text"
            >
            </Gist>
        </div>
    </div>
</template>

<script>
import axios from 'axios';
import Gist from '../components/Gist'

export default {
    data() {
        return {
            gists: []
        }
    },
    components: {
        Gist
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