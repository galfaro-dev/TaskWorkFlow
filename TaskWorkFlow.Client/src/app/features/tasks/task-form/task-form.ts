import { Component, inject, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { TaskService } from '../../../data/Services/task.service';

@Component({
  selector: 'app-task-form',
  imports: [FormsModule],
  templateUrl: './task-form.html',
  styleUrl: './task-form.scss',
})
export class TaskForm {

  private taskService = inject(TaskService);
  
  // Signals para manejar el formulario de forma limpia
  title = signal('');
  description = signal('');

  showSuccess = signal(false);

  // features/tasks/task-form/task-form.component.ts

onSubmit() {
  const nuevaTarea = { title: this.title(), description: this.description() };


  // ✅ BIEN: El .subscribe() dispara la petición y el código del tap en el service
  this.taskService.createTask(nuevaTarea).subscribe({
    next: (tareaCreada) => {
      this.title.set('');       // Limpiar campos
      this.description.set('');
      this.showSuccess.set(true); // Mostrar tu feedback visual
    },
    error: (err) => {
      console.error('Error al crear la tarea:', err);
    }
  });
}


  
}
