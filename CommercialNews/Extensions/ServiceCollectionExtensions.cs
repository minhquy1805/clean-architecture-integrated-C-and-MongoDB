using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Interfaces.Security;
using Application.Interfaces.Services;
using Application.Services;
using Infrastructure.BackgroundServices;
using Infrastructure.Repositories.Users;
using Infrastructure.Services;
using Infrastructure.Services.Auth;
using Infrastructure.Services.Email;
using Infrastructure.Services.Http;


namespace CommercialNews.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddProjectServices(this IServiceCollection services) 
        {
            
            //  HttpClient
            services.AddScoped<IHttpClientService, HttpClientService>();

            // Repository
            services.AddScoped<IUserRepository, MongoUserRepository>();
            services.AddScoped<IUserVerificationRepository, MongoUserVerificationRepository>();
            services.AddScoped<IRefreshTokenRepository, MongoRefreshTokenRepository>();
            services.AddScoped<IUserAuditRepository, MongoUserAuditRepository>();
            services.AddScoped<ILoginHistoryRepository, MongoLoginHistoryRepository>();

            // Service
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserVerificationService, UserVerificationService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ILoginHistoryService, LoginHistoryService>();
            services.AddScoped<IAuditService, AuditService>();

            // Dependency Group Records
            services.AddScoped<UserServiceDependencies>();
            services.AddScoped<AuthServiceDependencies>();

            // Token Generator
            services.AddScoped<ITokenGenerator, TokenGenerator>();

            // Register BackgroundService
            services.AddHostedService<VerificationTokenCleanupHostedService>();

            // Email Service
            services.AddScoped<IEmailService, EmailService>();

            //PasswordHash
            services.AddScoped<IPasswordHasher, PasswordHasher>();

            //Token Generator
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();



            return services;

        }
    }
}
