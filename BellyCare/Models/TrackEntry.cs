namespace BellyCare.Models
{
    public class TrackEntry
    {
        public DateTime Date { get; set; }
        public string FormattedDate => string.Format("{0:dd} de {0:MMMM} del {0:yyyy}", Date);
        public double? Weight { get; set; }
        public double? BellySize { get; set; }
        public double? HeartRate { get; set; }
        public double? RespiratoryRate { get; set; }
        public double? OxygenSaturation { get; set; }
        public double? BloodPressureMin { get; set; }
        public double? BloodPressureMax { get; set; }
        public double? Hemoglobin { get; set; }
        public double? Glucose { get; set; }
        public double? Temperature { get; set; }
        public double? AbdominalCircumference { get; set; }
        public string LabResults { get; set; }
        public bool? VdrlTest { get; set; }
        public bool? VdrlResult { get; set; }
        public DateTime? VdrlDate { get; set; }
        public string Treatment { get; set; }
        public string Note { get; set; }
        public double? IMC { get; set; }
    }

    public class FirebaseTrackEntry
    {
        public string Id { get; set; }
        public TrackEntry TrackEntry { get; set; }
    }
}