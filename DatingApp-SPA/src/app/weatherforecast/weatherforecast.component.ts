import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-weatherforecast',
  templateUrl: './weatherforecast.component.html',
  styleUrls: ['./weatherforecast.component.css']
})
export class WeatherforecastComponent implements OnInit {
  weatherForecasts: any;
  constructor(private http: HttpClient) { }

  // tslint:disable-next-line: typedef
  ngOnInit() {
    this.getWeatherForecasts();
  }

  // tslint:disable-next-line: typedef
  getWeatherForecasts() {
    this.http.get('http://localhost:5000/weatherforecast').subscribe(data => {
      console.log(data);
      this.weatherForecasts = data;
    }, error => {
      console.log(error);
    });
  }

}
