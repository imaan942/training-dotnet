namespace fleetmanagement.entity
{
public class Driver
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string License { get; set; }
    public string? Details { get; set; }

    public Driver(string name, string license, string details){
        Name = name;
        License = license;
        Details = details;
    }
}
}
