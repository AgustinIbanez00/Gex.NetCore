import Vue from 'vue'
import VueRouter from 'vue-router'
import Login from '../views/Login.vue'
import Examenes from '../views/comp_examenes.vue'
import Preguntas from '../views/comp_examen_preguntas.vue'
import Materias from '../views/comp_materias.vue'
Vue.use(VueRouter)
const routes = [
	{
		path: '/login',
		name: 'Iniciar sesión',
		component: Login
	},
	{
		path: '/materias',
		name: 'Materias',
		component: Materias
	},
	{
		path: '/materias/crear',
		name: 'Crear materia',
		component: Materias
	},
	{
		path: '/materias/:id',
		name: 'Materia',
		component: Materias,
		/*children: [
			{
				path: 'respuestas',
				name: 'Respuestas',
				component: Respuestas,
			}
		]*/
	},
	{
		path: '/examenes',
		name: 'Exámenes',
		component: Examenes
	},
	{
		path: '/examenes/crear',
		name: 'Crear exámen',
		component: Examenes
	},
	{
		path: '/examenes/:id',
		name: 'Exámen',
		component: Examenes,
		children: [
			{
				path: 'preguntas',
				name: 'Preguntas',
				component: Preguntas,
			}
		]
	},
]
const router = new VueRouter({
	mode: 'history',
	base: process.env.BASE_URL,
	routes
})
export default router
