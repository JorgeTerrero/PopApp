import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Company } from '../Models/Company';

@Injectable({
  providedIn: 'root'
})
export class CompanyService {

constructor(private http:HttpClient , @Inject('BASE_URL') private base_url: string) { }

getCompanies(){
  return this.http.get<Company[]>(this.base_url + 'api/company')
}

postCompany(company:Company){
  return this.http.post(this.base_url +'api/company' , company);
}

}
