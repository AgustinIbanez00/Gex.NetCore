<template>
	<v-app id="fondo">
		<v-expand-transition>
			<v-card dark class="mx-15 mt-8 text-center pa-5">
				<v-card-title>Login</v-card-title>
				<v-text-field v-model="email" :error-messages="errors.email" name="username" label="Correo electrónico" required
				></v-text-field>
				<v-text-field v-model="password" type="password" name="password" :error-messages="errors.password" label="Contraseña" required v-on:keyup.enter="login"
				></v-text-field>
				<v-btn @click="login">Iniciar sesión</v-btn>
			</v-card>
		</v-expand-transition>
	</v-app>
</template>

<script>
import axios from "axios";
import mixin_base from "../assets/mixin_base";

export default {
	data: function () {
		return {
			email: "",
			password: "",
			errors: [],
		};
	},
	mixins: [mixin_base],
	methods: {
		validate() {
			this.$refs.form.validate();
		},
		login() {
			var vm = this;
			let model = { email: vm.email, password: vm.password };
			axios
				.post(`${vm.url_api}/usuario/login`, model)
				.then((res) => {
					if (res.status == 200) {
						vm.$cookies.set("gex_session", res.data.data.token);
						vm.$router.push({name:`mi_usuario`});
					}
				})
				.catch((error) => {
					if (error.response) {
						vm.errors = error.response.data.error_messages;
						vm.alerta_error_txt = error.response.data.message;
					} else vm.alerta_error_txt = "No se pudo conectar con el servidor. Por favor revisa tu conexión.";
					vm.alerta_error = true;
				});
		},
	},
};
</script>
