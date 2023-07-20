import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Images } from 'src/app/constants/images';
import { AuthService } from 'src/app/services/auth.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  formLogin: FormGroup = new FormGroup({
    email: new FormControl(''),
    password: new FormControl(''),
  });

  constructor(public images: Images, private fb: FormBuilder, private router: Router, private auth: AuthService) { 
    this.crearFormulario();
    this.cargarData();
  }

  ngOnInit(): void {
  }

  get correoNoValido() {
    return this.formLogin.get('email')!.invalid && this.formLogin.get('email')!.touched;
  }

  get passwordNoValido() {
    return this.formLogin.get('password')!.invalid && this.formLogin.get('password')!.touched;
  }

  crearFormulario() {
    this.formLogin = this.fb.group({
      email: ['', [Validators.required, Validators.pattern('[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,3}$')], [], { updateOn: 'blur' }],
      password: ['', [Validators.required]],
    });
  }

  cargarData() {
    this.formLogin.setValue({
      email: '',
      password: '',
    });
  }


  OnLogin() {
    if (this.formLogin.invalid) {

    

      return Object.values(this.formLogin.controls).forEach(control => {

        if (control instanceof FormGroup) {
          Object.values(control.controls).forEach(control => control.markAsTouched());
        } else {
          control.markAsTouched();
        }

      });

    }

    Swal.fire({
      allowOutsideClick: false,
      icon: 'info',
      text: 'Espere por favor'
    });
    Swal.showLoading();

    this.auth.login(this.formLogin.get('email')?.value ?? '', this.formLogin.get('password')?.value ?? '')
    .subscribe(result => {
     

      if (result.status === 'Ok') {
        Swal.close();
        this.router.navigateByUrl('/dashboard');

        // console.log(result.token);
      }

    }, (err: any) => {
      // console.log(err.error.error.message);
      console.log(err);

      Swal.fire({
        allowOutsideClick: true,
        icon: 'error',
        title: 'Error al ingresar',
        text: 'usuario o contraseña no valido'
      });

    });

  }

}
