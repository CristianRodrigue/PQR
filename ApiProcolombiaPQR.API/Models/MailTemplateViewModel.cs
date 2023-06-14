namespace ApiProcolombiaPQR.API.Models
{
    public class MailTemplateViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Html { get; set; }

        public bool Enabled { get; set; }
    }
}
