import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ImageService {
  private readonly http = inject(HttpClient);

  private readonly apiUrl = 'http://localhost:8000'; 


  getImage(imageId:string){
    return this.http.get(`${this.apiUrl}/api/Image/${imageId}`,{responseType: 'blob'});
  }
}
