﻿namespace RTS.Commands
{
    public abstract class AsyncCommandBase : CommandBase
    {
        private readonly Action<Exception>? _onException;

        private bool _isExecuting;

        private bool IsExecuting
        {
            get => _isExecuting;
            set
            {
                _isExecuting = value;
                OnCanExecuteChanged();
            }
        }

        protected AsyncCommandBase(Action<Exception>? onException = null)
        {
            _onException = onException;
        }

        public override bool CanExecute(object? parameter)
        {
            return !IsExecuting && base.CanExecute(parameter);
        }

        public override async void Execute(object? parameter)
        {
            IsExecuting = true;

            try
            {
                if (parameter != null) await ExecuteAsync(parameter);
            }
            catch (Exception ex)
            {
                _onException?.Invoke(ex);
            }

            IsExecuting = false;
        }

        protected abstract Task ExecuteAsync(object parameter);
    }
}