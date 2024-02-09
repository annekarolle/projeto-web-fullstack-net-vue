import './assets/main.css';
import { createApp } from 'vue';
import App from './App.vue';
import router from './routes/router'; 
import 'vuetify/styles';
import { createVuetify } from 'vuetify';
import Vue3Toastify, { type ToastContainerOptions } from 'vue3-toastify';
import 'vuetify/dist/vuetify.css';
import * as components from 'vuetify/components'
import * as directives from 'vuetify/directives'


const vuetify = createVuetify({
    components,
    directives,
});
const app = createApp(App); 

app
  .use(router)
  .use(vuetify)
  .mount('#app');

app.use(Vue3Toastify, {
    autoClose: 5000,
} as ToastContainerOptions);


export default vuetify;
