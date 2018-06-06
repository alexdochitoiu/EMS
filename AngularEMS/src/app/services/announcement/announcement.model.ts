
export class Announcement {
  public Id: string;
  public Title: string;
  public Description: string;
  public AuthorName: string;
  public AuthorUsername: string;
  public PostedAt: string;
  public Severity: number;

  public constructor(id, title, description, author, postedAt, severity) {
    this.Id = id;
    this.Title = title;
    this.Description = description;
    this.AuthorName = `${author.firstName} ${author.lastName}`;
    this.AuthorUsername = author.username;
    this.PostedAt = postedAt;
    this.Severity = severity;
  }
}
