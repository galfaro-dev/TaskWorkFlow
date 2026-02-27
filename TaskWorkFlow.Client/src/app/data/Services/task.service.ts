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

  // Solo una declaración del almacén de datos (Signal)
  #tasks = signal<TaskResponseDto[]>([]);
  public tasks = this.#tasks.asReadonly();
  
  //Para notificar la actualizacion en la tabla de tasklist
  private taskCreatedSource = new Subject<void>();
  taskCreated$ = this.taskCreatedSource.asObservable(); // El "aviso" que escuchará la lista

  notifyTaskCreated() {
    this.taskCreatedSource.next();
  }

  // --- LECTURA Y FILTROS ---

  getTasks(): Observable<TaskResponseDto[]> {
    return this.http.get<TaskResponseDto[]>(`${this.apiUrl}/GetAll`).pipe(
      tap(response => this.#tasks.set(response))
    );
  }

  // Usando el endpoint GET /api/Task/state/{state} de tu Swagger
  getTasksByState(state: TaskState): Observable<TaskResponseDto[]> {
    return this.http.get<TaskResponseDto[]>(`${this.apiUrl}/state/${state}`).pipe(
      tap(res => this.#tasks.set(res))
    );
  }

  // --- CREACIÓN ---

  createTask(dto: { title: string; description?: string }): Observable<TaskResponseDto> {
    return this.http.post<TaskResponseDto>(this.apiUrl, dto); 
    // Quitamos el .pipe(tap...) que hacía el unshift manual. 
    // Ahora el componente llamará a notifyTaskCreated() y loadPage(1) hará el refresco limpio.
  }
//Pagination
getTasksPaged(pageNumber: number, pageSize: number, state?: number): Observable<PagedResultDto<TaskResponseDto>> {
    let url = `${this.apiUrl}/paged?pageNumber=${pageNumber}&pageSize=${pageSize}`;
    if (state !== undefined) {
      url += `&state=${state}`;
    }

    return this.http.get<PagedResultDto<TaskResponseDto>>(url).pipe(
      tap(response => this.#tasks.set(response.items)) // Reemplazo total, mantiene la tabla en 5
    );
  }

  // --- CAMBIOS DE ESTADO (REGLAS DE DOMINIO) ---

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

  // Método centralizado para actualizar la UI instantáneamente
  private updateLocalState(id: string, newState: TaskState) {
    this.#tasks.update(tasks => 
      tasks.map(t => t.id === id ? { ...t, state: newState } : t)
    );
  }
}