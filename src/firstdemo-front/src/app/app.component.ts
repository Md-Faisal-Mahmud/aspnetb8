import { Component } from '@angular/core';
import { ICourse } from './data/ICourse';
import { CourseService } from './services/course.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'firstdemo-front';
  courses: ICourse[] = [];
  private username: string = "jalal.exe@gmail.com";
  private password: string = "DevSkill%231";

  constructor(private courseService: CourseService){ }

  onClick(){
    console.log("button clicked");
   }

   onClick2 (){
    console.log("button clicked 2");
   }

   update(){
	  this.courseService.getToken(this.username, this.password).subscribe((token:string) =>
  	(
    	this.courseService.getCourses(token).subscribe((data:ICourse[]) =>
      	(this.courses = data)
    	)
  	)
	);

  }

  
}
