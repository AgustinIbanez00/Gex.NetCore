<script>
	import Vue from 'vue';
	import Vuex from 'vuex';
	import VueCookies from "vue-cookies";
	Vue.use(Vuex);
	Vue.use(VueCookies);
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
			alerta_ok: false,
			alerta_ok_txt: '',
			alerta_error: false,
			alerta_error_txt: '',
			usuario_actual: null,
			url_api: 'http://localhost:5000/api',
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
		watch:{
			async $route(to, from) {
				var vm = this;
				if(to.name != 'login'){
					if(!vm.$cookies.get('gex_session')){
						vm.$router.push({ path: 'login' });
						return;
					}else{
						await axios.get(`${vm.url_api}/usuario/me`, vm.axios_headers)
						.then( res => {
							vm.usuario = res.data.data;
							if(to.name == `listar_${vm.tab_actual}`) vm.cargar_tabla();//LISTA
							if (to && to.params.id && to.params != 'crear') {//EDICIÓN
								vm.edicion(to.params.id);
							}
						}).catch( err => {
							vm.usuario = null;
							if(vm.route != 'login') vm.$router.push({ path: 'login' });
						});
					}
				}
			},
		},
		methods:{
			...Vuex.mapMutations(['estado']),
			guardar(listar = false){
				var vm = this;
				var f_guardar = res => {
					if(res.data.success){
						vm.alerta_ok_txt = res.data.message;
						vm.alerta_ok = true
					}
					if(listar) vm.listar();
					else{
						vm[vm.tab_actual] = res.data.data;
						if(vm.id != vm[vm.tab_actual].id) vm.$router.push(`/${vm.tab_actual}/${vm[vm.tab_actual].id}`);
					}
				}
				var f_error = err =>{
					vm.alerta_error_txt = err.response.data.message;
					vm.alerta_error = true;
				}
				if(vm[vm.tab_actual].id){
					axios.patch(`${vm.url_api}/${vm.tab_actual}`,vm[vm.tab_actual], vm.axios_headers)
					.then(f_guardar).catch(f_error);
				}else{
					axios.post(`${vm.url_api}/${vm.tab_actual}`,vm[vm.tab_actual], vm.axios_headers)
					.then(f_guardar).catch(f_error);
				}
			},
			input_numeros(evt){
				var vm = this;
				var code = (evt.which) ? evt.which : evt.keyCode;
				if(code != 8 && (code<48 || code >57)) evt.preventDefault();
			},

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
				if(vm.$route.name != `listar_${vm.tab_actual}`) return;
				vm.cargando_lista = true;
				axios.get(`${vm.url_api}/${vm.tab_actual}`, vm.axios_headers).then(res => {
					vm.lista = res.data.data;
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
					vm[vm.tab_actual] = res.data.data;
				}).catch(err => console.log(err));
				vm.top();
			},
			async eliminar(eliminar = 0){
				var vm = this;
				if(eliminar){//ELIMINA Y CIERRA EL MODAL ELIMINAR
					await axios.delete(`${vm.url_api}/${vm.tab_actual}?id=${vm.eliminar_id}`, vm.axios_headers)
					.then(res => {
						vm.lista.splice(vm.lista.findIndex(item => item.id == vm.eliminar_id), 1);
						if(res.data.success){
							vm.alerta_ok_txt = res.data.message;
							vm.alerta_ok = true
						}
					})
					.catch(error => {
						vm.alerta_error_txt = error.response.data.message;
						vm.alerta_error = true
					})
					.then(() => {vm.modal_eliminar = false;});
				}else{//ABRE EL MODAL ELIMINAR
					await axios.get(`${vm.url_api}/${vm.tab_actual}/${vm.eliminar_id}`, vm.axios_headers)
					.then(res => {
						vm.eliminar_txt = `${vm.tab_actual}: ${res.data.data.nombre}`
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
			...Vuex.mapState(['estado_actual', 'id','titulo_txt','eliminar_txt','lista','tema_pregunta','alerta_error','alerta_error_txt','alerta_ok','alerta_ok_txt','usuario_actual','url_api']),
			url_api:{
				get: function () {return this.$store.state.url_api;},
				set: function (val) {this.$store.state.url_api = val;}
			},
			usuario_actual:{
				get: function () {return this.$store.state.usuario_actual;},
				set: function (val) {this.$store.state.usuario_actual = val;}
			},
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
			alerta_ok:{
				get: function () {return this.$store.state.alerta_ok;},
				set: function (val) {this.$store.state.alerta_ok = val;}
			},
			alerta_ok_txt:{
				get: function () {return this.$store.state.alerta_ok_txt;},
				set: function (val) {this.$store.state.alerta_ok_txt = val;}
			},
			alerta_error:{
					get: function () {return this.$store.state.alerta_error;},
					set: function (val) {
						var vm = this;
						var x = 0;
						if(!vm.alerta_error) x = val === 1 ? true : 1;
						else x = val === 0 ? false : 0;
						vm.$store.state.alerta_error = x;
					}
				},
			alerta_error_txt:{
				get: function () {return this.$store.state.alerta_error_txt;},
				set: function (val) {this.$store.state.alerta_error_txt = val;}
			},
			id(){
				var vm = this;
				return vm.$route.params.id;
			},
			token(){
				var vm = this;
				return vm.$cookies.get('gex_session');
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
				var vm = this;
				return {
					headers: {
						'Content-Type': 'application/json',
						'Authorization': `Bearer ${vm.token}`
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
						case "comision": titulo = "Comisiones"; break;
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
					case 'comision': res = 'Comisiones'; break;
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
					case 'comision': res = 'Comision'; break;
					case 'alumno': res = 'Alumno'; break;
					case 'inscripcion': res = 'Inscripción'; break;
					case 'usuario': res = 'Usuario'; break;
				}
				return res;
			},
		},
		async mounted(){
			var vm = this;
			if(vm.route != 'login'){
				if(!vm.$cookies.get('gex_session')){
					vm.$router.push({ path: 'login' });
					return;
				}else{
					await axios.get(`${vm.url_api}/usuario/me`, vm.axios_headers)
					.then( res => {
							vm.usuario = res.data.data;
							switch(vm.route){
								case `editar_${vm.tab_actual}`: vm.edicion(vm.id); break;
								case `listar_${vm.tab_actual}`: vm.cargar_tabla(); break;
							}
						}
					).catch( err => {
							vm.usuario = null;
							if(vm.route != 'login') vm.$router.push({ path: 'login' });
						}
					);
				}
			}
			/*else{
				await axios.get(`${vm.url_api}/usuario/me`, vm.axios_headers)
				.then(res =>{
					vm.usuario = res.data.data;
				})
				.catch(err => {
					if(vm.route != 'login') vm.$router.push({ path: 'login' });
				});
			} */

		},
	}
	export default mixin_base;
</script>

