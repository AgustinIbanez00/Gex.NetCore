<template>
  <v-app>
		<v-card-title class="white--text deep-purple darken-3 w-100 text-sm-h3 justify-center">
			<div>Mesas</div>
		</v-card-title>
		<v-data-table
			:headers="headers"
			:items="items"
			:items-per-page="5"
			class="elevation-1"
		></v-data-table>
  </v-app>
</template>

<script>
	export default {
		name: "comp_mesas",
		data: () => ({
			headers: [
				{ text: 'Comisión', align: 'start', sortable: false, value: 'comision', width: '80	%' },
				{ text: 'Cuatrimestre', value: 'cuatrimestre' },
				{ text: 'Ciclo lectivo', value: 'cicloLectivo' },
				{ text: 'Cant. Alumnos', value: 'cantAlumnos' },
				{ text: 'Estado', value: 'estado' },
			],
			items: []
		}),
		methods: {
			cargar_tabla: function(){
				var vm = this;
				console.log("Coso");
				console.log(vm);
				axios.get('http://127.0.0.1:5000/api/Cursos')
				.then(res => {
					vm.items = res.data;
				})
				.catch(err => console.log(err))
			},
			prueba_emit: function(){
				var vm = this;
				console.log('empezó a emitirse');
				vm.$emit('emitido',33);
			}
		},
		mounted() {
			var vm = this;
			vm.cargar_tabla();
		}
	};
</script>
