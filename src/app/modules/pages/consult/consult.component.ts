import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { PqrService } from 'src/app/services/pqr.service';
import { DatePipe } from '@angular/common';
import { FileService } from 'src/app/services/file.service';
import { saveAs } from 'file-saver';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-consult',
  templateUrl: './consult.component.html',
  styleUrls: ['./consult.component.scss']
})
export class ConsultComponent implements OnInit {

  formulario: FormGroup;

  private id: any | null;
  public file!: string;
  
  listPQR: any[] = [];
  filteredResults: any[] = [];
  

  constructor(private formBuilder: FormBuilder, private _Activatedroute: ActivatedRoute,private pqr: PqrService,private datePipe: DatePipe,private fileServices: FileService) {

    this.id = this._Activatedroute.snapshot.paramMap.get('id');
    
    this.formulario = this.formBuilder.group({
      numeroCaso: [''],

    });
   }

  ngOnInit(): void {
    this.listarPQR();
  }

  listarPQR() {

    this.pqr.getAll()
      .subscribe((response: any) => {
        
        this.listPQR = response.data.map((item: any) => {
          // Formatear la fecha usando DatePipe
          console.log(response.data[0])
          
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

  descargarArchivo(): void {
    console.log("ARCHIVO A DESCARGAR"+this.file)
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
        Swal.fire('Error', 'Error al descargar el archivo.', 'error');
      }
    );
  }


  noResultFound = false; // Variable para controlar si no se encontraron resultados

filtrarResultados(event?: Event) {
  if (event) {
    event.preventDefault();
  }

  const searchValues = this.formulario.value;
  const lowerCaseSearchValues = {
    numeroCaso: searchValues.numeroCaso,
  };

  // Validar si se ha ingresado un número de caso
  if (!lowerCaseSearchValues.numeroCaso) {
    this.filteredResults = [];
    this.noResultFound = false; // Reiniciar la variable de noResultFound
    return;
  }

  this.filteredResults = this.listPQR.filter(item => {
    let match = true;
    const itemNumeroCaso = item.numeroCaso;
    if (itemNumeroCaso !== lowerCaseSearchValues.numeroCaso) {
      match = false;
    } else {
      this.file = item.file;
      console.log("ID DEL ARCHIVOOOOOOOOOOOOO" + this.file);
    }
    return match;
  });

  this.filteredResults = this.filteredResults.reverse();

  // Establecer el valor de noResultFound
  this.noResultFound = this.filteredResults.length === 0;

  // Mostrar el mensaje de "No se encontraron resultados" utilizando Swal.fire
  if (this.noResultFound) {
    Swal.fire('No se encontraron resultados', '', 'warning');
  }
}
  

}
