namespace GistCopy.WebApi.Models;

public class CreateGistVm
{
    public string Description { get; set; } = null!;
    public string Filename { get; set; } = null!;
    public string Text { get; set; } = null!;
    public bool Private { get; set; }
}