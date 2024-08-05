using System.Collections.ObjectModel;
using System.Windows.Input;
using RTS.Commands;
using RTS.EntityFramework;
using RTS.Models;
using RTS.Services;
using RTS.Services.Interfaces;
using RTS.Views;

namespace RTS.ViewModels;

public class ApplicationListViewModel : ViewModelBase
{
  public ICommand OpenDetailCommand { get; private set; }
  private JobApplication _selectedApplication;

  public JobApplication SelectedApplication
  {
      get => _selectedApplication;
      set
      {
          _selectedApplication = value;
          OnPropertyChanged(nameof(SelectedApplication));
      }
  }
    private readonly IDataService<JobApplication> _jobApplicationDataService;
    public ObservableCollection<JobApplication> JobApplications { get; private set; }
    
    public ApplicationListViewModel(IDataService<JobApplication> jobApplicationDataService)
    {
        _jobApplicationDataService = jobApplicationDataService;
        JobApplications = new ObservableCollection<JobApplication>();
        OpenDetailCommand = new RelayCommand(OpenDetailExecute, OpenDetailCanExecute);
        LoadJobApplications();
    }

    private bool OpenDetailCanExecute()
    {
        return SelectedApplication != null;
    }

    private void OpenDetailExecute()
    {
        var detailViewModel = new ApplicationDetailViewModel(_jobApplicationDataService);
        detailViewModel.LoadApplicationDetails(SelectedApplication.Id);

        ApplicationDetailView detailView = new ApplicationDetailView
        {
            DataContext = detailViewModel
        };
        detailView.Show();
    }

    private async void LoadJobApplications()
    {
        var jobApplications = await _jobApplicationDataService.GetAll();
        foreach (var jobApplication in jobApplications)
        {
            JobApplications.Add(jobApplication);
        }
    }
}