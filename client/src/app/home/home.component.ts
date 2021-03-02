import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  registerMode = 0;
  
  constructor() { }

  ngOnInit(): void {
  }

  registerToggle()
  {
    //this.registerMode = !this.registerMode;
    this.registerMode=1;
  }

  registerCustomer()
  {
    this.registerMode=2;
  }

  registerAssociate()
  {
    this.registerMode=3;
  }
  registerEmployee()
  {
    this.registerMode=4;
  }
  cancelRegisterMode() {
    this.registerMode = 0;
  }
}
