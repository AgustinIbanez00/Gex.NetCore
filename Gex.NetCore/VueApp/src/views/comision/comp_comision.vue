<template>
	<v-app id="fondo">
		<!-- TABLA MESAS -->
		<v-expand-transition>
			<v-data-table dark v-show="route == `listar_${tab_actual}`" :headers="headers" :items="lista" :items-per-page="5" class="elevation-3 mx-15 my-3" :loading="enviando_ajax" :loading-text="`Cargando ${elementos}`">
				<template v-slot:item.actions="{ item }"><!-- Acciones -->
					<v-btn class="ma-2" text icon color="light-blue darken-1" :to="`/${tab_actual}/${item.id}`"><v-icon>mdi-pencil</v-icon></v-btn>
					<v-btn class="ma-2" text icon color="red darken-1" @click="eliminar_id = item.id; eliminar();"><v-icon>mdi-delete</v-icon></v-btn>
				</template>
			</v-data-table>
		</v-expand-transition>
		<!-- ABM -->
		<v-expand-transition  class="justify-center">
			<v-card v-show="route == `editar_${tab_actual}` || route == `crear_${tab_actual}`" class="mx-15 mt-8 text-center pa-5 pt-0">
				<v-card-title v-if="route == `editar_${tab_actual}`">EDITAR COMISIÓN</v-card-title>
				<v-card-title v-else>NUEVA COMISIÓN</v-card-title>
				<v-row>
					<v-col cols='6'>
						<v-text-field v-model="comision.nombre" label="Nombre">
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
	export default {
		name: "comp_comisiones",
		mixins: [mixin_base],
		data: () => ({
			headers: [
				{ text: 'Comisión', align: 'start', sortable: false, value: 'nombre', width: '60%' },
				{ text: 'Ciclo lectivo', align: 'center', sortable: false, value: 'ciclo_lectivo', width: '20%' },
				{ text: 'Acciones', value: 'actions' , width: '140px' },
			],
			comision: JSON.parse(JSON.stringify(comision_default)),
		}),
	};
</script>
