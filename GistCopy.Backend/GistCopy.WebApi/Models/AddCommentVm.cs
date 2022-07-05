namespace GistCopy.WebApi.Models;

public class AddCommentVm
{
    public Guid GistId { get; set; }
    public string Text { get; set; }
}