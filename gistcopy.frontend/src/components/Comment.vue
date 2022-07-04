<template>
  <div class="comment">
    <div class="comment__author">Vlad</div>
    <div class="comment__text">{{text}}</div>
  </div>
</template>

<script>
export default {
  props: {
    text: String
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
</style>
