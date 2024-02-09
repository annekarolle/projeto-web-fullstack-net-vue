 
 
import { api, customAxios } from '@/api/api';
import type StudentInterface from '@/interface/studentInterface';
import type StudentUpdateInterface from '@/interface/studentUpdateInterface';
 

export const GetAllStudent = async (): Promise<StudentInterface[]> => {
    try {
        const response = await customAxios().get('student/getAllStudent');     
      return response.data as StudentInterface[];
    } catch (error) {      
      console.error('Erro ao obter alunos:', error);
      throw error;
    }
  };
  export const CreateStudent = async (body: StudentInterface) => {
    try {
      const response = await customAxios().post('student/saveStudent', body);
  
      return response.data;
    } catch (error) {
      console.error('Erro ao criar aluno:', error);
      throw error;
    }
  };

export const EditStudent = async (body : StudentUpdateInterface) => {
    return await customAxios().put(`student/editStudent`, body);   
}

export const DeleteStudent = async (ra: string) => {
    return await customAxios().delete(`student/deleteStudent/${ra}`, 
    ); 
}

export const GetStudentBYRA = async (ra: string): Promise<any> => {
    console.log(ra)
    return await customAxios().get(`student/getStudentByRA/${ra}` ); 
}
