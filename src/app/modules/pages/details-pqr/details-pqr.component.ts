import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PqrService } from 'src/app/services/pqr.service';
import { PQRModel } from '../../../models/pqr.model';

@Component({
  selector: 'app-details-pqr',
  templateUrl: './details-pqr.component.html',
  styleUrls: ['./details-pqr.component.scss']
})
export class DetailsPqrComponent implements OnInit {

  private id: any | null;
  
  public numeroCaso!: string;
  public comentarios!: string;

  constructor(public router: Router, private _Activatedroute: ActivatedRoute, private pqr: PqrService) {
    this.id = this._Activatedroute.snapshot.paramMap.get('id');
  }

  ngOnInit(): void {
    
    this.pqr.getById(this.id)
      .subscribe((response: any) => {

        this.numeroCaso = response.data[0].caseNumber;
        this.comentarios = response.data[0].comentario;

        console.log('PQR: ', response.data[0]);
      });

  }

}
