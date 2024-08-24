using System.Windows.Input;
using RTS.Commands;
using RTS.Models;
using RTS.Services;
using RTS.Stores;

namespace RTS.ViewModels
{
    public class AddVacancyViewModel : ViewModelBase
    {
        private readonly IDataService<Vacancy> _vacancyDataService;

        private string _jobTitle = null!;

        public string JobTitle
        {
            get => _jobTitle;
            set
            {
                _jobTitle = value;
                OnPropertyChanged(nameof(JobTitle));
            }
        }

        private string _description = null!;

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        private string _status = null!;

        public string Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
            }
        }

        private string _eduReq = null!;

        public string EduReq
        {
            get => _eduReq;
            set
            {
                _eduReq = value;
                OnPropertyChanged(nameof(EduReq));
            }
        }
        private DateTime _datePosted = DateTime.Now;
        public DateTime DatePosted
        {
            get => _datePosted;
            set
            {
                _datePosted = value;
                OnPropertyChanged(nameof(DatePosted));
            }
        }
        private string _experienceReq = null!;
        public string ExperienceReq
        {
            get => _experienceReq;
            set
            {
                _experienceReq = value;
                OnPropertyChanged(nameof(ExperienceReq));
            }
        }
        private string _location = null!;
        public string Location
        {
            get => _location;
            set
            {
                _location = value;
                OnPropertyChanged(nameof(Location));
            }
        }
        private string _employmentType = null!;
        public string EmploymentType
        {
            get => _employmentType;
            set
            {
                _employmentType = value;
                OnPropertyChanged(nameof(EmploymentType));
            }
        }
        
        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }

        public AddVacancyViewModel(IDataService<Vacancy> vacancyDataService, INavigationService closeNavigationService)
        {
            _vacancyDataService = vacancyDataService;
            SubmitCommand = new AddVacancyCommand(this, closeNavigationService);
            CancelCommand = new NavigateCommand(closeNavigationService);
        }

        public async Task AddVacancy(Vacancy vacancy)
        {
            await _vacancyDataService.Create(vacancy);
        }
    }
}
