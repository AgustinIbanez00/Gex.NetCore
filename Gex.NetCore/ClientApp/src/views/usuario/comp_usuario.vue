<template>
	<v-app id="fondo">
		<!-- TABLA MESAS -->
		<v-expand-transition>
			<v-data-table v-show="route == 'listar_usuario'" :headers="headers" :items="usuarios" :items-per-page="5" class="elevation-3 px-10 mx-15 my-3">
				<template v-slot:item.actions="{ item }"><!-- Acciones -->
					<v-btn class="ma-2" text icon color="blue lighten-1" @click="edicion(item.id)"><v-icon>mdi-pencil</v-icon></v-btn>
					<v-btn class="ma-2" text icon color="blue-grey darken-1"><v-icon>mdi-delete</v-icon></v-btn>
				</template>
			</v-data-table>
		</v-expand-transition>
		<!-- ABM -->
		<v-expand-transition  class="justify-center">
			<v-card v-show="route == 'editar_usuario' || route == 'crear_usuario'" class="mx-15 mt-8 text-center pa-5 pt-0">
				<v-card-title v-if="route == 'editar_usuario'">EDITAR USUARIO</v-card-title>
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
	var usuario_default = {
		id: 1,
		usuario: '',
		password: '',
		password_confirm: '',
		email: '',
		nombre: '',
		dni: '',
	}
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
			tipos: ['Admin','Profesor','Alumno'],
		}),
		computed: {
		},
		methods: {
		},
		mounted() {
		}
	};
</script>
