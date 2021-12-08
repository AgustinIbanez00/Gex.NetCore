import Vue from 'vue'
import VueRouter from 'vue-router'
import Login from '../views/Login.vue'
import Examen from '../views/comp_examen.vue'
import ExamenPreguntas from '../views/comp_examen_preguntas.vue'
import Materia from '../views/comp_materia.vue'
import MateriaPreguntas from '../views/comp_materia_preguntas.vue'
import Mesa from '../views/mesas/comp_mesa.vue'
import ModalPregunta from '../views/comp_modal_pregunta.vue'
Vue.use(VueRouter)
const routes = [
	{
		path: '/login',
		name: 'Iniciar sesión',
		component: Login
	},
	//-- MATERIAS --
	{
		path: '/materia',
		name: 'listar_materia',
		component: Materia
	},
	{
		path: '/materia/crear',
		name: 'crear_materia',
		component: Materia
	},
	{
		path: '/materia/:id',
		name: 'editar_materia',
		component: Materia,
		children: [
			{
				path: 'preguntas',
				name: 'materia_preguntas',
				component: MateriaPreguntas,
				children: [
					{
						path: ':id',
						name: 'editar_materia_preguntas',
						component: ModalPregunta,
					}
				]
			},
		]
	},
	//-- /MATERIAS --
	//-- MESAS --
	{
		path: '/mesa/',
		name: 'listar_mesa',
		component: Mesa,
	},
	{
		path: '/mesa/crear',
		name: 'crear_mesa',
		component: Mesa
	},
	{
		path: '/mesa/:id',
		name: 'editar_mesa',
		component: Mesa,
	},
	//-- /MESAS --

	//-- EXÁMENES --
	{
		path: '/examen',
		name: 'listar_examen',
		component: Examen
	},
	{
		path: '/examen/crear',
		name: 'crear_examen',
		component: Examen
	},
	{
		path: '/examen/:id',
		name: 'editar_examen',
		component: Examen,
		children: [
			{
				path: 'preguntas',
				name: 'examen_preguntas',
				component: ExamenPreguntas,
			}
		]
	},
	//-- /EXÁMENES --
]
const router = new VueRouter({
	mode: 'history',
	base: process.env.BASE_URL,
	routes
})
export default router
