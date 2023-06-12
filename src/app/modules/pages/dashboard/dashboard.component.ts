import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PqrService } from 'src/app/services/pqr.service';
import { DatePipe } from '@angular/common';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import * as XLSX from 'xlsx';


interface PQR {
  numeroCaso: string;
  nombreSolicitante: string;
  tipoCaso: string;
  estado: string;
}


@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {

  formulario: FormGroup;

  public numeroCasoBusqueda: string | undefined;
  public tipoCasoBusqueda: string | undefined;
  private id: any | null;

  listPQR: any[] = [];
  filteredResults: any[] = [];

  constructor(private router: Router, private route: ActivatedRoute, private pqr: PqrService, private datePipe: DatePipe,private formBuilder: FormBuilder, private _Activatedroute: ActivatedRoute ) { 
    this.id = this._Activatedroute.snapshot.paramMap.get('id');

    this.formulario = this.formBuilder.group({
      numeroCaso: [''],
      tipoCaso: [''],
      tipoUsuario: [''],
    estado: [''],
    pais: [''],
    fecha: [''],

    });
  }

  ngOnInit(): void {
    this.listarPQR();
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
    if(event){
    event.preventDefault();} // Evitar el comportamiento predeterminado del formulario

    // Obtener los valores de los filtros de búsqueda
    const searchValues = this.formulario.value;

    const lowerCaseSearchValues = {
      numeroCaso: searchValues.numeroCaso?.toLowerCase(),
      tipoCaso: searchValues.tipoCaso?.toLowerCase(),
      tipoUsuario: searchValues.tipoUsuario?.toLowerCase(),
      estado: searchValues.estado?.toLowerCase(),
      pais: searchValues.pais?.toLowerCase(),
      fecha: searchValues.fecha
    };

    // Realizar el filtrado de resultados
    this.filteredResults = this.listPQR.filter(item => {
        // Aplica las condiciones de filtro
        let match = true;

        if (searchValues.numeroCaso.toString()
        && item.numeroCaso.toString() !== searchValues.numeroCaso.toString()) {
          match = false;

          console.log('match into: ',searchValues.numeroCaso,' + ', item.numeroCaso)
        }
    
        if (searchValues.tipoCaso && item.caseType.toLowerCase() !== searchValues.tipoCaso) {
          match = false;

          console.log('match into: ',searchValues.tipoCaso,' + ', item.caseType)
        }

        if (searchValues.tipoUsuario && item.userType.toLowerCase() !== searchValues.tipoUsuario) {
          match = false;

          console.log('match into: ',searchValues.tipoUsuario,' + ', item.userType)
        }
    
        if (searchValues.estado && item.estatus.toLowerCase() !== searchValues.estado) {
          match = false;

          console.log('match into: ',searchValues.estado,' + ', item.estatus)
        }

        if (searchValues.pais && item.country.toLowerCase() !== searchValues.pais) {
          match = false;

          console.log('match into: ',searchValues.pais,' + ', item.country)
        }
    
        if (this.datePipe.transform(searchValues.fecha, 'dd/MM/yyyy') && item.fechaPQR !== this.datePipe.transform(searchValues.fecha, 'dd/MM/yyyy')) {
          match = false;

          console.log('match into: ',searchValues.fecha,' + ', item.fechaPQR)
        }
        
        console.log('match: ',match)
        return match;
    });
}

  asignarPqr(item: any) {
    // Lógica para asignar un PQR
    console.log('asignarPqr', item);
  }

  generarCasoSalesforce(item: any) {
    // Lógica para generar un caso en Salesforce con PQR
    console.log('generarCasoSalesforce', item);
  }

  

}
