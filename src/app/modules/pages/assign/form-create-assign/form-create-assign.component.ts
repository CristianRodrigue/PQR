import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AssignService } from 'src/app/services/assign.service';
import { AuthService } from 'src/app/services/auth.service';
import { RoleService } from 'src/app/services/role.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-form-create-assign',
  templateUrl: './form-create-assign.component.html',
  styleUrls: ['./form-create-assign.component.scss']
})
export class FormCreateAssignComponent implements OnInit {

  formUser: FormGroup ;
  constructor(private fb: FormBuilder, public router: Router, private roleService: RoleService, private user: AssignService) {
    this.formUser = this.fb.group({
      nombre: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      
      
    },
      {
        
      });

    // this.cargarData();
  }
  ngOnInit(): void {
  }

  guardar() {
    

    if (this.formUser.invalid) {

      

      return Object.values(this.formUser.controls).forEach(control => {

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

    this.user.create(this.formUser.value.nombre, this.formUser.value.email, )
      .subscribe(result => {

        Swal.fire({
          title: 'Registro Exitoso',
          text: 'se creo el usuario correctamente',
          icon: 'success'
        });

      });


    // Limpiamos formulario
    this.formUser.reset();
    this.router.navigateByUrl('/dashboard');
  }

}
