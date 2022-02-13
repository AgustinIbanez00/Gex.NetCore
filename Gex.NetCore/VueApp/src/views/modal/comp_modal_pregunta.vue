<template>
  <v-row justify="center">
    <v-dialog :value="modal" fullscreen transition="dialog-bottom-transition">
      <v-card>
        <v-toolbar dark color="primary">
					<v-btn icon dark @click="modal = false;">
						<v-icon>mdi-close</v-icon>
					</v-btn>
          <v-toolbar-title>Editar pregunta</v-toolbar-title>
          <v-spacer></v-spacer>
          <v-toolbar-items>
            <v-btn dark text @click="guardar">Guardar</v-btn>
          </v-toolbar-items>
        </v-toolbar>
        <v-list three-line subheader>
          <v-list-item>
						<v-col cols="12">
							<v-text-field v-model="pregunta.tema" label="Tema"></v-text-field>
						</v-col>
          </v-list-item>
          <v-list-item>
						<v-col cols="12">
							<v-textarea v-model="pregunta.descripcion" color="teal" class="title" rows="1" auto-grow>
								<template v-slot:label>
									<div>Pregunta</div>
								</template>
							</v-textarea>
						</v-col>
					</v-list-item>
          <v-list-item>
						<v-select :items="tipos_preguntas" :item-text="'nombre'" :item-value="'id'" label="Tipo" v-model="pregunta.tipo" @change="respuestas_cambiadas"></v-select>
          </v-list-item>
        </v-list>
        <v-divider></v-divider>
        <div class="pa-4">
					<!-- Texto -->
					<h3>{{pregunta.tipo == 0 ? 'Respuesta a desarrollar (Se corrige manualmente al finalizar el exámen)' : 'Respuestas'}}</h3>
					<hr>
					<!-- Verdadero o false -->
					<v-radio-group row v-show="pregunta.tipo == 1" v-model="active">
						<v-radio v-for="(respuesta, i) in respuestas" :key="i" :value="i" :label="`${respuesta.valor}`" ></v-radio>
					</v-radio-group>
					<!-- Multiple Choice -->
					<div v-show="pregunta.tipo == 2 || pregunta.tipo == 3">
						<v-btn v-if="!modo_nueva_respuesta" @click="input_respuesta" class="mt-3 mx-5" outlined rounded color="green">Agregar respuesta <v-icon>mdi-plus</v-icon></v-btn>
						<v-text-field v-else id="input_respuesta"  v-model="nueva_respuesta_txt" v-on:keyup.enter="agregar_pregunta" label="Respuesta nueva" append-icon="mdi-close" @click:append="modo_nueva_respuesta = 0">
						</v-text-field>
						<div style="position:absolute;margin-top:12px">
							<v-btn v-show="!respuesta.borrar" v-for="(respuesta, i) in respuestas" :key="i" @click="eliminar_respuesta(i)" style="left:0px; display:block;" text icon color="light-red darken-1">
								<v-icon >mdi-trash-can-outline</v-icon>
							</v-btn>
						</div>
						<v-radio-group class="ml-10" v-show="pregunta.tipo == 2" v-model="active">
							<v-radio v-show="!respuesta.borrar" v-for="(respuesta, i) in respuestas" :key="i" class="mb-3" :label="`${String.fromCharCode(65+i)}) ${respuesta.valor}`" :value="i"></v-radio>
						</v-radio-group>
						<v-container v-show="pregunta.tipo == 3">
							<v-checkbox v-show="!respuesta.borrar" v-model="respuesta.correcto" style="margin-bottom:-17px; margin-top:3px;margin-left: 33px" v-for="(respuesta, i) in respuestas" :key="i" :label="`${String.fromCharCode(65+i)}) ${respuesta.valor}`" :value="respuesta.correcto"></v-checkbox>
						</v-container>
						<h4 v-show="respuestas && !respuestas.length">Sin respuestas agregadas</h4>
					</div>
        </div>
      </v-card>
    </v-dialog>
  </v-row>
