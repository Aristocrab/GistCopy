<template>
    <div class="gist">
        <router-link :to="'/gist/' + gist.id" class="gist__description" v-if="gist.user">
            <span class="gist__description__input">{{gist.description}} <i>by {{gist.user.username}}</i></span>
        </router-link>

        <div class="gist__filename">
            <span class="gist__filename__input">{{gist.filename}}</span>
        </div>

        <div class="gist__text">
            <pre class="gist__pre"><code>{{gist.text}}</code></pre>
        </div>
    </div>
</template>

<script>
export default {
  props: {
    gist: {}
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
    border-radius: 4px 4px 0 0;
    height: 48px;
    line-height: 32px;
    background-color: var(--secondary-bg-color);
    border-bottom: var(--border);
    padding: 8px 16px 8px 16px;
    color: var(--text-color);
}

.gist__description__input {
    width: 100%;
    height: 100%;
    border: none;
    background-color: rgba(0,0,0,0);
    color: var(--text-color);
    font-size: 16px;
}

/* Tabs */

.gist__filename {
    padding: 8px 16px 8px 16px;
    height: 44px;
    margin: 0;
    display: flex;
    list-style-type: none;
    border-bottom: var(--border);
}

.gist__filename__input {
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