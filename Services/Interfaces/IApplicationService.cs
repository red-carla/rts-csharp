﻿public interface IApplicationService
{
    IEnumerable<Application> GetAllApplications();
    Application GetApplicationById(int id);
    void CreateApplication(Application application);
    void UpdateApplication(Application application);
    void DeleteApplication(int id);
}