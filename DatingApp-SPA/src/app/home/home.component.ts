import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  registermode = true;
  constructor() { }

  ngOnInit() {
  }

  registertoggle()
  {
    this.registermode = !this.registermode;
  }

  cancelRegisterMode(registerMode)
  {
    this.registermode = registerMode;
  }

}
