import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'date-printer',
  templateUrl: './date-printer.component.html',
  styleUrls: ['./date-printer.component.scss']
})
export class DatePrinterComponent implements OnInit {

  @Input() date: string;
  formatedDate: Date;
  currentDate: Date = new Date();
  displayDate: string = '';

  printYear: boolean = false;
  printMonth: boolean = false;
  printDay: boolean = false;
  printHour: boolean = true;
  printMinutes: boolean = true;
  printSeconds: boolean = false;

  constructor() { 
  }

  ngOnInit(): void {
    this.formatedDate = new Date(this.date);

    const monthNames = ["January", "February", "March", "April", "May", "June","July", "August", "September", "October", "November", "December"];
    
    //If the year is not the same as the current year print it.
    if(this.formatedDate.getFullYear() !== this.currentDate.getFullYear()) {
      this.displayDate = this.formatedDate.getFullYear() + "-" + monthNames[this.formatedDate.getMonth()] + "-" + this.formatedDate.getDate();
    }
    else if(this.formatedDate.getMonth() !== this.currentDate.getMonth() || this.getWeekNumber(this.formatedDate) !== this.getWeekNumber(this.currentDate)) {
      this.displayDate = monthNames[this.formatedDate.getMonth()] + " " + this.formatedDate.getDate();
    }
    else if(this.formatedDate.getDate() !== this.currentDate.getDate()) {
      this.displayDate = "Last " + this.formatedDate.toLocaleDateString("en-EN", { weekday: 'long' });
    }
    else {
      this.displayDate = "Today"
    }
  }


  /* For a given date, get the ISO week number
 *
 * Based on information at:
 *
 *    http://www.merlyn.demon.co.uk/weekcalc.htm#WNR
 *
 * Algorithm is to find nearest thursday, it's year
 * is the year of the week number. Then get weeks
 * between that date and the first day of that year.
 *
 * Note that dates in one year can be weeks of previous
 * or next year, overlap is up to 3 days.
 *
 * e.g. 2014/12/29 is Monday in week  1 of 2015
 *      2012/1/1   is Sunday in week 52 of 2011
 */
 getWeekNumber(d: any) : number {
  // Copy date so don't modify original
  d = new Date(Date.UTC(d.getFullYear(), d.getMonth(), d.getDate()));
  // Set to nearest Thursday: current date + 4 - current day number
  // Make Sunday's day number 7
  d.setUTCDate(d.getUTCDate() + 4 - (d.getUTCDay()||7));
  // Get first day of year
  var yearStart: any = new Date(Date.UTC(d.getUTCFullYear(),0,1));
  // Calculate full weeks to nearest Thursday
  var weekNo = Math.ceil(( ( (d - yearStart) / 86400000) + 1)/7);
  // Return array of year and week number
  return weekNo;
   //display the calculated result
 }

}
