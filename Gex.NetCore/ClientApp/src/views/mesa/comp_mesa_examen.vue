<template>
	<v-app id="fondo">
		<!-- TABLA MESAS -->
		<v-expand-transition>
			<v-data-table  dark v-show="route == 'listar_mesaExamen'" :headers="headers" :items="lista" :items-per-page="5" class="elevation-3 mx-15 my-3" :loading="enviando_ajax" :loading-text="`Cargando ${elementos}`">
				<template v-slot:item.actions="{ item }"><!-- Acciones -->
					<v-btn class="ma-2" text icon color="blue lighten-1" :to="`/${tab_actual}/${item.id}`"><v-icon>mdi-pencil</v-icon></v-btn>
					<v-btn class="ma-2" text icon color="blue-grey darken-1" @click="eliminar_id = item.id; eliminar();"><v-icon>mdi-delete</v-icon></v-btn>
				</template>
			</v-data-table>
		</v-expand-transition>
		<!-- ABM -->
		<v-expand-transition  class="justify-center">
			<v-card v-show="route == 'editar_mesaExamen' || route == 'crear_mesaExamen'" class="mx-15 mt-8 text-center pa-5 pt-0">
				<v-card-title v-if="route == 'editar_mesaExamen'">EDITAR MESA EXÁMEN</v-card-title>
				<v-card-title v-else>NUEVA MESA EXÁMEN</v-card-title>
				<v-row>
					<v-col cols="12">
						<v-autocomplete v-model="mesaExamen.examen_id" :items="examenes" :item-text="'nombre_examen'" :item-value="'id'" dense filled label="Exámen"></v-autocomplete>
					</v-col>
					<v-col cols="6">
						<v-menu ref="datepicker" v-model="datepicker" :close-on-content-click="false" transition="scale-transition" offset-y max-width="290px" min-width="auto">
							<template v-slot:activator="{ on, attrs }">
								<v-text-field v-model="mesaExamen.fecha" label="Fecha del exámen" prepend-icon="mdi-calendar" v-bind="attrs" @blur="mesaExamen.fecha = parseDate(mesaExamen.fecha)" v-on="on"></v-text-field>
							</template>
							<v-date-picker v-model="mesaExamen.fecha" no-title @input="datepicker = false"></v-date-picker>
						</v-menu>
					</v-col>
					<v-col cols='6'>
						<v-text-field v-model="mesaExamen.duracion" label="Duración" append-icon="mdi-alarm">
						</v-text-field>
					</v-col>
				</v-row>
				<!-- Acciones ABM -->
				<v-card-actions>
					<v-col class="text-right">
						<v-btn text @click="cancelar">Cancelar</v-btn>
						<v-btn button class="white--text indigo darken-1" @click="guardar(1)">Guardar</v-btn>
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
		name: "comp_mesa_examen",
		mixins: [mixin_base],
		watch: {
		},
		data: () => ({
			headers: [
				{ text: 'Materia', align: 'start', sortable: false, value: 'materia.nombre', width: '80	%' },
				{ text: 'Fecha', value: 'fecha' },
				{ text: 'Duración', value: 'duracion' },
				{ text: 'Acciones', value: 'actions' , width: '140px' },
			],
			datepicker: false,
			mesaExamen: {},
			examenes: [],
		}),
		computed: {
		},
		methods: {
			cargar_examenes(){
				var vm = this;
				axios.get(`${vm.url_api}/examen`, vm.axios_headers)
				.then(function(res){
					vm.examenes = res.data.data;
					vm.examenes = vm.examenes.map(e=> ({ ...e, nombre_examen: `${vm.tipos_examen[vm.tipos_examen.findIndex(m => m.id == e.tipo)].nombre} ${e.materia.nombre}` }))
				});
			},
		},
		mounted() {
			var vm = this;
			vm.cargar_examenes();
		}
	};
</script>
