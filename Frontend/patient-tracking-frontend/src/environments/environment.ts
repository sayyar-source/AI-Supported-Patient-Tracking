const SERVICES = {
  production: false,
  authServiceUrl: 'http://localhost:5000/api',
  patientServiceUrl: 'http://localhost:4000',
  predictionServiceUrl: 'http://localhost:4000/api/Patients/prediction',
};

export const environment = {
  production: false,
  authApiUrl: SERVICES.authServiceUrl,
  patientsApiUrl: SERVICES.patientServiceUrl,
  predictionUrl: SERVICES.predictionServiceUrl,
};
