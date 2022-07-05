<template>
    <form method="post" @submit.prevent>
        <div class="gist">
            <div class="gist__description">
                <input required v-model="description" name="description" class="gist__description__input" type="text" placeholder="Gist description...">
            </div>

            <div class="gist__filename">
                <input oninvalid="this.setCustomValidity('Enter a filename')" required pattern="[\w.]*\.\w+" v-model="filename" name="filename" class="gist__filename__input" type="text" placeholder="Filename...">
            </div>

            <div class="gist__editor">
                <pre class="gist__pre"><code><textarea required v-model="text" name="text" placeholder="Your code here..." 
                    class="gist__editor__textarea" wrap='off'></textarea></code></pre>
            </div>
        </div> 

        <div class="gist__buttons">
            <button @click="createGist" class="gist__button gist__submit-button" type="submit">Create</button>
        </div>
    </form>
</template>

<script>
import axios from 'axios';

export default { 
    data() {
        return {
            description: '',
            filename: '',
            text: '',
        }
    },
    methods: {
        async createGist() {
            await axios.post('https://localhost:7005/api/Gists/new', {
                description: this.description,
                filename: this.filename,
                text: this.text,
            }, {
                headers: {
                    'Authorization': `Bearer ${localStorage.getItem('jwtToken')}` 
                }
            })
            await this.$router.push('/all')
        }
    } 
}
</script>

<style scoped>
.gist {
    margin-top: 16px;
    display: flex;
    flex-direction: column;
    width: 100%; 
    border-radius: 16px 16px 4px 4px;
    border: var(--border);
    background-color: var(--main-bg-color);
}

/* Description */

.gist__description {
    border-radius: 16px 16px 0 0;
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

.gist__editor {
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

.gist {
    height: 500px;
}

.gist__editor {
    font-size: 16px;
    padding: 16px;
    height: 100%;
}

.gist__pre {
    margin: 0;
    height: 100%;
}

.gist__pre > code {
    display: flex;
    height: 100%;
}

.gist__editor__textarea {
    font-size: 16px;
    color: var(--text-color);
    margin: 0;
    padding: 4px;
    resize: none;
    height: 100%;
    width: 100%;
    border: none;
    line-height: 1.2;
    background-color: rgba(0,0,0,0);
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