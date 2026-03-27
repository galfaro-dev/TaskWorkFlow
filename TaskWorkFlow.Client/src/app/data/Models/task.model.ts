
export enum TaskState {
  Pending = 1,
  InProgress = 2,
  Blocked = 3,
  Completed = 4
}


export interface TaskResponseDto {
  id: string;
  title: string;
  description?: string;
  state: TaskState;
  createdAt: string; 
}


export interface CreateTaskRequestDto {
  title: string;
  description?: string;
}