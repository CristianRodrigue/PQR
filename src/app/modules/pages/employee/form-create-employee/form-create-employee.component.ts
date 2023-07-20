import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { EmployeeService } from 'src/app/services/employee.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-form-create-employee',
  templateUrl: './form-create-employee.component.html',
  styleUrls: ['./form-create-employee.component.scss']
})
export class FormCreateEmployeeComponent implements OnInit {
  form: FormGroup;
  
  constructor(
    public router: Router,
    private fb: FormBuilder,
    private employe: EmployeeService
    
    ) {
    
      this.form = this.fb.group({
        nombre: ['', [Validators.required]],
        email: ['', [Validators.required, Validators.email]],
        area: ['', ],
        cargo: ['', ]
      });

      this.cargarData();
   }

  ngOnInit(): void {
  }

  cargarData() {
    this.form.setValue({
      nombre: '',
      email: '',
      area: '',
      cargo: '',
     
    });
  }

  enviar() {

    if (this.form.invalid) {
      const formData = this.form.value;
     

      return Object.values(this.form.controls).forEach(control => {

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

    this.employe.create(this.form.value.nombre,this.form.value.email,this.form.value.area,this.form.value.cargo).subscribe(result => {

      Swal.fire({
        title: 'Registro Exitoso',
        text: 'Su usuario se ha registrado exitosamente',
        icon: 'success'
      });

      // Limpiamos formulario
      this.form.reset();
      this.router.navigateByUrl('/empleados');

    });
  }


 
}
