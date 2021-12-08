<template>
	<v-app class="light-blue">
		<!-- TABLA EXÁMENES -->
		<v-expand-transition>
			<v-data-table v-show="route == 'listar_examen'" :headers="headers" :items="examenes" :items-per-page="5" class="elevation-3 px-10 mx-15 my-3">
				<template v-slot:item.actions="{ item }"><!-- Acciones -->
					<v-btn class="ma-2" text icon color="blue lighten-1" @click="edicion(item.id)"><v-icon>mdi-pencil</v-icon></v-btn>
					<v-btn class="ma-2" text icon color="blue-grey darken-1"><v-icon>mdi-delete</v-icon></v-btn>
				</template>
			</v-data-table>
		</v-expand-transition>
		<!-- ABM EXÁMENES -->
		<v-expand-transition  class="justify-center">
			<v-card v-show="route == 'editar_examen' || route == 'crear_examen'" class="mx-15 mt-8 text-center pa-5 pt-0">
				<v-card-title v-if="route == 'editar_examen'">EDITAR EXÁMEN</v-card-title>
				<v-card-title v-else>NUEVO EXÁMEN</v-card-title>
				<v-row>
					<v-col md="6"><v-select :items="materias" label="Materia" v-model="examen.materia" @change="materia_cambiada"></v-select></v-col>
					<v-col md="2"><v-select :items="tipos" label="Tipo"></v-select></v-col>
					<v-col md="2" v-show="examen.fecha"><v-select :items="modalidades" label="Modalidad"></v-select></v-col>
				</v-row>
				<v-row>
					<v-col md="2"><v-switch v-model="examen.promocional" inset label="Promocional"></v-switch></v-col>
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
		name: "comp_examen",
		mixins: [mixin_base],
		data: () => ({
			isLoading: false,
			examen: JSON.parse(JSON.stringify(examen_default)),
			materias: ['Laboratorio de programación','Android','Redes','Web II'],
			tipos: ['Final','Recuperatorio','Parcial','Global','Test'],
			modalidades: ['Multipleflai','Normal'],
			headers: [
				{ text: 'Comisión', align: 'start', sortable: false, value: 'comision', width: '80	%' },
				{ text: 'Cuatrimestre', value: 'cuatrimestre' },
				{ text: 'Ciclo lectivo', value: 'cicloLectivo' },
				{ text: 'Cant. Alumnos', value: 'cantAlumnos' },
				{ text: 'Estado', value: 'estado' },
				{ text: 'Acciones', value: 'actions' , width: '140px' },
			],
			examenes: [],
			currentTag: null,
			selection_examen: null,
			currentTag_examen: null,
		}),
		computed: {
		},
		methods: {
			/*cargar_tabla: function(){
				var vm = this;
				axios.get('http://127.0.0.1:5ax000/api/Cursos')
				.then(res => {
					vm.examenes = res.data;
				}).catch(err => console.log(err));
			},*/
			cancelar: function(){
				var vm = this;
				vm.estado(vm.estados.lista);
				vm.top();
			},
			materia_cambiada: function(){
				var vm = this;
			},
		},
		mounted() {
			var vm = this;
			vm.$nextTick(() => {
				if(vm.examen.id){
					vm.titulo_txt = vm.examen.materia;
				}
			})
		}
	};
</script>
