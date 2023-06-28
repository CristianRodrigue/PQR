import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-user-admin',
  templateUrl: './user-admin.component.html',
  styleUrls: ['./user-admin.component.scss']
})
export class UserAdminComponent implements OnInit {

  list: any[] = [];

  constructor(public router: Router, private userService: AuthService) {
    this.getUsers();
   }

  ngOnInit(): void {
    // this.getUsers();
  }

  getUsers() {
    this.userService.getAll()
      .subscribe((response: any) => {
        this.list = response.data;
      });
  }

  deleteUser(id: string, index: number) {
    console.log('Borrar');
    console.log('Id mensaje', id);
    console.log('Indice mensaje', index);

    Swal.fire({
      title: 'Esta seguro?',
      text: `Esta seguro que desea borrar este usuario`,
      icon: 'question',
      showConfirmButton: true,
      showCancelButton: true
    }).then(result => {

      /*if (result.status === 'NoPermitido') {
        console.log('La cuenta esta inactiva');

        Swal.fire({
          allowOutsideClick: true,
          icon: 'warning',
          title: 'La cuenta esta inactiva',
          html: 'ingrese a su correo y active su cuenta desde el correo que le enviamos, si no lo recibio haga click <a href="../activarusuario">aqui.</a>'
        });
      } */

      if (result.value) {
        this.userService.delete(id).subscribe(res => {

          // console.log('delete result', res)

          let resultado: any;
          resultado = res;

          if (resultado.status === 'NoPermitido') {
            console.log('No se puede eliminar el usuario');

            Swal.fire({
              allowOutsideClick: true,
              icon: 'warning',
              title: 'No se puede eliminar el usuario',
              html: 'Es el unico usuario existente, no se permite borrar este usuario'
            });
          }
        });
      }
    });

  }

}
