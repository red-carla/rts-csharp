using RTS.ViewModels.Navigators;

namespace RTS.ViewModels;

public class ViewModelFactory: IViewModelFactory
{
    private readonly CreateViewModel<HomeViewModel> _createHomeViewModel;
    private readonly CreateViewModel<VacancyListViewModel> _createVacancyListViewModel;
    private readonly CreateViewModel<ApplicationListViewModel> _createApplicationListViewModel;
    private readonly CreateViewModel<CandidateListViewModel> _createCandidateListViewModel;

    public ViewModelFactory(CreateViewModel<HomeViewModel> createHomeViewModel, CreateViewModel<VacancyListViewModel> createVacancyListViewModel, CreateViewModel<ApplicationListViewModel> createApplicationListViewModel, CreateViewModel<CandidateListViewModel> createCandidateListViewModel)
    {
        _createHomeViewModel = createHomeViewModel;
        _createVacancyListViewModel = createVacancyListViewModel;
        _createApplicationListViewModel = createApplicationListViewModel;
        _createCandidateListViewModel = createCandidateListViewModel;
    }

    public ViewModelBase CreateViewModel(ViewType viewType)
    {
        switch (viewType)
        {
            case ViewType.Home  :
                return _createHomeViewModel();
            case ViewType.VacancyList :
                return _createVacancyListViewModel();
            case ViewType.ApplicationList :
                return _createApplicationListViewModel();
            case ViewType.CandidateList :
                return _createCandidateListViewModel();
            default:
                throw new ArgumentException("No ViewModel for ViewType");
        }
    }
}