import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Profession } from '../_models/profession';

@Injectable({
  providedIn: 'root'
})
export class MastersService {
  baseUrl = environment.apiUrl;
  profession: Profession;
  professions: Profession[]=[];

  constructor(private http: HttpClient) { }

  getProfessions() {
    return this.http.get(this.baseUrl + 'masters/prof');
  }

  getQualifications() {
    return this.http.get(this.baseUrl + 'masters/q');
  }
  getProfessionsDto() {
    return this.http.get(this.baseUrl + 'masters/profdto');
  }
}

