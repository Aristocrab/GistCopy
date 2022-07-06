<template>
    <div class="gist">
        <div class="gist__description">
            <span class="gist__description__text" v-if="gist.user">
              {{gist.description}} <i>by {{gist.user.username}}</i> <span class="isPrivate" v-if="gist.private">private</span>
              <span v-if="currentUser != undefined">
                <a class="deleteButton" v-if="currentUser.id == gist.user.id" href="/all" @click.prevent="deleteGist"> ✖</a>
              </span>
            </span>
            <span class="gist__timeCreated">
                {{gist.timeCreated}}
            </span>
        </div>

        <div class="gist__filename">
            <span class="gist__filename__text">{{gist.filename}}</span>
        </div>

        <div class="gist__text">
            <pre class="gist__pre"><code>{{gist.text}}</code></pre>
        </div>
    </div>
</template>

<script>
import axios from 'axios';

export default {
  props: {
    gist: {},
    currentUser: undefined,
  },
  methods: {
    async deleteGist() {
      let del = confirm("Delete?")
      if (del) {
        await axios.delete("https://localhost:7005/api/Gists/" + this.$route.params.id,
            {
                headers: {
                    'Authorization': `Bearer ${localStorage.getItem('jwtToken')}` 
                }
            })
        await this.$router.push('/my')
      }
    }
  }
}
</script>

<style scoped>
.gist {
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

.gist__description {
    width: 100%;
    display: flex;
    justify-content: space-between;
    border-radius: 4px 4px 0 0;
    height: 48px;
    line-height: 32px;
    background-color: var(--secondary-bg-color);
    border-bottom: var(--border);
    padding: 8px 16px 8px 16px;
}

.isPrivate {
    background-color: var(--main-bg-color);
    padding: 4px;
    border-radius: 5px;
    border: var(--border);
}

.gist__description__text {
    border: none;
    background-color: rgba(0,0,0,0);
    color: var(--text-color);
    font-size: 16px;
}

.gist__description__text a {
    color: var(--text-color);
}

.gist__filename {
    padding: 8px 16px 8px 16px;
    height: 44px;
    margin: 0;
    display: flex;
    list-style-type: none;
    border-bottom: var(--border);
}

.gist__filename__text {
    height: 28px;
    width: 100%;
    border: none;
    background-color: rgba(0,0,0,0);
    color: var(--text-color);
    font-size: 16px;
    line-height: 28px;
}

/* Editor */
.gist__text {
    font-size: 16px;
    padding: 16px;
}

.gist__pre {
    white-space: pre-wrap;
    margin: 0;
}

.gist__pre > code {
    word-wrap: break-word;
    display: flex;
}
</style>