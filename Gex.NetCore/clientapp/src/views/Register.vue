<template>
    <v-form ref="form" v-model="valid">
		<v-text-field label="Nombre"></v-text-field>
		<p>asdsad</p>
	</v-form>
</template>

<script>

	export default {
		data: function () {

		}
		
	}

	/*
import Spinner from '@/components/Spinner.vue'; // @ is an alias to /src
import { Component, Vue } from 'vue-property-decorator';
import { UserRegistration } from '../../models/user.registration.interface';
import { accountService } from '../../services/account.service';

@Component({
  components: {
	Spinner,
  },
})
export default class RegistrationForm extends Vue {

private isBusy: boolean = false;
private errors: string = '';
private user = {} as UserRegistration;

private handleSubmit() {
  this.isBusy = true;
  accountService.register(this.user).finally(() => this.isBusy = false)
	.subscribe((result: any) => {
	  this.$router.push({name: 'loginForm', query: { new: 'true', firstName: this.user.firstName, email: this.user.email }});
	},
	(errors: any) =>  this.errors = errors);
}
}*/
</script>
