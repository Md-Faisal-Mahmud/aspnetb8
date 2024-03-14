import { Component, Input } from '@angular/core';
import { ICourse } from '../../data/ICourse';

@Component({
  selector: 'app-course',
  templateUrl: './course.component.html',
  styleUrls: ['./course.component.css']
})
export class CourseComponent {
  @Input() courses : ICourse[] = [];
}
