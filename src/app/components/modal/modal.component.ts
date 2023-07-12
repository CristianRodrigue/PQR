import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import Swal from 'sweetalert2';
import { AssignService } from '../../services/assign.service';

@Component({
  selector: 'app-modal',
  templateUrl: './modal.component.html',
  styleUrls: ['./modal.component.scss'],
})
export class ModalComponent implements OnInit {

  public modalContent: string = 'Estimado señor(a) {0}<br/><br/>PROCOLOMBIA le da la bienvenida al Simulador de Costos Logísticos.<br /><br />Su empresa {1} se ha validado satisfactoriamente con el NIT {2}<br /><br />Recuerde que cada vez que acceda al sistema debe ingresar con su usuario y contraseña que seleccionó en el registro.';

  formAsignar: FormGroup;
  listEmployee: any[] = [];

  constructor(
    private mensaje: MatDialogRef<ModalComponent>,
    private fb: FormBuilder,
    private assignService: AssignService
  ) {
    this.formAsignar = this.fb.group({
      employee: ['', [Validators.required]]
    });

    this.cargarData();
  }

  ngOnInit(): void {
    this.getEmployees();
  }

  get employeeNoValido() {
    return this.formAsignar.get('employee')!.invalid && this.formAsignar.get('employee')!.touched;
  }

  cargarData() {
    this.formAsignar.setValue({
      tipoCaso: '',
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

  enviarAsigncion() {
    
  }

  enviar() {
    this.assignService.asignarPQR(this.formAsignar.value.id, this.formAsignar.value.name, this.formAsignar.value.email)
      .subscribe((response: any) => {
        Swal.fire({
          title: 'Asignacion exitosa',
          text: 'su PQR se ha asignado exitosamente',
          icon: 'success'
        });
    });
  }

}


/*
 this.pqr.getAll()
      .subscribe((response: any) => {
        console.log('lista PQRS response: ', response);
        this.listPQR = response.data.map((item: any) => {
          // Formatear la fecha usando DatePipe
          const formattedDate = this.datePipe.transform(item.fechaPQR, 'dd/MM/yyyy');

          // Crear un nuevo objeto con la fecha formateada
          return {
            ...item,
            fechaPQR: formattedDate
          };
        });

        console.log('lista PQRS: ', this.listPQR);
      });

*/