</template>
<script>
	import mixin_base from '../../assets/mixin_base';
	var pregunta_default = {
		id: 0,
		tema: '',
		descripcion: '',
		materia_id: 0,
		tipo: 0,
		estado: 1
	}
	export default {
		mixins: [mixin_base],
		watch: {
			modal: function(val) {
				 var vm = this;
				if(!val) setTimeout(() => vm.$router.push(`/${vm.tab_actual}/${vm.id}/preguntas`),300);
			}
		},
		active(val){
			var vm = this;
			if(vm.pregunta.tipo == 1){
				vm.respuesta[0].correcto = val;
				vm.respuesta[1].correcto = !val;
			}
		},
		name: "comp_modal_pregunta",
		data: () => ({
			modal: false,
			pregunta: JSON.parse(JSON.stringify(pregunta_default)),
			respuestas: [],
			respuestas_bak: [],
			active: 1,
			respuestas_trueFalse: [
				{
					borrar : false,
					valor: 'Verdadero',
					correcto: true
				},
				{
					borrar : false,
					valor: 'Falso',
					correcto: false
				}
			],
			cargando_card : 0,
			nueva_respuesta_txt: '',
			tipos_preguntas: [
				{id: 0, nombre: 'Texto'},
				{id: 1, nombre: 'Verdadero/Falso'},
				{id: 2, nombre: 'Multiple Choice'},
				{id: 3, nombre: 'Selección Múltiple'},
			],
			modo_nueva_respuesta: 0,
		}),
		methods: {
			guardar(){
				var vm = this;
				var f_guardar_respuestas = () =>{
					if(vm.respuestas_bak.filter(r => r.id > 0).length){
						axios.delete(`${vm.url_api}/pregunta/${vm.pregunta.id}/respuestas`, vm.axios_headers)
						.catch(err => {
							vm.alerta_error_txt = err.response.data.message;
							vm.alerta_error = true;
						});
						vm.respuestas = [];
					}
					if(vm.pregunta.tipo > 0){
						for(var i = 0; i < vm.respuestas.length; i++){
							if (vm.pregunta.tipo == 1 || vm.pregunta.tipo == 2) vm.respuestas[i].correcto = i == vm.active;
						}
						axios.post(`${vm.url_api}/respuesta`,{pregunta_id: vm.pregunta.id, respuestas: vm.respuestas}, vm.axios_headers)
						.then( res =>{ 
							if(res.data.success){
								vm.alerta_ok_txt = res.data.message;
								vm.alerta_ok = true;
							}
						})
						.catch(err => {
							vm.alerta_error_txt = err.response.data.message;
							vm.alerta_error = true;
						});
					}
				}
				if(vm.pregunta.id){
					axios.patch(`${vm.url_api}/pregunta`, vm.pregunta, vm.axios_headers)
					.then(async res =>{
						f_guardar_respuestas();
					}).catch(err => {
						vm.alerta_error_txt = err.response.data.message;
						vm.alerta_error = true;
					});
				}else{//CREACIÓN
					axios.post(`${vm.url_api}/pregunta`, vm.pregunta, vm.axios_headers)
					.then( res => {
						vm.pregunta = res.data.data;
						f_guardar_respuestas();
					}).catch(err => {
						vm.alerta_error_txt = err.response.data.message;
						vm.alerta_error = true;
					});
				}
				vm.modal = false;
			},
			input_respuesta(){
				var vm = this;
				vm.modo_nueva_respuesta = 1;
				vm.nueva_respuesta_txt = '';
				vm.$nextTick(()=> $('#input_respuesta').focus());
			},
			agregar_pregunta(){
				var vm = this;
				if(vm.nueva_respuesta_txt && !/^\s*$/.test(vm.nueva_respuesta_txt))
				{
					this.respuestas.push({id : 0, correcto: false, valor: vm.nueva_respuesta_txt,borrar: false});
					vm.nueva_respuesta_txt = '';
				}
			},
			eliminar_respuesta(i){
				var vm = this;
				if(vm.respuestas[i].id > 0) vm.respuestas[i].borrar = true;
				else vm.respuestas.splice(i, 1);
			},
			async respuestas_cambiadas(){
				var vm = this;
				if(vm.pregunta.tipo == 1){
					vm.respuestas = JSON.parse(JSON.stringify(vm.respuestas_trueFalse));
				}
				vm.active = 0;
			}
		},
		computed: {
		},
		async mounted() {
			var vm = this;
			vm.modal = vm.route == 'editar_materia_pregunta' || vm.route == 'crear_materia_pregunta';
			if(vm.route == 'crear_materia_pregunta'){//CREACIÓN
				vm.pregunta = JSON.parse(JSON.stringify(pregunta_default));
				vm.pregunta.tema = vm.tema_pregunta;
				vm.pregunta.materia_id = vm.id;
				vm.respuestas = [];
			}else{
				await axios.get(`${vm.url_api}/pregunta/${vm.pregunta_id}`, vm.axios_headers)//CARGAR PREGUNTA
				.then(res => {
					vm.pregunta = res.data.data;
				}).catch(err => {
					vm.alerta_error_txt = err.response.data.message;
					vm.alerta_error = true;
				});
				if(vm.pregunta.tipo > 0){
					await axios.get(`${vm.url_api}/pregunta/${vm.pregunta_id}/respuestas`, vm.axios_headers)//RESPUESTAS
					.then(res =>{
						vm.respuestas = res.data.data.map(obj=> ({ ...obj, borrar: false }))
						vm.respuestas_bak = JSON.parse(JSON.stringify(vm.respuestas));
						if(vm.pregunta.tipo > 0 && vm.pregunta.tipo < 4) vm.active = vm.respuestas.findIndex(r => r.correcto);
					}).catch(err => {
						vm.alerta_error_txt = err.response.data.message;
						vm.alerta_error = true;
					});
				}
			}
		},
	}
</script>
