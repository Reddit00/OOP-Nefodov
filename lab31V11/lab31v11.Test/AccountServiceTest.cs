using Moq;
using Xunit;
using lab31v11;

namespace lab31v11.Tests;

public class AccountServiceTests
{
    private readonly Mock<IAccountRepository> _repoMock;
    private readonly Mock<IAuditLogger> _loggerMock;
    private readonly AccountService _service;

    public AccountServiceTests()
    {
        _repoMock = new Mock<IAccountRepository>();
        _loggerMock = new Mock<IAuditLogger>();
        _service = new AccountService(_repoMock.Object, _loggerMock.Object);
    }

    [Fact]
    public void Transfer_ValidData_ReturnsTrueAndCallsUpdate()
    {
        // Arrange 
        _repoMock.Setup(r => r.AccountExists(It.IsAny<int>())).Returns(true);
        _repoMock.Setup(r => r.GetBalance(1)).Returns(500m);
        _repoMock.Setup(r => r.GetBalance(2)).Returns(100m);

        // Act
        var result = _service.TransferFunds(1, 2, 200m);

        // Assert
        Assert.True(result);
        // Перевіряємо, що баланси оновлюються правильно
        _repoMock.Verify(r => r.UpdateBalance(1, 300m), Times.Once);
        _repoMock.Verify(r => r.UpdateBalance(2, 300m), Times.Once);
        _loggerMock.Verify(l => l.LogTransaction(1, It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public void Transfer_InsufficientFunds_ReturnsFalseAndLogsError()
    {
        // Arrange
        _repoMock.Setup(r => r.AccountExists(It.IsAny<int>())).Returns(true);
        _repoMock.Setup(r => r.GetBalance(1)).Returns(50m); // Мало грошей

        // Act
        var result = _service.TransferFunds(1, 2, 100m);

        // Assert
        Assert.False(result);
        _loggerMock.Verify(l => l.LogError("Insufficient funds"), Times.Once);
        _repoMock.Verify(r => r.UpdateBalance(It.IsAny<int>(), It.IsAny<decimal>()), Times.Never);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-100)]
    public void Transfer_NegativeOrZeroAmount_ReturnsFalse(decimal amount)
    {
        var result = _service.TransferFunds(1, 2, amount);
        Assert.False(result);
        _loggerMock.Verify(l => l.LogError(It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public void Transfer_AccountNotFound_ReturnsFalse()
    {
        _repoMock.Setup(r => r.AccountExists(It.IsAny<int>())).Returns(false);

        var result = _service.TransferFunds(1, 2, 100m);

        Assert.False(result);
        _loggerMock.Verify(l => l.LogError("One or both accounts do not exist"), Times.Once);
    }

    // Додатковий тест для перевірки конкретного повідомлення в логах
    [Fact]
    public void Transfer_Success_LogsSpecificMessage()
    {
        _repoMock.Setup(r => r.AccountExists(It.IsAny<int>())).Returns(true);
        _repoMock.Setup(r => r.GetBalance(It.IsAny<int>())).Returns(1000m);

        _service.TransferFunds(1, 2, 500m);

        _loggerMock.Verify(l => l.LogTransaction(1, "Transferred 500 to 2"), Times.Once);
    }
}