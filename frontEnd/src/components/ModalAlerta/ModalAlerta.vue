<template>
  <v-dialog :value="ra" @input="closeModal" max-width="600px"
            activator="parent">

    <v-card>
     <div style="display: flex; justify-content: center">
       <v-card-title>Atenção</v-card-title>
     </div>
      <v-card-text style="display: flex; justify-content: center">
        <p>Tem certeza que deseja Deletar o aluno(a) {{student}}?</p>
      </v-card-text>
      <v-card-actions style="display: flex; justify-content: center">
        <v-btn   variant="text" @click="this.closeModal()" style="background-color: var(--grey)">
          Fechar
        </v-btn>
        <v-btn   variant="text" @click="deletarAluno" style="background-color: var(--danger)">
          Deletar
        </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
import {DeleteStudent,GetStudentBYRA} from "./../../services/StudentsService";
import { toast } from 'vue3-toastify';
export default {
  props: {
   ra: String ,
  },
  methods: {
    closeModal() {
      this.$emit('closeModal');
    },
    async deletarAluno(){
        try {
          const response = await DeleteStudent(this.ra);
          toast.success(response.data)
           this.closeModal()

        } catch (error) {
          console.error("Erro ao remover alunos:", error);
        }
      },
    async getStudent(){
      try {
        const response = await GetStudentBYRA(this.ra);
         this.student = response.data.name


      } catch (error) {
        console.error("Erro ao obter alunos:", error);
      }
    },

  },

  mounted(){   
    this.getStudent()
  },
  data() {
    return {
      student: '',
    }
  },
};
</script>
