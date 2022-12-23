using System.Numerics;

namespace ProfileEditor.Core.DTOs;

public class ProfileEditDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string City { get; set; }
    public string Region { get; set; }
    public string Address { get; set; }
    public string Address1 { get; set; }
    public string PostalCode { get; set; }
    public string CountryCode { get; set; }
    public MobilePhoneDto MobilePhone { get; set; }
    public PhoneDto Phone { get; set; }
    public string CompanyName { get; set; }
}
