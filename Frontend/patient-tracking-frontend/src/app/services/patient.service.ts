import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class PatientService {
  private patientApiUrl = `${environment.patientsApiUrl}/api/patients`;
  private predictionApiUrl = `${environment.predictionUrl}`;

  constructor(private http: HttpClient) {}

  private getHeaders(): HttpHeaders {
    const token = localStorage.getItem('token');
    return new HttpHeaders().set('Authorization', `Bearer ${token}`);
  }

  getPatients(): Observable<any[]> {
    const patients = this.http.get<any[]>(this.patientApiUrl, { headers: this.getHeaders() });
    return patients;
  }

  getPatient(id: string): Observable<any> {
    const patient = this.http.get<any>(`${this.patientApiUrl}/${id}`, {
      headers: this.getHeaders(),
    });
    return patient;
  }

  createPatient(patient: any): Observable<any> {
    return this.http.post<any>(this.patientApiUrl, patient, { headers: this.getHeaders() });
  }

  deletePatient(id: number): Observable<void> {
    return this.http.delete<void>(`${this.patientApiUrl}/${id}`, { headers: this.getHeaders() });
  }

  getPrediction(id: string): Observable<any> {
    const url = `${this.predictionApiUrl}/${id}`;
    return this.http.get<any>(url, { headers: this.getHeaders() });
  }
}
