namespace ApiProcolombiaPQR.API.Models
{
    public class FileViewModel
    {
        public Guid Id { get; set; }
        public int height { get; set; }

        public DateTime timestamp { get; set; }

        public string? uri { get; set; }

        public string? fileName { get; set; }

        public byte[]? data { get; set; }

    }
}
