import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Company } from '../Models/Company';
import { CompanyService } from '../services/Company.service';


@Component({
  selector: 'app-Company',
  templateUrl: './Company.component.html',
  styleUrls: ['./Company.component.css']
})
export class CompanyComponent implements OnInit {

  company: Company = new Company();

  constructor(private companyServices: CompanyService) { }

  ngOnInit() {
    this.companyServices.getCompanies().subscribe(resp => console.log(resp));
  }
  //setformulario
  companyForm = new  FormGroup({
    name: new FormControl('' , Validators.required),
    code: new FormControl('' , Validators.required),
    adress: new FormControl('' , Validators.required),
    phone: new FormControl('' , Validators.required)
  })

  CreateCompany(){

    this.company.companyName = this.companyForm.controls.name.value;
    this.company.companyCode =this.companyForm.controls.code.value;
    this.company.companyAdrees = this.companyForm.controls.adress.value;
    this.company.companyPhone = this.companyForm.controls.phone.value;

    this.companyServices.postCompany(this.company).subscribe(resp => console.log(resp));

    this.companyForm.reset();
  }

}
