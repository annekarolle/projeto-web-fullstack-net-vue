import axios from 'axios';

export const customAxios = () => {
  const instance = axios.create({
    baseURL: 'https://localhost:7287/',
    headers: {
      Authorization: `Bearer ${localStorage.getItem('token')}`,
    },
  });
  return instance;
};

export const api = customAxios();

 