<template>
    <v-app class="light-blue">
        <v-expand-transition>
            <v-card class="mx-15 mt-8 text-center pa-5">
                <v-form ref="form">
									<v-text-field v-model="usuario" name="username" label="Correo electrónico" required></v-text-field>
									<v-text-field v-model="clave" name="password" label="Contraseña" required></v-text-field>
									<v-btn @click="login">Iniciar sesión</v-btn>
                </v-form>
            </v-card>
        </v-expand-transition>
    </v-app>
</template>

<script>
    import axios from 'axios';
    import VueCookies from 'vue-cookies';
		import mixin_base from '../assets/mixin_base';

    export default {
        data: function () {
					return {
						clave: "",
						usuario: "",
					}
        },
				mixins: [mixin_base],
        methods: {
            validate() {
							this.$refs.form.validate();
            },
            login() {
							var vm = this;
							let model = { email: vm.usuario, password: vm.clave };
							console.log(model)
							axios.post("http://localhost:5000/api/Auth/Login", model)
							.then(res => {
								if (res.status == 200){
									VueCookies.set('gex_session', res.data.data);
									vm.$router.push(`materia`);
								}
							}).catch(err => console.log(err))
            },
        }
    };
</script>
