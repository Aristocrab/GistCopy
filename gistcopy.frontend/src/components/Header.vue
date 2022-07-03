<script>
import VLink from './VLink.vue'

export default {
  components: {
    VLink
  }
}
</script>

<template>
  <header class="header">
    <nav class="nav">
        <div class="left">
            <span class="nav__logo">
                <v-link href="/">Gist copy</v-link>
            </span>
            <span class="nav__items">
                <ul class="nav__list">
                    <li class="nav__listitem">
                        <v-link href="/all">All gists</v-link>
                    </li>
                    <li class="nav__listitem">
                        <v-link href="/my">My gists</v-link>
                    </li>
                </ul>
            </span>
        </div>
        <div class="right">
            <span class="nav__items">
                <ul class="nav__list">
                    <li class="nav__listitem">
                        <a href="#">Login</a>
                    </li>
                </ul>
            </span>
        </div>
    </nav>
</header>
</template>

<style>
.header {
    flex-shrink: 0;
    height: 60px;
    width: 100%;
    display: grid;
    grid-template-columns: 4fr 8fr 4fr;
    border-bottom: var(--border);
}

.nav {
    grid-column-start: 2;
    grid-column-end: 3;
    display: flex;
    height: 100%;
    width: 100%;
    align-items: center;
    justify-content: space-between;
    padding: 6px 16px 6px 16px;
}

.nav__logo > a {
    text-decoration: none;
    color: var(--active-text-color);
}

.nav__list {
    margin: 0;
    padding: 0;
    display: inline-flex;
    list-style-type: none;
}

.nav__listitem > a {
    padding-left: 16px;
}

.nav__listitem > a {
    color: var(--text-color);
    text-decoration: none;
}

.nav__listitem > a:hover {
    color: var(--active-text-color);
}
</style>