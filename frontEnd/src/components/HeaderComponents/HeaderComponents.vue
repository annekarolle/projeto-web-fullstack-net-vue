<style src='./styles.css' ></style>
  
<script lang="ts">

import {toast} from "vue3-toastify";

export default {
  data() {
    return {
     isLogin: false
    };
  },
  methods: {
    logout(){
      localStorage.removeItem('token')
      localStorage.removeItem('user')
      this.$router.push('/');
      toast.success('Usuário deslogado com sucesso!')
    }
  },
  components: {},
  mounted() {
    const token = localStorage.getItem('token')
    if (token && token.length > 0){
      this.isLogin = true
    }
    else{
      this.isLogin = false
    }
  }
}
</script>

<template>
   <header>
    <div class="container-logo">
        <img src="./../../assets/logo.png" alt="Logo" width="150">
  </div>

   <div style="display: flex; flex-direction: row; gap: 1em;align-items: center">

     <button style="color: var(--brand-color3)" v-if="isLogin" @click="logout">
       <img src="./../../assets/user-solid.png" alt="Logo" width="20" height="20">
       Logout</button>
   </div>
   </header>
</template>
  
