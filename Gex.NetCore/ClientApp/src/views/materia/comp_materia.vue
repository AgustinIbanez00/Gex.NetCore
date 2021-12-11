<template>
	<v-app class="light-blue">
		<!-- TABLA MATERIAS -->
		<v-expand-transition>
			<v-data-table :ref="`tabla_${tab_actual}`" v-show="route == 'listar_materia' || route == 'eliminar_materia'" :headers="headers" :items="materias" :items-per-page="5" class="elevation-3 px-10 mx-15 my-3">
				<template v-slot:item.tipo="{ item }">{{tipos_materias[item.tipo].nombre}}</template>
				<template v-slot:item.actions="{ item }"><!-- Acciones -->
					<v-btn class="ma-2" text icon color="blue lighten-1" @click="edicion(item.id)"><v-icon>mdi-pencil</v-icon></v-btn>
					<RouterLink :to="`/${tab_actual}/eliminar/${item.id}`" style="text-decoration: none;">
						<v-btn class="ma-2" text icon color="blue-grey darken-1"><v-icon>mdi-delete</v-icon></v-btn>
					</RouterLink>
				</template>
			</v-data-table>
		</v-expand-transition>
		<!-- ABM -->
		<v-expand-transition  class="justify-center">
			<v-card v-show="route == 'editar_materia' || route == 'crear_materia'" class="mx-15 mt-8 text-center pa-5 pt-0">
				<v-card-title v-if="materia.id">EDITAR MATERIA</v-card-title>
				<v-card-title v-else>NUEVA MATERIA</v-card-title>
				<v-row>
					<v-col cols="6"><v-text-field v-model="materia.nombre" label="Nombre"></v-text-field></v-col>
					<v-col cols="6">
						<v-select :items="tipos_materias" :item-text="'nombre'" :item-value="'id'" label="Tipo" v-model="materia.tipo">
					</v-select>
					</v-col>
				</v-row>
				<!-- Acciones ABM -->
				<v-card-actions>
					<v-col class="text-right">
						<v-btn text @click="cancelar">Cancelar</v-btn>
						<v-btn text color="primary" @click="guardar(1)">Guardar y cerrar</v-btn>
						<v-btn button class="white--text indigo darken-1" @click="guardar">Guardar</v-btn>
					</v-col>
				</v-card-actions>
			</v-card>
		</v-expand-transition>
		<RouterView/>
	</v-app>
</template>
<script>
	import mixin_base from '../../assets/mixin_base';
	import VueCookies  from 'vue-cookies';

	var materia_default = {
		id: 0,
		nombre: '',
		tipo: '',
	};
	export default {
		name: "comp_materias",
		mixins: [mixin_base],
		watch: {
			preguntas (val) {
				this.periodos = val.reduce((acc, periodo) => {
					const nombre_periodo = periodo.periodo;
					if (!acc.includes(nombre_periodo)) acc.push(nombre_periodo);
					return acc;
				}, []).sort();
			},
			async $route(to, from) {
				var vm = this;
				if (to && to.params.id) {//EDICIÓN
					await axios.get(`${vm.url_api}/Materia/${to.params.id}`, vm.axios_headers).then(res => {
						vm.materia = res.data.data;
					}).catch(err => console.log(err));
					//this.materia = vm.materias.find(m => m.id == to.params.id);
				}
				if(to && to.params.eliminar_id) {//ELIMINAR
					vm.eliminar_txt = vm.materias.find(m => m.id == to.params.eliminar_id).nombre;
				}
			}
		},
		data: () => ({
			periodos: [],
			materias: [],
			tipos_materias: [
				{id: 0, nombre: 'Anual'},
				{id: 1, nombre: 'Cuatrimestral'},
				{id: 2, nombre: 'Trimestral'},
			],
			materia: JSON.parse(JSON.stringify(materia_default)),
			temas_elegidos: [],
			preguntas_elegidas: [],
			todas_preguntas: [],
			headers: [
				{ text: 'Nombre', value: 'nombre' },
				{ text: 'Tipo', value: 'tipo' , width: '200px' },
				{ text: 'Acciones', value: 'actions' , width: '140px' },
			],

		}),
		computed: {
		},
		methods: {
			cargar_tabla(){
				var vm = this;
				axios.get(`${vm.url_api}/Materia`, vm.axios_headers).then(res => {
					vm.materias = res.data.data;
					//ALERTA
				}).catch(err => console.log(err));
			},
			guardar(listar){
				var vm = this;
				let f_guardar = res => {
					if(listar) vm.lista();
					else vm.materia = res.data.data;
					//ALERTA
				}
				if(vm.materia.id){
					axios.patch(`${vm.url_api}/Materia`,vm.materia, vm.axios_headers)
					.then(f_guardar).catch(err => console.log(err));
				}else{
					axios.post(`${url_api}/Materia`,vm.materia, vm.axios_headers)
					.then(f_guardar).catch(err => console.log(err));
				}
			},
		},
		async mounted() {
			var vm = this;
			vm.cargar_tabla();
			if(vm.$route.params.id){
				await axios.get(`${vm.url_api}/Materia/${vm.$route.params.id}`, vm.axios_headers).then(res => {
					vm.materia = res.data.data;
				}).catch(err => console.log(err));
			}else{
				vm.cargar_tabla();
			}
			if(vm.$route.params.eliminar_id){
				vm.materia = vm.materias[vm.materias.findIndex(m => m.id == vm.$route.params.eliminar_id)];
				vm.eliminar_txt = vm.materia.nombre;
			}
		}
	};
</script>
