namespace metafar_challenge.Models
{
    public class BankAccount
    {
        public int Id { get; set; }
        public string AccountHolderName { get; set; }
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }

        public ICollection<Card> Cards { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}
