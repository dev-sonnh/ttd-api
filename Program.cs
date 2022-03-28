using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TTDesign.API.Persistence.Repositories;
using TTDesign.API.Domain.Repositories;
using TTDesign.API.Persistence.Contexts;
using TTDesign.API.MySQL.Services;
using TTDesign.API.Domain.Services;
using TTDesign.API.Security.Hashing;
using TTDesign.API.Domain.Security.Hashing;
using TTDesign.API.Domain.Security.Tokens;
using TTDesign.API.Security.Tokens;
using TTDesign.API.Domain.Services.Communication;
using TTDesign.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);


var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      builder =>
                      {
                          builder.WithOrigins("http://localhost:3000").AllowAnyHeader()
                                                  .AllowAnyMethod();
                      });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(
    options =>
    {
        options.UseMySql(builder.Configuration.GetConnectionString("TTDesignDB"),
            Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.26-mysql"));
    }
    );

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();

builder.Services.AddScoped<ITokenHandler, TTDesign.API.Security.Tokens.TokenHandler>();

builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

builder.Services.Configure<TokenOptions>(builder.Configuration.GetSection("TokenOptions"));
var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();

var signingConfigurations = new SigningConfigurations(tokenOptions.Secret);
builder.Services.AddSingleton(signingConfigurations);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer(jwtBearerOptions =>
	{
		jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters()
		{
			ValidateAudience = true,
			ValidateLifetime = true,
			ValidateIssuerSigningKey = true,
			ValidIssuer = tokenOptions.Issuer,
			ValidAudience = tokenOptions.Audience,
			IssuerSigningKey = signingConfigurations.SecurityKey,
			ClockSkew = TimeSpan.Zero
		};
	});

builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<ITeamRepository, TeamRepository>();

builder.Services.AddScoped<ITeamService, TeamService>();

builder.Services.AddScoped<ITimesheetRepository, TimesheetRepository>();

builder.Services.AddScoped<ITimesheetService, TimesheetService>();

builder.Services.AddScoped<ITimesheetCategoryRepository, TimesheetCategoryRepository>();

builder.Services.AddScoped<ITimesheetCategoryService, TimesheetCategoryService>();

builder.Services.AddScoped<ITimesheetObjectRepository, TimesheetObjectRepository>();

builder.Services.AddScoped<ITimesheetObjectService, TimesheetObjectService>();

builder.Services.AddScoped<ITimesheetProjectRepository, TimesheetProjectRepository>();

builder.Services.AddScoped<ITimesheetProjectService, TimesheetProjectService>();

builder.Services.AddScoped<ITimesheetTaskRepository, TimesheetTaskRepository>();

builder.Services.AddScoped<ITimesheetTaskService, TimesheetTaskService>();

builder.Services.AddScoped<ILeaveTypeRepository, LeaveTypeRepository>();

builder.Services.AddScoped<ILeaveTypeService, LeaveTypeService>();

builder.Services.AddScoped<ITeamUserRepository, TeamUserRepository>();

builder.Services.AddScoped<ITeamUserService, TeamUserService>();

builder.Services.AddScoped<ILeaveformRepository, LeaveformRepository>();

builder.Services.AddScoped<ILeaveformService, LeaveformService>();

builder.Services.AddScoped<IUserSettingRepository, UserSettingRepository>();

builder.Services.AddScoped<IUserSettingService, UserSettingService>();

builder.Services.AddScoped<IReportsRepository, ReportsRepository>();

builder.Services.AddScoped<IReportsService, ReportsService>();

builder.Services.AddMvc(option => option.EnableEndpointRouting = false).AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
