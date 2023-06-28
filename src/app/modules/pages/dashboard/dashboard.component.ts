import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PqrService } from 'src/app/services/pqr.service';
import { DatePipe } from '@angular/common';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import * as XLSX from 'xlsx';
import { NgxPaginationModule } from 'ngx-pagination';

import Swal from 'sweetalert2';

import { MatDialog } from '@angular/material/dialog';
import { ModalComponent } from 'src/app/components/modal/modal.component';
import { CountryService } from 'src/app/services/country.service';


@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {

  formulario: FormGroup;

  public page: number | undefined;
  public numeroCasoBusqueda: string | undefined;
  public tipoCasoBusqueda: string | undefined;
  private id: any | null;

  listPQR: any[] = [];
  filteredResults: any[] = [];
  listaPais: any[] = [];

  constructor(private dialog: MatDialog,private router: Router, private route: ActivatedRoute, private pqr: PqrService, private datePipe: DatePipe,private formBuilder: FormBuilder, private _Activatedroute: ActivatedRoute, private country: CountryService,  ) { 
    this.id = this._Activatedroute.snapshot.paramMap.get('id');

    this.formulario = this.formBuilder.group({
      numeroCaso: [''],
      tipoCaso: [''],
      tipoUsuario: [''],
    estado: [''],
    pais: [''],
    fecha: [''],
    autorizaTratamientoDatos: [''],

    });
  }

  ngOnInit(): void {
    this.listarPQR();
    this.consultarPaises();
  }
  openDialog() {
    const mensaje = this.dialog.open(ModalComponent);
  }

  consultarPaises() {
    this.country.getAll().subscribe((response: any) => {
      console.log('paises: ', response.data);
      this.listaPais = response.data;
      console.log('lista pais', this.listaPais);
    });
  }

  listarPQR() {

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
  }
  limpiarBusqueda(): void {
    this.formulario.get('tipoCaso')?.setValue(null); 
    this.formulario.get('numeroCaso')?.setValue(null);
    this.formulario.get('tipoUsuario')?.setValue(null);
    this.formulario.get('estado')?.setValue(null);
    this.formulario.get('pais')?.setValue(null); 
    this.formulario.get('fecha')?.setValue(null);
    this.formulario.get('autorizaTratamientoDatos')?.setValue(null);     
  }

 cerrarCaso(item: any){
  console.log("cerrar caso")
 }

  exportToExcel(event?: Event): void {
    this.filtrarResultados(event);
    const worksheet: XLSX.WorkSheet = XLSX.utils.json_to_sheet(this.filteredResults);
    const workbook: XLSX.WorkBook = { Sheets: { 'data': worksheet }, SheetNames: ['data'] };
    const excelBuffer: any = XLSX.write(workbook, { bookType: 'xlsx', type: 'array' });
    this.saveAsExcelFile(excelBuffer, 'informe_pqr');
  }

  saveAsExcelFile(buffer: any, fileName: string): void {
    const data: Blob = new Blob([buffer], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' });
    const url: string = window.URL.createObjectURL(data);
    const link: HTMLAnchorElement = document.createElement('a');
    link.href = url;
    link.download = fileName + '.xlsx';
    link.click();
  }
  

  filtrarResultados(event?: Event) {
    if (event) {
      event.preventDefault(); // Evitar el comportamiento predeterminado del formulario
    }
  
    // Obtener los valores de los filtros de búsqueda
    const searchValues = this.formulario.value;
  
    const lowerCaseSearchValues = {
      numeroCaso: searchValues.numeroCaso?.toLowerCase(),
      tipoCaso: searchValues.tipoCaso?.toLowerCase(),
      tipoUsuario: searchValues.tipoUsuario?.toLowerCase(),
      estado: searchValues.estado?.toLowerCase(),
      pais: searchValues.pais?.toLowerCase(),
      autorizaTratamientoDatos: searchValues.autorizaTratamientoDatos?.toLowerCase(),
      fecha: searchValues.fecha
    };
  
    // Realizar el filtrado de resultados
    this.filteredResults = this.listPQR.filter(item => {
      // Aplica las condiciones de filtro
      let match = true;
  
      if (lowerCaseSearchValues.numeroCaso && item.numeroCaso.toString().toLowerCase() !== lowerCaseSearchValues.numeroCaso) {
        match = false;
      }
  
      if (lowerCaseSearchValues.tipoCaso && item.caseType.toLowerCase() !== lowerCaseSearchValues.tipoCaso) {
        match = false;
      }
  
      if (lowerCaseSearchValues.tipoUsuario && item.userType.toLowerCase() !== lowerCaseSearchValues.tipoUsuario) {
        match = false;
      }
  
      if (lowerCaseSearchValues.estado && item.estatus.toLowerCase() !== lowerCaseSearchValues.estado) {
        match = false;
      }
  
      if (lowerCaseSearchValues.pais && item.country.toLowerCase() !== lowerCaseSearchValues.pais) {
        match = false;

        console.log(lowerCaseSearchValues.pais + "   " + item.country.toLowerCase())
      }
  
      if (lowerCaseSearchValues.autorizaTratamientoDatos && item.autorizaTratamientoDatos.toString().toLowerCase() !== lowerCaseSearchValues.autorizaTratamientoDatos) {
        match = false;
      }
  
      if (lowerCaseSearchValues.fecha && this.datePipe.transform(lowerCaseSearchValues.fecha, 'dd/MM/yyyy') !== item.fechaPQR) {
        match = false;
      }
  
      return match;
    });
  }
  

  asignar(item: any) {
    // Lógica para asignar un PQR
    console.log('asignarPqr', item);

    Swal.fire({
      title: '<div class="text-left" style="margin-top: 40px;"><strong>Asignar PQR</strong></div>',
      html:
        'You can use <b>bold text</b>, ' +
        '<a href="//sweetalert2.github.io">links</a> ' +
        'and other HTML tags' +
        '<button type="button" class="btn btn-info" (click)="enviarAsigncion()"><strong>Enviar</strong></button>',
      showCloseButton: true,
      
      
    })


  }

  enviarAsigncion() {
    console.log('funciono');
  }

  generarCasoSalesforce(item: any) {
    // Lógica para generar un caso en Salesforce con PQR
    console.log('generarCasoSalesforce', item);
  }

  

}
