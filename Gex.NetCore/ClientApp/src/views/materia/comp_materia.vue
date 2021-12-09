<template>
	<v-app class="light-blue">
		<!-- TABLA MATERIAS -->
		<v-expand-transition>
			<v-data-table v-show="route == 'listar_materia'" :headers="headers" :items="examenes" :items-per-page="5" class="elevation-3 px-10 mx-15 my-3">
				<template v-slot:item.actions="{ item }"><!-- Acciones -->
					<v-btn class="ma-2" text icon color="blue lighten-1" @click="edicion(item.id)"><v-icon>mdi-pencil</v-icon></v-btn>
					<v-btn class="ma-2" text icon color="blue-grey darken-1"><v-icon>mdi-delete</v-icon></v-btn>
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
					<v-col cols="6"><v-select :items="tipos_materias" label="Tipo" v-model="materia.tipo"></v-select></v-col>
				</v-row>
				<!-- Acciones ABM -->
				<v-card-actions>
					<v-col class="text-right">
						<v-btn text @click="cancelar">Cancelar</v-btn>
						<v-btn text color="primary">Guardar y cerrar</v-btn>
						<v-btn button class="white--text indigo darken-1">Guardar</v-btn>
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
		},
		data: () => ({
			periodos: [],
			examenes: [],
			tipos_materias: ['Anual','Cuatrimestral','Trimestral'],
			materia: JSON.parse(JSON.stringify(materia_default)),
			temas_elegidos: [],
			preguntas_elegidas: [],
			todas_preguntas: [],
			headers: [
				{ text: 'Nombre', value: 'nombre' },
				{ text: 'Estado', value: 'estado' },
				{ text: 'Acciones', value: 'actions' , width: '140px' },
			],

		}),
		computed: {
		},
		methods: {
			cargar_tabla(){
				var vm = this;
				axios.get('http://localhost:5000/api/Materia', {
					headers: {
						'Content-Type': 'application/json',
						'Authorization': `Bearer ${window.$cookies.get("gex_session")}`
					}
				}).then(res => {
					vm.materias = res.data.data;
				}).catch(err => console.log(err));
			},
		},
		mounted() {
			var vm = this;
			if(vm.$route.params.id){
				vm.materia = {
					id: 0,
					nombre: 'WEB II',
					tipo: 'Cuatrimestral',
				}
			}else	vm.cargar_tabla();
		}
	};
</script>
