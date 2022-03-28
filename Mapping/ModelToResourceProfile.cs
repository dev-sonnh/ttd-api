using AutoMapper;
using TTDesign.API.Domain.Models;
using TTDesign.API.Domain.Models.Extended;
using TTDesign.API.Domain.Security.Tokens;
using TTDesign.API.Resources;
using TTDesign.API.Resources.Extended;

namespace TTDesign.API.Mapping
{
    public class ModelToResourceProfile: Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<AccessToken, AccessTokenResource>()
                .ForMember(a => a.AccessToken, opt => opt.MapFrom(a => a.Token))
                .ForMember(a => a.RefreshToken, opt => opt.MapFrom(a => a.RefreshToken.Token))
                .ForMember(a => a.Expiration, opt => opt.MapFrom(a => a.Expiration));

            CreateMap<User, UserResource>();

            CreateMap<Team, TeamResource>();

            CreateMap<Timesheet, TimesheetResource>();

            CreateMap<TimesheetCategory, TimesheetCategoryResource>();

            CreateMap<TimesheetObject, TimesheetObjectResource>();

            CreateMap<TimesheetProject, TimesheetProjectResource>();

            CreateMap<TimesheetTask, TimesheetTaskResource>();

            CreateMap<Leaveform, LeaveformResource>();

            CreateMap<LeaveType, LeaveTypeResource>();

            CreateMap<TeamUser, TeamUserResource>();

            CreateMap<UserSetting, UserSettingResource>();

            CreateMap<ReportSummaryTimesheetOfTeam, ReportSummaryTimesheetOfTeamResource>();

            CreateMap<VwReportOfLeave, ViewReportOfLeaveResource>();

            //Extended
            CreateMap<User, VwUserCredential>();
            CreateMap<VwUserCredential, ViewUserCredentialResource>();
        }
    }
}
