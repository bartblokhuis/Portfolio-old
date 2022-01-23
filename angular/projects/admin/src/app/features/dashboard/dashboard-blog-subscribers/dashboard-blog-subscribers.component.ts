import { Component, OnInit } from '@angular/core';
import { BlogSubscribersService } from 'projects/shared/src/lib/services/api/blog-subscribers/blog-subscribers.service';
import { ChartDataset, ChartOptions, Chart, ChartConfiguration, ChartEvent, ChartType, DefaultDataPoint } from 'chart.js';

declare var $: any;
declare var moment: any;
declare var Sparkline: any;
@Component({
  selector: 'app-dashboard-blog-subscribers',
  templateUrl: './dashboard-blog-subscribers.component.html',
  styleUrls: ['./dashboard-blog-subscribers.component.scss']
})
export class DashboardBlogSubscribersComponent implements OnInit {

  constructor(private readonly blogSubscribersService: BlogSubscribersService) { }

  loaded: boolean = false;
  selectedPeriod = "week";


  lineChartData: ChartDataset[] = [];
 

  lineChartLabels = [''];
  lineChartOptions: (ChartOptions & { annotation: any }) = {
    annotation: { },
    responsive: true,
    maintainAspectRatio: false,
    plugins: {
      filler: {
        propagate: false
      }
    },
    interaction: {
      intersect: true
    },
    indexAxis: 'x',
    scales: {
      xAxis: {
        grid: {
          display: false,
        },
        display: true,
        ticks: {
          display: true,
          autoSkip: true,
          maxTicksLimit: 6
        }
      },
      yAxis: {
        display: true,
        min: 0
      }
    }
  };

  lineChartLegend = false;
  lineChartPlugins = [];
  lineChartType = 'line';

  ngOnInit(): void {
    this.loadChartData();
  }

  changeSelectedPeriod(newSelection: string) {
    this.selectedPeriod = newSelection;

    this.loadChartData();
  }

  loadChartData() {
    this.blogSubscribersService.loadBlogSubscriberStatistics(this.selectedPeriod).subscribe((result) => {
      if(!result.succeeded) return;

      this.lineChartData = [];
      result.data.forEach(element => {
       
        
      });
      this.lineChartLabels = result.data.map(x => x.date.toString());
      
      const data = result.data.map(x => x.value);
      this.lineChartData.push({
        data: data, 
        label: 'Blog subscribers', 
        fill: 'start', 
        tension: 0.4,
      });

      this.loaded = true;
    })
  }

}
