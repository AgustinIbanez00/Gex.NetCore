<script>
	import Vue from 'vue';
	import Vuex from 'vuex';
	Vue.use(Vuex);
	const store = new Vuex.Store({
		state: {
			estado_actual: 1,
			titulo_txt: '',
			eliminar_txt: '',
			eliminar_id: 0,
			modal_eliminar: false,
			tema_pregunta: '',
			alerta_txt: '',
			lista: [],
			cargando_lista: false,
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
			url_api: 'http://localhost:5000/api',
		}),
		watch: {
			async $route(to, from) {
				var vm = this;
				if(to.name == `listar_${vm.tab_actual}`) vm.cargar_tabla();//LISTA
				if (to && to.params.id && to.params != 'crear') {//EDICIÓN
					vm.edicion(to.params.id);
				}
			}
		},
		methods: {
			...Vuex.mapMutations(['estado']),
			top(){
				window.scroll({
					top: 0,
					left: 0,
					behavior: 'smooth'
				});
			},
			listar: function(recargar){
				var vm = this;
				vm.top();
				if(vm.route == `/${vm.tab_actual}`){
					vm.cargar_tabla(vm.tab_actual);
				}else{
					vm.$router.push(`/${vm.tab_actual}`);
				} 
			},
			cargar_tabla(){
				var vm = this;
				console.log(vm.$route.name )
				if(vm.$route.name != `listar_${vm.tab_actual}`) return;
				vm.cargando_lista = true;
				axios.get(`${vm.url_api}/${vm.tab_actual}`, vm.axios_headers).then(res => {
					switch(vm.tab_actual){
						case 'materia': vm.lista = res.data.data; break;
						case 'examen': vm.lista = res.data.data; break;
					}
					//ALERTA
				}).catch(err => console.log(err))
				.then(() => vm.cargando_lista = false);
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
			async edicion(id){
				var vm = this;
				await axios.get(`${vm.url_api}/${vm.tab_actual}/${id}`, vm.axios_headers).then(res => {
					switch(vm.tab_actual){
						case 'materia': vm.materia = res.data.data; break;
					}
				}).catch(err => console.log(err));
				vm.top();
			},
			async eliminar(eliminar = 0){
				var vm = this;
				if(eliminar){//ELIMINA Y CIERRA EL MODAL ELIMINAR
					await axios.delete(`${vm.url_api}/${vm.tab_actual}?id=${vm.eliminar_id}`, vm.axios_headers)
					.then(res => {
						vm.lista.splice(vm.lista.findIndex(item => item.id == vm.eliminar_id), 1);
					})
					.catch(error => {console.log(error);})
					.then(() => {vm.modal_eliminar = false;});
				}else{//ABRE EL MODAL ELIMINAR
					await axios.get(`${vm.url_api}/${vm.tab_actual}/${vm.eliminar_id}`, vm.axios_headers)
					.then(res => {
						switch(vm.tab_actual){
							case 'materia': vm.eliminar_txt = `la materia: ${res.data.data.nombre}`; break;
						}
						vm.modal_eliminar = true;
					}).catch(err => console.log(err));
				}
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
		computed:{
			...Vuex.mapState(['estado_actual', 'id','titulo_txt','eliminar_txt','lista','tema_pregunta']),
			estado_actual:{
				get: function () {return this.$store.state.estado_actual;},
				set: function (val) {this.$store.state.estado_actual = val;}
			},
			lista:{
				get: function () {return this.$store.state.lista;},
				set: function (val) {this.$store.state.lista = val;}
			},
			cargando_lista:{
				get: function () {return this.$store.state.cargando_lista;},
				set: function (val) {this.$store.state.cargando_lista = val;}
			},
			titulo_txt:{
				get: function () {return this.$store.state.titulo_txt;},
				set: function (val) {this.$store.state.titulo_txt = val;}
			},
			eliminar_txt:{
				get: function () {return this.$store.state.eliminar_txt;},
				set: function (val) {this.$store.state.eliminar_txt = val;}
			},
			tema_pregunta:{
				get: function () {return this.$store.state.tema_pregunta;},
				set: function (val) {this.$store.state.tema_pregunta = val;}
			},
			eliminar_id:{
				get: function () {return this.$store.state.eliminar_id;},
				set: function (val) {this.$store.state.eliminar_id = val;}
			},
			modal_eliminar:{
				get: function () {return this.$store.state.modal_eliminar;},
				set: function (val) {this.$store.state.modal_eliminar = val;}
			},
			alerta_txt:{
				get: function () {return this.$store.state.alerta_txt;},
				set: function (val) {this.$store.state.alerta_txt = val;}
			},
			id(){
				var vm = this;
				return vm.$route.params.id;
			},
			pregunta_id(){
				var vm = this;
				return vm.$route.params.pregunta_id;
			},
			route: function(){
				var vm = this;
				return vm.$route.name;
			},
			tab_actual: function(){
				var vm = this;
				return vm.$route.path.split('/')[1];
			},
			axios_headers(){
				return {
					headers: {
						'Content-Type': 'application/json',
						'Authorization': `Bearer ${window.$cookies.get("gex_session")}`
					}
				}
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
						case "usuario": titulo = "Usuarios"; break;
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
					case 'usuario': res = 'Usuarios'; break;
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
					case 'usuario': res = 'Usuario'; break;
				}
				return res;
			},
		},
		mounted() {
			var vm = this;
			switch(vm.route){
				case `editar_${vm.tab_actual}`: vm.edicion(vm.id); break;
				case `listar_${vm.tab_actual}`: vm.cargar_tabla(); break;
			}
		},
	}
	export default mixin_base;
</script>

