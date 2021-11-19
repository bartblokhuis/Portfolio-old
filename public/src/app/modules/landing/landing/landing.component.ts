import { Component, OnInit } from '@angular/core';

import { AboutMe } from 'src/app/data/AboutMe';
import { GeneralSettings } from 'src/app/data/GeneralSettings';
import { Project } from 'src/app/data/Project';
import { SkillGroup } from 'src/app/data/SkillGroup';
import { AboutMeService } from 'src/app/services/about-me/about-me.service';
import { ProjectsService } from 'src/app/services/projects/projects.service';
import { SettingsService } from 'src/app/services/settings/settings.service';
import { SkillsService } from 'src/app/services/skills/skills.service';

declare var particlesJS: any;

@Component({
  selector: 'app-landing',
  templateUrl: './landing.component.html',
  styleUrls: ['./landing.component.scss']
})
export class LandingComponent implements OnInit {

  finishedLoading: boolean = false;

  aboutMe: AboutMe | null = null;
  projects: Project[] | null = null;
  generalSettings: GeneralSettings | null = null;
  skillGroups: SkillGroup[] | null = null;

  constructor(private aboutMeService: AboutMeService, private projectService: ProjectsService, private settingsService: SettingsService, private skillService: SkillsService) { }

  ngOnInit(): void {

    let promises = [];

    promises.push(this.aboutMeService.get().toPromise().then((result) => {
      this.aboutMe = result;
    }));

    promises.push(this.projectService.get().toPromise().then((result) => {
      this.projects = result;
    }));

    promises.push(this.settingsService.get<GeneralSettings>('GeneralSettings').toPromise().then(result => {
      this.generalSettings = result;
    }));

    promises.push(this.skillService.getSkillGroups().toPromise().then((result) => {
      this.skillGroups = result;
    }));


    Promise.all(promises).then(() => {
      this.finishedLoading = true;
      setTimeout(this.initParticles, 5) //Add a small timeout to let the div render before making this call
    });
  }



  public initParticles(){
    particlesJS('particles-js',
  
  {
    "particles": {
      "number": {
        "value": 30,
        "density": {
          "enable": true,
          "value_area": 800
        }
      },
      "color": {
        "value": "#000"
      },
      "shape": {
        "type": "circle",
        "stroke": {
          "width": 0,
          "color": "#000000"
        },
        "polygon": {
          "nb_sides": 5
        },
        "image": {
          "src": "img/github.svg",
          "width": 100,
          "height": 100
        }
      },
      "opacity": {
        "value": 0.5,
        "random": false,
        "anim": {
          "enable": false,
          "speed": 1,
          "opacity_min": 0.1,
          "sync": false
        }
      },
      "size": {
        "value": 5,
        "random": true,
        "anim": {
          "enable": false,
          "speed": 40,
          "size_min": 0.1,
          "sync": false
        }
      },
      "line_linked": {
        "enable": true,
        "distance": 150,
        "color": "#000",
        "opacity": 0.4,
        "width": 1
      },
      "move": {
        "enable": true,
        "speed": 6,
        "direction": "none",
        "random": false,
        "straight": false,
        "out_mode": "out",
        "attract": {
          "enable": false,
          "rotateX": 600,
          "rotateY": 1200
        }
      }
    },
    "interactivity": {
      "detect_on": "canvas",
      "events": {
        "onhover": {
          "enable": true,
          "mode": "repulse"
        },
        "onclick": {
          "enable": true,
          "mode": "push"
        },
        "resize": true
      },
      "modes": {
        "grab": {
          "distance": 400,
          "line_linked": {
            "opacity": 1
          }
        },
        "bubble": {
          "distance": 400,
          "size": 40,
          "duration": 2,
          "opacity": 8,
          "speed": 3
        },
        "repulse": {
          "distance": 200
        },
        "push": {
          "particles_nb": 4
        },
        "remove": {
          "particles_nb": 2
        }
      }
    },
    "retina_detect": true,
    "config_demo": {
      "hide_card": false,
      "background_color": "#b61924",
      "background_image": "",
      "background_position": "50% 50%",
      "background_repeat": "no-repeat",
      "background_size": "cover"
    }
  }

);
  }

}
