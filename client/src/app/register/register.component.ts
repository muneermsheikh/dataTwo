import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { AbstractControl, FormArray, FormBuilder, FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Console } from 'console';
import { TabDirective, TabsetComponent } from 'ngx-bootstrap/tabs';
import { ToastrService } from 'ngx-toastr';
import { Profession } from '../_models/profession';
import { ProfessionDto } from '../_models/professionDto';
import { Qualification } from '../_models/userQualification';
import { AccountService } from '../_services/account.service';
import { MastersService } from '../_services/masters.service';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  
  @ViewChild('memberTabs', {static: true}) memberTabs: TabsetComponent;
  @Output() cancelRegister = new EventEmitter();
  activeTab: TabDirective;

  registerForm: FormGroup;
  maxDate: Date;
  validationErrors: string[]=[];
  profession: Profession;
  professions: Profession[]=[];
  qualifications: Qualification[];
  professionsDto: ProfessionDto[]=[];  

  constructor(private accountService: AccountService, 
      private mastersService: MastersService, 
      private toastService: ToastrService,
      private fb: FormBuilder, private router: Router) { }

  ngOnInit(): void {
    this.initializeForm();
    this.maxDate = new Date;
    this.maxDate.setFullYear(this.maxDate.getFullYear()-18);  //will not set date before 18 years
  }

  initializeForm() {
    //professions
    this.mastersService.getProfessions().subscribe((response: Profession[]) => {
      this.professions = response;
    })
    //professionDto
    this.mastersService.getProfessionsDto().subscribe((response: ProfessionDto[]) => {
      this.professionsDto = response;
    })
    //qualifications
    this.mastersService.getQualifications().subscribe((response: Qualification[]) => {
      this.qualifications = response;
    })

    this.registerForm = this.fb.group({
      userPhones: this.fb.array([]),
      userAddresses: this.fb.array([]),
      userQualifications: this.fb.array([]),
      userPassports: this.fb.array([]),
      userExperiences: this.fb.array([]),
      userProfessions: this.fb.array([]),
      aadharNo: ['123456789012', [Validators.minLength(12), Validators.maxLength(12)]],
      gender: ['male'],
      firstName: ['Subhash', [Validators.required, Validators.minLength(5), Validators.maxLength(20)]],
      secondName: 'Rajaram',
      familyName: 'Kukarni',
      username: ['subhash', [Validators.required, Validators.minLength(4), Validators.maxLength(15)]],
      knownAs: ['subhash', [Validators.required, Validators.minLength(4), Validators.maxLength(10)]],
      dateOfBirth: ['11 April 1985', Validators.required],
      city: ['Mumbai', Validators.required],
      country: ['India', Validators.required],
      userRole: 'candidate',
      password: ['Pa$$w0rd', [Validators.required, Validators.minLength(8), Validators.maxLength(15)]],
      confirmPassword: ['Pa$$w0rd',[Validators.required, this.matchValues('password')]]
    })
    this.registerForm.controls.password.valueChanges.subscribe(() => {
      this.registerForm.controls.confirmPassword.updateValueAndValidity();
    })
  }

//userProfessions
  get userProfessions(): FormArray {
    return this.registerForm.get("userProfessions") as FormArray
  }

  newUserProfession(): FormGroup {
    return this.fb.group({
      //id: '',
      professionId: '',
      isMain: false,
    })
  }

  addUserProfession() {
    this.userProfessions.push(this.newUserProfession());
  }

  removeUserProfession(i:number) {
    this.userProfessions.removeAt(i);
  }

  //passports
  get userPassports(): FormArray {
    return this.registerForm.get("userPassports") as FormArray
  }

  newUserPassport(): FormGroup {
    return this.fb.group({
      passportNo: 'L289899',
      issuedAt: 'Mumbai',
      issuedOn: '12 October 2010',
      validity: '11 October 2025',
      isValid: true,
      ecnr: false
    })
  }

  addUserPassport() {
    this.userPassports.push(this.newUserPassport());
  }

  removeUserPassport(i:number) {
    this.userPassports.removeAt(i);
  }


  // userPhones
  get userPhones(): FormArray {
    return this.registerForm.get("userPhones") as FormArray
  }

  newUserPhone(): FormGroup {
    return this.fb.group({
      //id: '',
      phoneNo: '9867638065',
      phoneNumber: '',    //from UserIdentity
      email: '',        // from Useridentity
      isMain: false,
    })
  }

  addUserPhone() {
    this.userPhones.push(this.newUserPhone());
  }

  removeUserPhone(i:number) {
    this.userPhones.removeAt(i);
  }

  //addresses
    get userAddresses(): FormArray {
      return this.registerForm.get("userAddresses") as FormArray
    }
    newUserAddress(): FormGroup {
      return this.fb.group({
        add: '12/56 bdd chawls',
        streetAdd: '',
        location: 'worli',
        city: 'mumbai',
        pin: '400018',
        state: 'Maharashtra',
        country: ['India', Validators.required],
        isMain: [false],
      })
    }
    addUserAddress() {
      this.userAddresses.push(this.newUserAddress());
    }
    removeUserAddress(i:number) {
      this.userAddresses.removeAt(i);
    }

//qualifications
  get userQualifications(): FormArray {
    return this.registerForm.get("userQualifications") as FormArray
  }
  newUserQualification(): FormGroup {
    return this.fb.group({
      //id: '',
      qualificationId: 2,
      isMain: false,
    })
  }
  addUserQualification() {
    this.userQualifications.push(this.newUserQualification());
  }
  removeUserQualification(i:number) {
    this.userQualifications.removeAt(i);
  }
  onQualificationChange() {
    console.log(this.userQualifications);
  }

  //exp
  get userExperiences(): FormArray {
    return this.registerForm.get("userExperiences") as FormArray
  }
  newUserExp(): FormGroup {
    return this.fb.group({
      srNo: [1, [Validators.min(1), Validators.max(10), Validators.required]],
      professionId: [4, Validators.required],
      employer: ['Reliance infocom',Validators.required],
      salaryCurrency: ['INR',[Validators.required, Validators.minLength(2), Validators.maxLength(3)]],
      monthlySalaryDrawn: [25000,[Validators.required, Validators.min(10), Validators.max(10000000)]],
      workedFrom: ['11 October 2010', Validators.required],
      workedUpto: ['25 December 2015', Validators.required],
    })
  }

  addUserExp() {
    this.userExperiences.push(this.newUserExp());
  }

  removeUserExp(i:number) {
    this.userExperiences.removeAt(i);
  }

  onWorkProfChange() {

  }

  //others
  matchValues(matchTo: string): ValidatorFn {
    return (control: AbstractControl) => {
      return control?.value === control?.parent?.controls[matchTo].value 
        ? null : {isMatching: true}
    }
  }

  EitherValueExists(matchTo: string): ValidatorFn {
    return (control: AbstractControl) => {
      return control?.value === null && control?.parent?.controls[matchTo].value === null
        ? null : {eitherExists: true}
    }
  }
  
  register() {
    console.log('calling accountService.Register()');
    this.accountService.register(this.registerForm.value).subscribe(response => {
      this.router.navigateByUrl('/members');
      this.cancel();
    }, error => {
      this.validationErrors = error;
    })
  }

  cancel() {
    this.cancelRegister.emit(false);
  }

  getProfessions(){
  }

  onProfChange(){
    console.log(this.registerForm.get('userQualifications'));
  }

  onTabActivated(data: TabDirective) {
    this.activeTab = data;
  }

  selectTab(tabId: number)
  {
    this.memberTabs.tabs[tabId].active = true;
  }
}
