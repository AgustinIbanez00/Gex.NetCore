<template>
	<v-dialog justify="center" v-model="modal_eliminar" persistent>
		<v-card>
			<v-card-title class="text-h5">
				Está seguro que desea eliminar {{tab_actual}}: {{eliminar_txt}}?
			</v-card-title>
			<v-card-text>Let Google help apps determine location. This means sending anonymous location data to Google, even when no apps are running.</v-card-text>
			<v-card-actions>
				<v-spacer></v-spacer>
				<v-btn color="green darken-1" text @click="$router.push(`/${tab_actual}/`); modal_eliminar = 0;">
					Cancelar
				</v-btn>
				<v-btn color="green darken-1" text @click="eliminar">
					Eliminar
				</v-btn>
			</v-card-actions>
		</v-card>
	</v-dialog>
</template>
<script>
	import mixin_base from '../../assets/mixin_base';
	export default {
		mixins: [mixin_base],
		name: "comp_modal_eliminar",
		data: () => ({
			examen: {},
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
			vm.modal_eliminar = vm.route == `eliminar_${vm.tab_actual}`;
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
