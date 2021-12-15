import Vue from 'vue';
import Vuetify from 'vuetify';
import 'vuetify/dist/vuetify.min.css';
import es from 'vuetify/lib/locale/es';
import draggable from 'vuedraggable'
Vue.use(Vuetify);
Vue.component('draggable', draggable);
export default new Vuetify({
    lang: {
      locales: { es },
      current: 'es',
    },
		components: {
			draggable
		},
		icons: {
			iconfont: 'fa4', // default - only for display purposes
		},
});
