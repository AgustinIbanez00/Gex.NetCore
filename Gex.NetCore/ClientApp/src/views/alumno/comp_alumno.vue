<template>
	<v-app class="light-blue">
		<!-- TABLA MESAS -->
		<v-expand-transition>
			<v-data-table v-show="route == 'listar_alumno'" :headers="headers" :items="lista" :items-per-page="5" class="elevation-3 px-10 mx-15 my-3">
				<template v-slot:item.actions="{ item }"><!-- Acciones -->
					<v-btn class="ma-2" text icon color="blue lighten-1" :to="`/${tab_actual}/${item.id}`"><v-icon>mdi-pencil</v-icon></v-btn>
					<v-btn class="ma-2" text icon color="blue-grey darken-1" @click="eliminar_id = item.id; eliminar();"><v-icon>mdi-delete</v-icon></v-btn>
				</template>
			</v-data-table>
		</v-expand-transition>
		<!-- ABM -->
		<v-expand-transition  class="justify-center">
			<v-card v-show="route == 'editar_alumno' || route == 'crear_alumno'" class="mx-15 mt-8 text-center pa-5 pt-0">
				<v-card-title v-if="route == 'editar_alumno'">EDITAR ALUMNO</v-card-title>
				<v-card-title v-else>NUEVO ALUMNO</v-card-title>
				<v-row>
					<v-col cols='6'>
						<v-text-field v-model="alumno.nombre" label="Nombre">
						</v-text-field>
					</v-col>
					<v-col cols='6'>
						<v-text-field v-model="alumno.email" label="Email">
						</v-text-field>
					</v-col>
					<v-col cols='6'>
						<v-text-field v-model="alumno.dni" label="Dni">
						</v-text-field>
					</v-col>
					<v-col cols="6">
						<v-menu ref="datepicker" v-model="datepicker" :close-on-content-click="false" transition="scale-transition" offset-y max-width="290px" min-width="auto">
							<template v-slot:activator="{ on, attrs }">
								<v-text-field v-model="alumno.fecha_nacimiento" label="Fecha de nacimiento" prepend-icon="mdi-calendar" v-bind="attrs" @blur="alumno.fecha_nacimiento = parseDate(alumno.fecha_nacimiento)" v-on="on"></v-text-field>
							</template>
							<v-date-picker v-model="alumno.fecha_nacimiento" no-title @input="datepicker = false"></v-date-picker>
						</v-menu>
					</v-col>
					<v-col cols='6'>
						<v-text-field v-model="alumno.clave" label="Contraseña">
						</v-text-field>
					</v-col>
					<v-col cols='6'>
						<v-text-field v-model="confirmar_clave" label="Confirmar contraseña">
						</v-text-field>
					</v-col>
				</v-row>
				<!-- Acciones ABM -->
				<v-card-actions>
					<v-col class="text-right">
						<v-btn text @click="cancelar">Cancelar</v-btn>
						<v-btn text color="primary" @click="guardar(1)">Guardar y cerrar</v-btn>
						<v-btn button class="white--text indigo darken-1" @click="guardar()">Guardar</v-btn>
					</v-col>
				</v-card-actions>
			</v-card>
		</v-expand-transition>
		<RouterView/>
	</v-app>
</template>
<script>
	import mixin_base from '../../assets/mixin_base';
	var alumno_default = {
		id: 0,
		nombre: '',
		email: '',
		dni: '',
		clave: '',
	}
	export default {
		name: "comp_alumnos",
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
			alumno: JSON.parse(JSON.stringify(alumno_default)),
			confirmar_clave: '',
		}),
		computed: {
		},
		methods: {
		},
		mounted() {
		}
	};
</script>
