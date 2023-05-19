import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { PQRModel } from 'src/app/models/pqr.model';
import { CaseTypeService } from 'src/app/services/case-type.service';
import { CountryService } from 'src/app/services/country.service';
import { PqrService } from 'src/app/services/pqr.service';
import { UserTypeService } from 'src/app/services/user-type.service';
//se agrego tipo de caso y cedula con metodo de solo digito numerico, no se eliminan los caracteres de cedula
@Component({
  selector: 'app-form-pqr',
  templateUrl: './form-pqr.component.html',
  styleUrls: ['./form-pqr.component.scss']
})
export class FormPqrComponent implements OnInit {

  formPQR: FormGroup;
  
  listaPais: any[] = [];
  listaTipoCaso: any[] = [];
  listaTipoUsuario: any[] = [];

  userTypeSelected: number = -1;

  constructor(private fb: FormBuilder, public router: Router, private country: CountryService, private caseType: CaseTypeService, private userType: UserTypeService, private pqrService: PqrService) {
    this.formPQR = this.fb.group({
      tipoCaso: ['', [Validators.required]],
      tipoUsuario: ['', [Validators.required]],
      pais: ['', [Validators.required]],
      razonSocial: ['', [Validators.minLength(3)]],
      nit: ['', [Validators.minLength(7)]],
      cedula: ['', [Validators.minLength(7), Validators.pattern(/^[0-9]+$/)]]
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
    this.consultarTipoUsuario();
  }

  cargarData() {
    this.formPQR.setValue({
      tipoCaso: '',
      tipoUsuario: '',
      razonSocial: '',
      nit: '',
      pais: '',
      cedula: '',

    });
  }
  get tipoUsuarioNoValido() {
    return this.formPQR.get('tipoUsuario')!.invalid && this.formPQR.get('tipoUsuario')!.touched;
  }
  // #region
  get razonSocialNoValida() {
    return this.formPQR.get('razonSocial')!.invalid && this.formPQR.get('razonSocial')!.touched;
  }

  get nitNoValido() {
    return this.formPQR.get('nit')!.invalid && this.formPQR.get('nit')!.touched;
  }

  get cedulaNoValido() {
    return this.formPQR.get('cedula')!.invalid;
  }

  get paisNoValido() {
    return this.formPQR.get('pais')!.invalid && this.formPQR.get('pais')!.touched;
  }

  get tipoCasoNoValido() {
    return this.formPQR.get('tipoCaso')!.invalid && this.formPQR.get('tipoCaso')!.touched;
  }
  // #endregion
  soloNumeros(event: { key: string; preventDefault: () => void; }) {

    if (event.key && /[^\d\b]/.test(event.key)) {
      event.preventDefault();
    }
  }

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

  consultarTipoUsuario() {
    this.userType.getAll().subscribe((response: any) => {
      console.log('tipos usuario: ', response.data);
      this.listaTipoUsuario = response.data;
    });
  }

  tipoUsuario(event: any) {
    console.log('Recibo esto: ', event.target["selectedIndex"]);

    /*this.formPQR.get('nit')?.clearValidators();
    this.formPQR.get('nit')?.setValidators([Validators.required]);
    this.formPQR.get('razonSocial')?.setValidators([Validators.required]);*/
    //this.userTypeSelected = event.target["selectedIndex"];

    if (event.target["selectedIndex"] === 0) {
      this.userTypeSelected = 0;

      this.formPQR.get('nit')?.clearValidators();
      this.formPQR.get('razonSocial')?.clearValidators();

      this.formPQR.get('cedula')?.setValidators([Validators.required, Validators.pattern(/^[0-9]+$/)]);

    } else if (event.target["selectedIndex"] === 1) {
      this.userTypeSelected = 1;

      this.formPQR.get('cedula')?.clearValidators();

      this.formPQR.get('nit')?.setValidators([Validators.required]);
      this.formPQR.get('razonSocial')?.setValidators([Validators.required]);
    }
  }

  validateFormat(event: any) {
    let key;
    if (event.type === 'paste') {
      key = event.clipboardData.getData('text/plain');
    } else {
      key = event.keyCode;
      key = String.fromCharCode(key);
    }
    const regex = /[0-9]|\./;
    if (!regex.test(key)) {
      event.returnValue = false;
      if (event.preventDefault) {
        event.preventDefault();
      }
    }
  }

  guardar() {
    console.log('Guardar PQR');

    if (this.formPQR.invalid) {

      console.log('Formulario no valido');

      return Object.values(this.formPQR.controls).forEach(control => {

        if (control instanceof FormGroup) {
          Object.values(control.controls).forEach(control => control.markAsTouched());
        } else {
          control.markAsTouched();
        }

      });

    }

    

    
    this.pqrService.createPQR(this.formPQR.value.pais)
      .subscribe(result => {

        console.log('formulario enviado exitosamente.');

      });


  }

}
