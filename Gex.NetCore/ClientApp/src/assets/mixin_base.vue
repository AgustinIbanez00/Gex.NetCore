<script>
	import Vue from 'vue';
	import Vuex from 'vuex';
	Vue.use(Vuex);
	const store = new Vuex.Store({
		state: {
			estado_actual: 1,
			id: 0,
			titulo_txt: '',
		},
		mutations: {
			estado(state, estado) {
				state.estado_actual = estado;
			},
		}
	});

	const mixin_base = {
		store,
		data: () => ({
			estados:{
				lista: 1,
				creacion: 2,
				edicion: 3,
				preguntas: 4,
			},
		}),
		methods: {
			...Vuex.mapMutations(['estado']),
			top(){
				window.scroll({
					top: 0,
					left: 0,
					behavior: 'smooth'
				});
			},
			lista: function(recargar){
				var vm = this;
				//if(recargar) vm.cargar_tabla();
				vm.estado_actual = vm.estados.lista;
				vm.top();
			},
			cancelar: function(){
				var vm = this;
				vm.estado_actual =vm.estados.lista;
				vm.top();
			},
			creacion: function(){
				var vm = this;
				vm.estado_actual = vm.estados.creacion;
				vm.top();
			},
			edicion: function(id,modo_preguntas = false){
				var vm = this;
				//var examen = vm.examenes.find(e =>e.id = id);
				//vm.examen = examen;

				vm.estado_actual = modo_preguntas ? vm.estados.preguntas : vm.estados.edicion;
				vm.top();
			},
			//Omg
			formatDate (date) {
				if (!date) return null;
				const [year, month, day] = date.split('-');
				return `${day}/${month}/${year}`;
			},
			parseDate (date) {
				if (!date) return null;
				const [day, month, year] = date.split('/');
				return `${year}-${month.padStart(2, '0')}-${day.padStart(2, '0')}`;
			},
		},
		mounted() {
			var vm = this;
			switch(vm.$route.name){
				case 'listar_examen': vm.estado_actual = vm.estados.lista; vm.lista(); break;
				case 'editar_examen': vm.estado_actual = vm.estados.edicion; vm.edicion(vm.$route.params.id); break;
				case 'crear_examen': vm.estado_actual = vm.estados.creacion; vm.creacion(); break;
				case 'editar_preguntas': vm.edicion(vm.$route.params.id,true); break;
			}
		},
		computed:{
			...Vuex.mapState(['estado_actual', 'id','titulo_txt']),
			estado_actual:{
				get: function () {return this.$store.state.estado_actual;},
				set: function (val) {this.$store.state.estado_actual = val;}
			},
			id:{
				get: function () {return this.$route.params.id;},
				set: function (val) {this.$route.params.id = val;}
			},
			titulo_txt:{
				get: function () {return this.$store.state.titulo_txt;},
				set: function (val) {this.$store.state.titulo_txt = val;}
			},
			route: function(){
				var vm = this;
				return vm.$route.name;
			},
			tab_actual: function(){
				var vm = this;
				return vm.$route.path.split('/')[1];
			},
			titulo(){
				var vm = this;
				let titulo = "";
				if(vm.route == `${vm.tab_actual}_preguntas` && vm.titulo_txt) titulo = vm.titulo_txt;
				else{
					switch(vm.tab_actual){
						case "examen": titulo = "Exámenes"; break;
						case "materia": titulo = "Materias"; break;
						case "mesa": titulo = "Mesas"; break;
						case "curso": titulo = "Cursos"; break;
						case "alumno": titulo = "Alumnos"; break;
						case "inscripcion": titulo = "Inscripciones"; break;
					}
				} 
				return titulo;
			},
			elementos(){
				var vm = this;
				let res = '';
				switch(vm.tab_actual){
					case 'examen': res = 'Exámenes'; break;
					case 'materia': res = 'Materias'; break;
					case 'mesa': res = 'Mesas'; break;
					case 'curso': res = 'Cursos'; break;
					case 'alumno': res = 'Alumnos'; break;
					case 'inscripcion': res = 'Inscripciones'; break;
				}
				return res;
			},
			elemento(){
				var vm = this;
				let res = '';
				switch(vm.tab_actual){
					case 'examen': res = 'Exámen'; break;
					case 'materia': res = 'Materia'; break;
					case 'mesa': res = 'Mesa'; break;
					case 'curso': res = 'Curso'; break;
					case 'alumno': res = 'Alumno'; break;
					case 'inscripcion': res = 'Inscripción'; break;
				}
				return res;
			},
		},
	}
	export default mixin_base;
</script>