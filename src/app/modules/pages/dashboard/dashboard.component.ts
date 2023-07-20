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
import { PQRModel } from 'src/app/models/pqr.model';
import { Byte } from '@angular/compiler/src/util';
import { SaleforceService } from 'src/app/services/saleforce.service';


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

  constructor(private dialog: MatDialog, private router: Router, private route: ActivatedRoute, private pqr: PqrService, private datePipe: DatePipe, private formBuilder: FormBuilder, private _Activatedroute: ActivatedRoute, private country: CountryService, private neo:SaleforceService) {
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

  openDialog(id: any) {
    const dialogRef = this.dialog.open(ModalComponent, {
      data: {
        id: id

      }
    });

  }

  consultarPaises() {
    this.country.getAll().subscribe((response: any) => {
      
      this.listaPais = response.data;
      
    });
  }

  listarPQR() {

    this.pqr.getAll()
      .subscribe((response: any) => {
        
        this.listPQR = response.data.map((item: any) => {
          // Formatear la fecha usando DatePipe
          const formattedDate = this.datePipe.transform(item.fechaPQR, 'dd/MM/yyyy');

          // Crear un nuevo objeto con la fecha formateada
          return {
            ...item,
            fechaPQR: formattedDate
          };
        });

        
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

  generarCasoSalesforce(id: any) {
    // Lógica para generar un caso en Salesforce con PQR
    if (!id) {
      return;
    }
  
    this.pqr.getById(id).subscribe((response: any) => {
      
  
      
  
      // Mostrar mensaje de confirmación
      Swal.fire({
        title: 'Confirmación',
        text: '¿Está seguro de generar el caso en Salesforce?',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Sí, generar caso',
        cancelButtonText: 'Cancelar'
      }).then((result) => {
        if (result.isConfirmed) {

          Swal.fire({
            title: 'Espere un momento',
            text: 'Actualizando informacion',
            icon: 'info',
            allowOutsideClick: false
          });

          Swal.showLoading();
          if (!response.data[0].nit) {
            Swal.fire('Error', 'No se encontró nit de la PQR para generar caso a Salesforce', 'error');
            return;
          }
          // Usuario confirmó generar caso
          this.neo.query(id).subscribe((queryResponse: boolean) => {
            if (queryResponse) {
              // Aquí puedes realizar las acciones correspondientes si el caso se generó correctamente en Salesforce
              // ...
              Swal.fire('Éxito', 'El caso se generó correctamente en Salesforce', 'success');
              
            } else {
              // Aquí puedes realizar las acciones correspondientes si ocurrió un error al generar el caso en Salesforce
              // ...
              Swal.fire('Error', 'Ocurrió un error al generar el caso en Salesforce', 'error');
            }
          });
        } else {
          // Usuario canceló generar caso
          Swal.fire('Cancelado', 'Se canceló la generación del caso en Salesforce', 'info');
        }
      });
    });
  }
  

  async cerrarCaso(id: any) {
    console.log(id)

    if (!id) {
      return;
    }
    

    this.pqr.getById(id).subscribe((response: any) => {
      console.log(response.data[0])
      
      

      Swal.fire({
        title: 'Espere un momento',
        text: 'Actualizando informacion',
        icon: 'info',
        allowOutsideClick: false
      });
      Swal.showLoading();
            const updatedStatus: PQRModel = {
              id: response.data[0].id,
              countryId: response.data[0].countryId
              ,
              caseTypeId: response.data[0].caseType              ,
              userTypeId: response.data[0].userType
              ,
              razonSocial: response.data[0].razonSocial
              ,
              nit: response.data[0].nit,
              cedula: response.data[0].cedula,
              name: response.data[0].nombre
              ,
              email: response.data[0].email,
              phoneNumber: response.data[0].telefono
              ,
              file: response.data[0].file,
              comentario: response.data[0].comentario,
              autorizaTratamientoDatos: response.data[0].autorizaTratamientoDatos,
              caseNumber: response.data[0].numeroCaso
              ,
              caseStatus: "3ee6cd97-e6c3-4873-a44c-e9ee91b45661",
              date: response.data[0].fechaPQR
              ,
            };
            
            

            this.pqr.update(id, updatedStatus).subscribe(
              () => {
                Swal.fire('Estado actualizado', 'El estado ha sido actualizado correctamente', 'success');
              },
              (error) => {
                Swal.fire('Error', 'Ocurrió un error al actualizar el estado', 'error');
              }
            );
          
    });
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

        console.log(lowerCaseSearchValues.estado + "   " + item.estatus)
      }

      if (lowerCaseSearchValues.pais && item.country.toLowerCase() !== lowerCaseSearchValues.pais) {
        match = false;

        
      }

      if (lowerCaseSearchValues.autorizaTratamientoDatos && item.autorizaTratamientoDatos.toString().toLowerCase() !== lowerCaseSearchValues.autorizaTratamientoDatos) {
        match = false;
      }

      if (lowerCaseSearchValues.fecha && this.datePipe.transform(lowerCaseSearchValues.fecha, 'dd/MM/yyyy') !== item.fechaPQR) {
        match = false;
      }

      return match;
    });
    this.filteredResults = this.filteredResults.reverse();

  }
}
