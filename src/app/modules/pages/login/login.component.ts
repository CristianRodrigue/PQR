import { Component, OnInit } from '@angular/core';
import { Images } from 'src/app/constants/images';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  constructor(public images: Images,) { }

  ngOnInit(): void {
  }

}
