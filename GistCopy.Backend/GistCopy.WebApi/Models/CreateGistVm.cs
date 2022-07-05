namespace GistCopy.WebApi.Models;

public class CreateGistVm
{
    public string Description { get; set; }
    public string Filename { get; set; }
    public string Text { get; set; }
}