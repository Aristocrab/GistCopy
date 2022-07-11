<template>
  <div class="main-column">
        <div class="loader-container" v-if="loading">
            <loading class="loader" :active="loading"
            :is-full-page="true" :color="color"></loading>
        </div>

        <h1>Gist:</h1>
        <Gist 
            :currentUser="$attrs.currentUser"
            :gist="gist"
        >
        </Gist>
    
        <h2 style="margin-bottom: 0;">Comments:</h2>
        <div v-if="comments.length > 0" class="comments" v-for="comment in comments">
          <Comment 
            :comment="comment" 
            :currentUser="$attrs.currentUser"
            @commentDelete="commentDelete">
          </Comment>
        </div>
        <div v-else>
          No comments yet
        </div>
            
        <h2 style="margin-bottom: 0;">Add comment:</h2>
        <NewCommentForm style="margin-bottom: 50px" @add="addComment"></NewCommentForm>
    </div>
</template>

<script>
import axios from 'axios';
import Gist from '../components/Gist'
import Comment from '../components/Comment'
import NewCommentForm from '../components/NewCommentForm'
import Loading from 'vue3-loading-overlay'
import url from '@/config'

export default {
    data() {
        return {
            gist: {},
            comments: [],
            loading: true,
            color: 'var(--text-color)'
        }
    },
    components: {
        Gist,
        Comment,
        NewCommentForm,
        Loading,
    },
    methods: {
        async getGistById() {
            const response = await axios.get(url + 'api/Gists/' + this.$route.params.id,
            {
                headers: {
                    'Authorization': `Bearer ${localStorage.getItem('jwtToken')}` 
                }
            })
            this.gist = response.data
        
            document.title = "Gist: " + response.data.description + " • Gist copy"
        },   
        async getComments() {
            const response = await axios.get(url + "api/Comments/"+this.$route.params.id,
            {
                headers: {
                    'Authorization': `Bearer ${localStorage.getItem('jwtToken')}` 
                }
            })
            this.comments  = response.data;
        },
        async commentDelete() {
            await this.getComments()
        },
        addComment() {
            this.getComments()
        }
    },
    mounted() {
        this.getGistById()
        this.getComments()
        this.loading = false
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
    height: calc(100vh - 60px);
    align-items: center;
    justify-content: center;
}
</style>
