<template>
  <Header></Header>
  <main>
    <component  :is="ViewComponent" />
  </main>
</template>

<script>
import routes from './routes'
import Header from './components/Header.vue'

export default {
  components: {
    Header
},

  data: () => ({
    currentRoute: window.location.pathname
  }),

  computed: {
    ViewComponent () {
      const matchingPage = routes[this.currentRoute]
      return require(`./pages/${matchingPage}.vue`).default
    }
  },

  render () {
    return h(this.ViewComponent)
  },

  created () {
    window.addEventListener('popstate', () => {
      this.currentRoute = window.location.pathname
    })
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
</style>