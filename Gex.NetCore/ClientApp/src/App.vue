<template>
	<v-app class="light-blue">
		<v-app-bar dark dense fixed><!-- Menú -->
			<v-btn :outlined="tab_actual == 'materia'" large to="/materia" exact>Materias</v-btn>
			<v-btn :outlined="tab_actual == 'examen'" large to="/examen" exact active-class>Exámenes</v-btn>
			<v-btn :outlined="tab_actual == 'mesa'" large to="/mesa" exact>Mesas</v-btn>
			<v-btn :outlined="tab_actual == 'curso'" large to="/curso" exact>Cursos</v-btn>
			<v-btn :outlined="tab_actual == 'alumno'" large to="/alumno" exact>Alumnos</v-btn>
			<v-btn :outlined="tab_actual == 'inscripcion'" large to="/inscripcion" exact>Inscripciones</v-btn>
			<v-btn :outlined="tab_actual == 'usuario'" large to="/usuario" exact>Contactos</v-btn>
			<v-spacer></v-spacer>
			<v-menu left bottom>
				<!-- Opciones -->
				<template v-slot:activator="{ on, attrs }">
					<v-btn icon v-bind="attrs" v-on="on">
						<v-icon>mdi-account</v-icon>
					</v-btn>
				</template>
				<v-list>
					<v-list-item-group>
						<v-list-item v-for="(item, i) in opciones" :key="i" link :to="item.link">
							<v-list-item-content>
								<v-list-item-title v-text="item.text"></v-list-item-title>
							</v-list-item-content>
						</v-list-item>
					</v-list-item-group>
				</v-list>
			</v-menu>
		</v-app-bar>
		<v-card-title style="padding-top:16px;" class="white--text indigo darken-1 w-100 justify-center mt-10" v-bind:style="{'height': route == `listar_${tab_actual}` || route == `eliminar_${tab_actual}` ? '70px': '50px','font-size': route == `${tab_actual}_preguntas` ? '30px': '50px'}">
			<div>{{route == `listar_${tab_actual}` || route == `eliminar_${tab_actual}` || route == `${tab_actual}_preguntas` ? titulo : ''}}</div>
		</v-card-title>
		<comp-botones ref="botones"/>
		<v-expand-transition class="justify-center">
			<v-row justify="center">
				<v-col>
				</v-col>
			</v-row>
		</v-expand-transition>
		<v-footer app><!-- Footer -->
		</v-footer>
		<RouterView/>
	</v-app>
</template>
<script>
	import comp_botones from "./views/comp_botones";
	import comp_modal_eliminar from "./views/modal/comp_modal_eliminar";
	import mixin_base from './assets/mixin_base';
	export default {
		mixins: [mixin_base],
		name: "App",
		components: {
			'comp-botones':comp_botones,
			'comp-modal-eliminar':comp_modal_eliminar,
		},
		data: () => ({
			selectedItem: 0,
			opciones:[{text: 'Perfil',link: 'configuracion de la persona'},{text: 'Reportes',link: 'informes'},{text:'Cerrar sesión',link:'salir'}],
		}),
		methods: {
		},
		computed:{
		},
		mounted() {
			var vm = this;
		}
	};
</script>
