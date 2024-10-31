import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Planes } from 'src/app/Models/Planes';
import { MessageService } from 'src/app/Service/message.service';
import { PlaneService } from 'src/app/Service/plane.service';

@Component({
  selector: 'app-plane-update',
  templateUrl: './plane-update.component.html',
  styleUrls: ['./plane-update.component.css']
})
export class PlaneUpdateComponent implements OnInit {

  planes: Planes[];
  plane: Planes | undefined;
  Planes: Planes | undefined;
  model : any = {};
  message : string;
  messageColor: string;

  constructor(
    private planeService: PlaneService,
    private router: Router,
    private route: ActivatedRoute,
    private messageService: MessageService
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.planeService.getPlaneById(+id).subscribe(plane => {
        this.plane = plane;
        this.Planes = { ...plane };
      });
    }
    this.messageService.currentMessage.subscribe(messageObj => {
      this.message = messageObj.text;
      this.messageColor = messageObj.color;
    });
  }
  updatePlane(): void {
    if (this.plane) {
      this.planeService.updatePlane(this.plane).subscribe(
        response => {
          console.log('Güncelleme başarılı', response);
          this.messageService.changeMessage('Başarıyla güncellendi.', 'warning');
          alert("Güncelleme işlemi başarılı.");
          this.router.navigate(['/planes']);
        },
        error => {
          console.error('Güncelleme işlemi başarısız oldu', error);
          alert('Güncelleme işlemi başarısız oldu.');
        }
      );
    }
  }


}
