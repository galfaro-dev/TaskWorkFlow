import { inject, Injectable, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { TaskResponseDto, TaskState } from '../Models/task.model';
import { Observable, tap } from 'rxjs';
import { PagedResultDto } from '../Models/paged-result.model';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class TaskService {
  private readonly http = inject(HttpClient);
  private readonly apiUrl = 'https://localhost:7299/api/Task';


  #tasks = signal<TaskResponseDto[]>([]);
  public tasks = this.#tasks.asReadonly();
  
  private taskCreatedSource = new Subject<void>();
  taskCreated$ = this.taskCreatedSource.asObservable(); 

  notifyTaskCreated() {
    this.taskCreatedSource.next();
  }

  getTasks(): Observable<TaskResponseDto[]> {
    return this.http.get<TaskResponseDto[]>(`${this.apiUrl}/GetAll`).pipe(
      tap(response => this.#tasks.set(response))
    );
  }

  getTasksByState(state: TaskState): Observable<TaskResponseDto[]> {
    return this.http.get<TaskResponseDto[]>(`${this.apiUrl}/state/${state}`).pipe(
      tap(res => this.#tasks.set(res))
    );
  }


  createTask(dto: { title: string; description?: string }): Observable<TaskResponseDto> {
    return this.http.post<TaskResponseDto>(this.apiUrl, dto); 
  }
//Pagination
getTasksPaged(pageNumber: number, pageSize: number, state?: number): Observable<PagedResultDto<TaskResponseDto>> {
    let url = `${this.apiUrl}/paged?pageNumber=${pageNumber}&pageSize=${pageSize}`;
    if (state !== undefined) {
      url += `&state=${state}`;
    }

    return this.http.get<PagedResultDto<TaskResponseDto>>(url).pipe(
      tap(response => this.#tasks.set(response.items)) 
    );
  }

  start(id: string): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}/start`, {}).pipe(
      tap(() => this.updateLocalState(id, TaskState.InProgress))
    );
  }

  complete(id: string): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}/complete`, {}).pipe(
      tap(() => this.updateLocalState(id, TaskState.Completed))
    );
  }

  block(id: string): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}/block`, {}).pipe(
      tap(() => this.updateLocalState(id, TaskState.Blocked))
    );
  }

  unblock(id: string): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}/unblock`, {}).pipe(
      tap(() => this.updateLocalState(id, TaskState.InProgress))
    );
  }

  private updateLocalState(id: string, newState: TaskState) {
    this.#tasks.update(tasks => 
      tasks.map(t => t.id === id ? { ...t, state: newState } : t)
    );
  }
}