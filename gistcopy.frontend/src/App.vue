<template>
  <Header @logged="logged" @logout="logout" :currentUser="currentUser"></Header>
  <main class="main">
    <router-view :currentUser="currentUser"/>
  </main>
</template>

<script>
import Header from "@/components/Header";
import axios from 'axios'
import url from '@/config'

export default {
  data() {
    return {
        currentUser: undefined
    }
  },
  components: {
    Header
  },
  methods: {
    logged() {
        this.loggedChange()
    },
    async logout() {
        localStorage.removeItem('jwtToken')
        this.loggedChange()
        if(this.$route.path === "/my") {
            await this.$router.push('/all')
        }
  },
    async loggedChange() {      
      const response = await axios.get(url + 'api/Users/currentUser', {
          headers: {
              'Authorization': `Bearer ${localStorage.getItem('jwtToken')}` 
          }
      })
      if(response.data.username !== "") {
            this.currentUser = response.data
      } else {
            this.currentUser = undefined
      }
    }
  },
  watch:{
      $route (to, from) {
          this.loggedChange()
      }
  },
  mounted() {
    this.loggedChange()
  }
}
</script>

<style>
:root {
    --text-color: rgb(203, 213, 225);
    --active-text-color: rgb(228, 233, 238);
    --main-bg-color: rgba(15, 23, 42, 0.7);
    --secondary-bg-color: rgb(30 41 59/.5);

    --border: 1px solid rgb(100 116 139/.3);
}

@import url('https://fonts.googleapis.com/css2?family=Inter&display=swap');

*, *::before, *::after {
    box-sizing: border-box;
}

html, body {
    margin: 0;
    padding: 0;
    height: 100%;
}

body {
    font-family: 'Inter', sans-serif;
    margin: 0;
    padding: 0;
    background-image: url('img/back.png');
    background-size: cover;
    
    color: var(--text-color);
}

#app {
    display: flex;
    flex-direction: column;
    width: 100%;
    min-height: 100%;
    background-image: url('img/cell.svg');
}

main {
    flex-grow: 1;
    width: 100%;
    display: grid;
    grid-template-columns: 4fr 8fr 4fr;
}

h1 {
    margin: 0;
}

.deleteButton {
    color: crimson !important;
    text-decoration: none;
}

.deleteButton:hover {
    color: hotpink !important;
}

@media screen and (max-width: 1200px) {
    main, .header {
        grid-template-columns: 2fr 8fr 2fr;
    }
}

@media screen and (max-width: 800px) {
    main, .header {
        grid-template-columns: 0.5fr 8fr 0.5fr;
    }
}
</style>