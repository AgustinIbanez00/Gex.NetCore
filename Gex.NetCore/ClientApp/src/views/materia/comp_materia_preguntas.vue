<template>
	<v-app id="fondo">
		<!-- ABM EXÁMENES -->
		<v-expand-transition  class="justify-center">
			<v-card v-show="route == 'materia_preguntas' || route == 'editar_materia_pregunta'" class="mx-15 mt-8 text-center">
				<v-toolbar color="primary" dark flat><v-icon>widgets</v-icon>&nbsp<v-toolbar-title>Preguntas</v-toolbar-title></v-toolbar>
				<v-row>
					<v-col cols="12">
						<v-card-text>
							<v-list>
								<v-list-group id="listar_temas" prepend-icon="mdi-calendar-multiple-check" class="text-left" :value="true">
									<template v-slot:activator >
										<v-list-item-title>Temas</v-list-item-title>
									</template>
									<v-btn v-if="!modo_nueva_pregunta || tema_click != ''" @click="input_tema" color="indigo" style="z-index:3; margin-top:-83px;margin-left:140px;" icon><v-icon class="pt-1 px-1">mdi-plus</v-icon></v-btn>
									<div  class="d-flex col-12 align-center" v-if="modo_nueva_pregunta && tema_click == ''">
										<v-text-field id="input_tema" v-model="nuevo_tema_txt" v-on:keyup.enter="agregar_tema" label="Tema nuevo" append-icon="mdi-close" @click:append="modo_nueva_pregunta = 0">
										</v-text-field>
										<v-btn v-if="nuevo_tema_txt" @click="agregar_tema" class="ma-2 pt-1" outlined color="indigo" small><v-icon>mdi-calendar-plus</v-icon></v-btn>
									</div>
									<v-list-group v-for="(tema, a) in temas" :key="a" link no-action sub-group prepend-icon="mdi-menu-down">
										<template v-slot:activator>
											<v-list-item-content>
												<v-list-item-title>{{tema}}</v-list-item-title>
											</v-list-item-content>
										</template>
										<v-list-item>
											<v-btn @click="crear_pregunta(tema)" class="ma-2 pr-1 col-12" outlined color="indigo" small>Agregar pregunta <v-icon>mdi-plus</v-icon></v-btn>
										</v-list-item>
										<v-list-item v-for="(pregunta, i) in tema_preguntas(tema)" :key="i" link v-show="pregunta.id != -1">
											<v-list-item-title v-text="pregunta.descripcion"></v-list-item-title>
											<v-list-item-icon>
												<v-btn text icon color="light-blue darken-1" :to="`/${tab_actual}/${id}/preguntas/${pregunta.id}`">
													<v-icon >mdi-pencil</v-icon>
												</v-btn>
												<v-btn text icon color="pink lighten-2" @click="eliminar_pregunta(pregunta.id)">
													<v-icon>mdi-close</v-icon>
												</v-btn>
											</v-list-item-icon>
										</v-list-item>
									</v-list-group>
								</v-list-group>
							</v-list>
						</v-card-text>
					</v-col>
				</v-row>
			</v-card>
		</v-expand-transition>
		<RouterView/>
	</v-app>
</template>
<script>
import axios from 'axios';
	import mixin_base from '../../assets/mixin_base';
	export default {
		name: "comp_materia_preguntas",
		mixins: [mixin_base],
		watch: {
			todas_preguntas (val) {
				this.temas = val.reduce((acc, tema) => {
					const nombre_tema = tema.tema;
					if (!acc.includes(nombre_tema)) acc.push(nombre_tema);
					return acc;
				}, []).sort(function (a, b) {
						return a.toLowerCase().localeCompare(b.toLowerCase());
				});
			},
			active(val){
				var vm = this;
				if(vm.pregunta.tipo == 1){
					vm.respuesta[0].correcto = val;
					vm.respuesta[1].correcto = !val;
				}
			}
		},
		data: () => ({
			temas: [],
			todas_preguntas: [],
			headers: [
				{ text: 'Comisión', align: 'start', sortable: false, value: 'comision', width: '80	%' },
				{ text: 'Cuatrimestre', value: 'cuatrimestre' },
				{ text: 'Ciclo lectivo', value: 'cicloLectivo' },
				{ text: 'Cant. Alumnos', value: 'cantAlumnos' },
				{ text: 'Estado', value: 'estado' },
				{ text: 'Acciones', value: 'actions' , width: '140px' },
			],
			tema_click: 0,
			nuevo_tema_txt: '',
			modo_nueva_pregunta: 0,
		}),
		computed: {
		},
		methods: {
			async cargar_preguntas(){
				var vm = this;
				await axios.get(`${vm.url_api}/${vm.tab_actual}/${vm.id}/preguntas`, vm.axios_headers)
				.then(res => {vm.todas_preguntas = res.data.data;})
				.catch(err => {console.log(err);});
				//vm.todas_preguntas =
			},
			input_pregunta(tema){
				var vm = this;
				vm.modo_nueva_pregunta = 1;
				vm.nuevo_tema_txt = '';
				vm.tema_click = tema;
				vm.$nextTick(()=> $('#input_pregunta').focus());
				
			},
			input_tema(){
				var vm = this;
				vm.modo_nueva_pregunta = 1;
				vm.nuevo_tema_txt = '';
				vm.tema_click = '';
				vm.$nextTick(()=> $('#input_tema').focus());
			},
			tema_preguntas(tema){
				var vm = this;
				return vm.todas_preguntas.filter(p => p.tema == tema);
			},
			async eliminar_pregunta(pregunta_id){
				var vm = this;
				await axios.delete(`${vm.url_api}/preguntas?id=${pregunta_id}`, vm.axios_headers)
				.then(res => {
					vm.cargar_preguntas();
				})
				.catch(error => {console.log(error);});
			},
			crear_pregunta(tema){
				var vm = this;
				vm.tema_pregunta = tema;
				vm.$router.push({path: `preguntas/crear`});
			},
			agregar_tema(){
				var vm = this;
				vm.todas_preguntas.push({
					id: -1,
					tema: vm.nuevo_tema_txt,
					name: '',
				});
				vm.nuevo_tema_txt = '';
				this.temas = vm.todas_preguntas.reduce((acc, tema) => {
					const nombre_tema = tema.tema;
					if (!acc.includes(nombre_tema)) acc.push(nombre_tema);
					return acc;
				}, []).sort(function (a, b) {return a.toLowerCase().localeCompare(b.toLowerCase());});
			}
		},
		mounted() {
			var vm = this;
			vm.cargar_preguntas();
		}
	};
</script>