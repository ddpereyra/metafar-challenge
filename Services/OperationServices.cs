using metafar_challenge.DTOs;
using metafar_challenge.Models;
using metafar_challenge.Services.Interfaces;
using static metafar_challenge.Enums.Types;

namespace metafar_challenge.Services
{
    public class OperationService : IOperationService
    {
        private readonly ICardService _cardService;
        private readonly ITransactionService _transactionService;
        private readonly IConfiguration _configuration;

        public OperationService(IConfiguration configuration, ICardService cardService, ITransactionService transactionService)
        {
            _cardService = cardService;
            _transactionService = transactionService;
            _configuration = configuration;
        }

        public async Task<BalanceDto> GetBalance(string cardNumber)
        {
            Card card = await _cardService.GetValidatedCard(cardNumber);

            return new BalanceDto
            {
                AccountHolderName = card.BankAccount.AccountHolderName,
                AccountNumber = card.BankAccount.AccountNumber,
                Balance = card.BankAccount.Balance,
                LastExtractionlDate = card.BankAccount.Transactions.OrderByDescending(dt => dt.TransactionDate).Select(dt => dt.TransactionDate).FirstOrDefault(),
            };
        }

        public async Task<ResumeOutDto> Operation(string cardNumber, decimal amount)
        {
            Card card = await _cardService.GetValidatedCard(cardNumber);

            if (amount < 0 && card.BankAccount.Balance < amount)
            {
                throw new Exception("Insufficient balance.");
            }

            card.BankAccount.Balance += amount;

            Transaction transaction = newTransaction(amount);
            transaction.BankAccount = card.BankAccount;
            await _transactionService.AddTransaction(transaction);

            await _cardService.UpdateDb(card);

            return new ResumeOutDto
            {
                AccountNumber = card.BankAccount.AccountNumber,
                Amount = amount,
                NewBalance = card.BankAccount.Balance,
                TransactionDate = transaction.TransactionDate,
                TransactionType = amount < 0 ? TransactionType.Withdrawal.ToString() : TransactionType.Withdrawal.ToString(),
            };
        }

        public async Task<PaginatedResult<PaginatedOperations>> GetPaginatedHistory(string cardNumber)
        {
            Card card = await _cardService.GetValidatedCard(cardNumber);

            List<HistoricDto> allOperations = card.BankAccount.Transactions
                                   .OrderByDescending(t => t.TransactionDate)
                                   .Select(t => new HistoricDto
                                   {
                                       CardNumber = cardNumber,
                                       AccountHolderName = card.BankAccount.AccountHolderName,
                                       AccountNumber = card.BankAccount.AccountNumber,
                                       Amount = t.Amount,
                                       TransactionDate = t.TransactionDate,
                                       TransactionType = t.Type.ToString(),
                                   })
                                   .ToList();

            int totalOperations = allOperations.Count;

            int pageSize = int.Parse(_configuration["PageSize"]);
            int totalPages = (int)Math.Ceiling((double)totalOperations / pageSize);

            List<PaginatedOperations> paginatedOperations = new List<PaginatedOperations>();

            for (int i = 0; i < totalPages; i++)
            {
                List<HistoricDto> operationsForPage = allOperations
                                        .Skip(i * pageSize)
                                        .Take(pageSize)
                                        .ToList();

                PaginatedOperations paginatedOps = new PaginatedOperations
                {
                    Operations = operationsForPage,
                    Page = i + 1,
                };

                paginatedOperations.Add(paginatedOps);
            }

            return new PaginatedResult<PaginatedOperations>
            {
                Data = paginatedOperations,
                TotalCount = totalOperations
            };
        }


        private Transaction newTransaction (decimal amount)
        {
            return new Transaction
            {
                Amount = amount,
                TransactionDate = DateTime.UtcNow,
                Type = amount < 0 ? TransactionType.Withdrawal : TransactionType.Deposit,
            };
        }
    }
}
