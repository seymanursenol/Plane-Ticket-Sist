import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MessageService {
  message : string;
  messageColor: string; // Mesaj rengi için değişken
  constructor() { }

  private messageSource = new Subject<{text: string, color: string}>();
  currentMessage = this.messageSource.asObservable();

  changeMessage(message: string, color: string) {
    this.messageSource.next({ text: message, color: color });
  }

  setMessage(message: string, color: string) {
    this.message = message;
    this.messageColor = color;
    this.changeMessage(message,this.messageColor);
  }
}
