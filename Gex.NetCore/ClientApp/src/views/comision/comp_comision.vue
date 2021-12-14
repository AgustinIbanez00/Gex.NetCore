<template>
	<v-app class="black" style="background: rgb(10,5,64);background: linear-gradient(153deg, rgba(10,5,64,1) 33%, rgba(1,3,23,1) 65%, rgba(0,0,0,1) 96%);">
		<!-- TABLA MESAS -->
		<v-expand-transition>
			<v-data-table v-show="route == 'listar_comision'" :headers="headers" :items="lista" :items-per-page="5" class="elevation-3 px-10 mx-15 my-3">
				<template v-slot:item.actions="{ item }"><!-- Acciones -->
					<v-btn class="ma-2" text icon color="blue lighten-1" :to="`/${tab_actual}/${item.id}`"><v-icon>mdi-pencil</v-icon></v-btn>
					<v-btn class="ma-2" text icon color="blue-grey darken-1" @click="eliminar_id = item.id; eliminar();"><v-icon>mdi-delete</v-icon></v-btn>
				</template>
			</v-data-table>
		</v-expand-transition>
		<!-- ABM -->
		<v-expand-transition  class="justify-center">
			<v-card v-show="route == 'editar_comision' || route == 'crear_comision'" class="mx-15 mt-8 text-center pa-5 pt-0">
				<v-card-title v-if="route == 'editar_comision'">EDITAR COMISIÓN</v-card-title>
				<v-card-title v-else>NUEVA COMISIÓN</v-card-title>
				<v-row>

					<v-col cols='6'>
						<v-text-field v-model="comision.nombre" label="Nombre del comisión">
						</v-text-field>
					</v-col>
					<v-col cols='6'>
						<v-text-field v-model="comision.ciclo_lectivo" @keypress="input_numeros($event)" label="Ciclo lectivo">
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
	var comision_default = {
		id: 0,
		nombre: '',
		ciclo_lectivo: new Date().getFullYear(),
		//obtener año actual
	}
	export default {
		name: "comp_comisiones",
		mixins: [mixin_base],
		watch: {
		},
		data: () => ({
			headers: [
				{ text: 'Comisión', align: 'start', sortable: false, value: 'nombre', width: '60%' },
				{ text: 'Ciclo lectivo', align: 'center', sortable: false, value: 'ciclo_lectivo', width: '20%' },
				{ text: 'Acciones', value: 'actions' , width: '140px' },
			],
			comision: JSON.parse(JSON.stringify(comision_default)),
			comisiones: [],
		}),
		computed: {
		},
		methods: {
		},
		mounted() {
			var vm = this;
		},
	};
</script>
