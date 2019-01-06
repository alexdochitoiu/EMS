export class Incident {
  Latitude: number;
  Longitude: number;
  Severity: string;
  Title: string;
  Description: string;
  ReporterName: string;
  IncidentId: string;

  public constructor(lat, long, severity, title, description, reporterName, incidentId) {
    this.Latitude = lat;
    this.Longitude = long;
    this.Severity = severity;
    this.Title = title;
    this.Description = description;
    this.ReporterName = reporterName;
    this.IncidentId = incidentId;
  }
}

export class CreateIncidentModel {
  Summary: string;
  Description: string;
  Severity: number;
  Latitude: number;
  Longitude: number;
  ReporterId: any;
}
