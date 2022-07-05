<template>
  <div class="comment">
    <div class="comment__author">
      {{comment.user.username}}
      <span v-if="currentUser != undefined">
        <a v-if="currentUser.id == comment.user.id" href="/all" @click.prevent="deleteComment"> ✖</a>
      </span>
    </div>
    <div class="comment__text">{{comment.text}}</div>
  </div>
</template>

<script>
import axios from 'axios'

export default {
  props: {
    comment: {},
    currentUser: undefined
  },
  methods: {
    async deleteComment() {
      let del = confirm("Delete?")
      if (del) {
        await axios.delete("https://localhost:7005/api/Comments/" + this.comment.id,
            {
                headers: {
                    'Authorization': `Bearer ${localStorage.getItem('jwtToken')}` 
                }
            })
        this.$emit('commentDelete', this.comment)
      }
    }
  }
}
</script>

<style scoped>
.comment {
    margin-top: 8px;
    margin-bottom: 8px;
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

/* Text */

.comment__text {
    padding: 16px;
}

a {
  color: crimson !important;
  text-decoration: none;
}
</style>
