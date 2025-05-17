import  { useEffect, useState } from 'react';
import { getTasks, createTask, updateTask, deleteTask } from './api';
import type { Task } from './TaskModel';

function App() {
  const [tasks, setTasks] = useState<Task[]>([]);
  const [newTaskTitle, setNewTaskTitle] = useState('');

  useEffect(() => {
    fetchTasks();
  }, []);

  async function fetchTasks() {
    const res = await getTasks();
    setTasks(res.data);
  }

  async function handleAddTask() {
    if (!newTaskTitle.trim()) return;
    await createTask({ title: newTaskTitle });
    setNewTaskTitle('');
    fetchTasks();
  }

  async function handleToggle(task: Task) {
    await updateTask(task.taskId, { isCompleted: !task.isCompleted });
    fetchTasks();
  }

  async function handleDelete(id: number) {
    await deleteTask(id);
    fetchTasks();
  }

  return (
    <div style={{ padding: '2rem' }}>
      <h1>Task Manager</h1>

      <input
        type="text"
        value={newTaskTitle}
        onChange={(e) => setNewTaskTitle(e.target.value)}
        placeholder="New task"
      />
      <button onClick={handleAddTask}>Add</button>

      <ul>
        {tasks.map((task) => (
          <li key={task.taskId}>
            <input
              type="checkbox"
              checked={task.isCompleted}
              onChange={() => handleToggle(task)}
            />
            {task.title}
            <button onClick={() => handleDelete(task.taskId)}>Delete</button>
          </li>
        ))}
      </ul>
    </div>
  );
}

export default App;
