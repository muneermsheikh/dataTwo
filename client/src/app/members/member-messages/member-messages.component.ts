import { Component, Input, OnInit } from '@angular/core';
import { Member } from 'src/app/_models/member';
import { Message } from 'src/app/_models/message';
import { MessageService } from 'src/app/_services/message.service';

@Component({
  selector: 'app-member-messages',
  templateUrl: './member-messages.component.html',
  styleUrls: ['./member-messages.component.scss']
})

export class MemberMessagesComponent implements OnInit {
  @Input() messages: Message[];

  constructor() { }

  ngOnInit(): void {
   
  }

}
