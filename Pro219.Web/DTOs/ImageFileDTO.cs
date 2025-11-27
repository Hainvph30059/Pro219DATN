using Microsoft.AspNetCore.Components.Forms;

namespace Pro219.Web.DTOs
{
    public class ImageFileDTO
    {
        public IBrowserFile File { get; set; } = default!;
        public string Url { get; set; } = string.Empty;
    }
}
