<template>
	<v-app id="fondo">
		<!-- TABLA MESAS -->
		<v-expand-transition>
			<v-data-table  dark v-show="route == 'listar_usuario'" :headers="headers" :items="usuarios" :items-per-page="5" class="elevation-3 mx-15 my-3" :loading="enviando_ajax" :loading-text="`Cargando ${elementos}`">
				<template v-slot:item.actions="{ item }"><!-- Acciones -->
					<v-btn class="ma-2" text icon color="blue lighten-1" :to="`/${tab_actual}/${item.id}`"><v-icon>mdi-pencil</v-icon></v-btn>
					<v-btn class="ma-2" text icon color="blue-grey darken-1" @click="eliminar_id = item.id; eliminar();"><v-icon>mdi-delete</v-icon></v-btn>
				</template>
			</v-data-table>
		</v-expand-transition>
		<!-- ABM -->
		<v-expand-transition  class="justify-center">
			<v-card v-show="route == 'editar_usuario' || route == 'crear_usuario' || route == 'mi_usuario'" class="mx-15 mt-8 text-center pa-5 pt-0">
				<v-card-title v-if="route == 'editar_usuario' || route == 'mi_usuario'">EDITAR USUARIO</v-card-title>
				<v-card-title v-else>NUEVA USUARIO</v-card-title>
				<v-row>
					<v-col cols="6">
						<v-text-field v-model="usuario.usario" label="Usuario">
						</v-text-field>
					</v-col>
					<v-col cols="6">
						<v-text-field v-model="usuario.nombre" label="Nombre">
						</v-text-field>
					</v-col>
					<v-col cols="6">
						<v-text-field v-model="usuario.email" label="Email">
						</v-text-field>
					</v-col>
					<v-col cols="6">
						<v-text-field v-model="usuario.dni" label="Dni">
						</v-text-field>
					</v-col>
					<v-col cols="6">
						<v-text-field v-model="usuario.password" label="Contraseña">
						</v-text-field>
					</v-col>
					<v-col cols="6">
						<v-text-field v-model="usuario.password_confirm" label="Confirmar contraseña">
						</v-text-field>
					</v-col>
					<v-col cols="6"><v-select :items="tipos_usuario" :item-text="'nombre'" label="Tipo" :item-value="'id'" v-model="usuario.tipo"></v-select></v-col>
				</v-row>
				<!-- Acciones ABM -->
				<v-card-actions>
					<v-col class="text-right">
						<v-btn v-if="route != 'mi_usuario'" text @click="cancelar">Cancelar</v-btn>
						<v-btn v-if="route == 'mi_usuario'" button class="white--text indigo darken-1" @click="guardar()">Guardar</v-btn>
						<v-btn v-else button class="white--text indigo darken-1" @click="guardar(1)">Guardar</v-btn>
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
		name: "comp_usuario",
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
			usuario: JSON.parse(JSON.stringify(usuario_default)),
			usuarios: [],
		}),
		computed: {
		},
		methods: {
		},
		mounted() {
			var vm = this;
			if(vm.route == 'mi_usuario') vm.usuario = vm.usuario_actual;
		}
	};
</script>
