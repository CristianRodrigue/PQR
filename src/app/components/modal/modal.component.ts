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
  public modalContent: string = '<b><i><u><font size="7" color="#eeff00">SE ASIGNA CASO</font></u></i></b>';

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

  enviarAsigncion() {
    console.log('funciono');
  }

}
