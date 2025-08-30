import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';

export interface CreateLinkRequest{
  name: string;
  platform: string;
  url: string;
  file: File;
}

@Injectable({
  providedIn: 'root'
})
export class LinkService {
  private readonly http = inject(HttpClient);
  private readonly apiUrl = 'http://localhost:8000'; 

  getLinks() : Observable<any>{
    return this.http.get(`${this.apiUrl}/api/Link`);
  }

  createLink(linkData: CreateLinkRequest): Observable<any>{
    const formData = new FormData();
    formData.append('name', linkData.name);
    formData.append('platform', linkData.platform);
    formData.append('url', linkData.url);
    formData.append('file', linkData.file);

    return this.http.post(`${this.apiUrl}/api/Link`, formData);
  }

  deleteLink(linkId:string): Observable<any>{
    return this.http.delete(`${this.apiUrl}/api/Link/${linkId}`);
  }

}
