<template>
    <div class="gist">
        <div class="gist__description">
            <span class="gist__description__input"><v-link :href="'/gist/' + id">{{description}}</v-link></span>
        </div>

        <div class="gist__filename">
            <span class="gist__filename__input">{{filename}}</span>
        </div>

        <div class="gist__text">
            <pre class="gist__pre"><code>{{text}}</code></pre>
        </div>
    </div>
</template>

<script>
import VLink from './VLink.vue'

export default {
  props: {
    id: String,
    description: String,
    filename: String,
    text: String,
  },
  components: {
    VLink
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
}

.gist__description__input {
    width: 100%;
    height: 100%;
    border: none;
    background-color: rgba(0,0,0,0);
    color: var(--text-color);
    font-size: 16px;
}

.gist__description__input a {
    color: var(--text-color);
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