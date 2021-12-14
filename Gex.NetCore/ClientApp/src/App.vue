<template>
	<v-app>
		<v-app-bar src="/vue1.jpg" dark dense fixed style="background: rgb(17,19,41); background: linear-gradient(151deg, rgba(17,19,41,1) 34%, rgba(0,0,0,1) 59%, rgba(0,0,0,1) 76%, rgba(19,24,69,1) 93%);"><!-- Menú -->
			<template v-slot:img="{ props }">
				<v-img v-bind="props" gradient="to top right, rgba(0,0,0,1), rgba(19,24,69,0.8)"></v-img>
			</template>
			<template v-if="token">
				<v-tabs align-with-title>
					<v-tab color="black" large to="/materia" exact>Materias</v-tab>
					<v-tab color="black" large to="/examen" exact active-class>Exámenes</v-tab>
					<v-tab color="black" large to="/mesa" exact>Mesas</v-tab>
					<v-tab color="black" large to="/comision" exact>Comisiones</v-tab>
					<v-tab color="black" large to="/alumno" exact>Alumnos</v-tab>
					<v-tab color="black" large to="/inscripcion" exact>Inscripciones</v-tab>
					<v-tab color="black" large to="/usuario" exact>Contactos</v-tab>
				</v-tabs>
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
			</template>
		</v-app-bar>
		<v-card-title style="padding-top:16px; background: rgb(18,8,108); background: linear-gradient(0deg, rgba(18,8,108,1) 1%, rgba(19,32,145,1) 100%);" class="white--text w-100 justify-center mt-10" v-bind:style="{'height': route == `listar_${tab_actual}` || route == `eliminar_${tab_actual}` ? '70px': '50px','font-size': route == `${tab_actual}_preguntas` ? '30px': '50px'}">
			<div>{{route == `listar_${tab_actual}` || route == `eliminar_${tab_actual}` || route == `${tab_actual}_preguntas` ? titulo : ''}}</div>
		</v-card-title>
		<!-- COMPONENTES -->
		<comp-botones ref="botones"/>
		<comp-alerta ref="alerta"/>
		<comp-modal-eliminar ref="modal_eliminar"/>
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
	import comp_alerta from "./views/comp_alerta";
	import comp__modal_eliminar from "./views/modal/comp_modal_eliminar.vue";
	import mixin_base from './assets/mixin_base';
	export default {
		mixins: [mixin_base],
		name: "App",
		components: {
			'comp-botones':comp_botones,
			'comp-alerta':comp_alerta,
			'comp-modal-eliminar':comp__modal_eliminar,
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
		}
	};
</script>
