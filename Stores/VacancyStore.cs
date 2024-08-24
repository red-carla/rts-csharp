
using RTS.Models;

namespace RTS.Stores
{
    public class VacancyStore
    {
        public event Action<Vacancy>? VacancyAdded;

        public void AddVacancy(Vacancy vacancy)
        {
            VacancyAdded?.Invoke(vacancy);
        }
    }
}