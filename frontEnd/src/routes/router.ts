 import { createRouter, createWebHistory } from 'vue-router';

import Dashboard from '../pages/dash-board/dash-board.vue';
import StudentsCrudVue from '@/components/StudentsCrud/StudentsCrud.vue';
import StudentsListVue from '@/components/StudentsList/StudentsList.vue';
import EditStudentComponentsVue from '@/components/EditStudentComponents/EditStudentComponents.vue';
 import homePage from "@/pages/home-page/homePage.vue";
 import Welcome from '@/components/Welcome/Welcome.vue';


const routes = [
  {
    path: '/',
    component:  homePage,
  },
  {
    path: '/dashboard',
    component: Dashboard,
    children: [
      {
        path: 'cadastrar',  
        component: StudentsCrudVue,  
      },   
      {
        path: 'editar/:ra',  
        component: EditStudentComponentsVue,  
      },
      {
        path: 'listar',  
        component: StudentsListVue,  
      },

    ],
    meta: {
      requiresAuth: true,
    },
  },
];

const router = createRouter({
  history: createWebHistory(""), 
  routes,
});

router.beforeEach((to, from, next) => {
    if (to.path !== '/' && !isAuthenticated()) {
      next({ path: '/' });
    } else {
      next();
    }
  });

  function isAuthenticated() {
    const token = localStorage.getItem('token')
    if(token && token.length > 0){
      return true
    }
    return false;
  }


export default router

