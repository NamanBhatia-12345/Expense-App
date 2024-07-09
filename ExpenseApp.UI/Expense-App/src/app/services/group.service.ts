import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class GroupService {

  private apiUrl = 'https://localhost:7011/api/group';

  constructor(private http:HttpClient) { }

  getGroups():Observable<any>{
    let url = `${this.apiUrl}/get-all-groups`
    return this.http.get<any>(url,{responseType:'json'});
  }

  getGroupById(groupId:number):Observable<any>{
    let url = `${this.apiUrl}/${groupId}`
    return this.http.get<any>(url,{responseType : 'json'});
  }

  createGroup(group:any):Observable<any>{
    let url = `${this.apiUrl}/create-group`
    return this.http.post<any>(url,group,{responseType:'json'});
  }

  addMembers(groupId:number):Observable<any>{
    let url = `${this.apiUrl}/add-members/${groupId}`;
    return this.http.post<any>(url,{},{responseType:'json'});
  }

  deleteGroup(groupId:number):Observable<any>{
    let url = `${this.apiUrl}/delete-group/${groupId}`;
    return this.http.delete<any>(url,{responseType:'json'});
  }
}
