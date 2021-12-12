<template>
  <v-row justify="center">
    <v-dialog :value="modal" fullscreen hide-overlay transition="dialog-bottom-transition">
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
							<v-textarea v-model="pregunta.descripcion" color="teal" class="title" rows="1"auto-grow>
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
					<v-radio-group v-model="respuesta_correcta" row v-show="pregunta.tipo == 1">
						<v-radio label="Verdadero" :value="1"></v-radio>
						<v-radio label="Falso" :value="0"></v-radio>
					</v-radio-group>
					<!-- Multiple Choice -->
					<div v-show="pregunta.tipo == 2 || pregunta.tipo == 3">
						<v-btn v-if="!modo_nueva_respuesta" @click="input_respuesta" class="mt-3 mx-5" outlined rounded color="green">Agregar respuesta <v-icon>mdi-plus</v-icon></v-btn>
						<v-text-field v-else id="input_respuesta" v-model="nueva_respuesta_txt" v-on:keyup.enter="agregar_pregunta" label="Respuesta nueva" append-icon="mdi-close" @click:append="modo_nueva_respuesta = 0">
						</v-text-field>
						<div style="position:absolute;margin-top:12px">
							<v-btn v-show="!respuesta.borrar" v-for="(respuesta, i) in respuestas" :key="i" @click="eliminar_respuesta(i)" style="left:0px; display:block;" text icon color="light-red darken-1">
								<v-icon >mdi-trash-can-outline</v-icon>
							</v-btn>
						</div>
						<v-radio-group v-model="respuesta_correcta" class="ml-10" v-show="pregunta.tipo == 2">
							<v-radio v-show="!respuesta.borrar" v-for="(respuesta, i) in respuestas" :key="i" class="mb-3" :label="`${String.fromCharCode(65+i)}) ${respuesta.valor}`" :value="i"></v-radio>
						</v-radio-group>
						<v-container v-show="pregunta.tipo == 3">
							<v-checkbox v-model="respuestas_correctas" style="margin-bottom:-17px; margin-top:3px;margin-left: 33px" v-for="(respuesta, i) in respuestas" :key="i" :label="`${String.fromCharCode(65+i)}) ${respuesta.valor}`" :value="i"></v-checkbox>
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
		tema: '',
		descripcion: '',
		materia_id: 0,
		tipo: 0,
		estado: 1,
	}
	export default {
		mixins: [mixin_base],
		watch: {
			modal: function(val) {
				 var vm = this;
				if(!val) setTimeout(() => vm.$router.push(`/${vm.tab_actual}/1/preguntas`),300);
			}
		},
		name: "comp_modal_pregunta",
		data: () => ({
			modal: false,
			pregunta: JSON.parse(JSON.stringify(pregunta_default)),
			respuesta_correcta: null,
			respuestas_correctas: [],
			respuestas: [],
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
			async guardar(){
				var vm = this;
				if(vm.pregunta.id){
					await axios.patch(`${vm.url_api}/pregunta`, vm.pregunta, vm.axios_headers)
					.then( async res =>{
						//RESPUESTAS
						let respuestas_guardar = JSON.parse(JSON.stringify(vm.respuestas));
						if(vm.pregunta.tipo == 0) respuestas_guardar = [];
						if(vm.pregunta.tipo == 1) respuestas_guardar = [{valor: vm.respuesta_correcta, correcta: 1}];
						if(vm.pregunta.tipo == 2 || vm.pregunta.tipo == 3){
							for(var i = 0; i < vm.respuestas.length; i++){
								if( vm.pregunta.tipo ==  2){
									vm.respuestas_guardar[i].correcto = i == vm.respuesta_correcta;
								}else vm.respuestas_guardar[i].correcto = vm.respuestas_correctas.includes(i);
								vm.respuestas_guardar[i].pregunta_id = vm.pregunta.id;
							}
						} 
						await axios.post(`${vm.url_api}/pregunta/${vm.pregunta_id}/respuestas`,[respuestas_guardar], vm.axios_headers)
						.then( async res =>{
							
						}).catch(err => console.log(err));
						
					}).catch(err => console.log(err));
				}else{//CREACIÓN
					await axios.post(`${vm.url_api}/pregunta`, vm.pregunta, vm.axios_headers)
					.then( res => { vm.$router.push(`/${vm.tab_actual}/${vm.id}/preguntas`);}).catch(err => console.log(err));
				}
			},
			input_respuesta(){
				var vm = this;
				vm.modo_nueva_respuesta = 1;
				vm.nueva_respuesta_txt = '';
				vm.$nextTick(()=> $('#input_respuesta').focus());
			},
			agregar_pregunta(){
				var vm = this;
				this.respuestas.push({valor: vm.nueva_respuesta_txt,borrar: 0});
				vm.nueva_respuesta_txt = '';
				vm.respuesta_correcta = 0;
			},
			eliminar_respuesta(i){
				var vm = this;
				if(!vm.respuestas[i].id) vm.respuestas.splice(i, 1);
				else vm.respuestas[i].borrar = 1;
				if(vm.respuestas.find(r => !r.borrar).length > 0) vm.respuesta_correcta = 0;
			},
			respuestas_cambiadas(){
				var vm = this;
				vm.respuestas = [];
				vm.respuestas_correctas = [];
				vm.respuesta_correcta = null;
			}
		},
		computed: {
		},
		async mounted() {
			var vm = this;
			vm.modal = vm.route == 'editar_materia_pregunta' || vm.route == 'crear_materia_pregunta';
			if(vm.route == 'crear_materia_pregunta'){
				vm.pregunta = JSON.parse(JSON.stringify(pregunta_default));
				vm.pregunta.tema = vm.tema_pregunta;
				vm.pregunta.materia_id = vm.id;
				vm.respuestas = [];
				vm.respuesta_correcta = null;
				vm.respuestas_correctas = [];
			}else{
				await axios.get(`${vm.url_api}/pregunta/${vm.pregunta_id}`, vm.axios_headers)//PREGUNTAS
				.then(res => {
					vm.pregunta = res.data.data;
					//vm.respuestas = vm.pregunta.respuestas;
					//vm.respuesta_correcta = vm.pregunta.respuesta_correcta;
				//vm.respuestas_correctas = vm.pregunta.respuestas_correctas;
				}).catch(err => console.log(err));
			/*	if(vm.pregunta.tipo > 0){
					await axios.get(`${vm.url_api}/respuestas/${vm.pregunta_id}`, vm.axios_headers)//RESPUESTAS
					.then(res => {}).catch(err => console.log(err));
				}*/
			}
		},
	}
</script>
