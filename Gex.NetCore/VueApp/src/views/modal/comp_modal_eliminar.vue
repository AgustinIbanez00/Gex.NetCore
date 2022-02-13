<template>
	<v-dialog justify="center" v-model="modal_eliminar" persistent>
		<v-card>
			<v-card-title class="text-h5">
				Está seguro que desea eliminar {{eliminar_txt}}?
			</v-card-title>
			<v-card-text>Let Google help apps determine location. This means sending anonymous location data to Google, even when no apps are running.</v-card-text>
			<v-card-actions>
				<v-spacer></v-spacer>
				<v-btn color="green darken-1" text @click="cancelar">
					Cancelar
				</v-btn>
				<v-btn color="green darken-1" text @click="eliminar(1)">
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
		}),
		methods: {
			cancelar(){
				var vm = this;
				vm.modal_eliminar = 0;
				vm.eliminar_txt = "";
				vm.eliminar_id = 0;
			}
		},
		computed: {
		},
		mounted() {
			var vm = this;
		},
	}
</script>
