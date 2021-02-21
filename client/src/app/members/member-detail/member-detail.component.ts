import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { NgxGalleryAnimation, NgxGalleryImage, NgxGalleryOptions } from '@kolkov/ngx-gallery';
import { TabDirective, TabsetComponent } from 'ngx-bootstrap/tabs';
import { ToastrService } from 'ngx-toastr';
import { Member } from 'src/app/_models/member';
import { Message } from 'src/app/_models/message';
import { MembersService } from 'src/app/_services/members.service';
import { MessageService } from 'src/app/_services/message.service';

@Component({
  selector: 'app-member-detail',
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.scss']
})
export class MemberDetailComponent implements OnInit {
  @ViewChild('memberTabs', {static: true}) memberTabs: TabsetComponent;
  member: Member;
  galleryOptions: NgxGalleryOptions[];
  galleryImages: NgxGalleryImage[];
  activeTab: TabDirective;
  messages : Message[] = [];
  
  constructor(private memberService: MembersService, private route: ActivatedRoute,
    private toast: ToastrService, private messageService: MessageService) { }

  ngOnInit(): void {
    this.loadMember();
    
    /* this.route.data.subscribe(data => {    //remove comments if route resolver used
      this.member = data.member;
    })
    */
    
    //commetn out flg if route resolver used.
    this.route.queryParams.subscribe(params => {
      params.tab ? this.selectTab(params.tab) : this.selectTab(0);    //activate the tab contained in query params
    })

    this.galleryOptions = [{
      width: '500px',
      height: '500px',
      imagePercent: 80,
      thumbnailsColumns: 4,
      imageAnimation: NgxGalleryAnimation.Slide,
      preview: false
    }]
    //this.galleryImages = this.getImages();    //remove comments if route resolver used
  }

  loadMember() {
    this.memberService.getMember(this.route.snapshot.paramMap.get('username')).subscribe( member => {
      this.member = member;
      this.galleryImages = this.getImages();
    })
  }
 
  loadMessages() {
    if(this.member !== undefined){
      this.messageService.getMessageThread(this.member.username).subscribe(response =>
        {
          this.messages = response;
        })
    }
  }

  onTabActivated(data: TabDirective) {
    this.activeTab = data;
    if (this.activeTab.heading === 'Messages' && this.messages.length === 0) 
    {
      this.loadMessages();
    }
  }

  selectTab(tabId: number)
  {
    this.memberTabs.tabs[tabId].active = true;
  }

  getImages(): NgxGalleryImage[] {
    const imageUrls = [];
    for (const photo of this.member.photos) {
      imageUrls.push({
        small: photo?.url,
        medium: photo?.url,
        big: photo?.url
      })
    }
    return imageUrls;
  }

  
}
