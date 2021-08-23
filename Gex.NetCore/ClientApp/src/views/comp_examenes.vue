<template>
  <v-app class="light-blue">
		<!-- TABLA EXÁMENES -->
		<v-expand-transition>
			<v-data-table v-show="estado_actual == estados.lista" :headers="headers" :items="examenes" :items-per-page="5" class="elevation-3 px-10 mx-15 my-5">
				<template v-slot:item.actions="{ item }"><!-- Acciones -->
					<v-btn class="ma-2" text icon color="blue lighten-1" @click="edicion(item.id)"><v-icon>mdi-pencil</v-icon></v-btn>
					<v-btn class="ma-2" text icon color="blue-grey darken-1"><v-icon>mdi-delete</v-icon></v-btn>
				</template>
			</v-data-table>
		</v-expand-transition>
		<!-- ABM EXÁMENES -->
		<v-expand-transition class="justify-center">
			<v-card v-show="estado_actual != estados.lista" class="mx-15 mt-5 text-center pa-5 pt-0">
				<v-card-title>NUEVO EXÁMEN</v-card-title>
				<v-row>
					<v-col md="6"><v-select :items="materias" label="Materia"></v-select></v-col>
					<v-col md="2"><v-select :items="tipos" label="Tipo"></v-select></v-col>
					<v-col lg="2">
						<v-menu ref="datepicker_examen" v-model="datepicker_examen" :close-on-content-click="false" transition="scale-transition" offset-y max-width="290px" min-width="auto">
							<template v-slot:activator="{ on, attrs }">
								<v-text-field v-model="examen.fecha" label="Fecha del exámen" prepend-icon="mdi-calendar" v-bind="attrs" @blur="date = parseDate(examen.fecha)" v-on="on"></v-text-field>
							</template>
							<v-date-picker v-model="date" no-title @input="datepicker_examen = false"></v-date-picker>
						</v-menu>
					</v-col>
					<v-col md="2" v-show="examen.fecha"><v-select :items="modalidades" label="Modalidad"></v-select></v-col>
				</v-row>
				<v-row>
					<v-col md="2"><v-switch v-model="examen.promocional" inset label="Promocional"></v-switch></v-col>
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
		<!-- DATOS EXÁMEN -->
		<v-expand-transition>
			<v-card v-show="estado_actual == estados.edicion" class="mx-15 mt-3 text-center pa-5">
				<v-card class="mb-3">
					<v-toolbar color="primary" dark flat><v-icon>widgets</v-icon>&nbsp<v-toolbar-title>Preguntas</v-toolbar-title></v-toolbar>
					<v-row>
						<v-col>
							<v-card-text>			
								<v-treeview v-model="examen.temas" :load-children="cargar_temas" :items="items" selected-color="indigo" open-on-click selectable return-object expand-icon="mdi-chevron-down" on-icon="mdi-bookmark" off-icon="mdi-bookmark-outline" indeterminate-icon="mdi-bookmark-minus">
								</v-treeview>
							</v-card-text>
						</v-col>
						<v-divider vertical></v-divider>
						<v-col cols="12" md="6">
							<v-card-text>
								<div v-if="examen.temas.length === 0" key="title" class="text-h6 font-weight-light grey--text pa-4 text-center">
									Seleccionar preguntas del exámen
								</div>
								<v-scroll-x-transition group hide-on-leave>
									<v-chip v-for="(examen_elejido, i) in examen.temas" :key="i" :color="color_random(examen_elejido.name)" dark small class="ma-1">
										{{`${i+1}) ${examen_elejido.name}` }}&nbsp&nbsp&nbsp<v-icon left small @click="examen.temas.splice(i, 1); colores_preguntas.splice(i, 1);">mdi-minus</v-icon>
									</v-chip>
								</v-scroll-x-transition>
							</v-card-text>
						</v-col>
					</v-row>
				</v-card>
			</v-card>
		</v-expand-transition>
  </v-app>
</template>

