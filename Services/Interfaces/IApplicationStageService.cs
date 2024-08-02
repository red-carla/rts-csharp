﻿using DefaultNamespace;
using RecruitmentApp;
using RecruitmentApp.Data;
using RecruitmentApp.Models;
public interface IApplicationStageService
{
    IEnumerable<ApplicationStage> GetAllApplicationStages();
    ApplicationStage GetApplicationStageById(int id);
    void CreateApplicationStage(ApplicationStage applicationStage);
    void UpdateApplicationStage(ApplicationStage applicationStage);
    void DeleteApplicationStage(int id);
}