using AutoMapper;
using TTDesign.API.Domain.Models;
using TTDesign.API.Domain.Models.Extended;
using TTDesign.API.Resources;
using TTDesign.API.Resources.Extended;

namespace TTDesign.API.Mapping
{
    public class ResourceToModelProfile : Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SaveUserResource, User>();

            CreateMap<SaveTeamResource, Team>();

            CreateMap<SaveTimesheetResource, Timesheet>();

            CreateMap<UpdateTimesheetByFingerprintMachineResource, Timesheet>();

            CreateMap<SaveTimesheetCategoryResource, TimesheetCategory>();

            CreateMap<SaveTimesheetProjectResource, TimesheetProject>();

            CreateMap<SaveTimesheetObjectResource, TimesheetObject>();

            CreateMap<SaveTimesheetTaskResource, TimesheetTask>();

            CreateMap<SaveLeaveformResource, Leaveform>();

            CreateMap<SaveLeaveTypeResource, LeaveType>();
            
            CreateMap<SaveTeamUserResource, TeamUser>();

            CreateMap<SaveUserSettingResource, UserSetting>();

            //Extended
            CreateMap<SaveUserCredentialResource, VwUserCredential>();
        }
    }
}
