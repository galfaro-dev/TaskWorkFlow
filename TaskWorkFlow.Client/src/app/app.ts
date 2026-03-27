import { Component, inject, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { TaskService } from './data/Services/task.service';
import { TaskList } from './features/tasks/task-list/task-list';
import { TaskForm } from './features/tasks/task-form/task-form';


@Component({
  selector: 'app-root',
  standalone: true, 
  imports: [RouterOutlet, TaskList,TaskForm],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})
export class App implements OnInit {

  private taskService = inject(TaskService);

  ngOnInit(): void {

    
    this.taskService.getTasks().subscribe({
      next: (tasks) => {
      },
      error: (err) => {
        console.error('❌ Error de conexión:', err);
      }
    });
  }
}