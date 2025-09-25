using Bogus;

namespace TestingRestApiDemo.Models;

public class CreatePetRequest
{
    public Category Category { get; set; } = default!;
    public string Name { get; set; } = default!;
    public List<string> PhotoUrls { get; set; } = [];
    public List<Tag> Tags { get; set; } = [];
    public string Status { get; set; } = default!;

    public static CreatePetRequest Random()
    {
        var faker = new Faker();

        return new CreatePetRequest
        {
            Category = new Category { Id = 0, Name = faker.Commerce.ProductName() },
            Name = faker.Person.FullName,
            PhotoUrls = [faker.Internet.Url(), faker.Internet.Url()],
            Tags = [ new Tag { Id = 0, Name = faker.Random.AlphaNumeric(4) } ],
            Status = "Available"
        };
    }
}

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class Tag
{
    public int Id { get; set; }
    public string Name { get; set; }
}
