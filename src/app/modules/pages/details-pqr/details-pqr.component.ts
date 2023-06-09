import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PqrService } from 'src/app/services/pqr.service';
import { PQRModel } from '../../../models/pqr.model';
import { Byte } from '@angular/compiler/src/util';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-details-pqr',
  templateUrl: './details-pqr.component.html',
  styleUrls: ['./details-pqr.component.scss']
})
export class DetailsPqrComponent implements OnInit {

  private id: any | null;
  
  public numeroCaso!: string;
  public tipoCaso!: string;
  public tipoUsuario!: string;
  public razonSocial!: string;
  public nit!: string;
  public pais!: string;
  public cedula!: string;
  public nombre!: string;
  public email!: string;
  public telefono!: string;
  public file!: Byte;
  public comentarios!: string;
  public autorizaTratamientoDatos!: boolean;
  public estado!: string;
  public fecha!:string;

  constructor(public router: Router, private _Activatedroute: ActivatedRoute, private pqr: PqrService, private datePipe: DatePipe) {
    this.id = this._Activatedroute.snapshot.paramMap.get('id');
  }

  ngOnInit(): void {
    
    this.pqr.getById(this.id)
      .subscribe((response: any) => {

        this.numeroCaso = response.data[0].numeroCaso;
        this.tipoCaso = response.data[0].caseType;
        this.tipoUsuario = response.data[0].userType;
        this.razonSocial = response.data[0].razonSocial;
        this.nit = response.data[0].nit;
        this.cedula = response.data[0].cedula;
        this.pais = response.data[0].country;
        this.nombre = response.data[0].nombre;
        this.email = response.data[0].email;
        this.telefono = response.data[0].telefono;
        this.file = response.data[0].file;
        this.autorizaTratamientoDatos = response.data[0].autorizaTratamientoDatos;
        this.comentarios = response.data[0].comentario;
        this.estado = response.data[0].estatus;
        this.fecha = response.data[0].fechaPQR;

        const formattedDate = this.datePipe.transform(response.data[0].fechaPQR, 'dd-MM-yyyy HH:mm:ss');
      this.fecha = formattedDate !== null ? formattedDate : '';

        console.log('PQR: ', response.data[0]);
      });

  }

}
