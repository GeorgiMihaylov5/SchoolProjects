using System;
using BankAccount;
using NUnit.Framework;

namespace BankAccount.Tests
{
    [TestFixture]
    public class BankAccountTest
    {
        [Test]
        public void AccountInitilizeWithPositiveValue()
        {
            BankAccount account = new BankAccount(2000m);

            Assert.AreEqual(2000m,account.Balance);
        }
        [Test]
        public void DepositShoulAddMoney()
        {
            BankAccount account = new BankAccount();
            account.Deposit(50);

            Assert.IsTrue(account.Balance==50);
        }
        [Test]
        public void IsCreditWorkCorrecly()
        {
            BankAccount account = new BankAccount(2000m);
            account.Credit(500m);

            Assert.AreEqual(1500m,account.Balance);
        }
        [Test]
        public void IsIncreaseWorkCorrecly()
        {
            BankAccount account = new BankAccount(2000m);
            account.Increase(10);

            Assert.AreEqual(2200m, account.Balance);
        }
        [Test]
        public void IsBonusWorkCorreclyBetween1000And2000()
        {
            BankAccount account = new BankAccount(1200m);
            account.Bonus();
            Assert.AreEqual(1300m,account.Balance);
        }
        [Test]
        public void IsBonusWorkCorreclyBetween2000And3000()
        {
            BankAccount account = new BankAccount(2200m);
            account.Bonus();
            Assert.AreEqual(2400m, account.Balance);
        }
        [Test]
        public void IsBonusWorkCorreclyAfter3000()
        {
            BankAccount account = new BankAccount(3200m);
            account.Bonus();
            Assert.AreEqual(3500m, account.Balance);
        }
    }
}
