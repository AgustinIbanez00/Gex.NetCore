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
		<v-card-title class="white--text indigo darken-1 w-100 text-sm-h3 justify-center mt-10">
			<div>Exámenes</div>
		</v-card-title>
		<comp_botones ref="botones" @accion='accion_boton'/><!-- Botones -->
		<v-expand-transition class="justify-center">
			<v-row justify="center">
				<v-col><!-- Componentes -->
					<comp_examenes ref="examenes" v-show="route == 'examenes'" @cancelar="cancelar_abm"/>
				</v-col>
			</v-row>
		</v-expand-transition>
    <v-footer app><!-- Footer -->
    </v-footer>
  </v-app>
</template>
<script>
	import comp_examenes from "./views/comp_examenes";
	import comp_botones from "./views/comp_botones";

	export default {
		name: "App",
		components: {
			comp_examenes,
			comp_botones,
		},
		data: () => ({
			route: 'examenes',
			abm: true,
  	  selectedItem: 0,
			opciones:[{text: 'Perfil',link: 'configuracion de la persona'},{text: 'Reportes',link: 'informes'},{text:'Cerrar sesión',link:'salir'}]
		}),
		methods: {
			accion_boton: function(modo){
				var vm = this;
				if(modo == 'creacion') vm.$refs[vm.route].lista();
				else vm.$refs[vm.route].creacion();
			},
			creacion: function(){
				var vm = this;
				vm.$refs[vm.route].creacion();
			},
			lista: function(recargar = false){
				var vm = this;
				vm.$refs[vm.route].lista(recargar);				
			},
			cancelar_abm: function(){
				var vm = this;
				vm.lista();
				vm.$refs['botones'].lista();
			}
		},
		computed:{
		},
		mounted() {
			var vm = this;
			// window.onhashchange = function (e) {
			// 	if (window.location.hash) vm.editar_plantilla(window.location.hash.substring(1));
			// 	else vm.ver_plantillas();
			// }
			window.addEventListener('locationchange', function(){
				var url = window.location.href.split('/');
				vm.route = url[url.length-1];
			})
		}
	};
</script>
