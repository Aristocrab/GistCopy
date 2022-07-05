<template>
    <Login ref="login" @logged="logged"></Login>
    <Register ref="register" @logged="logged"></Register>
  <header class="header">
    <nav class="nav">
        <div class="left">
            <span class="nav__items">
                <ul class="nav__list">
                    <li class="nav__listitem">
                        <router-link v-if="$route.path === '/'" class="active" to="/">Home</router-link>
                        <router-link v-else="$route.path === '/'" to="/">Home</router-link>
                    </li>
                    <li class="nav__listitem">
                        <router-link v-if="$route.path === '/all'" class="active" to="/all">All gists</router-link>
                        <router-link v-else="$route.path === '/all'" to="/all">All gists</router-link>
                    </li>
                    <li v-if="currentUser !== undefined" class="nav__listitem">
                        <router-link v-if="$route.path === '/my'" class="active" to="/my">My gists</router-link>
                        <router-link v-else="$route.path === '/my'" to="/my">My gists</router-link>
                    </li>
                </ul>
            </span>
        </div>
        <div class="right">
            <span class="nav__items">
                <ul class="nav__list">
                    <li v-if="currentUser === undefined" class="nav__listitem">
                        <a href="/" @click.prevent="openLoginModal">Login</a>
                    </li>
                    <li v-if="currentUser === undefined" class="nav__listitem">
                        <a href="/" @click.prevent="openRegisterModal">Register</a>
                    </li>

                    <li style="display: flex; gap: 4px" v-if="currentUser !== undefined" class="nav__listitem">
                        <img src="https://i.picsum.photos/id/237/300/300.jpg?hmac=9iUR3VHqf0Y9abGyuPZTpEIxHJL0sSvyNtJtDIMSylM" alt="">
                        <span>{{currentUser.username}}</span>
                    </li>
                    <li v-if="currentUser !== undefined" class="nav__listitem">
                        <a href="/" @click.prevent="logout">Logout</a>
                    </li>
                </ul>
            </span>
        </div>
    </nav>
</header>
</template>

<script>
import Login from './Login'
import Register from './Register'

export default { 
    props: {
        currentUser: undefined
    },
    components: {
        Login,
        Register
    },
    emits: ["logged", "logout"],
    methods: {
        openLoginModal() {
            this.$refs.login.openModal()
        },
        openRegisterModal() {
            this.$refs.register.openModal()
        },
        logout() {
            this.$emit('logout')
        },
        logged() {
            this.$refs.login.closeModal()
            this.$refs.register.closeModal()
            this.$emit('logged')
        }
    }
}
</script>

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
}

.nav a.active {
    text-decoration: underline;
}

.nav a:hover {
    text-decoration: underline dotted;
}

.nav__logo > a {
    text-decoration: none;
    color: var(--active-text-color);
}

.nav__list {
    margin: 0;
    padding: 0;
    display: inline-flex;
    align-items: center;
    list-style-type: none;
    gap: 16px;
}

.nav__listitem {
    display: inline-flex;
    align-items: center;
}

.nav__listitem > img {
    border-radius: 100%;
    border: var(--border);
    width: 30px;
}

.nav__listitem > a {
}

.nav__listitem > a {
    color: var(--text-color);
    text-decoration: none;
}

.nav__listitem > a:hover {
    color: var(--active-text-color);
}
</style>