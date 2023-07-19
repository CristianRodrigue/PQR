import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators,  } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import Swal from 'sweetalert2';
import { AssignService } from '../../services/assign.service';
import { Router } from '@angular/router';
import { EmployeeService } from 'src/app/services/employee.service';
import {EmployeeModel} from 'src/app/models/employee.model'


@Component({
  selector: 'app-modal',
  templateUrl: './modal.component.html',
  styleUrls: ['./modal.component.scss'],
})
export class ModalComponent implements OnInit {

  users: any[] = [];
  public modalContent: string = 'Estimado señor(a) {0}<br/><br/>PROCOLOMBIA le da la bienvenida al Simulador de Costos Logísticos.<br /><br />Su empresa {1} se ha validado satisfactoriamente con el NIT {2}<br /><br />Recuerde que cada vez que acceda al sistema debe ingresar con su usuario y contraseña que seleccionó en el registro.';

  formAsignar: FormGroup;
  listEmployee: any[] = [];
  id: any;

  constructor(
    
    private employeService: EmployeeService,
    public router: Router,
    private mensaje: MatDialogRef<ModalComponent>,
    private fb: FormBuilder,
    private assignService: AssignService, 
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {
    
    this.id = data.id;
    
    this.formAsignar = this.fb.group({

      selectedUser: ['', [Validators.required]],
      nombre: [''],
      email: ['']

      
    });

    this.cargarData();
  }


  seleccionarUsuario() {
    const selectedUserControl = this.formAsignar.get('selectedUser') as FormControl;
    const selectedEmail = selectedUserControl.value;
  
    const selectedUser = this.users.find(user => user.email === selectedEmail);
    console.log(selectedUser)
    if (selectedUser) {
      this.formAsignar.patchValue({
        nombre: selectedUser.nombre,
        email: selectedUser.email
      });
    }
  }
  
  ngOnInit(): void {
    this.employeService.getAll().subscribe(
      (response: any) => {
        this.users = response.data.map((user: EmployeeModel)=> ({
          email: user.email,
          nombre: user.nombre,
          cargo: user.cargo,
          area: user.division
        }));
      },
      (error: any) => {
        console.log('Error al obtener la lista de usuarios:', error);
      }
    );
  
    this.getEmployees();
  }

  get emailNoValido() {
    return this.formAsignar.get('email')!.invalid && this.formAsignar.get('email')!.touched;
  }

  cargarData() {
    this.formAsignar.setValue({
     
      selectedUser:'',
      nombre:'',
      email:''

    });
  }

  getEmployees() {
    this.assignService.getAll()
      .subscribe((response: any) => {
        this.listEmployee = response.data
      });
  }

  closeDialog() {
    this.mensaje.close();
  }

  enviar() {
    Swal.fire({
      title: 'Espere un momento',
      text: 'Actualizando informacion',
      icon: 'info',
      allowOutsideClick: false
    });
    Swal.showLoading();

    this.assignService.asignarPQR(this.formAsignar.value.email,this.id,this.formAsignar.value.nombre)
      .subscribe((response: any) => {
        Swal.fire({
          title: 'Asignacion exitosa',
          text: 'su PQR se ha asignado exitosamente',
          icon: 'success'
        });
        this.formAsignar.reset();
      this.router.navigateByUrl('/dashboard');
    });
  }

}