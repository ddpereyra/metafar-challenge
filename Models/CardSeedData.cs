namespace metafar_challenge.Models
{
    public class CardSeedData
    {
        public string CardNumber { get; set; }
        public string Pin { get; set; }
        public bool IsBlocked { get; set; }
        public int FailedAttempts { get; set; }
    }
}
