<template>
	<div>
		<!-- DATOS EXÁMEN -->
		<v-expand-transition>
			<v-card v-show="route == 'examen_preguntas'" class="mx-15 mt-8 text-center">
				<v-toolbar color="primary" dark flat><v-icon>widgets</v-icon>&nbsp<v-toolbar-title>Preguntas</v-toolbar-title></v-toolbar>
				<v-row>
					<v-col cols="12">
						<v-list id="examen_preguntas">
							<v-list-item v-for="(examen_pregunta, i) in examen.preguntas" :key="i">
								<v-list-item-content>
									<v-list-item-title v-text="examen_pregunta.name"></v-list-item-title>
								</v-list-item-content>
								</v-list-item>
						</v-list>
					</v-col>
					<v-col cols="12">
						<v-toolbar color="primary" dark flat><v-icon>widgets</v-icon>&nbsp<v-toolbar-title>Glosario de preguntas</v-toolbar-title></v-toolbar>
						<v-text-field @keyup="buscar_preguntas()" v-model="buscar_preguntas_texto" label="Buscar pregunta" prepend-icon="fa4-search"></v-text-field>
						<v-card-text>
							<v-treeview v-model="preguntas_elegidas" :items="preguntas_items" selected-color="indigo" open-on-click selectable return-object expand-icon="mdi-chevron-down" on-icon="mdi-bookmark" off-icon="mdi-bookmark-outline" indeterminate-icon="mdi-bookmark-minus">
							</v-treeview>
						</v-card-text>
					</v-col>
					<v-col cols="12">
						<v-card-text>
							<div v-if="examen.preguntas.length === 0" key="title" class="text-h6 font-weight-light grey--text pa-4 text-center">
								Seleccionar preguntas del exámen
							</div>
							<v-chip-group v-model="selection" column active-class="primary--text">
								<draggable v-model="preguntas_elegidas" @start="dragStart" @end="dragEnd">
									<v-chip v-for="(pregunta_elegida, i) in preguntas_elegidas" :key="i" :color="color_random(pregunta_elegida.name)" dark small class="ma-1">
										{{`${i+1}) ${pregunta_elegida.name}` }}&nbsp&nbsp&nbsp<v-icon left small @click="preguntas_elegidas.splice(i, 1); colores_preguntas.splice(i, 1);">mdi-minus</v-icon>
									</v-chip>
								</draggable>
							</v-chip-group>
							<v-btn @click="agregar_preguntas" class="col-md-10" outlined rounded color="green">Agregar preguntas</v-btn>
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
	</div>
</template>
<script>
	import mixin_base from '../../assets/mixin_base';
	export default {
		mixins: [mixin_base],
		name: "comp_examen_preguntas",
		data: () => ({
			periodos: [],
			buscar_preguntas_texto: '',
			examen: JSON.parse(JSON.stringify(examen_default)),
			colores: ['red','pink','purple','deep-purple','indigo','blue','light-blue','cyan','teal','green','light-green','lime','yellow','amber','orange','deep-orange','brown','blue-grey','grey'],
			tipos_colores : ['',' lighten',' darken',' accent'],
			colores_preguntas: [],
			todas_preguntas: [],
			preguntas_elegidas: [],
			selection: null,
		}),
		watch: {
			preguntas (val) {
				this.periodos = val.reduce((acc, periodo) => {
					const nombre_periodo = periodo.periodo;
					if (!acc.includes(nombre_periodo)) acc.push(nombre_periodo);
					return acc;
				}, []).sort();
			},
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
			buscar_preguntas(){
				var vm = this;
				let preguntas = [];
				if(vm.buscar_preguntas_texto == '') vm.modo_busqueda = false;
				else{
					vm.modo_busqueda = true;
					preguntas = vm.preguntas.filter(function(p){
						return (p.name.toUpperCase()).includes(vm.buscar_preguntas_texto.toUpperCase());
					});
				}
				vm.preguntas_elegidas = preguntas;
			},
			agregar_preguntas(){
				var vm = this;
				let preguntas = JSON.parse(JSON.stringify(vm.preguntas_elegidas));
				preguntas.forEach((p) => vm.examen.preguntas.push(p));
				vm.$nextTick(() => $('#examen_preguntas .v-list-item')[0].click());
			},
			getChildren (type) {
				var vm = this;
				const preguntas = [];
				for (const p of this.todas_preguntas) {
					if (p.periodo !== type || vm.examen.preguntas.includes(p)) continue;
					preguntas.push({
						...p,
						name: p.name,
					});
				}
				return preguntas.sort((a, b) => a.name > b.name ? 1 : -1);
			},
			color_random (i){
				var vm = this;
				if(!vm.colores_preguntas[i]){
					var color = '';
					var tipo_color = '';
					var index_color = Math.floor(Math.random() * 10 ) % vm.colores.length;
					var index_tipo_color = Math.floor(Math.random() * 10) % vm.tipos_colores.length;
					color = vm.colores[index_color];
					tipo_color = vm.tipos_colores[index_tipo_color];
					color += tipo_color+(tipo_color == '' ? '' : tipo_color == ' lighten' ? '-1' : '-4');
					vm.colores_preguntas[i] = color;
				}
				return vm.colores_preguntas[i];
			},
			dragStart() {
				var vm = this;
				if (vm.preguntas_elegidas[vm.selection]) vm.currentTag = vm.preguntas_elegidas[vm.selection].name;
				else vm.currentTag = null;
			},
			dragEnd() {
				var vm = this;
				if (vm.currentTag) {
					vm.preguntas_elegidas.forEach((x, i) => {
						if (x.name === vm.currentTag) vm.selection = i;
					});
				}
			},
			dragStart_examen() {
				var vm = this;
				if (vm.examen.preguntas[vm.selection_examen]) vm.currentTag_examen = vm.examen.preguntas[vm.selection_examen].name;
				else vm.currentTag_examen = null;
			},
			dragEnd_examen() {
				var vm = this;
				if (vm.currentTag_examen) {
					vm.examen.preguntas.forEach((x, i) => {
						if (x.name === vm.currentTag_examen) vm.selection_examen = i;
					});
				}
			},
		},
		computed: {
			preguntas:{
				get: function() {
					var vm = this;
					let preguntas = [];
					vm.todas_preguntas.forEach(function(p){
						if(!vm.examen.preguntas.find(p => p.id == p)) preguntas.push(p);
					});
					return preguntas;
				},
				set: function(value) {
				},
			},
			preguntas_items(){
				var vm = this;
				let children_aux = vm.periodos.map(type => {
					let preguntas = vm.getChildren(type);
					preguntas =	preguntas.filter(function(pregunta){
						return !vm.examen.preguntas.find(p => p.id == pregunta.id);
					});
					return {
						id: type,
						name: type,
						children: preguntas,
					}
				});
				const children = children_aux.filter(function(periodo){
					return periodo.children.length > 0;
				});
				return children.length > 0 ? [{id: 0, name: 'Todos los periodos', children}]:[];
			},
			examen_preguntas:{
				get: function() {
					var vm = this;
					let preguntas = [];
					for(let i = 0; i < vm.examen.preguntas.length; i++){
						var p = JSON.parse(JSON.stringify(vm.examen.preguntas[i]));
						p.name = `${i+1}) ${p.name}`;
						preguntas.push(p);
					}
					return preguntas;
				},
				set: function(value) {
					var vm = this;
					vm.examen.preguntas = value;
				},
			}
		},
		mounted() {
			var vm = this;
			vm.cargar_preguntas();
		},
	};
</script>
