using RTS.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RTS.Stores
{
    public class AccountStore
    {
        private Recruiter _currentAccount;
        public Recruiter CurrentAccount
        {
            get => _currentAccount;
            set
            {
                _currentAccount = value;
                CurrentAccountChanged?.Invoke();
            }
        }

        public bool IsLoggedIn => CurrentAccount != null;

        public event Action CurrentAccountChanged;

        public void Logout()
        {
            CurrentAccount = null;
        }
    }
}
