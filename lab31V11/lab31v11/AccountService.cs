namespace lab31v11;

// 1. Інтерфейс репозиторію 
public interface IAccountRepository
{
    decimal GetBalance(int accountId);
    void UpdateBalance(int accountId, decimal newBalance);
    bool AccountExists(int accountId);
}

// 2. Інтерфейс логера 
public interface IAuditLogger
{
    void LogTransaction(int accountId, string message);
    void LogError(string error);
}

// 3. Основний сервіс 
public class AccountService
{
    private readonly IAccountRepository _repository;
    private readonly IAuditLogger _logger;

    // Впровадження залежностей через конструктор
    public AccountService(IAccountRepository repository, IAuditLogger logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public bool TransferFunds(int fromId, int toId, decimal amount)
    {
        if (amount <= 0)
        {
            _logger.LogError("Transfer amount must be positive");
            return false;
        }

        if (!_repository.AccountExists(fromId) || !_repository.AccountExists(toId))
        {
            _logger.LogError("One or both accounts do not exist");
            return false;
        }

        decimal fromBalance = _repository.GetBalance(fromId);
        if (fromBalance < amount)
        {
            _logger.LogError("Insufficient funds");
            return false;
        }

        // Логіка переказу
        _repository.UpdateBalance(fromId, fromBalance - amount);
        _repository.UpdateBalance(toId, _repository.GetBalance(toId) + amount);
        
        _logger.LogTransaction(fromId, $"Transferred {amount} to {toId}");
        
        return true;
    }
}