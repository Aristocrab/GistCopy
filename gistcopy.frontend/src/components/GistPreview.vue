<template>
    <div class="gist">
        <router-link :to="'/gist/' + gist.id" class="gist__link" v-if="gist.user">
            <span class="gist__description">
                <span class="gist__description__text">{{gist.description}}</span> <i>by {{gist.user.username}}</i> <span class="isPrivate" v-if="gist.private">private</span>
            </span>
            
            <span class="gist__timeCreated">
                {{gist.timeCreated}}
            </span>
        </router-link>

        <div class="gist__filename">
            <span class="gist__filename__text">{{gist.filename}}</span>
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
.gist__link {
    width: 100%;
    display: flex;
    justify-content: space-between;
    border-radius: 4px 4px 0 0;
    height: 48px;
    line-height: 32px;
    background-color: var(--secondary-bg-color);
    border-bottom: var(--border);
    padding: 8px 16px 8px 16px;
    color: var(--text-color);
    text-decoration: none;
}

.gist__description {
   text-decoration: none;
}

.gist__description__text {
    border: none;
    background-color: rgba(0,0,0,0);
    color: var(--text-color);
    font-size: 16px;
    text-decoration: underline;
}

.isPrivate {
    background-color: var(--main-bg-color);
    padding: 4px;
    border-radius: 5px;
    border: var(--border);
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