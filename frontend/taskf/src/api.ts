import axios from 'axios';
import type { Task } from './TaskModel';

const API_URL = 'https://localhost:7292/api/Task'; 

export function getTasks() {
    return axios.get<Task[]>(API_URL);
  }
  
  export function getTask(id: number) {
    return axios.get<Task>(`${API_URL}/${id}`);
  }
  
  export function createTask(task: { title: string }) {
    return axios.post(API_URL, task);
  }
  
  export function updateTask(id: number, update: { isCompleted: boolean }) {
    return axios.put(`${API_URL}/${id}`, update);
  }
  
  export function deleteTask(id: number) {
    return axios.delete(`${API_URL}/${id}`);
  }