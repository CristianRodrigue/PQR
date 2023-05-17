import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CaseTypeService } from 'src/app/services/case-type.service';
import { CountryService } from 'src/app/services/country.service';

@Component({
  selector: 'app-form-pqr',
  templateUrl: './form-pqr.component.html',
  styleUrls: ['./form-pqr.component.scss']
})
export class FormPqrComponent implements OnInit {

  formPQR: FormGroup;

  listaPais: any[] = [];
  listaTipoCaso: any[] = [];

  constructor(private fb: FormBuilder, public router: Router, private country: CountryService, private caseType: CaseTypeService) {
    this.formPQR = this.fb.group({
      pais: ['', [Validators.required]],
      tipoCaso: ['', [Validators.required]],
      razonSocial: ['', [Validators.required, Validators.minLength(3)]],
      nit: ['', [Validators.required, Validators.minLength(3)]] 
      // password: ['', [Validators.required, Validators.minLength(8), Validators.pattern("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{8,}$")]]
    },
      {
        // validators: [this.EmailUserAppValidator('correo'), this.UserAppValidator('usuarioapp')]
      });

    this.cargarData();
   }

  ngOnInit(): void {
    this.consultarPaises();
    this.consultarTipoCaso();
  }

  cargarData() {
    this.formPQR.setValue({
      pais: '',
      tipoCaso: '',
      razonSocial: '',
      nit: ''
      
    });
  }

  // #region
  get razonSocialNoValida() {
    return this.formPQR.get('razonSocial')!.invalid && this.formPQR.get('razonSocial')!.touched;
  }

  get nitNoValido() {
    return this.formPQR.get('nit')!.invalid && this.formPQR.get('nit')!.touched;
  }

  get paisNoValido() {
    return this.formPQR.get('pais')!.invalid && this.formPQR.get('pais')!.touched;
  }

  get tipoCasoNoValido() {
    return this.formPQR.get('tipoCaso')!.invalid && this.formPQR.get('tipoCaso')!.touched;
  }
  // #endregion

  consultarPaises() {
    this.country.getAll().subscribe((response: any) => {
      console.log('paises: ', response.data);
      this.listaPais = response.data;
      console.log('lista pais', this.listaPais);
    });
  }

  consultarTipoCaso() {
    this.caseType.getAll().subscribe((response: any) => {
      console.log('tipos caso: ', response.data);
      this.listaTipoCaso = response.data;
    });
  }



  guardar() {
    console.log('Guardar PQR');
  }

}