<script>
	const examen_default = {
		fecha: null,
		temas: [],
	};
	export default {
		name: "comp_examenes",
		data: () => ({
      isLoading: false,
      periodos: [],
			examen: JSON.parse(JSON.stringify(examen_default)),
			date: (new Date(Date.now() - (new Date()).getTimezoneOffset() * 60000)).toISOString().substr(0, 10),
			datepicker_examen: false,
			materias: ['Laboratorio IV','Android','Redes','Web II'],
			estado_actual: 3,
			estados:{
				lista: 1,
				creacion: 2,
				edicion: 3,
			},
			temas: [],
			tipos: ['Final','Recuperatorio','Parcial','Global','Test'],
			modalidades: ['Multipleflai','Normal'],
			headers: [
				{ text: 'Comisión', align: 'start', sortable: false, value: 'comision', width: '80	%' },
				{ text: 'Cuatrimestre', value: 'cuatrimestre' },
				{ text: 'Ciclo lectivo', value: 'cicloLectivo' },
				{ text: 'Cant. Alumnos', value: 'cantAlumnos' },
				{ text: 'Estado', value: 'estado' },
				{ text: 'Acciones', value: 'actions' , width: '140px' },
			],
			examenes: [],
			colores: ['red','pink','purple','deep-purple','indigo','blue','light-blue','cyan','teal','green','light-green','lime','yellow','amber','orange','deep-orange','brown','blue-grey','grey'],
			tipos_colores : ['',' lighten',' darken',' accent'],
			colores_preguntas: []
		}),
		computed: {
			items () {
        const children = this.periodos.map(type => ({
          id: type,
          name: type,
          children: this.getChildren(type),
        }));

        return [{id: 0, name: 'Todos los periodos', children}];
      },
			shouldShowTree () {
        return this.temas.length > 0 && !this.isLoading;
      },
		},
		watch: {
      temas (val) {
        this.periodos = val.reduce((acc, cur) => {
          const type = cur.periodo;
          if (!acc.includes(type)) acc.push(type);
          return acc;
        }, []).sort();
      },
    },
		methods: {
 			cargar_temas () {
        if (this.temas.length) return;
				this.temas = [
					{
						id: 1,
						periodo: 'I',
						name: '¿Como se construye un formulario?'
					},
					{
						id: 2,
						periodo: 'I',
						name: '¿que es un botón?'
					},
					{
						id: 3,
						periodo: 'I',
						name: '¿que es un datepicker?'
					},
					{
						id: 4,
						periodo: 'II',
						name: '¿Como funciona .NET?'
					},
					{
						id: 5,
						periodo: 'II',
						name: '¿Como usar base de datos en .NET?'
					},
					{
						id: 6,
						periodo: 'II',
						name: '¿Que estrucura tiene un objeto?'
					},
					{
						id: 7,
						periodo: 'III',
						name: '¿Como hacer ejecutable un programa?'
					},
					{
						id: 8,
						periodo: 'III',
						name: '¿Cuales son las cualidades de un programa ejecutable?'
					},
					{
						id: 9,
						periodo: 'III',
						name: '¿Que sucede en la computadora cuando se ejecuta un programa?'
					},
					{
						id: 10,
						periodo: 'IV',
						name: '¿Que es una página web?'
					},
					{
						id: 11,
						periodo: 'IV',
						name: '¿Que es un framework?'
					},
					{
						id: 11,
						periodo: 'IV',
						name: '¿Como funciona Angular?'
					},
				];

				/*
        return fetch('https://api.openbrewerydb.org/temas')
          .then(res => res.json())
          .then(data => (this.temas = data))
          .catch(err => console.log(err))*/
      },
      getChildren (type) {
        const temas = [];
        for (const brewery of this.temas) {
          if (brewery.periodo !== type) continue;
          temas.push({
            ...brewery,
            name: brewery.name,
          });
        }
        return temas.sort((a, b) => a.name > b.name ? 1 : -1);
      },
			cargar_tabla: function(){
				var vm = this;
				axios.get('http://127.0.0.1:5000/api/Cursos')
				.then(res => {
					vm.examenes = res.data;
				}).catch(err => console.log(err));
			},
			lista: function(recargar){
				var vm = this;
				if(recargar) vm.cargar_tabla();
				vm.estado_actual = vm.estados.lista;
			},
			creacion: function(){
				var vm = this;
				vm.estado_actual = vm.estados.creacion;
			},
			edicion: function(id){
				var vm = this;
				var examen = vm.examenes.find(e =>e.id = id);
				examen.temas = [];
				vm.examen = examen;
				vm.estado_actual = vm.estados.edicion;
			},
			cancelar: function(){
				var vm = this;
				vm.$emit('cancelar');
			},

			//Omg
			formatDate (date) {
        if (!date) return null;
        const [year, month, day] = date.split('-');
        return `${day}/${month}/${year}`;
      },
      parseDate (date) {
        if (!date) return null;
        const [day, month, year] = date.split('/');
        return `${year}-${month.padStart(2, '0')}-${day.padStart(2, '0')}`;
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
			}
		},
		mounted() {
			var vm = this;
			vm.cargar_tabla();
		}
	};
</script>
/*
	<v-divider></v-divider>
	<v-card-actions>
						<v-btn text @click="tree = []">
							Limpiar
						</v-btn>
						<v-spacer></v-spacer>
						<v-btn class="white--text" color="green darken-1" depressed>
							Guardar temas
							<v-icon right>
								mdi-content-save
							</v-icon>
						</v-btn>
					</v-card-actions>
 */
