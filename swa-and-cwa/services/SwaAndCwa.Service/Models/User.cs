using System.Runtime.Serialization;

namespace SwaAndCwa.Service.Models;

public class User
{
    [DataMember(Name="id")]
    public int Id { get; set; }

    [DataMember(Name="name")]
    public string Name { get; set; }
}