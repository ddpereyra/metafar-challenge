namespace metafar_challenge.Models
{
    public class Card
    {
        public int Id { get; set; }
        public string CardNumber { get; set; }
        public string Pin { get; set; }
        public bool IsBlocked { get; set; }
        public int FailedAttempts { get; set; }

        // Foreign Key
        public int BankAccountId { get; set; }
        public BankAccount BankAccount { get; set; }
    }
}
