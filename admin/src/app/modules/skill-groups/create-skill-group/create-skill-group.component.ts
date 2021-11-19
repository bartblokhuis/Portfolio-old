import { AfterViewInit, Component, ElementRef, EventEmitter, OnInit, Output, ViewChild } from '@angular/core';
import { FormControl } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { SkillGroup } from 'src/app/data/SkillGroup';
import { SkillGroupService } from 'src/app/services/skillgroup/skillgroup.service';

@Component({
  selector: 'app-create-skill-group',
  templateUrl: './create-skill-group.component.html',
  styleUrls: ['./create-skill-group.component.scss']
})
export class CreateSkillGroupComponent implements OnInit {

  @ViewChild('titleControl') titleControl: ElementRef;

  @Output() onCreated: EventEmitter<any> = new EventEmitter();
  title: FormControl = new FormControl('');

  constructor(private skillGroupService: SkillGroupService, private toastr: ToastrService) { }
  
  ngAfterViewInit(): void {
    this.titleControl.nativeElement.focus();
    
  }

  ngOnInit(): void {
  }

  submit(openNewSkillModal: boolean): void {
    const newSkillgroup: SkillGroup = {
      id: 0,
      displayNumber: 0,
      title: this.title.value,
      skills: null
    }

    this.skillGroupService.createSkillGroup(newSkillgroup).subscribe((skillGroup) => {
      this.toastr.success("Saved skill group " + skillGroup.title);
      this.onCreated.emit([skillGroup, openNewSkillModal]);
    })
  }

}
