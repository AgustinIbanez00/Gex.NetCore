import Vue from 'vue'
import VueRouter from 'vue-router'
import Login from '../views/Login.vue'
import comp_examenes from '../views/comp_examenes.vue'

Vue.use(VueRouter)

const routes = [
  {
    path: '/login',
    name: 'Iniciar sesión',
    component: Login
  },
	{
		path: '/examenes',
		name: 'Exámenes',
		component: comp_examenes,
  },
]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
