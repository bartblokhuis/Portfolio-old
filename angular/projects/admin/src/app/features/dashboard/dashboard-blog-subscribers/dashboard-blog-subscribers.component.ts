import { Component, OnInit, ViewChild } from '@angular/core';
import { BlogSubscribersService } from 'projects/shared/src/lib/services/api/blog-subscribers/blog-subscribers.service';
import { ChartDataset, ChartOptions, Chart, ChartConfiguration, ChartEvent, ChartType, DefaultDataPoint } from 'chart.js';
import { BaseChartDirective, ThemeService } from 'ng2-charts';
import { ThemingService } from '../../../services/theming/theming.service';

declare var $: any;
declare var moment: any;
declare var Sparkline: any;
@Component({
  selector: 'app-dashboard-blog-subscribers',
  templateUrl: './dashboard-blog-subscribers.component.html',
  styleUrls: ['./dashboard-blog-subscribers.component.scss']
})
export class DashboardBlogSubscribersComponent implements OnInit {

  loaded: boolean = false;
  selectedPeriod = "week";
  themeColor = '#fff';

  lineChartData: ChartDataset[] = [];
  lineChartLabels = [''];
  lineChartOptions: (ChartOptions & { annotation: any }) = {
    annotation: { },
    responsive: true,
    color: this.themeColor,
    maintainAspectRatio: false,
    plugins: {
      filler: {
        propagate: false
      },
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
          maxTicksLimit: 6,
          color: this.themeColor
        }
      },
      yAxis: {
        display: true,
        min: 0,
        ticks: {
          color: this.themeColor
        }
      }
    }
  };

  lineChartLegend = false;
  lineChartPlugins = [];
  lineChartType = 'line';

  constructor(private readonly blogSubscribersService: BlogSubscribersService, private readonly themingService: ThemingService, private themeService: ThemeService) { }

  ngOnInit(): void {
    this.loadChartData();

    this.themingService.theme.subscribe((theme) => {
      if(theme === 'dark-mode') this.themeColor = '#fff';
      else this.themeColor = '#000';

      
      this.themeService.setColorschemesOptions({
        scales: {
          xAxis: {
            grid: {
              display: false,
            },
            display: true,
            ticks: {
              display: true,
              autoSkip: true,
              maxTicksLimit: 6,
              color: this.themeColor
            }
          },
          yAxis: {
            display: true,
            min: 0,
            ticks: {
              color: this.themeColor
            }
          }
        }
      })
      
    })
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
