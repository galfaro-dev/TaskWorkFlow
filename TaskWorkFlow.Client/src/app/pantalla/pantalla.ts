import { CommonModule } from '@angular/common';
import { Component, signal } from '@angular/core';
import { CreateTaskRequestDto } from '../data/Models/task.model';
import { Validators, FormBuilder, FormGroup } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-pantalla',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './pantalla.html',
  styleUrl: './pantalla.scss',
})
export class Pantalla {
  isSubmitted = false;
  taskForm: FormGroup;
  showSuccess = signal(false);
  constructor(private fb: FormBuilder) {
    this.taskForm = this.fb.group({
      title: ['', Validators.required],
      description: ['', Validators.required],
    });
  }

  submit() {
    this.isSubmitted = true;


    if (this.taskForm.invalid) {
      return;
    }

    console.log(this.taskForm.value);
    this.showSuccess.set(true);
    this.taskForm.reset();
    this.isSubmitted = false;
    setTimeout(() => {
        this.showSuccess.set(false);
      }, 1500);
  }
}
