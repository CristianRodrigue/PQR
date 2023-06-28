import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { RoleService } from 'src/app/services/role.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-form-create-user-admin',
  templateUrl: './form-create-user-admin.component.html',
  styleUrls: ['./form-create-user-admin.component.scss']
})
export class FormCreateUserAdminComponent implements OnInit {

  formAuth: FormGroup;
  listaRole: any[] = [];
  emailMach: boolean = false;

  constructor(private fb: FormBuilder, public router: Router, private roleService: RoleService, private auth: AuthService) {
    this.formAuth = this.fb.group({
      nombre: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      role: ['', [Validators.required]]
    },
      {
        validators: [this.validarEmail('email')]
      });

    // this.cargarData();
  }

  ngOnInit(): void {
    this.consultarRoles();
  }

  get nombreNoValido() {
    return this.formAuth.get('nombre')!.invalid && this.formAuth.get('nombre')!.touched;
  }
  get emailNoValido() {
    return this.formAuth.get('email')!.invalid && this.formAuth.get('email')!.touched;
  }
  get passwordNoValido() {
    return this.formAuth.get('password')!.invalid && this.formAuth.get('password')!.touched;
  }
  get roleNoValido() {
    return this.formAuth.get('role')!.invalid && this.formAuth.get('role')!.touched;
  }

  consultarRoles() {
    this.roleService.getAll().subscribe((response: any) => {
      console.log('roles: ', response.data);
      this.listaRole = response.data;
      console.log('lista roles', this.listaRole);
    });
  }

  validarEmail(controlName: string) {

    return (formGroup: FormGroup) => {
      const control = formGroup.controls[controlName];

      var email = control.value; //.toString();
      var emailData = '-1';

      if (email !== '') {
        this.auth.getEmailValidator(email)
          .subscribe((data: any) => {
            if (data.success === true) {
              emailData = email;
            }

            if (control.value === emailData) {
              control.setErrors({ confirmed: false });
              this.emailMach = false;
            } else {
              control.setErrors(null);
              this.emailMach = true;
            }
          });
      }
    };
  }

  guardar() {
    console.log('Guardar usuario');

    if (this.formAuth.invalid) {

      console.log('Formulario no valido');

      return Object.values(this.formAuth.controls).forEach(control => {

        if (control instanceof FormGroup) {
          Object.values(control.controls).forEach(control => control.markAsTouched());
        } else {
          control.markAsTouched();
        }

      });

    }

    Swal.fire({
      title: 'Espere un momento',
      text: 'Actualizando informacion',
      icon: 'info',
      allowOutsideClick: false
    });
    Swal.showLoading();

    this.auth.create(this.formAuth.value.nombre, this.formAuth.value.email, this.formAuth.value.password, this.formAuth.value.role)
      .subscribe(result => {

        Swal.fire({
          title: 'Registro Exitoso',
          text: 'se creo el usuario correctamente',
          icon: 'success'
        });

      });


    // Limpiamos formulario
    this.formAuth.reset();
    this.router.navigateByUrl('/dashboard');
  }

}
