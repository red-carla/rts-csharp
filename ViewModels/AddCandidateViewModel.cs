using System.Windows.Input;
using RTS.Commands;
using RTS.Models;
using RTS.Services;

namespace RTS.ViewModels;

public class AddCandidateViewModel : ViewModelBase
{
    private readonly IDataService<Candidate> _candidateDataService;

    private string _address = null!;

    private string _avatar = null!;

    private string _email = null!;

    private string _name = null!;


    private string _phone = null!;

    private string _resumeLink = null!;

    private string _status = null!;

    private string _title = null!;

    public AddCandidateViewModel(IDataService<Candidate> candidateDataService,
        INavigationService closeNavigationService)
    {
        _candidateDataService = candidateDataService;
        SubmitCommand = new AddCandidateCommand(this, closeNavigationService);
        CancelCommand = new NavigateCommand(closeNavigationService);
    }

    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            OnPropertyChanged(nameof(Name));
        }
    }

    public string Title
    {
        get => _title;
        set
        {
            _title = value;
            OnPropertyChanged(nameof(Title));
        }
    }

    public string Avatar
    {
        get => _avatar;
        set
        {
            _avatar = value;
            OnPropertyChanged(nameof(Avatar));
        }
    }

    public string ResumeLink
    {
        get => _resumeLink;
        set
        {
            _resumeLink = value;
            OnPropertyChanged(nameof(ResumeLink));
        }
    }

    public string Email
    {
        get => _email;
        set
        {
            _email = value;
            OnPropertyChanged(nameof(Email));
        }
    }

    public string Phone
    {
        get => _phone;
        set
        {
            _phone = value;
            OnPropertyChanged(nameof(Phone));
        }
    }

    public string Address
    {
        get => _address;
        set
        {
            _address = value;
            OnPropertyChanged(nameof(Address));
        }
    }

    public string Status
    {
        get => _status;
        set
        {
            _status = value;
            OnPropertyChanged(nameof(Status));
        }
    }

    public ICommand SubmitCommand { get; }
    public ICommand CancelCommand { get; }

    public async Task AddCandidate(Candidate candidate)
    {
        await _candidateDataService.Create(candidate);
    }
}