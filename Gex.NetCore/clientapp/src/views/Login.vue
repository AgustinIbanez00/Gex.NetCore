<template>
    <v-app class="light-blue">
        <v-expand-transition>
            <v-card class="mx-15 mt-8 text-center pa-5">
                <v-form ref="form" v-model="valid">
                    <v-text-field v-model="usuario"
                                  name="username"
                                  :rules="usernameRules"
                                  label="Correo electrónico"
                                  required></v-text-field>

                    <v-text-field v-model="clave"
                                  name="password"
                                  :counter="10"
                                  :rules="passwordRules"
                                  label="Contraseña"
                                  required></v-text-field>
                    <v-btn @click="login">Iniciar sesión</v-btn>
                </v-form>
            </v-card>
        </v-expand-transition>
    </v-app>
</template>

<script>
    import axios from 'axios';
    import VueCookies from 'vue-cookies';

    export default {
        data: function () {
            return {
                valid: true,
                clave: "",
                passwordRules: [
                    (v) => !!v || "Es necesario una contraseña",
                    (v) =>
                        (v && v.length <= 10) ||
                        "La contraseña debe tener menos de 10 caracteres.",
                ],
                usuario: "",
                usernameRules: [
                    (v) => !!v || "Es obligatorio ingresar un correo electrónico",
                    (v) => /.+@.+\..+/.test(v) || "El correo electrónico debe ser válido.",
                ],
                checkbox: false,
                prices: null,
            }
        },
        methods: {
            validate() {
                this.$refs.form.validate();
            },
            reset() {
                this.$refs.form.reset();
            },
            resetValidation() {
                this.$refs.form.resetValidation();
            },
            login() {
                let model = { email: this.usuario, password: this.clave };
                console.log(model)
                axios.post("http://localhost:5000/api/Auth/Login", model)
                    .then(res => {
                        if (res.status == 200)
                            VueCookies.set('gex_session', res.data.data)
                    })
                    .catch(err => console.log(err))
            },
        }
    };
</script>
