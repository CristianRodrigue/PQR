import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { CreateMessageService } from 'src/app/services/create-message.service';

@Component({
  selector: 'app-message',
  templateUrl: './message.component.html',
  styleUrls: ['./message.component.scss']
})
export class MessageComponent implements OnInit {

  list: any[] = [];

  constructor(public router: Router, private mailService: CreateMessageService) { }

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

}
