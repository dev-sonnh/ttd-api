using Microsoft.EntityFrameworkCore;
using TTDesign.API.Domain.Models;
using TTDesign.API.Domain.Models.Extended;

namespace TTDesign.API.Persistence.Contexts
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Calendar> Calendars { get; set; } = null!;
        public virtual DbSet<CalendarAssignment> CalendarAssignments { get; set; } = null!;
        public virtual DbSet<Leave> Leaves { get; set; } = null!;
        public virtual DbSet<Leaveform> Leaveforms { get; set; } = null!;
        public virtual DbSet<LeaveType> LeaveTypes { get; set; } = null!;
        public virtual DbSet<Overtime> Overtimes { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Team> Teams { get; set; } = null!;
        public virtual DbSet<TeamUser> TeamUsers { get; set; } = null!;
        public virtual DbSet<Timesheet> Timesheets { get; set; } = null!;
        public virtual DbSet<TimesheetCategory> TimesheetCategories { get; set; } = null!;
        public virtual DbSet<TimesheetObject> TimesheetObjects { get; set; } = null!;
        public virtual DbSet<TimesheetProject> TimesheetProjects { get; set; } = null!;
        public virtual DbSet<TimesheetTask> TimesheetTasks { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserRole> UserRoles { get; set; } = null!;
        public virtual DbSet<UserSetting> UserSettings { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Calendar>(entity =>
            {
                entity.ToTable("calendar");

                entity.HasIndex(e => e.CalendarId, "calendar_note_id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.CalendarId)
                    .ValueGeneratedNever()
                    .HasColumnName("calendar_id");

                entity.Property(e => e.Color)
                    .HasMaxLength(45)
                    .HasColumnName("color");

                entity.Property(e => e.Content)
                    .HasMaxLength(500)
                    .HasColumnName("content");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasColumnName("date");

                entity.Property(e => e.IsPublic)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_public")
                    .HasDefaultValueSql("b'1'");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_date");
            });

            modelBuilder.Entity<CalendarAssignment>(entity =>
            {
                entity.ToTable("calendar_assignments");

                entity.HasIndex(e => e.CalendarAssignmentId, "calendar_assiment_id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.CalendarId, "fk_assignments_calendar_idx");

                entity.HasIndex(e => e.UserId, "fk_calendar_assiments_users_idx");

                entity.Property(e => e.CalendarAssignmentId)
                    .ValueGeneratedNever()
                    .HasColumnName("calendar_assignment_id");

                entity.Property(e => e.CalendarId).HasColumnName("calendar_id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_date");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Calendar)
                    .WithMany(p => p.CalendarAssignments)
                    .HasForeignKey(d => d.CalendarId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_assignments_calendar");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CalendarAssignments)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_assignments_users");
            });

            modelBuilder.Entity<Leave>(entity =>
            {
                entity.ToTable("leave");

                entity.HasIndex(e => e.UserId, "fk_leave_user_idx");

                entity.Property(e => e.LeaveId).HasColumnName("leave_id");

                entity.Property(e => e.AdditionalDays).HasColumnName("additional_days");

                entity.Property(e => e.AnnualLeaveDays)
                    .HasColumnName("annual_leave_days")
                    .HasDefaultValueSql("'12'");

                entity.Property(e => e.IsValid)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_valid")
                    .HasDefaultValueSql("b'1'");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Year).HasColumnName("year");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Leaves)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_leaves_users");
            });

            modelBuilder.Entity<LeaveType>(entity =>
            {
                entity.ToTable("leave_type");

                entity.Property(e => e.LeaveTypeId).HasColumnName("leave_type_id");

                entity.Property(e => e.CreatedBy)
                    .HasColumnName("created_by")
                    .HasDefaultValueSql("'2'");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_date");

                entity.Property(e => e.Type)
                    .HasMaxLength(128)
                    .HasColumnName("type");
            });

            modelBuilder.Entity<Leaveform>(entity =>
            {
                entity.ToTable("leaveform");

                entity.HasIndex(e => e.LeaveTypeId, "fk_leaveform_leave_type_idx");

                entity.HasIndex(e => e.UserId, "fk_leaveform_user_idx");

                entity.Property(e => e.LeaveformId)
                    .ValueGeneratedNever()
                    .HasColumnName("leaveform_id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.FromDate)
                    .HasColumnType("datetime")
                    .HasColumnName("from_date");

                entity.Property(e => e.Hours).HasColumnName("hours");

                entity.Property(e => e.LeaveTypeId).HasColumnName("leave_type_id");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_date");

                entity.Property(e => e.Reason)
                    .HasMaxLength(500)
                    .HasColumnName("reason");

                entity.Property(e => e.Status)
                    .HasMaxLength(45)
                    .HasColumnName("status");

                entity.Property(e => e.ToDate)
                    .HasColumnType("datetime")
                    .HasColumnName("to_date");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.LeaveType)
                    .WithMany(p => p.Leaveforms)
                    .HasForeignKey(d => d.LeaveTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_leaveform_leave_type");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Leaveforms)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_leaveform_user");
            });

            modelBuilder.Entity<Overtime>(entity =>
            {
                entity.ToTable("overtimes");

                entity.HasIndex(e => e.TimesheetProjectId, "fk_overtimes_timesheet_projects_idx");

                entity.HasIndex(e => e.TimesheetId, "fk_overtimes_timesheets_idx");

                entity.HasIndex(e => e.UserId, "fk_overtimes_users_idx");

                entity.Property(e => e.OvertimeId)
                    .ValueGeneratedNever()
                    .HasColumnName("overtime_id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.FromTime)
                    .HasColumnType("time")
                    .HasColumnName("from_time");

                entity.Property(e => e.Hours).HasColumnName("hours");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_date");

                entity.Property(e => e.Reason)
                    .HasMaxLength(500)
                    .HasColumnName("reason");

                entity.Property(e => e.Status)
                    .HasMaxLength(45)
                    .HasColumnName("status")
                    .HasDefaultValueSql("'pending'");

                entity.Property(e => e.TimesheetId).HasColumnName("timesheet_id");

                entity.Property(e => e.TimesheetProjectId).HasColumnName("timesheet_project_id");

                entity.Property(e => e.ToTime)
                    .HasColumnType("time")
                    .HasColumnName("to_time");

                entity.Property(e => e.Type)
                    .HasMaxLength(45)
                    .HasColumnName("type")
                    .HasDefaultValueSql("'weekday'");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Timesheet)
                    .WithMany(p => p.Overtimes)
                    .HasForeignKey(d => d.TimesheetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_overtimes_timesheets");

                entity.HasOne(d => d.TimesheetProject)
                    .WithMany(p => p.Overtimes)
                    .HasForeignKey(d => d.TimesheetProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_overtimes_timesheet_projects");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Overtimes)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_overtimes_users");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("roles");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(50)
                    .HasColumnName("role_name");
            });

            modelBuilder.Entity<Team>(entity =>
            {
                entity.ToTable("teams");

                entity.HasIndex(e => e.TeamCode, "department_code_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.TeamId, "department_id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.TeamId).HasColumnName("team_id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.IsDepartment)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_department")
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_date");

                entity.Property(e => e.TeamCode)
                    .HasMaxLength(45)
                    .HasColumnName("team_code");

                entity.Property(e => e.TeamDescription)
                    .HasMaxLength(500)
                    .HasColumnName("team_description")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.TeamName)
                    .HasMaxLength(128)
                    .HasColumnName("team_name")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");
            });

            modelBuilder.Entity<TeamUser>(entity =>
            {
                entity.ToTable("team_users");

                entity.HasIndex(e => e.TeamUserId, "group_user_id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.TeamId, "group_user_t_group_t_id_idx");

                entity.HasIndex(e => e.UserId, "user_id_idx");

                entity.Property(e => e.TeamUserId).HasColumnName("team_user_id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_date");

                entity.Property(e => e.Role)
                    .HasMaxLength(45)
                    .HasColumnName("role");

                entity.Property(e => e.Status)
                    .HasMaxLength(45)
                    .HasColumnName("status")
                    .HasDefaultValueSql("'unavailable'");

                entity.Property(e => e.TeamId).HasColumnName("team_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.TeamUsers)
                    .HasForeignKey(d => d.TeamId)
                    .HasConstraintName("fk_team");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TeamUsers)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("fk_user");
            });

            modelBuilder.Entity<Timesheet>(entity =>
            {
                entity.ToTable("timesheet");

                entity.HasIndex(e => e.UserId, "fk_timesheet_user_idx");

                entity.HasIndex(e => e.TimesheetId, "timesheet_overview_id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.TimesheetId).HasColumnName("timesheet_id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasColumnName("date");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_date");

                entity.Property(e => e.Note)
                    .HasMaxLength(500)
                    .HasColumnName("note")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.TimeIn)
                    .HasColumnType("datetime")
                    .HasColumnName("time_in");

                entity.Property(e => e.TimeOut)
                    .HasColumnType("datetime")
                    .HasColumnName("time_out");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Timesheets)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("fk_timesheet_user");
            });

            modelBuilder.Entity<TimesheetCategory>(entity =>
            {
                entity.ToTable("timesheet_categories");

                entity.HasIndex(e => e.TeamId, "fk_timesheet_catogeries_teams_idx");

                entity.Property(e => e.TimesheetCategoryId).HasColumnName("timesheet_category_id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.IsActive)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("b'1'");

                entity.Property(e => e.IsPublic)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_public")
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_date");

                entity.Property(e => e.Name)
                    .HasMaxLength(128)
                    .HasColumnName("name");

                entity.Property(e => e.TeamId).HasColumnName("team_id");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.TimesheetCategories)
                    .HasForeignKey(d => d.TeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_timesheet_catogeries_teams");
            });

            modelBuilder.Entity<TimesheetObject>(entity =>
            {
                entity.ToTable("timesheet_objects");

                entity.HasIndex(e => e.TeamId, "fk_timesheet_objects_teams_idx");

                entity.Property(e => e.TimesheetObjectId).HasColumnName("timesheet_object_id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.IsActive)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("b'1'");

                entity.Property(e => e.IsPublic)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_public")
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_date");

                entity.Property(e => e.Name)
                    .HasMaxLength(128)
                    .HasColumnName("name");

                entity.Property(e => e.TeamId).HasColumnName("team_id");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.TimesheetObjects)
                    .HasForeignKey(d => d.TeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_timesheet_objects_teams");
            });

            modelBuilder.Entity<TimesheetProject>(entity =>
            {
                entity.ToTable("timesheet_projects");

                entity.HasIndex(e => e.TeamId, "fk_timesheet_projects_teams_idx");

                entity.HasIndex(e => e.Code, "timesheet_project_code_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.TimesheetProjectId, "timesheet_project_id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.TimesheetProjectId).HasColumnName("timesheet_project_id");

                entity.Property(e => e.Code)
                    .HasMaxLength(128)
                    .HasColumnName("code");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.FinishedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("finished_date");

                entity.Property(e => e.IsActive)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_active")
                    .HasDefaultValueSql("b'1'");

                entity.Property(e => e.IsPublic)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_public")
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_date");

                entity.Property(e => e.Name)
                    .HasMaxLength(500)
                    .HasColumnName("name");

                entity.Property(e => e.StartedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("started_date");

                entity.Property(e => e.TeamId).HasColumnName("team_id");

                entity.HasOne(d => d.Team)
                    .WithMany(p => p.TimesheetProjects)
                    .HasForeignKey(d => d.TeamId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_timesheet_projects_teams");
            });

            modelBuilder.Entity<TimesheetTask>(entity =>
            {
                entity.ToTable("timesheet_tasks");

                entity.HasIndex(e => e.TimesheetCategoryId, "fk_timesheet_tasks_timesheet_categories_idx");

                entity.HasIndex(e => e.TimesheetId, "fk_timesheet_tasks_timesheet_idx");

                entity.HasIndex(e => e.TimesheetObjectId, "fk_timesheet_tasks_timesheet_objects_idx");

                entity.HasIndex(e => e.TimesheetProjectId, "fk_timesheet_tasks_timesheet_projects_idx");

                entity.HasIndex(e => e.UserId, "fk_timesheet_tasks_users_idx");

                entity.Property(e => e.TimesheetTaskId).HasColumnName("timesheet_task_id");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Description)
                    .HasMaxLength(500)
                    .HasColumnName("description");

                entity.Property(e => e.Hours).HasColumnName("hours");

                entity.Property(e => e.IsOvertime)
                    .HasColumnType("bit(1)")
                    .HasColumnName("is_overtime")
                    .HasDefaultValueSql("b'0'");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_date");

                entity.Property(e => e.TimesheetCategoryId).HasColumnName("timesheet_category_id");

                entity.Property(e => e.TimesheetId).HasColumnName("timesheet_id");

                entity.Property(e => e.TimesheetObjectId).HasColumnName("timesheet_object_id");

                entity.Property(e => e.TimesheetProjectId).HasColumnName("timesheet_project_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.TimesheetCategory)
                    .WithMany(p => p.TimesheetTasks)
                    .HasForeignKey(d => d.TimesheetCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_timesheet_tasks_timesheet_categories");

                entity.HasOne(d => d.Timesheet)
                    .WithMany(p => p.TimesheetTasks)
                    .HasForeignKey(d => d.TimesheetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_timesheet_tasks_timesheet");

                entity.HasOne(d => d.TimesheetObject)
                    .WithMany(p => p.TimesheetTasks)
                    .HasForeignKey(d => d.TimesheetObjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_timesheet_tasks_timesheet_objects");

                entity.HasOne(d => d.TimesheetProject)
                    .WithMany(p => p.TimesheetTasks)
                    .HasForeignKey(d => d.TimesheetProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_timesheet_tasks_timesheet_projects");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TimesheetTasks)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_timesheet_tasks_users");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.HasIndex(e => e.OrderNo, "order_no_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.StaffId, "user_code_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.UserId, "user_id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.UserName, "user_name_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.AboutMe)
                    .HasMaxLength(128)
                    .HasColumnName("about_me");

                entity.Property(e => e.Address)
                    .HasMaxLength(128)
                    .HasColumnName("address");

                entity.Property(e => e.Avatar)
                    .HasMaxLength(500)
                    .HasColumnName("avatar");

                entity.Property(e => e.Birthday)
                    .HasColumnType("datetime")
                    .HasColumnName("birthday");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Email)
                    .HasMaxLength(128)
                    .HasColumnName("email");

                entity.Property(e => e.FullName)
                    .HasMaxLength(128)
                    .HasColumnName("full_name")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.IdNo)
                    .HasMaxLength(128)
                    .HasColumnName("id_no");

                entity.Property(e => e.IssuedTo)
                    .HasMaxLength(128)
                    .HasColumnName("issued_to");

                entity.Property(e => e.ModifiedBy).HasColumnName("modified_by");

                entity.Property(e => e.ModifiedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("modified_date");

                entity.Property(e => e.OrderNo).HasColumnName("order_no");

                entity.Property(e => e.Password)
                    .HasMaxLength(128)
                    .HasColumnName("password");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(50)
                    .HasColumnName("phone_number");

                entity.Property(e => e.StaffId)
                    .HasMaxLength(128)
                    .HasColumnName("staff_id");

                entity.Property(e => e.UserName)
                    .HasMaxLength(128)
                    .HasColumnName("user_name");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.ToTable("user_roles");

                entity.HasIndex(e => e.RoleId, "fk_user_roles_role_idx");

                entity.HasIndex(e => e.UserId, "fk_user_roles_user_idx");

                entity.Property(e => e.UserRoleId).HasColumnName("user_role_id");

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_user_roles_role");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRoles)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_user_roles_user");
            });

            modelBuilder.Entity<UserSetting>(entity =>
            {
                entity.ToTable("user_settings");

                entity.HasIndex(e => e.UserId, "fk_users_idx")
                    .IsUnique();

                entity.HasIndex(e => e.UserSettingId, "user_setting_id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.UserSettingId).HasColumnName("user_setting_id");

                entity.Property(e => e.EmailNotification)
                    .HasColumnType("bit(1)")
                    .HasColumnName("email_notification")
                    .HasDefaultValueSql("b'1'");

                entity.Property(e => e.Language)
                    .HasMaxLength(50)
                    .HasColumnName("language")
                    .HasDefaultValueSql("'English'");

                entity.Property(e => e.Timezone)
                    .HasMaxLength(128)
                    .HasColumnName("timezone")
                    .HasDefaultValueSql("'BangKok, Ha Noi, Jakarta (UTC +7)'");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.UserSetting)
                    .HasForeignKey<UserSetting>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_users");
            });

            modelBuilder.Entity<VwReportOfLeave>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_report_of_leave");

                entity.Property(e => e.AnnualUse).HasColumnName("annual_use");

                entity.Property(e => e.LastYearLeft).HasColumnName("last_year_left");

                entity.Property(e => e.RestOfLeaveDays).HasColumnName("rest_of_leave_days");

                entity.Property(e => e.SummerUse).HasColumnName("summer_use");

                entity.Property(e => e.ThisAnnualLeft).HasColumnName("this_annual_left");

                entity.Property(e => e.ThisSummerLeft).HasColumnName("this_summer_left");

                entity.Property(e => e.ThisYearTotal).HasColumnName("this_year_total");

                entity.Property(e => e.TotalUse).HasColumnName("total_use");

                entity.Property(e => e.UserId).HasColumnName("user_id");
            });


            modelBuilder.Entity<VwUserCredential>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_usercredential");

                entity.Property(e => e.AboutMe)
                    .HasMaxLength(128)
                    .HasColumnName("about_me");

                entity.Property(e => e.Address)
                    .HasMaxLength(128)
                    .HasColumnName("address");

                entity.Property(e => e.Avatar)
                    .HasMaxLength(500)
                    .HasColumnName("avatar");

                entity.Property(e => e.BirthDay)
                    .HasColumnType("datetime")
                    .HasColumnName("birthday");

                entity.Property(e => e.CreatedBy).HasColumnName("created_by");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date")
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.Email)
                    .HasMaxLength(128)
                    .HasColumnName("email");

                entity.Property(e => e.FullName)
                    .HasMaxLength(128)
                    .HasColumnName("full_name")
                    .UseCollation("utf8_general_ci")
                    .HasCharSet("utf8");

                entity.Property(e => e.IdNo)
                    .HasMaxLength(128)
                    .HasColumnName("id_no");

                entity.Property(e => e.IssuedTo)
                    .HasMaxLength(128)
                    .HasColumnName("issued_to");

                entity.Property(e => e.Password)
                    .HasMaxLength(128)
                    .HasColumnName("password");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(50)
                    .HasColumnName("phone_number");

                entity.Property(e => e.Role)
                    .HasMaxLength(45)
                    .HasColumnName("role");

                entity.Property(e => e.StaffId)
                    .HasMaxLength(128)
                    .HasColumnName("staff_id");

                entity.Property(e => e.OrderNo)
                    .HasColumnName("order_no");

                entity.Property(e => e.TeamId).HasColumnName("team_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.UserName)
                    .HasMaxLength(128)
                    .HasColumnName("user_name");
            });

            modelBuilder.Entity<ReportSummaryTimesheetOfTeam>(entity =>
            {
                entity.HasNoKey();
                
                entity.Property(e => e.TimesheetProjectId)
                    .HasColumnName("timesheet_project_id");

                entity.Property(e => e.TimesheetProjectName)
                    .HasColumnName("name");

                entity.Property(e => e.UserId)
                    .HasColumnName("user_id");
                
                entity.Property(e => e.FullName)
                    .HasColumnName("full_name");
                
                entity.Property(e => e.Hours)
                    .HasColumnName("hours");

                entity.Property(e => e.TeamId)
                    .HasColumnName("team_id");

                entity.Property(e => e.TeamCode)
                    .HasColumnName("team_code");
            });


            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
