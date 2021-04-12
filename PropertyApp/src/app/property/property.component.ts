import { Component, OnInit } from '@angular/core';
import { Property } from './property';
import {PropertyApiService} from '../property-api.service'

@Component({
  selector: 'app-property',
  templateUrl: './property.component.html',
  styleUrls: ['./property.component.scss']
})
export class PropertyComponent implements OnInit {
  selectedProperty?: Property;
  properties: any;

  constructor(private propertApi: PropertyApiService) {

  }

  ngOnInit(): void {
    this.propertApi.GetProperties().subscribe(data => {
      this.properties = data;
    })
  }

  saveProperties(event: any, row: any){
    event.target.disable = true;
    this.propertApi.SaveProperties(event, row);
}

}
