namespace ProfileEditor.Core.DTOs;
public class ProfileDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public bool FirstLogin { get; set; }
    public MobilePhoneDto MobilePhone { get; set; }
    public PhoneDto Phone { get; set; }
    public string CompanyName { get; set; }
    public List<FileDto> Files { get; set; }
    public List<InterviewDto> Interviews { get; set; }
    public List<AddressDto> Addresses { get; set; }
}


