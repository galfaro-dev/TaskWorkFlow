import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TaskService } from '../../../data/Services/task.service';
import { TaskState } from '../../../data/Models/task.model';

@Component({
  selector: 'app-task-list',
  imports: [CommonModule],
  templateUrl: './task-list.html',
  styleUrl: './task-list.scss',
})
export class TaskList implements OnInit {
private taskService = inject(TaskService);
  tasks = this.taskService.tasks;
  TaskState = TaskState;

  ngOnInit(): void {
    this.taskService.getTasks().subscribe();
  }
  loadAll() {
    this.taskService.getTasks().subscribe();
  }

  filterBy(state: number) {
    this.taskService.getTasksByState(state).subscribe();
  }

  getStateName(state: number): string {
    return TaskState[state] || 'Unknown';
  }

  // Handlers para los botones del HTML
  onStartTask(id: string) { this.taskService.start(id).subscribe(); }
  onCompleteTask(id: string) { this.taskService.complete(id).subscribe(); }
  onBlockTask(id: string) { this.taskService.block(id).subscribe(); }
  onUnblockTask(id: string) { this.taskService.unblock(id).subscribe(); }

}
