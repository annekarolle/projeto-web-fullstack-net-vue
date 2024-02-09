<style src='./styles.css' ></style>

<script lang="ts">

import {CreateStudent } from "@/services/StudentsService"
import { useRoute } from 'vue-router';
import { toast } from 'vue3-toastify';
import 'vue3-toastify/dist/index.css';
export default {
  data() {
    return {
      ra: "",
      student: {
        name: '',
        email:'',
        ra: '',
        cpf: '',
      },
      route : useRoute(),
    };
  },
  methods: {
    async handleSubmit() {
       
      if(this.isValidCPF(this.student.cpf)){          
      try {        
        await CreateStudent(this.student).then(response => {
          toast.success(response)
         this.student = {
                      name: '',
                      email: '',
                      ra: '',
                      cpf: '',
                    };
        });   

      } catch (err: any) {    
        toast.error(err.response.data)         
      }
    }else{
      toast.error("CPF inválido, favor informar somente números!") 
    }
    },
    isValidCPF(cpf: string) {     
      const cpfRegex = /^\d{11}$/;   
      return cpfRegex.test(cpf);
    }, 
 
  },
  
}
</script>

<template>
  <section>
    <div class="container-header">
        <h3>
           Cadastro de aluno 
        </h3>
    </div>
 
   <div class="container-crud">
    <form @submit.prevent="handleSubmit">

      <div class="container-input">
        <label for="">Nome</label>
        <input type="text" placeholder="Informe o nome completo" v-model="student.name" required>
      </div>
      <div class="container-input">
        <label for="">E-mail</label>
        <input type="email" placeholder="Informe apenas um e-mail" v-model="student.email" required>
      </div>
      <div class="container-input">
        <label for="">RA</label>
        <input type="text" placeholder="Informe o registro acadêmico" v-model="student.ra" required 
        title="Informe RA máximo 3 caracteres"
        minlength="2"
        maxlength="3">
      </div>
      <div class="container-input">
        <label for="">CPF</label>
        <input type="text" placeholder="Informe o número do documento" v-model="student.cpf" required
        maxlength="11"
        title="Informe CPF sem pontos">
      </div>
        <div class="btn-container">
          <router-link to="/dashboard/listar" class="link">
          <button class="btn btn-cancel">Cancelar</button>
        </router-link>
          <button class="btn btn-save" type="submit">Salvar</button>
        </div>
    </form>
   </div>
  </section>
</template>
