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
					<v-col cols="12">
						<v-text-field label="Nombre">
						</v-text-field>
					</v-col>
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
	import mixin_base from '../assets/mixin_base';
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
			materia: JSON.parse(JSON.stringify(materia_default)),
			temas_elegidos: [],
			preguntas_elegidas: [],
			todas_preguntas: [],
			headers: [
				{ text: 'Comisión', align: 'start', sortable: false, value: 'comision', width: '80	%' },
				{ text: 'Cuatrimestre', value: 'cuatrimestre' },
				{ text: 'Ciclo lectivo', value: 'cicloLectivo' },
				{ text: 'Cant. Alumnos', value: 'cantAlumnos' },
				{ text: 'Estado', value: 'estado' },
				{ text: 'Acciones', value: 'actions' , width: '140px' },
			],

		}),
		computed: {
		},
		methods: {
		},
		mounted() {
			var vm = this;
			if(vm.route == 'materia_preguntas'){
				vm.materia = {
					id: 1,
					nombre: 'Matemáticas',
				}
			}
			vm.$nextTick(() => {
				if(vm.materia.id){
					vm.titulo_txt = vm.materia.nombre;
				}
			})
		}
	};
</script>
