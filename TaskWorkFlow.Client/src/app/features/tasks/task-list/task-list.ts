import { Component, inject, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TaskService } from '../../../data/Services/task.service';
import { TaskState } from '../../../data/Models/task.model';

@Component({
  selector: 'app-task-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './task-list.html',
  styleUrl: './task-list.scss',
})
export class TaskList implements OnInit {
  private taskService = inject(TaskService);
  currentFilter = signal<number | undefined>(undefined);

  tasks = this.taskService.tasks;
  TaskState = TaskState;

  currentPage = signal(1);
  totalPages = signal(0);
  totalCount = signal(0);
  pageSize = 5;

  ngOnInit(): void {
    this.loadPage(1);
    this.taskService.taskCreated$.subscribe(() => {
      this.loadPage(1);
    });
  }

  loadPage(page: number): void {
    if (page < 1 || (this.totalPages() > 0 && page > this.totalPages())) return;

    this.taskService.getTasksPaged(page, this.pageSize, this.currentFilter()).subscribe(res => {
      this.currentPage.set(res.pageNumber);
      this.totalPages.set(res.totalPages);
      this.totalCount.set(res.totalCount);
    });
  }

  nextPage() {
    this.loadPage(this.currentPage() + 1);
  }

  prevPage() {
    this.loadPage(this.currentPage() - 1);
  }

  filterBy(state: number) {
    this.currentFilter.set(state);
    this.loadPage(1);
  }

  loadAll() {
    this.currentFilter.set(undefined);
    this.loadPage(1);
  }

  getStateName(state: number): string {
    return TaskState[state] || 'Unknown';
  }

  emptyRows(): unknown[] {
    const diff = 5 - this.tasks().length;
    return diff > 0 ? new Array(diff).fill(null) : [];
  }

  onStartTask(id: string) {
  this.taskService.start(id).subscribe(() => {
    this.loadPage(this.currentPage());
  });
}

onCompleteTask(id: string) {
  this.taskService.complete(id).subscribe(() => {
    this.loadPage(this.currentPage());
  });
}

onBlockTask(id: string) {
  this.taskService.block(id).subscribe(() => {
    this.loadPage(this.currentPage());
  });
}

onUnblockTask(id: string) {
  this.taskService.unblock(id).subscribe(() => {
    this.loadPage(this.currentPage());
  });
}
}