using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using TTDesign.API.Domain.Models;
using TTDesign.API.Domain.Repositories;
using TTDesign.API.Persistence.Contexts;

namespace TTDesign.API.Persistence.Repositories
{
    public class LeaveformRepository : BaseRepository, ILeaveformRepository
    {
        public LeaveformRepository(AppDbContext context) : base(context)
        {

        }

        public async Task CreateLeaveform(Leaveform leaveform)
        {
            var parameters = FromModelToParams(leaveform);
            await _context.Database.ExecuteSqlRawAsync("CALL usp_Leaveform_InsertLeaveform({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7} )", parameters);
        }

        public void DeleteLeaveform(Leaveform leaveform)
        {
            _context.Leaveforms.Remove(leaveform);
        }

        public async Task<IEnumerable<Leaveform>> GetAllLeaveforms()
        {
            return await _context.Leaveforms.ToListAsync();
        }

        public async Task<Leaveform> GetLeaveformById(long id)
        {
            return await _context.Leaveforms.FindAsync(id);
        }

        public void UpdateLeaveform(Leaveform leaveform)
        {
            _context.Leaveforms.Update(leaveform);
        }

        public List<MySqlParameter> FromModelToParams(Leaveform model)
        {
            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(new MySqlParameter("user_id_parms", model.UserId));
            parameters.Add(new MySqlParameter("leave_type_id_parms", model.LeaveTypeId));
            parameters.Add(new MySqlParameter("from_date_parms", model.FromDate));
            parameters.Add(new MySqlParameter("to_date_parms", model.ToDate));
            parameters.Add(new MySqlParameter("hours_parms", model.Hours));
            parameters.Add(new MySqlParameter("status_parms", model.Status));
            parameters.Add(new MySqlParameter("reason_parms", model.Reason));
            parameters.Add(new MySqlParameter("created_by", model.CreatedBy));

            return parameters;
        }

        public void ChangeStatusOfRequest(Leaveform leaveform)
        {
            _context.Database.ExecuteSqlRawAsync("CALL usp_Leaveform_UpdateLeaveformStatus({0}, {1}, {2})", leaveform.LeaveformId, leaveform.Status, leaveform.ModifiedBy);
        }

        public async Task<IEnumerable<Leaveform>> GetLeaveformByUserRole(long id)
        {
            return await _context.Leaveforms.FromSqlRaw("CALL usp_Leaveform_GetLeaveformByUserRole({0})", id).ToListAsync();
        }

        public async Task<IEnumerable<Leaveform>> GetLeaveformByUserIdAndCurrentYear(long id)
        {
            return await _context.Leaveforms.FromSqlRaw("CALL usp_Leaveform_GetLeaveformByUserIdAndCurrentYear({0})", id).ToListAsync();
        }
    }
}
