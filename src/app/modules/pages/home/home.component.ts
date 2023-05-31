import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Images } from 'src/app/constants/images';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  constructor(private router: Router, public images: Images) { }

  ngOnInit(): void {
  }

}
