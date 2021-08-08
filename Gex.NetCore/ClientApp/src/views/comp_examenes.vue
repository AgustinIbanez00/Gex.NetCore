<template>
  <v-app class="light-blue">
		<!-- ABM EXÁMENES -->
		<v-expand-transition class="justify-center">
			<v-card v-show="abm" class="mx-15 mt-5 text-center pa-5 pt-0">
				<v-card-title>NUEVO EXÁMEN</v-card-title>
				<v-row>
					<v-col md="6">
						<v-select :items="materias" label="Materia"></v-select>
					</v-col>
					<v-col md="2">
						<v-select :items="tipos" label="Tipo"></v-select>
					</v-col>
					<v-col md="2">
						<v-select :items="modalidades" label="Modalidad"></v-select>
					</v-col>	
						<v-col lg="2">
						<v-menu ref="datepicker_examen" v-model="datepicker_examen" :close-on-content-click="false" transition="scale-transition" offset-y max-width="290px" min-width="auto">
							<template v-slot:activator="{ on, attrs }">
								<v-text-field v-model="examen_nuevo.fecha" label="Fecha del exámen" prepend-icon="mdi-calendar" v-bind="attrs" @blur="date = parseDate(examen_nuevo.fecha)" v-on="on"></v-text-field>
							</template>
							<v-date-picker v-model="date" no-title @input="datepicker_examen = false"></v-date-picker>
						</v-menu>
					</v-col>
				</v-row>
				<v-row>
					<v-col md="12">
						<v-combobox v-model="examen_nuevo.temas" :items="temas" chips clearable label="Seleccionar" multiple prepend-icon="widgets" persistent-hint hint="Temas" solo>
							<template v-slot:selection="{ attrs, item, select, selected }">
								<v-chip v-bind="attrs" :input-value="selected" close @click="select" @click:close="quitar_tema(item)">
									<strong>{{ item }}</strong>
								</v-chip>
							</template>
						</v-combobox>
					</v-col>
				
				</v-row>
				<v-row>
					<v-col md="2">
							<v-select :items="['Si','No']" label="Promocional"></v-select>
					</v-col>
					<v-col md="2">
						<v-text-field label="Comisión" hide-details="auto"></v-text-field>
					</v-col>
					<v-col md="2">
						<v-text-field label="Cuatrimestre" hide-details="auto"></v-text-field>
					</v-col>
					<v-col md="2">
						<v-text-field label="Ciclo lectivo" hide-details="auto"></v-text-field>
					</v-col>
					<v-col md="2">
						<v-text-field label="Cant. Alumnos" type="number" hide-details="auto"></v-text-field>
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

		<!-- TABLA EXÁMENES -->
		<v-expand-transition>
		<v-data-table v-show="estado_actual == estados.lista && !abm" :headers="headers" :items="items" :items-per-page="5" class="elevation-3 px-10 mx-15 my-5">
			<template v-slot:item.actions="{ item }"><!-- Acciones -->
				<v-btn class="ma-2" text icon color="blue lighten-1"><v-icon>mdi-pencil</v-icon></v-btn>
				<v-btn class="ma-2" text icon color="blue-grey darken-1"><v-icon>mdi-delete</v-icon></v-btn>
			</template>
		</v-data-table>
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
			abm: true,
			examen_nuevo: JSON.parse(JSON.stringify(examen_default)),
			date: (new Date(Date.now() - (new Date()).getTimezoneOffset() * 60000)).toISOString().substr(0, 10),
			datepicker_examen: false,
			materias: ['Laboratorio IV','Android','Redes','Web II'],
			temas: ['Bitcoin', 'Breakdance','Cubo rubik','Guitarra','Ayuno intermitente','Software developer'],
			estado_actual: 1,
			estados:{
				lista: 1,
				creacion: 2,
				edicion: 3,
			},
			tipos: ['Final','Recuperatorio','Parcial'],
			modalidades: ['Multipleflai','Normal'],
			headers: [
				{ text: 'Comisión', align: 'start', sortable: false, value: 'comision', width: '80	%' },
				{ text: 'Cuatrimestre', value: 'cuatrimestre' },
				{ text: 'Ciclo lectivo', value: 'cicloLectivo' },
				{ text: 'Cant. Alumnos', value: 'cantAlumnos' },
				{ text: 'Estado', value: 'estado' },
				{ text: 'Acciones', value: 'actions' , width: '140px' },
			],
			items: [],
		}),
		computed: {
		},
		watch: {
      date (val) {
        this.examen_nuevo.fecha = this.formatDate(this.date)
      },
    },
		methods: {
			cargar_tabla: function(){
				var vm = this;
				axios.get('http://127.0.0.1:5000/api/Cursos')
				.then(res => {
					vm.items = res.data;
				})
				.catch(err => console.log(err))
			},
			lista: function(recargar){
				var vm = this;
				if(recargar) vm.cargar_tabla();
				vm.abm = false;
			},
			creacion: function(){
				var vm = this;
				vm.abm = true;
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
			quitar_tema (item) {
        this.examen_nuevo.temas.splice(this.examen_nuevo.temas.indexOf(item), 1)
        this.examen_nuevo.temas = [...this.examen_nuevo.temas]
      },
		},
		mounted() {
			var vm = this;
			vm.cargar_tabla();
		}
	};
</script>
