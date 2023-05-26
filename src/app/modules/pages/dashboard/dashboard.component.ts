import { Component, OnInit } from '@angular/core';

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

  constructor() { }

  ngOnInit(): void {
  }

  pqrList: PQR[] = [
    {
      numeroCaso: '1234567',
      nombreSolicitante: 'Juan Perez',
      tipoCaso: 'queja',
      estado: 'pendiente'
    },
    {
      numeroCaso: '2345678',
      nombreSolicitante: 'María Rodríguez',
      tipoCaso: 'reclamo',
      estado: 'en-proceso'
    },
    {
      numeroCaso: '3456789',
      nombreSolicitante: 'Carlos Hernández',
      tipoCaso: 'solicitud',
      estado: 'resuelto'
    },
  ];

  verDetalles(pqr: PQR) {
    // Lógica para ver detalles de PQR
  }

  asignarPqr(pqr: PQR) {
    // Lógica para asignar un PQR
  }

  generarCasoSalesforce(pqr: PQR) {
    // Lógica para generar un caso en Salesforce con PQR
  }

}
