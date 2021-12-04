<template>
	<v-app class="light-blue">
		<v-app-bar dark dense fixed><!-- Menú -->
			<v-btn large to="/examenes" exact>Exámenes</v-btn>
			<v-btn large to="/mesas" exact>Mesas</v-btn>
			<v-btn large to="/materias" exact>Materias</v-btn>
			<v-btn large to="/cursos" exact>Cursos</v-btn>
			<v-btn large to="/alumnos" exact>Alumnos</v-btn>
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
		<v-card-title style="padding-top:13px;" class="white--text indigo darken-1 w-100 text-sm-h3 justify-center mt-10" v-bind:style="{'height': $route.name == 'Exámenes' ? '70px': '50px'}">
			<div>{{$route.name == 'Exámenes' ? $route.name : ''}}</div>
		</v-card-title>
		<comp_botones ref="botones"/>
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
	import mixin_base from './assets/mixin_base';
	export default {
		mixins: [mixin_base],
		name: "App",
		components: {
			comp_botones,
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
