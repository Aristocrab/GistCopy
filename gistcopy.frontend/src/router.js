import { createRouter, createWebHistory } from 'vue-router'
import Index from './pages/Index.vue'
import AllGists from './pages/AllGists.vue'
import MyGists from './pages/MyGists.vue'
import Gist from './pages/Gist.vue'

const routes = [
  {
    path: '/',
    name: 'index',
    component: Index
  },
  {
    path: '/all',
    name: 'all',
    component: AllGists
  },
  {
    path: '/gist/:id',
    name: 'gist',
    component: Gist
  },
  {
    path: '/my',
    name: 'my',
    component: MyGists
  }
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
