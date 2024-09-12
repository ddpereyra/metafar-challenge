namespace metafar_challenge.DTOs
{
    public class HistoricDto
    {
        public string CardNumber { get; set; }
        public string AccountHolderName { get; set; }
        public string AccountNumber { get; set; }
        public decimal Amount { get; set; }
        public string TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
