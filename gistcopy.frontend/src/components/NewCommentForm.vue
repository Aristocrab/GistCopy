<template>
  <form method="post" @submit.prevent>
    
    <div class="comment">
    <div class="comment__text">
      <input required v-model="comment.text" name="description" class="comment__text__input" type="text" placeholder="Comment...">
    </div>
  </div>
    
    <div class="gist__buttons">
            <button @click="addComment" class="gist__button gist__submit-button" type="submit">Add comment</button>
        </div>
    </form>
</template>

<script>
import axios from "axios";

export default {
    data() {
        return {
            comment: {}
        }
    },
    methods: {
        async addComment() {
            await axios.post('https://localhost:7005/api/Comments/add', {
                gistId: this.$route.params.id,
                text: this.comment.text,
            })
            this.$emit('add', this.comment)
            this.comment = {
              text: ""
            }
        }
    } 
}
</script>


<style scoped>
.comment {
    margin-top: 16px;
    display: flex;
    flex-direction: column;
    width: 100%; 
    border-radius: 4px;
    border: var(--border);
    background-color: var(--main-bg-color);
}

/* Description */

.comment__author {
    border-radius: 4px 4px 0 0;
    height: 48px;
    line-height: 32px;
    background-color: var(--secondary-bg-color);
    border-bottom: var(--border);
    padding: 8px 16px 8px 16px;
}

.comment__author__input {
    width: 100%;
    height: 100%;
    border: none;
    background-color: rgba(0,0,0,0);
    color: var(--text-color);
    font-size: 16px;
}

/* Editor */
.comment__text {
    height: 48px;
    line-height: 32px;
    padding: 8px 16px 8px 16px;
}

.comment__text__input {
    width: 100%;
    height: 100%;
    border: none;
    background-color: rgba(0,0,0,0);
    color: var(--text-color);
    font-size: 16px;
}

/* Gist buttons */
.gist__buttons {
    margin-top: 10px;
    display: flex;
    justify-content: right;
}

.gist__button  {
    cursor: pointer;
    color: var(--text-color);
    display: inline-flex;
    align-items: center;
    justify-content: center;
    padding: 14px;
    height: 40px;
    font-size: 14px;
    border: var(--border);
    border-radius: 4px;
    background-color: var(--main-bg-color);
}
</style>