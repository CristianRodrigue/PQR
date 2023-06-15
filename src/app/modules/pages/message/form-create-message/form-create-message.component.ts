import { Component, OnInit } from '@angular/core';
import { AngularEditorConfig } from '@kolkov/angular-editor';
import { FormBuilder, FormGroup } from '@angular/forms';
import { CreateMessageService } from 'src/app/services/create-message.service';
import Swal from 'sweetalert2';
import { Router } from '@angular/router';

@Component({
  selector: 'app-form-create-message',
  templateUrl: './form-create-message.component.html',
  styleUrls: ['./form-create-message.component.scss']
})
export class FormCreateMessageComponent implements OnInit {

  formulario: FormGroup;

  htmlContent: string = '';

  constructor(private router: Router, private formBuilder: FormBuilder, private mailService: CreateMessageService) {
    this.formulario = this.formBuilder.group({
      nombre: [''],
      descripcion: [''],
      html: [''],
    });
   }

  ngOnInit(): void {
  
  }

  config: AngularEditorConfig = {
    editable: true,
    spellcheck: true,
    height: '15rem',
    minHeight: '5rem',
    placeholder: 'Escriba aqui ...',
    translate: 'yes',
    defaultParagraphSeparator: 'p',
    defaultFontName: 'Arial',
    toolbarHiddenButtons: [
      ['bold']
      ],
    customClasses: [
      {
        name: "quote",
        class: "quote",
      },
      {
        name: 'redText',
        class: 'redText'
      },
      {
        name: "titleText",
        class: "titleText",
        tag: "h1",
      },
    ]
  };

  cargarData(){
    this.formulario.setValue({
      nombre:'',
      descripcion:'',
      html:'',
    })
  }

  guardar() {
    console.log('Guardar Mensaje');

    if (this.formulario.invalid) {

      console.log('Formulario no valido');

      return Object.values(this.formulario.controls).forEach(control => {

        if (control instanceof FormGroup) {
          Object.values(control.controls).forEach(control => control.markAsTouched());
        } else {
          control.markAsTouched();
        }

      });

    }

    this.mailService.createMail(this.formulario.value.nombre, this.formulario.value.descripcion, this.formulario.value.html)
      .subscribe(result => {

        Swal.fire({
          title: 'Atencion !!!',
          text: `El template se a creado con exito`,
          icon: 'info',
          showConfirmButton: true
        });

        this.router.navigateByUrl('/mensajes');
      });
  }
 
}
