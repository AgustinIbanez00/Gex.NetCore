<template>
	<v-app class="light-blue">
		<!-- TABLA MESAS -->
		<v-expand-transition>
			<v-data-table v-show="route == 'listar_mesa'" :headers="headers" :items="mesas" :items-per-page="5" class="elevation-3 px-10 mx-15 my-3">
				<template v-slot:item.actions="{ item }"><!-- Acciones -->
					<v-btn class="ma-2" text icon color="blue lighten-1" @click="edicion(item.id)"><v-icon>mdi-pencil</v-icon></v-btn>
					<v-btn class="ma-2" text icon color="blue-grey darken-1"><v-icon>mdi-delete</v-icon></v-btn>
				</template>
			</v-data-table>
		</v-expand-transition>
		<!-- ABM -->
		<v-expand-transition  class="justify-center">
			<v-card v-show="route == 'editar_mesa' || route == 'crear_mesa'" class="mx-15 mt-8 text-center pa-5 pt-0">
				<v-card-title v-if="route == 'editar_mesa'">EDITAR MESA</v-card-title>
				<v-card-title v-else>NUEVA MESA</v-card-title>
				<v-row>
					<v-col col="6"><v-select :items="materias" label="Materia" v-model="mesa.materia"></v-select></v-col>
					<v-col col="6"><v-select :items="cursos" label="Curso" v-model="mesa.curso"></v-select></v-col>
					<v-col col="2">
						<v-menu ref="datepicker" v-model="datepicker" :close-on-content-click="false" transition="scale-transition" offset-y max-width="290px" min-width="auto">
							<template v-slot:activator="{ on, attrs }">
								<v-text-field v-model="mesa.fecha" label="Fecha del exámen" prepend-icon="mdi-calendar" v-bind="attrs" @blur="mesa.fecha = parseDate(mesa.fecha)" v-on="on"></v-text-field>
							</template>
							<v-date-picker v-model="mesa.fecha" no-title @input="datepicker = false"></v-date-picker>
						</v-menu>
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
	import mixin_base from '../../assets/mixin_base';
	export default {
		name: "comp_mesas",
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
			mesa: JSON.parse(JSON.stringify(mesa_default)),
			mesas: [],
			materias: ['Laboratorio de programación','Matemáticas','Ingles'],
			cursos: ['1','2','3','4','5','6','7','8','9','10'],
		}),
		computed: {
		},
		methods: {
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
		}
	};
</script>
