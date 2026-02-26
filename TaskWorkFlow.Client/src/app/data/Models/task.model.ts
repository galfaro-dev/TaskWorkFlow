// Basado exactamente en tu TaskWorkFlow.Domain.Enums.TaskState
export enum TaskState {
  Pending = 1,
  InProgress = 2,
  Blocked = 3,
  Completed = 4
}

// Interfaz que hace espejo a tu TaskResponseDto de C#
export interface TaskResponseDto {
  id: string;
  title: string;
  description?: string;
  state: TaskState;
  createdAt: string; // ISO String desde .NET
}

// Interfaz para las peticiones de creación (CreateTaskRequestDto)
export interface CreateTaskRequestDto {
  title: string;
  description?: string;
}