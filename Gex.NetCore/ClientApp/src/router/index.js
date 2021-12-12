import Vue from 'vue'
import VueRouter from 'vue-router'
import Login from '../views/Login.vue'
import Examen from '../views/examen/comp_examen.vue'
import ExamenPreguntas from '../views/examen/comp_examen_preguntas.vue'
import Materia from '../views/materia/comp_materia.vue'
import MateriaPreguntas from '../views/materia/comp_materia_preguntas.vue'
import Mesa from '../views/mesa/comp_mesa.vue'
import Curso from '../views/curso/comp_curso.vue'
import Alumno from '../views/alumno/comp_alumno.vue'
import Inscripcion from '../views/inscripcion/comp_inscripcion.vue'
import Usuario from '../views/usuario/comp_usuario.vue'
import ModalRendir from '../views/modal/comp_modal_rendir.vue'
import ModalPregunta from '../views/modal/comp_modal_pregunta.vue'
Vue.use(VueRouter)
const routes = [
	{
		path: '/login',
		name: 'Iniciar sesión',
		component: Login
	},

	//-- USUARIOS --
	{
		path: '/usuario',
		name: 'listar_usuario',
		//ADMIN    -> USUARIOS
		//PROFESOR -> ALUMNOS
		//ALUMNO   -> COMPAÑEROS|PROFESORES
		component: Usuario,
		children: [
			{
				path: 'crear',
				name: 'crear_usuario',
			},
		]
	},
	//-- USUARIO
	{
		path: '/usuario/:id',
		name: 'editar_usuario',
		component: Usuario,
	},
	//-- /USUARIOS --

	//-- MATERIAS --
	{
		path: '/materia',
		name: 'listar_materia',
		component: Materia,
		children: [{
			path: 'crear',
			name: 'crear_materia',
		},]
	},
	//-- MATERIA --
	{
		path: '/materia/:id',
		name: 'editar_materia',
		component: Materia,
		children: [
			{
				path: 'preguntas/crear',
				name: 'crear_materia_pregunta',
				component: ModalPregunta,
			},
			{
				path: 'preguntas',
				name: 'materia_preguntas',
				component: MateriaPreguntas,
			},
			{
				path: 'preguntas/:pregunta_id',
				name: 'editar_materia_pregunta',
				component: ModalPregunta,
				params: true
			}
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
	//-- MESA --
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
			},
			{
				path: 'rendir',
				name: 'examen_rendir',
				component: ModalRendir,
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
