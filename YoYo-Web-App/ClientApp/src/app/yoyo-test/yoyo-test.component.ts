import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Component, OnInit, ViewChild } from '@angular/core';
import { CircleProgressComponent } from 'ng-circle-progress';
import { Athlete } from '../models/Athlete';
import { FitnessRating } from '../models/FitnessRating';

@Component({
  selector: 'app-yoyo-test',
  templateUrl: './yoyo-test.component.html',
  styleUrls: ['./yoyo-test.component.css']
})
export class YoyoTestComponent implements OnInit {

  @ViewChild(CircleProgressComponent, { static: false }) circleProgress: CircleProgressComponent;
  private _timer: number;
  private _fitnessRating: FitnessRating[];
  previousFitnessRating: FitnessRating;
  currentFitnessRating: FitnessRating;
  nextFitnessRating: FitnessRating;
  titleName: string[] = ['Click'];
  subTitleName: string[] = ['to Start'];
  totalMin: number = 0;
  totalSec: number = 0;
  totalDistance: number=0;
  shuttleTimer: string;
  nextShuttleTimer: string;
  constructor(private _httpClient: HttpClient) {

  }
  athletes: Athlete[];
  percent: number = 0;
  ngOnInit() {
    this._httpClient.get<Athlete[]>("/Athlete/GetAthletes").subscribe(val => this.athletes = val);
    this._httpClient.get<FitnessRating[]>("/Athlete/GetRatings").subscribe(val => {
      this._fitnessRating = val
    });
    
  }

  Warn(id: number) {
    this._httpClient.post<Athlete>("/Athlete/Warn?id=" + id, null).subscribe(val => this.athletes[id - 1].isWarned = val.isWarned);
  }
  selectedDropdown(fitnessrating:FitnessRating,athlete:Athlete):boolean{
    if(+fitnessrating.speedLevel==athlete.speedLevel && +fitnessrating.shuttleNo==athlete.shuttleNumber)
    return true;

    return false;
  }
  Stop(id: number) {
    let headers: HttpHeaders = new HttpHeaders();
    headers = headers.append('Content-Type', 'application/json');
    this._httpClient.post<Athlete>("/Athlete/Stop?id=" + id, this.previousFitnessRating !=null?JSON.stringify(this.previousFitnessRating):{},{headers:headers}).subscribe(val => {
      this.athletes[id - 1].isStopped = val.isStopped;
      this.athletes[id-1].shuttleNumber=val.shuttleNumber;
      this.athletes[id-1].speedLevel=val.speedLevel;
    });
  }

  circleclick() {
    this.shuttleTimer = this._fitnessRating[0].commulativeTime;
    this.currentFitnessRating = this._fitnessRating[0];
    this.updateTotalTimer();
    if (this._timer !== null) {
      clearInterval(this._timer);
    }
    this._timer = window.setInterval(() => {
      this.percent = ((this.totalSec-this.convertStringToSeconds(this.currentFitnessRating.startTime))/
                      (this.convertStringToSeconds(this.currentFitnessRating.commulativeTime)
                     - this.convertStringToSeconds(this.currentFitnessRating.startTime))
                     )* 100;
                     
      this.titleName = ['Level '+ this.currentFitnessRating.speedLevel];
      this.subTitleName = ['Shuttle ' + this.currentFitnessRating.shuttleNo, this.currentFitnessRating.speed + ' km/ph'];
      if (this.percent === 100) {
        this.percent = 0;
      }
    }, 1000)
  }
  updateTotalTimer() {
    setInterval(() => {
      this.totalSec++
      this.totalMin = parseInt((this.totalSec / 60).toString());
      this.updateShuttleDistance();
      this.updateNextShuttleTime();
    }, 1000);
  }

  updateShuttleDistance() {
    let currentTime = this.padzero(this.totalMin.toString()) + ":" + this.padzero((this.totalSec % 60).toString());
    for (let i = 0; i < this._fitnessRating.length - 1; i++) {
      if (currentTime === this._fitnessRating[i].startTime) {
        this.currentFitnessRating = this._fitnessRating[i];
        this.previousFitnessRating=this._fitnessRating[i-1];
      }
      if (currentTime === this._fitnessRating[i].commulativeTime) {
        this.nextFitnessRating = this._fitnessRating[i];
      }
    }
  }

  updateNextShuttleTime(){
   let totalCommSeconds = this.convertStringToSeconds(this.currentFitnessRating.commulativeTime);
   let timeToNextShuttle = totalCommSeconds - this.totalSec;    
   this.nextShuttleTimer = this.convertSecondsToString(timeToNextShuttle);
  }
  padzero(value: string): string {
    return value.length < 2 ? '0' + value : value;
  }

  convertSecondsToString(seconds: number): string{
    return parseInt((seconds / 60).toString()).toString() + ":" + (seconds % 60).toString();
  }

  convertStringToSeconds(time: string): number{
    let commtime = time.split(":");
    return +commtime[0]*60 + +commtime[1];
  }
}
