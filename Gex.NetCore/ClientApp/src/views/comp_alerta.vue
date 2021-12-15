<template>
	<div>
		<v-snackbar :timeout="4000" :value="alerta_error" relative right shaped bottom color="red">
			<div style="font-size:18px">
				{{alerta_error_txt}}
			</div>
		</v-snackbar>
		<v-snackbar  :timeout="4000" :value="alerta_ok" relative center rounded="pill" botton color="light-blue darken-1">
			<div style="font-size:18px">
				{{alerta_ok_txt}}
			</div>
		</v-snackbar>
	</div>
</template>
<script>
	import mixin_base from '../assets/mixin_base';
	export default {
		mixins: [mixin_base],
		name: "comp_alerta",
	}
</script>
