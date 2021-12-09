<template>
	<v-app class="light-blue">
		<!-- ABM EXÁMENES -->
		<v-expand-transition  class="justify-center">
			<v-card v-show="route == 'materia_preguntas' || route == 'editar_materia_preguntas'" class="mx-15 mt-8 text-center">
				<v-toolbar color="primary" dark flat><v-icon>widgets</v-icon>&nbsp<v-toolbar-title>Preguntas</v-toolbar-title></v-toolbar>
				<v-row>
					<v-col cols="12">
						<v-card-text>
							<v-list>
								<v-list-group id="listar_periodos" prepend-icon="mdi-calendar-multiple-check" class="text-left" :value="true">
									<template v-slot:activator>
										<v-list-item-title>Periodos</v-list-item-title>
									</template>
									<v-btn v-if="!modo_nueva_pregunta || periodo_click != ''" @click="input_periodo" color="indigo" style="z-index:3; margin-top:-83px;margin-left:140px;" icon><v-icon class="pt-1 px-1">mdi-plus</v-icon></v-btn>
									<div  class="d-flex col-12 align-center" v-if="modo_nueva_pregunta && periodo_click == ''">
										<v-text-field id="input_periodo" v-model="nueva_pregunta_txt" v-on:keyup.enter="agregar_pregunta('')" label="Periodo nuevo" append-icon="mdi-close" @click:append="modo_nueva_pregunta = 0">
										</v-text-field>
										<v-btn v-if="nueva_pregunta_txt" @click="agregar_pregunta('')" class="ma-2 pt-1" outlined color="indigo" small><v-icon>mdi-calendar-plus</v-icon></v-btn>
									</div>
									<v-list-group v-for="(periodo, a) in periodos" :key="a" link no-action sub-group prepend-icon="mdi-menu-down">
										<template v-slot:activator>
											<v-list-item-content>
												<v-list-item-title>{{periodo}}</v-list-item-title>
											</v-list-item-content>
										</template>
										<v-list-item>
											<v-text-field id="input_pregunta" v-if="modo_nueva_pregunta && periodo_click == periodo"  v-on:keyup.enter="agregar_pregunta(periodo)" v-model="nueva_pregunta_txt" label="Pregunta nueva" append-icon="mdi-close" @click:append="modo_nueva_pregunta = 0">
											</v-text-field>
											<v-btn v-else @click="input_pregunta(periodo)" class="ma-2 pr-1 col-12" outlined color="indigo" small>Agregar pregunta <v-icon>mdi-plus</v-icon></v-btn>
											<v-btn v-if="modo_nueva_pregunta && nueva_pregunta_txt && periodo_click == periodo" @click="agregar_pregunta(periodo)" class="ma-2" outlined color="indigo" small><v-icon>mdi-plus</v-icon></v-btn>	
										</v-list-item>
										<v-list-item v-for="(pregunta, i) in periodo_preguntas(periodo)" :key="i" link v-show="pregunta.id">
											<v-list-item-title v-text="pregunta.name"></v-list-item-title>
											<v-list-item-icon>
												<RouterLink :to="`/${tab_actual}/1/preguntas/1`" style="text-decoration: none;">
													<v-btn text icon color="light-blue darken-1">
														<v-icon >mdi-pencil</v-icon>
													</v-btn>
												</RouterLink>
												<v-btn text icon color="pink lighten-2" @click="eliminar_pregunta(pregunta)">
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
	export default {
		name: "comp_materia_preguntas",
		mixins: [mixin_base],
		watch: {
			todas_preguntas (val) {
				this.periodos = val.reduce((acc, periodo) => {
					const nombre_periodo = periodo.periodo;
					if (!acc.includes(nombre_periodo)) acc.push(nombre_periodo);
					return acc;
				}, []).sort(function (a, b) {
						return a.toLowerCase().localeCompare(b.toLowerCase());
				});
			},
		},
		data: () => ({
			periodos: [],
			examenes: [],
			temas_elegidos: [],
			preguntas_elegidas: [],
			todas_preguntas: [],
			headers: [
				{ text: 'Comisión', align: 'start', sortable: false, value: 'comision', width: '80	%' },
				{ text: 'Cuatrimestre', value: 'cuatrimestre' },
				{ text: 'Ciclo lectivo', value: 'cicloLectivo' },
				{ text: 'Cant. Alumnos', value: 'cantAlumnos' },
				{ text: 'Estado', value: 'estado' },
				{ text: 'Acciones', value: 'actions' , width: '140px' },
			],
			periodo_click: 0,
			nueva_pregunta_txt: '',
			modo_nueva_pregunta: 0,
		}),
		computed: {
		},
		methods: {
			cargar_preguntas (){
				var vm = this;
				if (this.todas_preguntas.length) return;
				vm.todas_preguntas = [
					{
						id: 1,
						periodo: 'Introducción',
						name: '¿Como se construye un formulario?',
						estado: 1
					},
					{
						id: 2,
						periodo: 'Datos',
						name: '¿que es un botón?',
						estado:0
					},
					{
						id: 3,
						periodo: 'SQL',
						name: '¿que es un datepicker?',
						estado:0
					},
					{
						id: 4,
						periodo: 'Algebra relacional',
						name: '¿Como funciona .NET?',
						estado:0
					},
					{
						id: 5,
						periodo: 'Introducción',
						name: '¿Como usar base de datos en .NET?',
						estado:0
					},
					{
						id: 6,
						periodo: 'Algebra relacional',
						name: '¿Que estrucura tiene un objeto?',
						estado:0
					},
					{
						id: 7,
						periodo: 'Algebra relacional',
						name: '¿Como hacer ejecutable un programa?',
						estado:0
					},
					{
						id: 8,
						periodo: 'Algebra relacional',
						name: '¿Cuales son las cualidades de un programa ejecutable?',
						estado:0
					},
					{
						id: 9,
						periodo: 'Algebra relacional',
						name: '¿Que sucede en la computadora cuando se ejecuta un programa?',
						estado:0
					},
					{
						id: 10,
						periodo: 'Datos',
						name: '¿Que es una página web?',
						estado:0
					},
					{
						id: 11,
						periodo: 'Datos',
						name: '¿Que es un framework?',
						estado:0
					},
					{
						id: 12,
						periodo: 'Datos',
						name: '¿Como funciona Angular?',
						estado:0
					},
				];
			},
			input_pregunta(periodo){
				var vm = this;
				vm.modo_nueva_pregunta = 1;
				vm.nueva_pregunta_txt = '';
				vm.periodo_click = periodo;
				vm.$nextTick(()=> $('#input_pregunta').focus());
				
			},
			input_periodo(){
				var vm = this;
				vm.modo_nueva_pregunta = 1;
				vm.nueva_pregunta_txt = '';
				vm.periodo_click = '';
				vm.$nextTick(()=> $('#input_periodo').focus());
			},
			periodo_preguntas(periodo){
				var vm = this;
				return vm.todas_preguntas.filter(p => p.periodo == periodo);
			},
			eliminar_pregunta(pregunta){
				var vm = this;
				vm.todas_preguntas = vm.todas_preguntas.filter((p) => { return p.id != pregunta.id;});
			},
			agregar_pregunta(periodo){
				var vm = this;
				vm.todas_preguntas.push({
					id: periodo ? vm.todas_preguntas.length + 1 : 0,
					periodo: periodo ? periodo : vm.nueva_pregunta_txt,
					name: periodo ? vm.nueva_pregunta_txt : '',
					estado:1
				});
				vm.nueva_pregunta_txt = '';
				this.periodos = vm.todas_preguntas.reduce((acc, periodo) => {
					const nombre_periodo = periodo.periodo;
					if (!acc.includes(nombre_periodo)) acc.push(nombre_periodo);
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