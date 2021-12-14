<template>
	<v-app id="fondo">
		<!-- TABLA EXÁMENES -->
		<v-expand-transition>
			<v-data-table :ref="`tabla_${tab_actual}`" v-show="route == 'listar_examen'" :headers="headers" :items="lista" :items-per-page="5" class="elevation-3 px-10 mx-15 my-3" :loading="cargando_lista" :loading-text="`Cargando ${elementos}`">
				<template v-slot:item.actions="{ item }">
					<!-- Acciones -->
					<v-btn class="ma-2" @click="rendir(item.id)" outlined dark color="green darken-1">Rendir <v-icon>mdi-book-play-outline</v-icon></v-btn>
					<v-btn class="ma-2" text icon color="blue lighten-1" @click="edicion(item.id)"><v-icon>mdi-pencil</v-icon></v-btn>
					<v-btn class="ma-2" text icon color="blue-grey darken-1"><v-icon>mdi-delete</v-icon></v-btn>
				</template>
			</v-data-table>
		</v-expand-transition>
		<!-- ABM EXÁMENES -->
		<v-expand-transition  class="justify-center">
			<v-card v-show="route == 'editar_examen' || route == 'crear_examen'" class="mx-15 mt-8 text-center pa-5 pt-0">
				<v-card-title v-if="route == 'editar_examen'">EDITAR EXÁMEN</v-card-title>
				<v-card-title v-else>NUEVO EXÁMEN</v-card-title>
				<v-row>
					<v-col cols="6"><v-select :items="materias"  :item-text="'nombre'"  :item-value="'id'" label="Materia" v-model="examen.materia_id" @change="materia_cambiada"></v-select></v-col>
					<v-col cols="4"><v-select :items="tipos" :item-text="'nombre'" label="Tipo" :item-value="'id'" v-model="examen.tipo"></v-select></v-col>
					<v-col cols="2" v-show="examen.fecha"><v-select :items="modalidades" label="Modalidad"></v-select></v-col>
				</v-row>
				<v-row>
					<v-col cols="3"><v-switch v-model="examen.promocional" inset label="Promocional"></v-switch></v-col>
					<v-col cols="3"><v-switch v-model="examen.recuperatorio" inset label="Recuperatorio"></v-switch></v-col>
					<v-col cols="3">
						<v-text-field v-model="examen.nota_regular" label="Regular">
						</v-text-field>
					</v-col>
					<v-col cols="3">
						<v-text-field v-model="examen.nota_promo" label="Promocional">
						</v-text-field>
					</v-col>
				</v-row>
				<!-- Acciones ABM -->
				<v-card-actions>
					<v-col class="text-right">
						<v-btn text @click="cancelar">Cancelar</v-btn>
						<v-btn text color="primary" @click="guardar(1)">Guardar y cerrar</v-btn>
						<v-btn button class="white--text indigo darken-1" @click="guardar">Guardar</v-btn>
					</v-col>
				</v-card-actions>
			</v-card>
		</v-expand-transition>
		<RouterView/>
	</v-app>
</template>
<script>
	import mixin_base from '../../assets/mixin_base';
	//VARIABLES
    var examen_default = {
        id: 0,
        tipo: 0,
        nota_regular: 0,
        nota_promo: 0,
        recuperatorio: true,
        materia_id: 0
    }
	export default {
		name: "comp_examen",
		mixins: [mixin_base],
		data: () => ({
			isLoading: false,
			examen: JSON.parse(JSON.stringify(examen_default)),
			materias: ['Laboratorio de programación', 'Android', 'Redes', 'Web II'],
            tipos: [
                { id: 0, nombre: 'Final' },
                { id: 1, nombre: 'Parcial' },
                { id: 2, nombre: 'Global' },
            ],
			modalidades: ['Multipleflai','Normal'],
			headers: [
				{ text: 'Materia', align: 'start', sortable: true, value: 'materia', width: '80	%' },
				{ text: 'Tipo', value: 'tipo', sortable: true},
				{ text: 'Regular', value: 'nota_regular' },
				{ text: 'Promocional', value: 'nota_promocional' },
				{ text: 'Recuperatorio', value: 'recuperatorio', sortable: true },
				{ text: 'Promocional', value: 'promocional', sortable: true },
				{ text: 'Acciones', value: 'actions' , width: '300px' },
			],
			examenes: [],
			currentTag: null,
			selection_examen: null,
			currentTag_examen: null,
		}),
		computed: {
		},
		methods: {
			cancelar(){
				var vm = this;
				vm.estado(vm.estados.lista);
				vm.top();
			},
			materia_cambiada(){
				var vm = this;
			},
			rendir(id){
				var vm = this;
				vm.$router.push(`/${vm.tab_actual}/${id}/rendir`);
			},
            guardar(listar) {
                var vm = this;
                let f_guardar = res => {
                    if (listar) vm.listar();
					else vm.examen = res.data.data;
					console.log("Examen ", vm.examen)
                }
				if (vm.examen.id) {
                    console.table("examen body", vm.examen)
                    axios.patch(`${vm.url_api}/examen`, vm.examen, vm.axios_headers)
                        .then(f_guardar).catch(err => { if (err.response) console.table(err.response.data.error_messages); });
				} else {
                    console.table("examen body", vm.examen)
                    axios.post(`${vm.url_api}/examen`, vm.examen, vm.axios_headers)
                        .then(f_guardar).catch(err => { if(err.response) console.table(err.response.data.error_messages); });
                }
            }
		},
		async mounted() {
			var vm = this;
			vm.$nextTick(() => {
				if(vm.examen.id){
					vm.titulo_txt = vm.examen.materia;
				}
			})
			vm.cargar_tabla();

            await axios.get(`${vm.url_api}/materia`, vm.axios_headers)
				.then(res => {
					vm.materias = res.data.data
                }).catch(err => { if (err.response) console.table(err.response.data.error_messages); });
		}
	};
</script>
