namespace BackEnd.API.Modules;

public sealed class Student
{
    private Student(string name, string address, string phoneNumber, string email)
    {
        Id = Guid.NewGuid();
        Name = name;
        Address = address;
        PhoneNumber = phoneNumber;
        Email = email;
    }

    private Student() { }

    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Address { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public string Email { get; set; } = default!;
}
