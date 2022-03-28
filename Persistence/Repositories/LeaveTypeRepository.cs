using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using TTDesign.API.Domain.Models;
using TTDesign.API.Domain.Repositories;
using TTDesign.API.Persistence.Contexts;

namespace TTDesign.API.Persistence.Repositories
{
    public class LeaveTypeRepository : BaseRepository, ILeaveTypeRepository
    {
        public LeaveTypeRepository(AppDbContext context) : base(context)
        {

        }

        public async Task CreateLeaveType(LeaveType leaveType)
        {
            var parameters = FromModelToParams(leaveType);
            await _context.Database.ExecuteSqlRawAsync("CALL usp_LeaveType_InsertLeaveType({0}, {1} )", parameters);
        }

        public void DeleteLeaveType(LeaveType leaveType)
        {
            _context.LeaveTypes.Remove(leaveType);
        }

        public async Task<IEnumerable<LeaveType>> GetAllLeaveTypes()
        {
            return await _context.LeaveTypes.ToListAsync();
        }

        public async Task<LeaveType> GetLeaveTypeById(long id)
        {
            return await _context.LeaveTypes.FindAsync(id);
        }

        public void UpdateLeaveType(LeaveType leaveType)
        {
            _context.LeaveTypes.Update(leaveType);
        }

        public List<MySqlParameter> FromModelToParams(LeaveType model)
        {
            List<MySqlParameter> parameters = new List<MySqlParameter>();

            parameters.Add(new MySqlParameter("type", model.Type));
            parameters.Add(new MySqlParameter("created_by", model.CreatedBy));

            return parameters;
        }
    }
}
