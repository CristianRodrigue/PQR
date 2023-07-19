namespace ApiProcolombiaPQR.API.Models
{
    public class UpdateMailTemplateViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Html { get; set; }

        public bool Enabled { get; set; }

        public string? Message { get; set; }
    }
}

