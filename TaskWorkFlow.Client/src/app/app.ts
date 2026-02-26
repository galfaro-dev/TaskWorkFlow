import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { TaskService } from './data/Services/task.service';// Asegúrate de que la ruta sea correcta
import { TaskList } from './features/tasks/task-list/task-list';
import { TaskForm } from './features/tasks/task-form/task-form';

@Component({
  selector: 'app-root',
  standalone: true, // Aseguramos que sea standalone
  imports: [RouterOutlet, TaskList,TaskForm],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App implements OnInit {
  // Inyectamos el servicio
  private taskService = inject(TaskService);

  ngOnInit(): void {
    console.log('--- Prueba de Conexión ---');
    
    this.taskService.getTasks().subscribe({
      next: (tasks) => {
        console.log('✅ ¡Conectado al Backend!');
        console.table(tasks); // Verás tus tareas en la consola
      },
      error: (err) => {
        console.error('❌ Error de conexión:', err);
      }
    });
  }
}