import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Profession } from '../_models/profession';
import { UserParams } from '../_models/userParams';

@Injectable({
  providedIn: 'root'
})
export class MastersService {
  baseUrl = environment.apiUrl;
  profession: Profession;
  professions: Profession[]=[];
  userParams: UserParams;

  constructor(private http: HttpClient) { }

  getProfessions() {
    return this.http.get(this.baseUrl + 'masters/prof');
  }
  getIndustries(){
    return this.http.get(this.baseUrl+'masters/inds');
  }
  getQualifications() {
    return this.http.get(this.baseUrl + 'masters/q');
  }
  getProfessionsDto() {
    return this.http.get(this.baseUrl + 'masters/profdto');
  }

  getAssociateIdAndNamesWithoutPaging(userType: string) {
    //this.userParams.userType=userType;
    return this.http.get(this.baseUrl + 'users/associateswithoutpages?userType='+userType);
  }
}

