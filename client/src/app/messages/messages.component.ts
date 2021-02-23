import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Message } from '../_models/message';
import { Pagination } from '../_models/pagination';
import { ConfirmService } from '../_services/confirm.service';
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
  container = 'Unread';
  pageNumber = 1;
  pageSize = 3;
  loading = false;

  constructor(private messageService: MessageService, private confirmService: ConfirmService) { }

  ngOnInit(): void {
    this.loadMessages();
  }

  loadMessages() {
    this.loading=true;
    return this.messageService.getMessages(this.pageNumber, this.pageSize, this.container)
      .subscribe (response => {
        this.messages = response.result;
        this.pagination = response.pagination;
        this.loading=false;
      })
  }

    pageChanged(event: any) {
      this.pageNumber = event.pageNumber;
      this.loadMessages();
    }

    deleteMessage(id: number) {
      this.confirmService.confirm('confirm delete message', 'this cannot be undone').subscribe( result => {
        if (result) {
          return this.messageService.deleteMessage(id).subscribe(() => {
            this.messages.splice(this.messages.findIndex(m => m.id == id),1);
          })
        }
      })
    }
}
