import { Component, OnInit } from '@angular/core';
import { AngularEditorConfig } from '@kolkov/angular-editor';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { CreateMessageService } from 'src/app/services/create-message.service';

@Component({
  selector: 'app-form-create-message',
  templateUrl: './form-create-message.component.html',
  styleUrls: ['./form-create-message.component.scss']
})
export class FormCreateMessageComponent implements OnInit {

  formulario: FormGroup;

  list: any[] = [];
  private id: any | null;
  htmlContent = '';

  constructor(private formBuilder: FormBuilder,private _Activatedroute: ActivatedRoute, private mailService:CreateMessageService) {
    this.id = this._Activatedroute.snapshot.paramMap.get('id');

    this.formulario = this.formBuilder.group({
      nombre: [''],
      descripcion: [''],
      html: [''],
    });
   }

  ngOnInit(): void {
    this.listar();
  }

  cargarData(){
    this.formulario.setValue({
      nombre:'',
      descripcion:'',
      html:'',
    })
  }

  guardar(){
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

    this.mailService.createMail(this.formulario.value.nombre,this.formulario.value.descripcion,this.formulario.value.html)
      .subscribe(result => {

        console.log('formulario enviado exitosamente.');
        console.log(this.mailService);

      });
  }

  listar(){
    this.mailService.getAll()
    .subscribe((response: any) => {
      console.log('lista mensajes response: ', response);
      this.list = response.data.map((item: any) => {
   
      });

      console.log('lista Mensajes: ', this.list);
    });
  }

  config: AngularEditorConfig = {
    editable: true,
    spellcheck: true,
    height: '15rem',
    minHeight: '5rem',
    placeholder: 'Enter text here...',
    translate: 'no',
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
 
}
