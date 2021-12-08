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
            <v-btn dark text @click="console.log('Pregunta guardada')">Guardar</v-btn>
          </v-toolbar-items>
        </v-toolbar>
        <v-list three-line subheader>
          <v-list-item>
						<v-col cols="12">
							<v-textarea v-model="pregunta.pregunta" color="teal" class="title" rows="1"auto-grow>
								<template v-slot:label>
									<div>Pregunta</div>
								</template>
							</v-textarea>
						</v-col>          
					</v-list-item>
          <v-list-item>
						<v-select :items="tipos_preguntas" label="Tipo" v-model="pregunta.tipo"></v-select>
          </v-list-item>
        </v-list>
        <v-divider></v-divider>
        <div class="pa-4">
					<!-- Texto -->
					<h3>{{pregunta.tipo == 'Texto' ? 'Respuesta a desarrollar (Se corrige manualmente al finalizar el exámen)' : 'Respuestas'}}</h3>
					<hr>
					<!-- Verdadero o false -->
					<v-radio-group v-model="respuesta_correcta" row v-show="pregunta.tipo == 'Verdadero/Falso'">
						<v-radio label="Verdadero" :value="1"></v-radio>
						<v-radio label="Falso" :value="0"></v-radio>
					</v-radio-group>
					<!-- Multiple Choice -->
					<div v-show="pregunta.tipo == 'Multiple Choice' || pregunta.tipo == 'Selección Múltiple'">
						<v-btn v-if="!modo_nueva_respuesta" @click="input_respuesta" class="mt-3 mx-5" outlined rounded color="green">Agregar respuesta <v-icon>mdi-plus</v-icon></v-btn>
						<v-text-field v-else id="input_respuesta" v-model="nueva_respuesta_txt" v-on:keyup.enter="agregar_pregunta" label="Respuesta nueva" append-icon="mdi-close" @click:append="modo_nueva_respuesta = 0">
						</v-text-field>
						<div style="position:absolute;margin-top:12px">
							<v-btn v-for="(respuesta, i) in respuestas" :key="i" @click="eliminar_respuesta(i)" style="left:0px; display:block;" text icon color="light-red darken-1">
								<v-icon >mdi-trash-can-outline</v-icon>
							</v-btn>
						</div>
						<v-radio-group v-model="respuesta_correcta" class="ml-10" v-show="pregunta.tipo == 'Multiple Choice'">
							<v-radio v-for="(respuesta, i) in respuestas" :key="i" class="mb-3" :label="`${String.fromCharCode(65+i)}) ${respuesta.valor}`" :value="i"></v-radio>
						</v-radio-group>
						<v-container v-show="pregunta.tipo == 'Selección Múltiple'">
							<v-checkbox v-model="respuestas_correctas" style="margin-bottom:-17px; margin-top:3px;margin-left: 33px" v-for="(respuesta, i) in respuestas" :key="i" :label="`${String.fromCharCode(65+i)}) ${respuesta.valor}`" :value="i"></v-checkbox>
						</v-container>
						<h4 v-show="!respuestas.length">Sin respuestas agregadas</h4>
					</div>
        </div>
      </v-card>
    </v-dialog>
  </v-row>
</template>
<script>
	import mixin_base from '../assets/mixin_base';
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
			pregunta:{
				pregunta: '',
				tipo: 'Texto',
			},
			respuesta_correcta: null,
			respuestas_correctas: [],
			respuestas: [],
			nueva_respuesta_txt: '',
			tipos_preguntas: ['Multiple Choice', 'Verdadero/Falso', 'Texto','Selección Múltiple'],
			modo_nueva_respuesta: 0,
		}),
		methods: {
			guardar(){
				var vm = this;
				//controlar si es selección multiple y enviar arreglo de preguntas correctas
			},
			input_respuesta(){
				var vm = this;
				vm.modo_nueva_respuesta = 1;
				vm.nueva_respuesta_txt = '';
				vm.$nextTick(()=> $('#input_respuesta').focus());
			},
			agregar_pregunta(){
				var vm = this;
				this.respuestas.push({
					valor: vm.nueva_respuesta_txt,
				});
				vm.nueva_respuesta_txt = '';
				vm.respuesta_correcta = 0;
			},
			eliminar_respuesta(i){
				var vm = this;
				vm.respuestas.splice(i, 1);
				if(vm.respuestas.length > 0) vm.respuesta_correcta = 0;
			}
		},
		computed: {
		},
		mounted() {
			var vm = this;
			vm.modal = vm.route == 'editar_materia_preguntas';
		},
	}
</script>
