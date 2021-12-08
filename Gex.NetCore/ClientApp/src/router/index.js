import Vue from 'vue'
import VueRouter from 'vue-router'
import Login from '../views/Login.vue'
import Examen from '../views/examenes/comp_examen.vue'
import ExamenPreguntas from '../views/examenes/comp_examen_preguntas.vue'
import Materia from '../views/materias/comp_materia.vue'
import MateriaPreguntas from '../views/materias/comp_materia_preguntas.vue'
import Mesa from '../views/mesas/comp_mesa.vue'
import ModalPregunta from '../views/comp_modal_pregunta.vue'
import Curso from '../views/cursos/comp_curso.vue'
import Alumno from '../views/alumnos/comp_alumno.vue'
import Inscripcion from '../views/inscripciones/comp_inscripcion.vue'
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
		component: Materia,
		children: [
			{
				path: 'crear',
				name: 'crear_materia',
			},
		]
	},
	//-- MATERIA --
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
						path: ':pregunta_id',
						name: 'editar_materia_preguntas',
						component: ModalPregunta,
					}
				]
			},
		]
	},
	//-- /MATERIAS --

	// -- CURSOS --
	{
		path: '/curso',
		name: 'listar_curso',
		component: Curso,
		children: [
			{
				path: 'crear',
				name: 'crear_curso',
			},
		]
	},
	// -- /CURSOS --

	// -- ALUMNOS --
	{
		path: '/alumno',
		name: 'listar_alumno',
		component: Alumno,
		children: [
			{
				path: 'crear',
				name: 'crear_alumno',
			},
		]
	},
	// -- /ALUMNOS --

	// -- INSCRIPCIONES --
	{
		path: '/inscripcion',
		name: 'listar_inscripcion',
		component: Inscripcion,
		children: [
			{
				path: 'crear',
				name: 'crear_inscripcion',
			},
		]
	},
	// -- /INSCRIPCIONES --

	//-- MESAS --
	{
		path: '/mesa/',
		name: 'listar_mesa',
		component: Mesa,
		children: [
			{
				path: 'crear',
				name: 'crear_mesa',
			},
		]
	},
	//-- MESAS --
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
		component: Examen,
		children: [
			{
				path: 'crear',
				name: 'crear_examen',
			},
		]
	},
		//-- EXÁMEN --
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
