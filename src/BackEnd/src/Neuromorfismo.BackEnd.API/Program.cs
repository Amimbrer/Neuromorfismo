using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using Neuromorfismo.BackEnd.API;
using Neuromorfismo.BackEnd.Dal;
using Neuromorfismo.BackEnd.Dto;
using Neuromorfismo.BackEnd.Model;
using Neuromorfismo.BackEnd.Service;
using Neuromorfismo.BackEnd.ServicesDependencies;

var builder = WebApplication.CreateBuilder(args);

// Es entorno de desarrollo
bool isDevelopment = builder.Environment.IsDevelopment();

string urlClientApp = builder.Configuration.GetSection("ClientApp")["BaseUrl"] ?? throw new InvalidOperationException("No se ha encontrado la url la client app");

// JWT SETTINGS
JWTConfig? jwtSettings = builder.Configuration.GetSection("JWT").Get<JWTConfig>();
if (jwtSettings is null || string.IsNullOrWhiteSpace(jwtSettings.Key) || string.IsNullOrWhiteSpace(jwtSettings.Issuer) || string.IsNullOrWhiteSpace(jwtSettings.Audience)) {
    throw new InvalidOperationException();
}

// Add services to the controladores
builder.Services.AddControllers(options =>
{
	// Configuramos que los valores no nulleables se consideren requeridos en los dto
	options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true; 
});

// Para swagger
builder.Services.AddSwaggerGen();

// conexion para BBDD
string connectionString = DBSettings.DBConnectionString(builder.Configuration);

builder.Services.AddDbContext<WebmedicinaContext>(options => {
	options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

	// ����� NO SUBIR A PRODUCCION !!!!!
	if (builder.Environment.IsDevelopment()) {
		options.EnableDetailedErrors(); 
		options.EnableSensitiveDataLogging();
	}
});


// IDENTITY
builder.Services.AddIdentity<UserModel, RoleModel>(options => {
	// no requerir cuenta confirmada
	options.SignIn.RequireConfirmedAccount = false;

	// Bloqueo settings
	options.Lockout.MaxFailedAccessAttempts = 5; // 5 intentos fallidos para bloqueo 
	options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(1);

	// Password settings
	options.Password.RequiredLength = 8;
	options.Password.RequireNonAlphanumeric = true;
	options.Password.RequireLowercase = true;
	options.Password.RequireUppercase = true;
	options.Password.RequireDigit = true;

	// User settings
	options.User.RequireUniqueEmail = false;
	options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ��������������";

})
	.AddEntityFrameworkStores<WebmedicinaContext>() // usar entityframework core para trabajar con la BBDD
.AddDefaultTokenProviders();  // para los tokens de inicio de sesion


// JWT TOKENS - AUTENTICACION
builder.Services.AddAuthentication(x => {
	x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
	x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
	x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
})
	.AddJwtBearer(options => {
		options.SaveToken = true; // guardamos el token para que sea accesible desde los services

		options.TokenValidationParameters = new TokenValidationParameters {
			ValidateIssuer = true,
			ValidateAudience = true,
			NameClaimType = ClaimTypes.Name,
			RoleClaimType = ClaimTypes.Role,
			ValidateLifetime = true,
			ValidateIssuerSigningKey = true,
			ValidIssuer = jwtSettings.Issuer,
			ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
		};
});
builder.Services.AddAuthorization();


// Activamos CORS para permitir llamadas a la api desde otras url
builder.Services.AddCors(option => {
	option.AddDefaultPolicy(
		policy => 
		{
			policy.WithOrigins(urlClientApp)
			.AllowCredentials()
			.AllowAnyHeader()
			.AllowAnyMethod();
        }
    );
});


// DAL - BASE DE DATOS
builder.Services.AddScoped<AdminDal>(); // Dal de administradores
builder.Services.AddScoped<MedicoDal>(); // Dal de medicos
builder.Services.AddScoped<PacientesDal>(); // Dal de pacientes
builder.Services.AddScoped<EpilepsiasDal>(); // Dal de epilepsias
builder.Services.AddScoped<FarmacosDal>(); // Dal de farmacos
builder.Services.AddScoped<MutacionesDal>(); // Dal de mutaciones
builder.Services.AddScoped<LineaTemporalDal>(); // Dal de mutaciones
builder.Services.AddScoped<TokensDal>(); // Dal de tokens, registro y login
builder.Services.AddScoped<EstadisticasDal>(); // Dal de gr�ficas para estad�sticas

// SERVICES
builder.Services.AddScoped<IIdentityService, IdentityService>(); // Servicios que trabajan con identity
builder.Services.AddScoped<IAdminsService, AdminsService>(); // Servicios de administradores
builder.Services.AddScoped<IPacientesService, PacientesService>(); // Servicios de pacientes
builder.Services.AddScoped<ILineaTemporalService, LineaTemporalService>(); // Servicios de linea temporal
builder.Services.AddScoped<IUserAccountService, UserAccountService>(); // Servicios de cuentas de usuario
builder.Services.AddScoped<IJWTManagerRepository, JWTManagerRepository>(); // Servicios de jwt tokens
builder.Services.AddScoped<IDocumentacionService, DocumentacionService>(); // Servicios excel 
builder.Services.AddScoped<IEmailService, EmailService>(); // Servicios envio de correo
builder.Services.AddScoped<IEstadisticasService, EstadisticasService>(); // Servicios obtener gr�ficas de estad�sticas


// IOPTIONS JWT
builder.Services.Configure<JWTConfig>(builder.Configuration.GetSection("JWT"))
	.PostConfigure<JWTConfig>(config => {
		if(string.IsNullOrWhiteSpace(config.Key) || string.IsNullOrWhiteSpace(config.Issuer) || string.IsNullOrWhiteSpace(config.Audience) || !double.TryParse(config.ValidezRefreshTokenEnDias, out double dias) || dias == 0 || !double.TryParse(config.ValidezTokenEnHoras, out double horas) || horas == 0) {
			throw new Exception();
		}
	}
);


// IOPTIONS EMAIL
builder.Services.Configure<EmailConfig>(builder.Configuration.GetSection("Email")).
	PostConfigure<EmailConfig>(config => { 
		if (string.IsNullOrWhiteSpace(config.Usuario) || string.IsNullOrWhiteSpace(config.Contrasena) || string.IsNullOrWhiteSpace(config.Host) || config.Puerto == 0) {
            throw new Exception();
        }
	}
);

var app = builder.Build();

// Migraciones entity framework
using (var scope = app.Services.CreateScope()) {
	WebmedicinaContext context = scope.ServiceProvider.GetRequiredService<WebmedicinaContext>();
	context.Database.Migrate();
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseDeveloperExceptionPage();
	app.UseSwagger();
	app.UseSwaggerUI();
} 

// Permitimos llamadas http
app.UseHttpsRedirection();
app.MapControllers();

// Usamos nuestra politica para cors
app.UseCors();

// Usamos autentificacion y autorizacion
app.UseAuthentication();
app.UseAuthorization();

// Acceder a las imagenes de forma estatica
app.UseStaticFiles(new StaticFileOptions {
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Imagenes")),
    RequestPath = "/img", // La URL desde la que se servir�n las im�genes
});



app.Run();
