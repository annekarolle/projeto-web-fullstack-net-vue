<style lang="css" src="./styles.css"></style>
  
<script>
import { Login } from '@/services/AuthService';
import {toast} from "vue3-toastify";
import 'vue3-toastify/dist/index.css';
export default {
  components: {

  },
  data() {
    return {
      email: '',
      password: '',
    };
  }, 
  methods: {
    login() {   
      const data = {
        email: this.email,
        password: this.password
      }

      Login(data).then((response) => {
        console.log(response)
        toast.success('Login Efetuado com Sucesso')
        this.$router.push('/dashboard/listar');
      }).catch((error) => {
        toast.error('Oops! Usuário ou senha incorretos!')
        console.error('Erro durante o login:', error);
      });
      
    },
  },
};
</script>

<template>

<div class="container-home-page">
  <div class="container-imagem">
    <img src="./../../assets/home.png">
  </div>
  <div class="container-form">
    <div class="container-logo-home">
      <img src="./../../assets/logo.png" alt="" width="200">
    </div>
    <h2 style="color: var(--brand-color3);">Login</h2>
    <form @submit.prevent="login">   
        <v-text-field
        label="Email"
        type="text"
        id="email"
        v-model="email" 
        required
        :rules="rules"
        hide-details="auto"
      ></v-text-field>
      <v-text-field
      label="Password"
      type="password"
      id="pass  word"
      v-model="password" 
      required
      :rules="rules"
      hide-details="auto"
    ></v-text-field>     

    
      <button class="btn btn-save" type="submit">Login</button>
    </form>
  </div>
</div>
</template>
  
