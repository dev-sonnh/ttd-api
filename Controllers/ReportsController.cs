#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TTDesign.API.Domain.Models;
using TTDesign.API.Domain.Models.Extended;
using TTDesign.API.Domain.Services;
using AutoMapper;
using TTDesign.API.Resources;
using TTDesign.API.Extensions;
using TTDesign.API.Resources.Extended;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace TTDesign.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportsService _reportsService;
        private readonly ILeaveformService _leaveformService;
        private readonly IMapper _mapper;

        public ReportsController(IReportsService reportsService, ILeaveformService leaveformService, IMapper mapper)
        {
            _reportsService = reportsService;
            _leaveformService = leaveformService;
            _mapper = mapper;
        }


        // GET: api/Users
        [HttpGet("GetReportSummaryTimesheetOfTeams")]
        [Authorize]
        public async Task<IEnumerable<ReportSummaryTimesheetOfTeamResource>> GetReportSummaryTimesheetOfTeams(DateTime fromDate, DateTime toDate)
        {
            var reports = await _reportsService.GetReportSummaryTimesheetOfTeams(fromDate, toDate);

            List<ReportSummaryTimesheetOfTeamResource> resources = new List<ReportSummaryTimesheetOfTeamResource>();

            List<Division> divisions = new List<Division>();

            var listDivision = reports.Select(r => r.TeamCode).Distinct().ToList();

            foreach (var division in listDivision)
            {
                var listProject = reports.Where(r => r.TeamCode == division).DistinctBy(r => r.TimesheetProjectId).ToList();

                List<Project> projects = new List<Project>();

                foreach (var project in listProject)
                {
                    var listMember = reports.Where(r => r.TeamCode == division && r.TimesheetProjectId == project.TimesheetProjectId).DistinctBy(r => r.UserId).ToList();

                    List<Member> members = new List<Member>();

                    foreach (var member in listMember)
                    {
                        var hours = reports.Where(r => r.TeamCode == division
                       && r.TimesheetProjectId == project.TimesheetProjectId
                       && r.UserId == member.UserId).Select(r => r.Hours);

                        members.Add(new Member
                        {
                            MemberId = member.UserId,
                            MemberName = member.FullName,
                            Hours = hours.FirstOrDefault()
                        });

                    }

                    projects.Add(new Project { MemberList = members, ProjectName = project.TimesheetProjectName });
                }

                divisions.Add(new Division { ProjectList = projects, DivisionName = division });
            }

            resources.Add(new ReportSummaryTimesheetOfTeamResource { DivisionList = divisions });

            return resources;
        }

        [HttpGet("GetVwReportOfLeavesByUserId/{id}")]
        [Authorize]
        public async Task<ViewReportOfLeaveResource> GetVwReportOfLeavesByUserId(long id)
        {
            var report = await _reportsService.GetVwReportOfLeavesByUserId(id);

            var resources = _mapper.Map<VwReportOfLeave, ViewReportOfLeaveResource>(report);

            return resources;
        }

        [HttpGet("GetCommonLeaveFormComponentByUserId/{id}")]
        [Authorize]
        public async Task<ReportOfLeaveAndLeaveformResource> GetCommonLeaveFormComponentByUserId(long id)
        {
            var report = await _reportsService.GetVwReportOfLeavesByUserId(id);
            var leaveforms = await _leaveformService.GetLeaveformByUserIdAndCurrentYear(id);

            var reportResource = _mapper.Map<VwReportOfLeave, ViewReportOfLeaveResource>(report);
            var leaveformResource = _mapper.Map<IEnumerable<Leaveform>, IEnumerable<LeaveformResource>>(leaveforms);

            ReportOfLeaveAndLeaveformResource resources = new ReportOfLeaveAndLeaveformResource(reportResource, leaveformResource);

            return resources;
        }
    }
}
