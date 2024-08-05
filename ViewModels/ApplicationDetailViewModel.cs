using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using RTS.Models;
using RTS.Services.Interfaces;

namespace RTS.ViewModels;
public class ApplicationDetailViewModel : ViewModelBase

    {
        private readonly IDataService<JobApplication> _dataService;
        private JobApplication _jobApplication;
        public JobApplication JobApplication
        {
            get => _jobApplication;
            set
            {
                _jobApplication = value;
                OnPropertyChanged(nameof(JobApplication));
                OnPropertyChanged(nameof(VacancyTitle));
                OnPropertyChanged(nameof(CandidateName));
                OnPropertyChanged(nameof(ApplicationStageName));
                OnPropertyChanged(nameof(RecruiterName));
            }
        }
        public string VacancyTitle => JobApplication?.Vacancy?.JobTitle;
        public string CandidateName => JobApplication?.Candidate?.Name;
        public string ApplicationStageName => JobApplication?.ApplicationStage?.ApplicationStageName;
        public string RecruiterName => JobApplication?.Vacancy?.Recruiter?.Name;
        public ApplicationDetailViewModel(IDataService<JobApplication> dataService)
        {
            _dataService = dataService;
        }

        public async Task LoadApplicationDetails(int applicationId)
        {
            JobApplication = await _dataService.GetById(applicationId, query => query
                
                .Include(ja => ja.Candidate)
                .Include(ja => ja.ApplicationStage)
                .Include(ja => ja.Vacancy)
                .ThenInclude(v => v.Recruiter));
            OnPropertyChanged(nameof(JobApplication));
        }
    }


