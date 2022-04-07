using System;
using System.Collections.Generic;
using System.Text;

namespace BankAccount
{
    public class BankAccount
    {
        public decimal Balance { get; set; }

        public BankAccount(decimal amount=0)
        {
            this.Balance = amount;
        }
        public void Deposit(decimal cash)
        {
            this.Balance += cash;
        }
        public void Credit(decimal cash)
        {
            this.Balance -= cash;
        }
        public void Increase(decimal percent)
        {
            this.Balance += this.Balance / 100 * percent;
        }
        public void Bonus()
        {
            if (this.Balance > 1000 && this.Balance < 2000) this.Balance += 100;
            if (this.Balance >= 2000 && this.Balance <= 3000) this.Balance += 200;
            if (this.Balance > 3000) this.Balance += 300;
        }
    }
}
