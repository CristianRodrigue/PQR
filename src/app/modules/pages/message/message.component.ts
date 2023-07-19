import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MailModel } from 'src/app/models/mail.model';
import { CreateMessageService } from 'src/app/services/create-message.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-message',
  templateUrl: './message.component.html',
  styleUrls: ['./message.component.scss']
})
export class MessageComponent implements OnInit {

  list: any[] = [];
  private id: any | null;

  constructor(public router: Router, private mailService: CreateMessageService,private route: ActivatedRoute) { 
    this.id = this.route.snapshot.paramMap.get('id');
  }

  mensaje: string = '';

  ngOnInit(): void {

    this.getMessages();
  }

  getMessages() {
    this.mailService.getAll()
      .subscribe((response: any) => {
        this.list = response.data;

        console.log('lista mensaje: ', this.list);
      });
  }

  enabledMailTemplate(id: string, index: number) {
    console.log('Activar o desactivar');
    console.log('Id mensaje', id);
    console.log('Indice mensaje', index);

  }


  deleteMailTemplate(id: string, index: number) {
    console.log('Borrar');
    console.log('Id mensaje', id);
    console.log('Indice mensaje', index);

  }


  async editar(id: string) {
    if (!id) {
      return;
    }
  
    this.mailService.getById(id).subscribe((mail: MailModel[]) => {
      const mailData: MailModel = mail[0];
      if (!mailData) {
        return;
      }
  
      Swal.fire({
        title: 'Editar Mensaje',
        input: 'textarea',
        inputLabel: 'Mensaje',
        inputValue: mailData.message,
        showCancelButton: true,
        confirmButtonText: 'Guardar',
        inputValidator: (value) => {
          if (!value) {
            return '¡Debe ingresar un mensaje!';
          } else {
            return null; // Retornar null si el valor es válido
          }
        }
      }).then((result) => {
        if (result.isConfirmed) {
          const nuevoMensaje = result.value;
          if (!nuevoMensaje) {
            Swal.showValidationMessage('¡Debe ingresar un mensaje!');
          } else {
            const updatedMail: MailModel = {
              id: mailData.id,
              name: mailData.name,
              description: mailData.description,
              html: mailData.html,
              enabled: mailData.enabled,
              message: nuevoMensaje
            };
  
            this.mailService.updateMessage(id, updatedMail).subscribe(
              () => {
                Swal.fire('Mensaje actualizado', 'El mensaje ha sido actualizado correctamente', 'success');
              },
              (error) => {
                Swal.fire('Error', 'Ocurrió un error al actualizar el mensaje', 'error');
              }
            );
          }
        }
      });
    });
  }

}