<template>
  <v-row justify="center">
    <v-dialog :value="modal" fullscreen hide-overlay transition="dialog-bottom-transition">
      <v-card>
        <v-toolbar dark color="primary">
					<v-btn icon dark @click="modal = false;">
						<v-icon>mdi-close</v-icon>
					</v-btn>
          <v-toolbar-title>Exámen: {{examen.tipo}} {{examen.materia}}</v-toolbar-title>
          <v-spacer></v-spacer>
          <v-toolbar-items>
            <v-btn dark text @click="console.log('Pregunta guardada')">Entregar</v-btn>
          </v-toolbar-items>
        </v-toolbar>
				<v-container class="pa-7 px-4">
					<v-row justify="center">
						<v-expansion-panels>
							<v-expansion-panel v-for="(pregunta,i) in examen.preguntas" :key="i">
								<v-expansion-panel-header :color="color_pregunta(pregunta)">{{`${String.fromCharCode(65+i)}) ${pregunta.pregunta}`}}</v-expansion-panel-header>
								<!-- Texto -->
								<v-expansion-panel-content v-if="pregunta.tipo == 'Texto'">
									<v-textarea :disabled="pregunta.respuesta == 'No responde'" v-model="pregunta.respuesta" color="teal" class="title" rows="1" auto-grow>
										<template v-slot:label>
											<div>Respuesta</div>
										</template>
									</v-textarea>
									<v-checkbox class="ml-4" @click="no_responde(pregunta)" label="No responde"></v-checkbox>
								</v-expansion-panel-content>
								<!-- Verdadero o false -->
								<v-expansion-panel-content v-if="pregunta.tipo == 'Verdadero/Falso'">
									<v-radio-group :disabled="pregunta.respuesta == 'No responde'" v-model="pregunta.respuesta" row>
										<v-radio label="Verdadero" :value="1"></v-radio>
										<v-radio label="Falso" :value="0"></v-radio>
									</v-radio-group>
									<v-checkbox class="ml-4" @click="no_responde(pregunta)" label="No responde"></v-checkbox>
								</v-expansion-panel-content>
								<!-- Multiple Choice -->
								<v-expansion-panel-content v-if="pregunta.tipo == 'Multiple Choice'">
									<v-radio-group class="ml-10" v-model="pregunta.respuesta" :disabled="pregunta.respuesta == 'No responde'">
										<v-radio v-for="(opcion, i) in pregunta.opciones" :key="i" class="mb-3" :label="`${String.fromCharCode(65+i)}) ${opcion}`" :value="i"></v-radio>
									</v-radio-group>
									<v-checkbox class="ml-4" @click="no_responde(pregunta)" label="No responde"></v-checkbox>
								</v-expansion-panel-content>
								<!-- Selección Multiple -->
								<v-expansion-panel-content v-if="pregunta.tipo == 'Selección Múltiple'">
									<v-container v-show="pregunta.tipo == 'Selección Múltiple'">
										<v-checkbox v-for="(opcion, i) in pregunta.opciones" :key="i" v-model="pregunta.respuesta" :disabled="pregunta.respuesta.length > 0 && pregunta.respuesta == 'No responde'" style="margin-bottom:-17px; margin-top:3px;margin-left: 33px" :label="`${String.fromCharCode(65+i)}) ${opcion}`" :value="i"></v-checkbox>
									</v-container>
								<v-checkbox class="ml-4" @click="no_responde(pregunta)" label="No responde"></v-checkbox>
								</v-expansion-panel-content>
							</v-expansion-panel>
						</v-expansion-panels>
					</v-row>
				</v-container>
      </v-card>
    </v-dialog>
  </v-row>
</template>
<script>
	import mixin_base from '../../assets/mixin_base';
	export default {
		mixins: [mixin_base],
		watch: {
			modal: function(val) {
				 var vm = this;
				if(!val) setTimeout(() => vm.$router.push(`/${vm.tab_actual}/1/preguntas`),300);
			}
		},
		name: "comp_modal_rendir",
		data: () => ({
			examen: {},
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
			},
			color_pregunta(pregunta){
				var vm = this;
				let respuesta = pregunta.respuesta;
				if(pregunta.tipo == 'Selección Múltiple'){
					return respuesta.length > 0 && respuesta[0] == 'No responde'?'purple lighten-5':respuesta.length != 0?'blue lighten-4':'';
				}else return respuesta == 'No responde'?'purple lighten-5':respuesta !== ''?'blue lighten-4':'';
			},
			no_responde(pregunta){
				var vm = this;
				let respuesta = pregunta.respuesta;
				if(pregunta.tipo == 'Selección Múltiple'){
					pregunta.respuesta = respuesta.length > 0 && respuesta[0] == 'No responde' ? []:['No responde'];
				}else pregunta.respuesta = respuesta == 'No responde' ? '':'No responde';
			},
		},
		computed: {
		},
		mounted() {
			var vm = this;
			vm.modal = vm.route == 'examen_rendir';
			vm.examen = {
				id: 1,
				materia: 'Laboratorio de programación',
				tipo: 'Final',
				fecha: null,
				preguntas: [],
				nota_regular: 0,
				nota_promocional: 0,
				recuperatorio: false,
				preguntas: [
					{
						id: 1,
						pregunta: '¿Que estrucura tiene un objeto?',
						opciones: [],
						tipo: 'Texto',
						respuesta: ''
					},
					{
						id: 2,
						pregunta: '¿Como usar base de datos en .NET?',
						tipo: 'Multiple Choice',
						opciones: ['A','B','C','D'],
						respuesta: ''
					},
					{
						id: 3,
						pregunta: '¿que es un datepicker?',
						tipo: 'Verdadero/Falso',
						opciones: [],
						respuesta: ''
					},
					{
						id: 4,
						pregunta: '¿Como se construye un formulario?',
						tipo: 'Selección Múltiple',
						opciones: ['A','B','C','D'],
						respuesta: []
					},
				]
			}
		},
	}
</script>
