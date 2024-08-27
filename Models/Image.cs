namespace Back.Models;

public partial class Image
{
    public int Id { get; set; }

    public int Author { get; set; }

    public string Url { get; set; } = null!;

    public DateTime Uploaded { get; set; }

    public string? Description { get; set; }

    public virtual User AuthorNavigation { get; set; } = null!;

    public virtual ICollection<ImageHashtag> ImageHashtags { get; set; } = new List<ImageHashtag>();
}

public class ImageBuilder()
{
    private int _id;
    private int _author;
    private string _url = null!;
    private DateTime _uploaded;
    private string? _description;

    public ImageBuilder SetId(int id)
    {
        _id = id;
        return this;
    }

    public ImageBuilder SetAuthor(int author)
    {
        _author = author;
        return this;
    }

    public ImageBuilder SetUrl(string url)
    {
        _url = url;
        return this;
    }

    public ImageBuilder SetUploaded(DateTime uploaded)
    {
        _uploaded = uploaded;
        return this;
    }

    public ImageBuilder SetDescription(string? description)
    {
        _description = description;
        return this;
    }

    public Image Build()
    {
        return new Image
        {
            Id = _id,
            Author = _author,
            Url = _url,
            Uploaded = _uploaded,
            Description = _description,
            AuthorNavigation = null!,
            ImageHashtags = new List<ImageHashtag>()
        };
    }
}
