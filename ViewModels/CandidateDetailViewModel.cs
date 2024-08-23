using System;
using System.Collections.Generic;
using System.Text;

namespace RTS.ViewModels
{
    public class CandidateDetailViewModel : ViewModelBase
    {
        public string Name { get; }

        public CandidateDetailViewModel(string name)
        {
            Name = name;
        }
    }
}
