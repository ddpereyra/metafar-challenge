using static metafar_challenge.Enums.Types;

namespace metafar_challenge.DTOs
{
    public class BalanceDto
    {
        public string AccountHolderName { get; set; }
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public DateTime? LastExtractionlDate { get; set; }
    }

    public class OperationInDto
    {
        public string CardNumber { get; set; }
        public decimal Amount { get; set; }
    }

    public class ResumeOutDto
    {
        public string AccountNumber { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransactionType { get; set; }
        public decimal NewBalance { get; set; }
    }
}
