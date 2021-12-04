<script>
	import Vue from 'vue';
	import Vuex from 'vuex';
	Vue.use(Vuex);
	const store = new Vuex.Store({
		state: {
			estado_actual: 1,
			id: 0
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
			...Vuex.mapMutations(['estado']),
		},
		mounted() {
			var vm = this;
			switch(vm.$route.name){
				case 'Exámenes': vm.estado_actual = vm.estados.lista; vm.lista(); break;
				case 'Exámen': vm.estado_actual = vm.estados.edicion; vm.edicion(vm.$route.params.id); break;
				case 'Crear exámen': vm.estado_actual = vm.estados.creacion; vm.creacion(); break;
				case 'Preguntas': vm.edicion(vm.$route.params.id,true); break;
			}
		},
		computed:{
			...Vuex.mapState(['estado_actual', 'id']),
			estado_actual:{
				get: function () {return this.$store.state.estado_actual;},
				set: function (val) {this.$store.state.estado_actual = val;}
			},
			id:{
				get: function () {return this.$route.params.id;},
				set: function (val) {this.$route.params.id = val;}
			},
		},
	}
	export default mixin_base;
</script>