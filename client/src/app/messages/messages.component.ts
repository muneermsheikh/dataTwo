import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Message } from '../_models/message';
import { Pagination } from '../_models/pagination';
import { MessageService } from '../_services/message.service';

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.scss']
})
export class MessagesComponent implements OnInit {
  baseUrl = environment.apiUrl;
  pagination: Pagination;
  messages: Message[]=[];
  container = 'Inbox';
  pageNumber = 1;
  pageSize = 3;

  constructor(private messageService: MessageService) { }

  ngOnInit(): void {
    this.loadMessages();
  }

  loadMessages() {
    return this.messageService.getMessages(this.pageNumber, this.pageSize, this.container)
      .subscribe (response => {
        this.messages = response.result;
        this.pagination = response.pagination;;
      })
  }

  pageChanged(event: any) {
    this.pageNumber = event.pageNumber;
    this.loadMessages();
  }
}
