<template>
	<v-app id="fondo">
		<!-- TABLA MESAS -->
		<v-expand-transition>
			<v-data-table  dark v-show="route == 'listar_inscripcionMesa'" :headers="headers" :items="lista" :items-per-page="5" class="elevation-3 mx-15 my-3" :loading="enviando_ajax" :loading-text="`Cargando ${elementos}`">
				<template v-slot:item.actions="{ item }"><!-- Acciones -->
					<v-btn class="ma-2" text icon color="blue lighten-1" :to="`/${tab_actual}/${item.id}`"><v-icon>mdi-pencil</v-icon></v-btn>
					<v-btn class="ma-2" text icon color="blue-grey darken-1" @click="eliminar_id = item.id; eliminar();"><v-icon>mdi-delete</v-icon></v-btn>
				</template>
			</v-data-table>
		</v-expand-transition>
		<!-- ABM -->
		<v-expand-transition  class="justify-center">
			<v-card v-show="route == 'editar_inscripcionMesa' || route == 'crear_inscripcionMesa'" class="mx-15 mt-8 text-center pa-5 pt-0">
				<v-card-title v-if="route == 'editar_inscripcionMesa'">EDITAR INSCRIPCIÓN</v-card-title>
				<v-card-title v-else>NUEVA INSCRIPCIÓN</v-card-title>
				<v-row>
					<v-col cols="12"><v-select :items="mesas" label="Mesa" v-model="inscripcion.mesa"></v-select></v-col>
					<v-col cols="6">
						<v-text-field v-model="inscripcion.alumno" label="Alumno">
						</v-text-field>
					</v-col>
					<v-col cols="6"><v-select :items="condiciones" label="Condición" v-model="inscripcion.condicion"></v-select></v-col>
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
	export default {
		name: "comp_inscripcionMesa",
		mixins: [mixin_base],
		watch: {
		},
		data: () => ({
			headers: [
				{ text: 'Comisión', align: 'start', sortable: false, value: 'comision', width: '80	%' },
				{ text: 'Cuatrimestre', value: 'cuatrimestre' },
				{ text: 'Ciclo lectivo', value: 'cicloLectivo' },
				{ text: 'Cant. Alumnos', value: 'cantAlumnos' },
				{ text: 'Estado', value: 'estado' },
				{ text: 'Acciones', value: 'actions' , width: '140px' },
			],
			datepicker: false,
			inscripcion: JSON.parse(JSON.stringify(inscripcionMesa_default)),
			mesas: ['Mesa 1', 'Mesa 2', 'Mesa 3', 'Mesa 4', 'Mesa 5'],
			condiciones: ['Regular','Libre','Equivalencia'],
			confirmar_clave: '',
		}),
		computed: {
		},
		methods: {
			guardar(){
				//
			},
		},
		mounted() {
		}
	};
</script>
