using System.Collections.ObjectModel;
using RTS.Models;
using RTS.Services;

namespace RTS.ViewModels
{
    public class VacancyDetailViewModel : ViewModelBase

    {
        private readonly Vacancy _vacancy;

        public string JobTitle => _vacancy.JobTitle;

        public string Description => _vacancy.Description;

        /*public string Location => _vacancy.Location;
        public string EmploymentType => _vacancy.EmploymentType;
        public string EducationReq => _vacancy.EducationReq;
        public string ExperienceReq => _vacancy.ExperienceReq;*/
        public string Status => _vacancy.Status;
        /*public string DatePosted => _vacancy.DatePosted.ToString();*/


        public VacancyDetailViewModel(Vacancy vacancy)
        {
            _vacancy = vacancy;
        }
    }
}