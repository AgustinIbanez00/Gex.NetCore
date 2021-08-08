<template>
		<!-- Botones -->
		<v-btn v-if="modo =='creacion'" color="pink" dark absolute bottom fab style="top:75px; right:50px;" @click="accion('creacion')">
			<v-icon>add</v-icon>
		</v-btn>
		<v-btn v-else dark absolute bottom fab style="top:75px; right:50px;" @click="accion('lista')">
			<v-icon>mdi-format-list-bulleted-square</v-icon>
		</v-btn>
		<!-- Botones -->
</template>

<script>
	export default {
		name: "comp_botones",
		data: () => ({
			modo: 'lista',
		}),
		methods: {
			accion(accion){
				var vm = this;
				vm.modo = accion == 'creacion' ?  'lista' : 'creacion';
				vm.$emit('accion',vm.modo);
			},
			creacion: function(){
				var vm = this;
				vm.modo = 'lista';
			},
			lista: function(){
				var vm = this;
				vm.modo = 'creacion';
				vm.$emit('accion',vm.modo);
			}
		},
		mounted() {
			var vm = this;
		}
	};
</script>
