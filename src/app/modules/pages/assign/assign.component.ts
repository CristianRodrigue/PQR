import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AssignService } from 'src/app/services/assign.service';

@Component({
  selector: 'app-assign',
  templateUrl: './assign.component.html',
  styleUrls: ['./assign.component.scss']
})
export class AssignComponent implements OnInit {

  list: any[] = [];

  constructor(public router: Router, private assignService: AssignService) { }

  ngOnInit(): void {
    this.getAssigns();
  }

  getAssigns() {
    this.assignService.getAll()
      .subscribe((response: any) => {
        this.list = response.data;
        console.log('lista', this.list);
      });
  }

}
