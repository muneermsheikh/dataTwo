import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { TabDirective, TabsetComponent } from 'ngx-bootstrap/tabs';
import { AgencySpecialty } from '../_models/agencySpecialty';
import { Industry } from '../_models/industry';
import { Profession } from '../_models/profession';
import { AccountService } from '../_services/account.service';
import { MastersService } from '../_services/masters.service';

@Component({
  selector: 'app-register-customer',
  templateUrl: './register-customer.component.html',
  styleUrls: ['./register-customer.component.scss']
})
export class RegisterCustomerComponent implements OnInit {
  @ViewChild('memberTabs', {static: true}) memberTabs: TabsetComponent;
  @Input() customerType: string;
  @Output() cancelRegister = new EventEmitter();
  activeTab: TabDirective;

  validationErrors: string[]=[];
  industries: Industry[];
  agentSpecialties: AgencySpecialty[]=[];
  professions: Profession[];
  registerForm: FormGroup;
  

  constructor(private route: ActivatedRoute, private mastersService: MastersService,
      private fb: FormBuilder, private router: Router, private accountService: AccountService) { }

  ngOnInit(): void {
    //this.customerType = this.route.snapshot.paramMap.get('usertype');
    this.initializeForm();
  }

  initializeForm() {
    //professions
    this.mastersService.getProfessions().subscribe((response: Profession[]) => {
      this.professions = response;
    })
    //profession
    this.mastersService.getProfessions().subscribe((response: Profession[]) => {
      this.professions = response;
    })
    
    this.registerForm = this.fb.group({
      customerIndustries: this.fb.array([]),
      agencySpecialties: this.fb.array([]),
      customerOfficials: this.fb.array([]),
      customerName: ['Tata Telecom Services', [Validators.required, Validators.minLength(5), Validators.maxLength(25)]],
      knownAs: ['Tata Telecom', [Validators.required, Validators.minLength(4), Validators.maxLength(15)]],
      customerType: ['customer', Validators.required],
      add: '234, A.B. Vardhan Road',
      add2: 'Jahangiraad',
      city: ['Delhi', Validators.required],
      pin: '110020',
      state: 'Delhi',
      country: 'India',
      introducedBy: '',
      isActive: true
    })
    this.registerForm.controls.password.valueChanges.subscribe(() => {
      this.registerForm.controls.confirmPassword.updateValueAndValidity();
    })
  }

  //customerOfficials
    get customerOfficials(): FormArray {
      return this.registerForm.get("customerOfficials") as FormArray
    }
    newCustomerOfficial(): FormGroup {
      return this.fb.group({
        //id: '',
      gender: 'M',
      title: '',
      officialName: ['', Validators.required],
      designation: ['', Validators.required],
      mobile: '',
      phoneNo: '',
      email: ['', Validators.required],
      isValid: true
      })
    }

    addCustomerOfficial() {
      this.customerOfficials.push(this.newCustomerOfficial());
    }

    removeCustomerOfficial(i:number) {
      this.customerOfficials.removeAt(i);
    }

  //customer industries
    get customerIndustries(): FormArray {
      return this.registerForm.get("customerIndustries") as FormArray
    }
    newCustomerIndustry(): FormGroup {
      return this.fb.group({
        //customerId: 0,
        industryId: [0, Validators.required]
      })
    }

    addCustomerIndustry() {
      this.customerIndustries.push(this.newCustomerIndustry());
    }

    removeCustomerIndustry(i:number) {
      this.customerIndustries.removeAt(i);
    }
  //agency specialties
    get agencySpecialties(): FormArray {
      return this.registerForm.get("agencySpecialties") as FormArray
    }
    newAgencySpecialty(): FormGroup {
      return this.fb.group({
        professionId: [0, Validators.required],
        industryId: [0, Validators.required]
      })
    }

    addAgencySpecialty() {
      this.agencySpecialties.push(this.newAgencySpecialty());
    }

    removeAgencySpecialty(i:number) {
      this.agencySpecialties.removeAt(i);
    }

    register() {
      this.accountService.registerCustomer(this.registerForm.value).subscribe(response => {
        this.router.navigateByUrl('/members');
        this.cancel();
      }, error => {
        this.validationErrors = error;
      })
    }
  
    cancel() {
      this.cancelRegister.emit(false);
    }

    onTabActivated(data: TabDirective) {
      this.activeTab = data;
    }
  
    selectTab(tabId: number)
    {
      this.memberTabs.tabs[tabId].active = true;
    }
}

