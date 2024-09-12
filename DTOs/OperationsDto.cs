namespace metafar_challenge.DTOs
{
    public class BalanceDto
    {
        public string AccountHolderName { get; set; }
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public DateTime? LastWithdrawalDate { get; set; }
    }
}
