using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientService.Application.DTOs;
public record PredictionDto(string Prediction, double Confidence);
