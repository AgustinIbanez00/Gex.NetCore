<template>
	<v-app id="fondo">
		<!-- TABLA MATERIAS -->
		<v-expand-transition>
			<v-data-table :ref="`tabla_${tab_actual}`" v-show="route == 'listar_materia'" :headers="headers" :items="lista" :items-per-page="5" class="elevation-3 px-10 mx-15 my-3" :loading="cargando_lista" :loading-text="`Cargando ${elementos}`">
				<template v-slot:item.tipo="{ item }">{{tipos_materias[item.tipo].nombre}}</template>
				<template v-slot:item.actions="{ item }"><!-- Acciones -->
					<v-btn class="ma-2" text icon color="blue lighten-1" :to="`/${tab_actual}/${item.id}`"><v-icon>mdi-pencil</v-icon></v-btn>
					<v-btn class="ma-2" text icon color="blue-grey darken-1" @click="eliminar_id = item.id; eliminar();"><v-icon>mdi-delete</v-icon></v-btn>
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
	};
</script>
