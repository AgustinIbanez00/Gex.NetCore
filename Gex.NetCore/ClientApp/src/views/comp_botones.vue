<template>
		<div>
			<!-- Botones -->
			<RouterLink :to="`/${tab_actual}/crear`" v-show="route == `listar_${tab_actual}`">
				<v-btn @click="creacion" outlined color="white" dark absolute bottom style="font-size:16px;padding-right:10px;top:60px; right:60px; height:40px; z-index:2;">
					Crear <v-icon>add</v-icon>
				</v-btn>
			</RouterLink>
			<RouterLink :to="`/${tab_actual}`" v-show="route == `editar_${tab_actual}` || route == `crear_${tab_actual}`">
				<v-btn @click="lista" color="pink" dark absolute bottom style="top:65px; height:45px;font-size:16px; z-index:2; padding-right: 4px; right: 60px;">
					{{elementos}} <v-icon>mdi-chevron-right</v-icon>
				</v-btn>
			</RouterLink>
			<RouterLink :to="`/${tab_actual}/${id}`" v-show="route == `${tab_actual}_preguntas`">
				<v-btn color="white" absolute bottom style="padding-left:2px; right:60px; top:65px;z-index:2; height:45px;font-size:16px;">
					<v-icon>mdi-chevron-left</v-icon>
					{{elemento}}
				</v-btn>
			</RouterLink>
			<RouterLink :to="`/${tab_actual}/${id}/preguntas`" v-show="route == `editar_${tab_actual}`">
				<v-btn color="white" absolute bottom style="padding-left:6px; right:210px; top:65px; z-index:2; height:45px; font-size:16px;">
					<v-icon>mdi-playlist-check</v-icon> Preguntas
				</v-btn>
			</RouterLink>
			<!-- Botones -->
		</div>
</template>
<script>
	import mixin_base from '../assets/mixin_base';
	export default {
		mixins: [mixin_base],
		name: "comp_botones",
		data: () => ({
		}),
		methods: {
			creacion(){
				var vm = this;
				vm.estado(vm.estados.creacion);
			},
			lista(){
				var vm = this;
				vm.estado(vm.estados.lista);
			},
		},
		computed: {
		},
	}
</script>
