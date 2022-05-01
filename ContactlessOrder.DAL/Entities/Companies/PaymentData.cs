namespace ContactlessOrder.DAL.Entities.Companies
{
    public class PaymentData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Bank { get; set; }
        public string Mfo { get; set; }
        public string LegalEntityCode { get; set; }
        public string CurrentAccount { get; set; }
        public string TaxNumber { get; set; }

        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
