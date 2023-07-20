import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PqrService } from 'src/app/services/pqr.service';
import { PQRModel } from '../../../models/pqr.model';
import { Byte } from '@angular/compiler/src/util';
import { DatePipe } from '@angular/common';
import { FileService } from 'src/app/services/file.service';
import { FileModel } from 'src/app/models/file.model';
import { saveAs } from 'file-saver';
import { HttpResponse } from '@angular/common/http';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-details-pqr',
  templateUrl: './details-pqr.component.html',
  styleUrls: ['./details-pqr.component.scss']
})
export class DetailsPqrComponent implements OnInit {

  private id: any | null;
  public data: Uint8Array | undefined;
  public fileName: string | undefined;
  public height: any;
  public timestamp: any;
  public uri:any
  
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
  public file!: string;
  public comentarios!: string;
  public autorizaTratamientoDatos!: boolean;
  public estado!: string;
  public fecha!:string;
  
  constructor(public router: Router, private _Activatedroute: ActivatedRoute, private pqr: PqrService, private datePipe: DatePipe, private fileServices: FileService,) {
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

        
      });

  }

  descargarArchivo(): void {
    if(this.file=="48cc9863-a763-4d68-8883-59d39ccec665"){
      Swal.fire('Error', 'No se encontró archivo adjunto.', 'error');
            return;
    }
    this.fileServices.getById(this.file).subscribe(
      (response: Blob) => {
        
        console.log(response);
        const fileName = this.file.toString(); // Reemplaza con el nombre de archivo correcto
        saveAs(response, fileName);
      },
      (error) => {
        console.error('Error al descargar el archivo:', error);
      }
    );
  }
  
}
