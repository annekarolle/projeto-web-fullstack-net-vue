<style src='./styles.css' ></style>

<script lang="ts">
 
import type StudentInterface from "@/interface/studentInterface"
import {GetAllStudent} from "@/services/StudentsService"
import { ref } from "vue";
import ModalAlerta from "@/components/ModalAlerta/ModalAlerta.vue"


export default {
  data() {
    return {
      showModal: false,
      students: ref<StudentInterface[]>([]),
      ra: '',
      name:''
    };

  },
  components: {
    ModalAlerta,
  },
  methods: {    
    async getStudent() {   
      try {
        const response = await GetAllStudent();
        this.students = response;
        
      } catch (error) {
        console.error("Erro ao obter alunos:", error);
      }
    },

    closeModal() {
      this.showModal = false;
      this.getStudent()
    },
    handleModal(raselecionado: string){
      this.ra = raselecionado
      this.showModal = true;
    },
    editar(ra: string){
      this.$router.push(`/dashboard/editar/${ra}`);
    },
    filter(){
      console.log(this.name.length < 0)
      if(this.name.trim() === ""){
        this.getStudent()
      }else{
        this.students = this.students.filter(item => item.name.toUpperCase().includes(this.name.toUpperCase()));
      }

    },
    formatCPF(cpf: string) {       
      const cleanedCPF = cpf.replace(/\D/g, '');       
      const formattedCPF = `${cleanedCPF.substring(0, 3)}.${cleanedCPF.substring(3, 6)}.${cleanedCPF.substring(6, 9)}-${cleanedCPF.substring(9)}`;

      return formattedCPF;
    },
  },
  mounted() {
    this.getStudent();
  },
  watch: {
    name(newName) {
      this.filter();
    },
  }
}
</script>

<template>
  <section>
    <div class="container-header">
        <h3>
           Consulta de alunos
        </h3>
    </div>
 <div>
    <div class="main-container-search-students">
        <form class="container-search-students" @submit.prevent="filter">
            <input type="text"  id="name" v-model="name" placeholder="Informe o nome do aluno">
            <button class="btn">Pesquisar</button>
        </form>
        <router-link to="/dashboard/cadastrar" class="link">
         <button class="btn btn-save" style="width: 150px">Cadastrar Aluno</button>
        </router-link>
    </div>
 </div>
 <table>
    <thead>
      <tr>
        <th>Registro Acadêmico</th>
        <th>Nome</th>
        <th>CPF</th>
        <th>Ações</th>
      </tr>
    </thead>
    <tbody>      
      <tr v-for="(student, index) in students" :key="index">
        <td>{{ student.ra }}</td>
        <td>{{ student.name }}</td>
        <td>{{ formatCPF(student.cpf) }}</td>
        <td style="display: flex; flex-direction: row; gap: 1em;justify-content: center">
          <v-btn class="btn-action" @click="editar(student.ra)">Editar</v-btn>
          <v-btn @click="handleModal(student.ra)" class="btn-action">Excluir</v-btn>
        </td>
      </tr>    
    </tbody>
   <p v-if="students.length <= 0">Nenhum registro de alunos</p>
  </table>

  </section>
  <ModalAlerta :ra=ra  v-if="showModal" @closeModal="closeModal" />
</template>
