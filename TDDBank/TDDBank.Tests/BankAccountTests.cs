namespace TDDBank.Tests
{
    public class BankAccountTests
    {
        [Fact]
        public void New_account_should_have_0_as_Balance()
        {
            var acc = new BankAccount();

            Assert.Equal(0m, acc.Balance);
        }

        [Fact]
        public void Can_deposit()
        {
            var acc = new BankAccount();

            acc.Deposit(3m);
            Assert.Equal(3m, acc.Balance);

            acc.Deposit(7m);
            Assert.Equal(10m, acc.Balance);
        }

        [Fact]
        public void Deposit_a_negative_or_zero_value_should_throws_ArgumentEx()
        {
            var acc = new BankAccount();

            Assert.Throws<ArgumentException>(() => acc.Deposit(-1));
            Assert.Throws<ArgumentException>(() => acc.Deposit(0));
        }

        [Fact]
        public void Can_withdraw()
        {
            var acc = new BankAccount();
            acc.Deposit(12m);

            acc.Withdraw(4m);
            Assert.Equal(8m, acc.Balance);

            acc.Withdraw(8m);
            Assert.Equal(0m, acc.Balance);
        }

        [Fact]
        public void Withdraw_a_negative_or_zero_value_should_throws_ArgumentEx()
        {
            var acc = new BankAccount();
            acc.Deposit(12m);

            Assert.Throws<ArgumentException>(() => acc.Withdraw(-1));
            Assert.Throws<ArgumentException>(() => acc.Withdraw(0));
        }

        [Fact]
        public void Withdraw_below_balance_throws_InvalidOpEx()
        {
            var acc = new BankAccount();
            acc.Deposit(12m);

            Assert.Throws<InvalidOperationException>(() => acc.Withdraw(13));
        }

    }
}