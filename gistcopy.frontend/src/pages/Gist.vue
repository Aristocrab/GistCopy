<template>
  <div class="main-column">
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

export default {
    data() {
        return {
            gist: {},
            comments: []
        }
    },
    components: {
        Gist,
        Comment,
        NewCommentForm
    },
    methods: {
        async getGistById() {
            const response = await axios.get('https://localhost:7005/api/Gists/' + this.$route.params.id)
            this.gist = response.data
        
            document.title = "Gist: " + response.data.description + " • Gist copy"
        },   
        async getComments() {
            const response = await axios.get("https://localhost:7005/api/Comments/"+this.$route.params.id)
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