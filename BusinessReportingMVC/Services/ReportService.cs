using BusinessReportingMVC.Data;
using BusinessReportingMVC.Models;
using BusinessReportingMVC.Repositories;
using BusinessReportingMVC.ViewModels;
using Microsoft.CodeAnalysis.Elfie.Model.Strings;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace BusinessReportingMVC.Services
{
    public class ReportService : IReportService
    {
        private readonly BusinessReportingDbContext _context;
        private readonly IAuthService _authService;
        private readonly IHttpContextAccessor _accessor;
        private readonly IBusinessReportingRepository _repo;

        public ReportService(BusinessReportingDbContext Context, 
            IHttpContextAccessor Accessor,
            IAuthService AuthService,
            IBusinessReportingRepository Repo)
        {
            _context = Context;
            _accessor = Accessor;
            _authService = AuthService;
            _repo = Repo;
        }

        public async Task CreateReportAsync()
        {
            try
            {
                try
                {
                    var id = _authService.GetIDClaim();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error. Could not find ID.", ex);
                }

                // ======================
                // Navigation Properties
                // ======================

                // || Financials
                FinancialsActual financialsActual = new FinancialsActual();
                FinancialsDeviation financialsDeviation = new FinancialsDeviation();
                Financial financal = new Financial
                {
                    FinancialsActual = financialsActual,
                    FinancialsDeviation = financialsDeviation
                };

                // || Projects
                Project project = new Project();

                // For bottom
                for (int i = 1; i < 6; i++)
                {
                    ProjectIndividual individual = new ProjectIndividual
                    {
                        ProjectId = project.ProjectId,
                        IsBottom = true,
                        Position = (byte)i
                    };

                    project.ProjectIndividuals.Add(individual);
                }

                // For top
                for (int i = 1; i < 6; i++)
                {
                    ProjectIndividual individual = new ProjectIndividual
                    {
                        ProjectId = project.ProjectId,
                        IsBottom = false,
                        Position = (byte)i
                    };

                    project.ProjectIndividuals.Add(individual);
                }

                // Business Development
                BusinessDevelopmentValue value = new BusinessDevelopmentValue();
                BusinessDevelopmentNote note = new BusinessDevelopmentNote();
                BusinessDevelopment business = new BusinessDevelopment
                {
                    BusinessDevelopmentValue = value,
                    BusinessDevelopmentNotes = note
                };

                // Key Highlights
                KeyHighlight keyHighlight = new KeyHighlight();

                // Admin And Resource
                AdminAndResource admin = new AdminAndResource();

                // Strategy
                Strategy strategy = new Strategy();

                // All into Report (except project sub-tables)
                Report report = new Report
                {
                    ReportName = $"New Report: {DateTime.Now}",
                    CreatedByUserId = _authService.GetIDClaim(),
                    CreatedDate = DateTime.Now,
                    IsDraft = true,
                    KeyHighlights = keyHighlight,
                    Financials = financal,
                    AdminAndResources = admin,
                    Project = project,
                    Strategy = strategy,
                    BusinessDevelopment = business,
                    IsDeleted = false
                };

                _context.Add(report);

                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        // ==========================
        // || Report Display Methods
        // ==========================

        public async Task<ReportViewModel> DisplayDraftReportAsync(int id)
        {
            Report report = await _repo.GetReportByIdAsync(id);

            ReportViewModel viewModel = await MapReportToViewModelAsync(report);

            return viewModel;
        }

        public async Task<List<Report>> DisplayUserDraftReportsAsync()
        {
            return await _repo.GetListOfDraftReportsAsync(_authService.GetIDClaim());
        }

        public async Task<List<Report>> DisplayUserPublishedReportsAsync()
        {
            return await _repo.GetListOfPublishedReportsByUserAsync(_authService.GetIDClaim());
        }


        // ===================
        // || Mapping Methods
        // ===================
        public async Task<ReportViewModel> MapReportToViewModelAsync(Report report)
        {
            ReportViewModel reportViewModel = new ReportViewModel
            {
                ReportId = report.ReportId,
                ReportName = report.ReportName,
                FromDateRange = report.FromDateRange,
                ToDateRange = report.ToDateRange,
                //IsDraft = report.IsDraft,

                KeyHighlights = new KeyHighlightsViewModel
                {
                    Performance = report.KeyHighlights.Performance,
                    Risks = report.KeyHighlights.Risks,
                    Opportunities = report.KeyHighlights.Opportunities,
                },

                Financials = new FinancialsViewModel
                {
                    FinancialsActual = new FinancialsActualViewModel
                    {
                        TurnoverActual = report.Financials.FinancialsActual.TurnoverActual,
                        DirectCostsActual = report.Financials.FinancialsActual.DirectCostsActual,
                        GrossProfitActual = report.Financials.FinancialsActual.GrossProfitActual,
                        IndirectCostsActual = report.Financials.FinancialsActual.IndirectCostsActual,
                        NetProfitActual = report.Financials.FinancialsActual.NetProfitActual,
                        WipActual = report.Financials.FinancialsActual.WipActual,
                        ProductionHoursQuarter = report.Financials.FinancialsActual.ProductionHoursQuarter,
                        UtilisationQuarter = report.Financials.FinancialsActual.UtilisationQuarter,
                        WorkInHandHoursQuarter = report.Financials.FinancialsActual.WorkInHandHoursQuarter,
                        WorkInHandMoneyQuarter = report.Financials.FinancialsActual.WorkInHandMoneyQuarter,
                        CashPositionActual = report.Financials.FinancialsActual.CashPositionActual
                    },

                    FinancialsDeviation = new FinancialsDeviationViewModel
                    {
                        TurnoverDeviation = report.Financials.FinancialsDeviation.TurnoverDeviation,
                        DirectCostsDeviation = report.Financials.FinancialsDeviation.DirectCostsDeviation,
                        GrossProfitDeviation = report.Financials.FinancialsDeviation.GrossProfitDeviation,
                        IndirectCostsDeviation = report.Financials.FinancialsDeviation.IndirectCostsDeviation,
                        NetProfitDeviation = report.Financials.FinancialsDeviation.NetProfitDeviation,
                        WipDeviation = report.Financials.FinancialsDeviation.WipDeviation,
                        ProductionHoursDeviation = report.Financials.FinancialsDeviation.ProductionHoursDeviation,
                        UtilisationQuarter = report.Financials.FinancialsDeviation.UtilisationQuarter,
                        WorkInHandHoursQuarter = report.Financials.FinancialsDeviation.WorkInHandHoursQuarter,
                        WorkInHandMoneyQuarter = report.Financials.FinancialsDeviation.WorkInHandMoneyQuarter,
                        CashPositionForecast = report.Financials.FinancialsDeviation.CashPositionForecast
                    }
                },

                AdminAndResources = new AdminAndResourceViewModel
                {
                    PointsOfNote = report.AdminAndResources.PointsOfNote,
                },

                Projects = new ProjectsViewModel
                {
                    ForecastOverallForecast = report.Project.ForecastOverallForecast,
                    ForecastOverallDeviation = report.Project.ForecastOverallDeviation,
                    ProjectIndividual = new List<ProjectIndividualViewModel>()
                },

                Strategy = new StrategyViewModel
                {
                    BusinessDevelopment = report.Strategy.BusinessDevelopment,
                    Innovation = report.Strategy.Innovation,
                    Other = report.Strategy.Other
                },

                BusinessDevelopment = new BusinessDevelopmentViewModel
                {
                    BusinessDevelopmentValue = new BusinessDevelopmentValueViewModel
                    {
                        AwarenessValue = report.BusinessDevelopment.BusinessDevelopmentValue.AwarenessValue,
                        IntentValue = report.BusinessDevelopment.BusinessDevelopmentValue.IntentValue,
                        Tendered033Value = report.BusinessDevelopment.BusinessDevelopmentValue.Tendered033Value,
                        Tendered3466Value = report.BusinessDevelopment.BusinessDevelopmentValue.Tendered3466Value,
                        Tendered67100Value = report.BusinessDevelopment.BusinessDevelopmentValue.Tendered67100Value
                    },

                    BusinessDevelopmentNote = new BusinessDevelopmentNoteViewModel
                    {
                        AwarenessNotes = report.BusinessDevelopment.BusinessDevelopmentNotes.AwarenessNotes,
                        IntentNotes = report.BusinessDevelopment.BusinessDevelopmentNotes.IntentNotes,
                        Tendered033Notes = report.BusinessDevelopment.BusinessDevelopmentNotes.Tendered033Notes,
                        Tendered3466Notes = report.BusinessDevelopment.BusinessDevelopmentNotes.Tendered3466Notes,
                        Tendered67100Notes = report.BusinessDevelopment.BusinessDevelopmentNotes.Tendered67100Notes
                    }
                }
            };

            List<ProjectIndividual> projectIndividuals = report.Project.ProjectIndividuals.ToList();

            foreach(var individual in report.Project.ProjectIndividuals)
            {
                var existingIndividual = report.Project.ProjectIndividuals
                                                            .FirstOrDefault(pi => pi.Position == individual.Position && pi.IsBottom == individual.IsBottom);

                if (existingIndividual is not null)
                {
                    var newViewModel = new ProjectIndividualViewModel
                    {
                        ProjectCode = individual.ProjectCode,
                        ProjectName = individual.ProjectName,
                        ForecastProfit = individual.ForecastProfit,
                        Deviation = individual.Deviation,
                        IsBottom = individual.IsBottom,
                        Position = individual.Position
                    };

                    reportViewModel.Projects.ProjectIndividual.Add(newViewModel);
                }
            }

            reportViewModel.Projects.ProjectIndividual = reportViewModel.Projects.ProjectIndividual
                                                                                        .OrderBy(p => p.IsBottom)
                                                                                        .ThenBy(p => p.Position)
                                                                                        .ToList();

            return reportViewModel;
        }

        public async Task<Report> MapViewModelToReportAsync(ReportViewModel viewModel, Report report)
        {
            // Find the report that was initially clicked (was passed into the viewModel when clicked)
            //      Changed so that the report is included in 

            report.ReportName = viewModel.ReportName;
            report.FromDateRange = viewModel.FromDateRange;
            report.ToDateRange = viewModel.ToDateRange;
            //report.IsDraft = viewModel.IsDraft;

            report.KeyHighlights.Performance = viewModel.KeyHighlights.Performance;
            report.KeyHighlights.Risks = viewModel.KeyHighlights.Risks;
            report.KeyHighlights.Opportunities = viewModel.KeyHighlights.Opportunities;

            report.Financials.FinancialsActual.TurnoverActual = viewModel.Financials.FinancialsActual.TurnoverActual;
            report.Financials.FinancialsActual.DirectCostsActual = viewModel.Financials.FinancialsActual.DirectCostsActual;
            report.Financials.FinancialsActual.GrossProfitActual = viewModel.Financials.FinancialsActual.GrossProfitActual;
            report.Financials.FinancialsActual.IndirectCostsActual = viewModel.Financials.FinancialsActual.IndirectCostsActual;
            report.Financials.FinancialsActual.NetProfitActual = viewModel.Financials.FinancialsActual.NetProfitActual;
            report.Financials.FinancialsActual.WipActual = viewModel.Financials.FinancialsActual.WipActual;
            report.Financials.FinancialsActual.ProductionHoursQuarter = viewModel.Financials.FinancialsActual.ProductionHoursQuarter;
            report.Financials.FinancialsActual.UtilisationQuarter = viewModel.Financials.FinancialsActual.UtilisationQuarter;
            report.Financials.FinancialsActual.WorkInHandHoursQuarter = viewModel.Financials.FinancialsActual.WorkInHandHoursQuarter;
            report.Financials.FinancialsActual.WorkInHandMoneyQuarter = viewModel.Financials.FinancialsActual.WorkInHandMoneyQuarter;
            report.Financials.FinancialsActual.CashPositionActual = viewModel.Financials.FinancialsActual.CashPositionActual;

            report.Financials.FinancialsDeviation.TurnoverDeviation = viewModel.Financials.FinancialsDeviation.TurnoverDeviation;
            report.Financials.FinancialsDeviation.DirectCostsDeviation = viewModel.Financials.FinancialsDeviation.DirectCostsDeviation;
            report.Financials.FinancialsDeviation.GrossProfitDeviation = viewModel.Financials.FinancialsDeviation.GrossProfitDeviation;
            report.Financials.FinancialsDeviation.IndirectCostsDeviation = viewModel.Financials.FinancialsDeviation.IndirectCostsDeviation;
            report.Financials.FinancialsDeviation.NetProfitDeviation = viewModel.Financials.FinancialsDeviation.NetProfitDeviation;
            report.Financials.FinancialsDeviation.WipDeviation = viewModel.Financials.FinancialsDeviation.WipDeviation;
            report.Financials.FinancialsDeviation.ProductionHoursDeviation = viewModel.Financials.FinancialsDeviation.ProductionHoursDeviation;
            report.Financials.FinancialsDeviation.UtilisationQuarter = viewModel.Financials.FinancialsDeviation.UtilisationQuarter;
            report.Financials.FinancialsDeviation.WorkInHandHoursQuarter = viewModel.Financials.FinancialsDeviation.WorkInHandHoursQuarter;
            report.Financials.FinancialsDeviation.WorkInHandMoneyQuarter = viewModel.Financials.FinancialsDeviation.WorkInHandMoneyQuarter;
            report.Financials.FinancialsDeviation.CashPositionForecast = viewModel.Financials.FinancialsDeviation.CashPositionForecast;

            report.BusinessDevelopment.BusinessDevelopmentValue.AwarenessValue = viewModel.BusinessDevelopment.BusinessDevelopmentValue.AwarenessValue;
            report.BusinessDevelopment.BusinessDevelopmentValue.IntentValue = viewModel.BusinessDevelopment.BusinessDevelopmentValue.IntentValue;
            report.BusinessDevelopment.BusinessDevelopmentValue.Tendered033Value = viewModel.BusinessDevelopment.BusinessDevelopmentValue.Tendered033Value;
            report.BusinessDevelopment.BusinessDevelopmentValue.Tendered3466Value = viewModel.BusinessDevelopment.BusinessDevelopmentValue.Tendered3466Value;
            report.BusinessDevelopment.BusinessDevelopmentValue.Tendered67100Value = viewModel.BusinessDevelopment.BusinessDevelopmentValue.Tendered67100Value;

            report.BusinessDevelopment.BusinessDevelopmentNotes.AwarenessNotes = viewModel.BusinessDevelopment.BusinessDevelopmentNote.AwarenessNotes;
            report.BusinessDevelopment.BusinessDevelopmentNotes.IntentNotes = viewModel.BusinessDevelopment.BusinessDevelopmentNote.IntentNotes;
            report.BusinessDevelopment.BusinessDevelopmentNotes.Tendered033Notes = viewModel.BusinessDevelopment.BusinessDevelopmentNote.Tendered033Notes;
            report.BusinessDevelopment.BusinessDevelopmentNotes.Tendered3466Notes = viewModel.BusinessDevelopment.BusinessDevelopmentNote.Tendered3466Notes;
            report.BusinessDevelopment.BusinessDevelopmentNotes.Tendered67100Notes = viewModel.BusinessDevelopment.BusinessDevelopmentNote.Tendered67100Notes;

            report.Strategy.BusinessDevelopment = viewModel.Strategy.BusinessDevelopment;
            report.Strategy.Innovation = viewModel.Strategy.Innovation;
            report.Strategy.Other = viewModel.Strategy.Other;

            report.Project.ForecastOverallForecast = viewModel.Projects.ForecastOverallForecast;
            report.Project.ForecastOverallDeviation = viewModel.Projects.ForecastOverallDeviation;

            report.AdminAndResources.PointsOfNote = viewModel.AdminAndResources.PointsOfNote;

            /*
             1. For each view model item, check to find the model that has the same is_bottom value and position value
                - Iterate throught viewmodel report
                - For each item, find an item in report that matches position and is bottom
                - When found, assign viewmodel properties to that report

             2. Assign to the model, the values of the view model 
             
             */

            foreach (var viewModelIndividual in viewModel.Projects.ProjectIndividual)
            {
                var existingIndividual = report.Project.ProjectIndividuals
                                                            .FirstOrDefault(p => p.IsBottom == viewModelIndividual.IsBottom && p.Position == viewModelIndividual.Position);

                if (existingIndividual is not null)
                {
                    existingIndividual.ProjectCode = viewModelIndividual.ProjectCode;
                    existingIndividual.ProjectName = viewModelIndividual.ProjectName;
                    existingIndividual.ForecastProfit = viewModelIndividual.ForecastProfit;
                    existingIndividual.Deviation = viewModelIndividual.Deviation;
                }
            }

            return report;
        }

        public SmallReportViewModel MapReportToSmallReport(Report report)
        {
            return new SmallReportViewModel
            {
                Id = report.ReportId,
                Name = report.ReportName
            };
        }


        // ========
        // || Misc
        // ========
        public async Task<Result> CheckReportIsCompleted(long id)
        {
            Report report = await _repo.GetReportByIdAsync(id);

            bool isCompleted =
                (
                    report.ReportName is not null &&
                    report.FromDateRange is not null &&
                    report.ToDateRange is not null &&

                    report.KeyHighlights.Performance is not null &&
                    report.KeyHighlights.Risks is not null &&
                    report.KeyHighlights.Opportunities is not null &&

                    report.Financials.FinancialsActual.TurnoverActual is not null &&
                    report.Financials.FinancialsActual.DirectCostsActual is not null &&
                    report.Financials.FinancialsActual.GrossProfitActual is not null &&
                    report.Financials.FinancialsActual.IndirectCostsActual is not null &&
                    report.Financials.FinancialsActual.NetProfitActual is not null &&
                    report.Financials.FinancialsActual.WipActual is not null &&
                    report.Financials.FinancialsActual.ProductionHoursQuarter is not null &&
                    report.Financials.FinancialsActual.UtilisationQuarter is not null &&
                    report.Financials.FinancialsActual.WorkInHandHoursQuarter is not null &&
                    report.Financials.FinancialsActual.WorkInHandMoneyQuarter is not null &&
                    report.Financials.FinancialsActual.CashPositionActual is not null &&

                    report.Financials.FinancialsDeviation.TurnoverDeviation is not null &&
                    report.Financials.FinancialsDeviation.DirectCostsDeviation is not null &&
                    report.Financials.FinancialsDeviation.GrossProfitDeviation is not null &&
                    report.Financials.FinancialsDeviation.IndirectCostsDeviation is not null &&
                    report.Financials.FinancialsDeviation.NetProfitDeviation is not null &&
                    report.Financials.FinancialsDeviation.WipDeviation is not null &&
                    report.Financials.FinancialsDeviation.ProductionHoursDeviation is not null &&
                    report.Financials.FinancialsDeviation.UtilisationQuarter is not null &&
                    report.Financials.FinancialsDeviation.WorkInHandHoursQuarter is not null &&
                    report.Financials.FinancialsDeviation.WorkInHandMoneyQuarter is not null &&
                    report.Financials.FinancialsDeviation.CashPositionForecast is not null &&

                    report.BusinessDevelopment.BusinessDevelopmentValue.AwarenessValue is not null &&
                    report.BusinessDevelopment.BusinessDevelopmentValue.IntentValue is not null &&
                    report.BusinessDevelopment.BusinessDevelopmentValue.Tendered033Value is not null &&
                    report.BusinessDevelopment.BusinessDevelopmentValue.Tendered3466Value is not null &&
                    report.BusinessDevelopment.BusinessDevelopmentValue.Tendered67100Value is not null &&

                    report.BusinessDevelopment.BusinessDevelopmentNotes.AwarenessNotes is not null &&
                    report.BusinessDevelopment.BusinessDevelopmentNotes.IntentNotes is not null &&
                    report.BusinessDevelopment.BusinessDevelopmentNotes.Tendered033Notes is not null &&
                    report.BusinessDevelopment.BusinessDevelopmentNotes.Tendered3466Notes is not null &&
                    report.BusinessDevelopment.BusinessDevelopmentNotes.Tendered67100Notes is not null &&

                    report.Strategy.BusinessDevelopment is not null &&
                    report.Strategy.Innovation is not null &&
                    report.Strategy.Other is not null &&

                    report.Project.ForecastOverallForecast is not null &&
                    report.Project.ForecastOverallDeviation is not null &&

                    report.AdminAndResources.PointsOfNote is not null
                );

            if (!isCompleted)
            {
                return new Result
                {
                    IsSuccess = false,
                    Message = "Not all fields filled in"
                };
            }

            int i = 0;

            foreach(ProjectIndividual individual in report.Project.ProjectIndividuals)
            {
                if 
                    (
                        //individual.ProjectCode is null || // Not incorporated
                        individual.ProjectName is null ||
                        individual.ForecastProfit is null ||
                        individual.Deviation is null
                    )
                {
                    return new Result
                    {
                        IsSuccess = false,
                        Message = "Not all fields filled in (Projects)"
                    };
                } 
            }

            return new Result
            {
                IsSuccess = true,
                Message = ""
            };
        }
    }
}