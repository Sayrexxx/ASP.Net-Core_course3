namespace WEB_253504_RESHETNEV.Domain.Entities;

public class Book
{
    public int Id { get; set; }
    public string? Name { get; set; } 
    public string? Description { get; set; }
    public Genre? Genre { get; set; } 
    public int PageCount { get; set; }
    public string? ImagePath { get; set; }
}