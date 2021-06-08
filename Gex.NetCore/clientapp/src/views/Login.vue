<template>

    <v-form ref="form" v-model="valid">
        <v-text-field v-model="username"
                      :rules="usernameRules"
                      label="Correo electrónico"
                      required></v-text-field>

        <v-text-field v-model="password"
                      :counter="10"
                      :rules="passwordRules"
                      label="Contraseña"
                      required></v-text-field>
        <v-btn @click="login">Iniciar sesión</v-btn>

    </v-form>

</template>

<script>
    import axios from 'axios';

    export default {
        data: function () {
            return {

                valid: true,
                password: "",
                passwordRules: [
                    (v) => !!v || "Es necesario una contraseña",
                    (v) =>
                        (v && v.length <= 10) ||
                        "La contraseña debe tener menos de 10 caracteres.",
                ],
                username: "",
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

//.post("http://localhost:50598/api/Auth/login", { username: this.username, password: this.password })
//.post("http://localhost:50598/api/cursos", curso)
let curso = 
{
  id : 1,
  comision : "a",
  cuatrimestre: 1,
  ciclolectivo: 2020,
  cantalumnos: 24,
  estado: 0
  
}



                axios
                    .get("http://localhost:50598/api/cursos")
                    .then((response) => {
                        console.log(JSON.stringify(response))
                    })
                    .catch((error) => {
                        console.log(JSON.stringify(error));
                    });
            },
        }
    };
</script>

<!--
<script>
import Spinner from  '@/components/Spinner.vue'; // @ is an alias to /src
import { Component, Vue } from 'vue-property-decorator';
import { Credentials } from '../../models/credentials.interface';
// this.$route.query.page

@Component({
    components: {
        Spinner,
    },
})
export default
{
    name: 'Login',
    components: {

    }
}

var isBusy = false;
var errors = '';
var credentials = {} as Credentials;

private created() {
    if (this.$route.query.new) {
        this.credentials.userName = this.$route.query.email;
    }
}

private handleSubmit() {
         this.isBusy = true;
         this.$store.dispatch('auth/authRequest', this.credentials).then((result) => {
         this.$router.push('/dashboard/home');
        })
     .catch((err) => {
        this.errors = err;
    })
    .then(() => {
        this.isBusy = false;
    });
 }
}
</script>
-->
