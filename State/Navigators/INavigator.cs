using RTS.ViewModels;

namespace RTS.State.Navigators
{
    public enum ViewType
    {
        Home,
        VacancyList,
        VacancySingle,
        ApplicationList,
        ApplicationSingle,
        CandidateList,
        CandidateSingle
    }
    public interface INavigator
    {
        ViewModelBase CurrentViewModel { get; set; }
        event Action StateChanged;
    }

}