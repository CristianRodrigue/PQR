import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PqrService } from 'src/app/services/pqr.service';

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

  listPQR: any[] = [];

  constructor(private router: Router, private route: ActivatedRoute, private pqr: PqrService) { }

  ngOnInit(): void {
    this.listarPQR();
  }

  listarPQR() {

    this.pqr.getAll()
      .subscribe((response: any) => {

        this.listPQR = response.data;
        //this.mensualidades = response;
        //this.cargando = false;

        console.log('lista PQRS: ', this.listPQR);
      });
  }

  generarInforme() {
    // Aquí puedes llamar a una función que genere el informe de PQR y lo descargue en formato PDF
    // Por ejemplo, puedes utilizar una librería como pdf-lib para generar el PDF
    // y la función saveAs de FileSaver.js para descargar el archivo PDF
  }
  verDetalles(item: any) {
    // Lógica para ver detalles de PQR
    console.log('verDetalles', item);
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
