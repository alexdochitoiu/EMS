export class Incident {
  Latitude: number;
  Longitude: number;
  Severity: string;
  Title: string;
  Description: string;
  ReporterName: string;
  IncidentId: string;

  public constructor(lat: number, long: number, severity: string,
    title: string, description: string, reporterName: string, incidentId: string) {
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

export class Hospital {
  Latitude: number;
  Longitude: number;
  Name: string;
  Vicinity: string;

  public constructor(lat: number, long: number, name: string, vicinity: string) {
    this.Latitude = lat;
    this.Longitude = long;
    this.Name = name;
    this.Vicinity = vicinity;
  }
}
