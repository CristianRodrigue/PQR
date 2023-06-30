import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-modal',
  templateUrl: './modal.component.html',
  styleUrls: ['./modal.component.scss'],
})
export class ModalComponent implements OnInit {
  modal: FormGroup;
  public modalContent: string = 'Estimado señor(a) {0}<br/><br/>PROCOLOMBIA le da la bienvenida al Simulador de Costos Logísticos.<br /><br />Su empresa {1} se ha validado satisfactoriamente con el NIT {2}<br /><br />Recuerde que cada vez que acceda al sistema debe ingresar con su usuario y contraseña que seleccionó en el registro.';

  constructor(
    private mensaje: MatDialogRef<ModalComponent>,
    private modalBuilder: FormBuilder
  ) {
    this.modal = this.modalBuilder.group({
      nombre: [''],
      email: ['']
    });
  }

  ngOnInit(): void {}

  closeDialog() {
    this.mensaje.close();
  }
}
