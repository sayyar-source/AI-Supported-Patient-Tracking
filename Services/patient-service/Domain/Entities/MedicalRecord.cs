using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientService.Domain.Entities;
public class MedicalRecord:BaseEntity
{
    public int PatientId { get;  set; }
    public string DoctorRemarks { get;  set; }
    public DateTime RecordDate { get;  set; }

    public MedicalRecord(string doctorRemarks, DateTime recordDate)
    {
        DoctorRemarks = doctorRemarks;
        RecordDate = recordDate;
    }

    // For EF Core
    private MedicalRecord() { }
}