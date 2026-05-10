using HIPA_BE.Logger;
using HIPA_BE.CustomTokenProviders;
using HIPA_BE.Data;
using HIPA_BE.Models;
using HIPA_BE.Services;
using HIPA_BE.Services.OrganServices;
using HIPA_BE.Services.DiagnosisServices;
using HIPA_BE.Services.SampleImageAnnotationServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using HIPA_BE;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Cryptography;
using HIPA_BE.Services.SampleImageServices;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using tusdotnet;
using tusdotnet.Models;
using tusdotnet.Stores;
using tusdotnet.Models.Expiration;
using tusdotnet.Models.Configuration;
using System.Text;
using Duende.IdentityServer.EntityFramework.DbContexts;
using HIPA_BE.Services.BodySystemService;
using HIPA_BE.Controllers.Resources;
using log4net;
using System.Reflection;
using Microsoft.Extensions.FileProviders;
using Duende.IdentityServer.Extensions;
using System.IO.Compression;
using tusdotnet.Interfaces;
using HIPA_BE.Services.TableReaderServices;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddLog4Net();

Logger logger_setup = new();
logger_setup.Setup(LoggingLevel.All);

ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod()?.DeclaringType ?? typeof(AssetsController));

var assembly = typeof(Program).Assembly.GetName().Name;

switch (builder.Environment.IsDevelopment())
{
    case true:
        {
            Log.Info("Application running in Development mode.");
            break;
        }
    case false: {
            Log.Info("Application running in Production mode.");
            break;
        }
}

///var connectionString = builder.Configuration.GetConnectionString("WebApiDatabase");
///Console.WriteLine(connectionString);
var webApiDatabase = $"Host={Environment.GetEnvironmentVariable("DB_HOST")}; Port={Environment.GetEnvironmentVariable("DB_PORT")}; Database={Environment.GetEnvironmentVariable("DB")}; Username={Environment.GetEnvironmentVariable("DB_USERNAME")}; Password={Environment.GetEnvironmentVariable("DB_PASSWORD")}";
Log.Info($"Following parameters will be used for database connection: {webApiDatabase}");
builder.Configuration["ConnectionStrings:WebApiDatabase"] = webApiDatabase;

// Construct frontend base url, ignoring port if standard port is used
string frontend_host = Environment.GetEnvironmentVariable("FRONTEND_HOST") ?? "http://localhost";
string frontend_port = Environment.GetEnvironmentVariable("FRONTEND_PORT") ?? "80";
string frontend_baseurl = String.Empty;
if (frontend_port == "80" || frontend_port == "443")
{
    frontend_baseurl = frontend_host;
}
else
{
    frontend_baseurl = $"{frontend_host}:{frontend_port}";
}
Log.Info($"Setting FRONTEND_BASEURL={frontend_baseurl}");
Environment.SetEnvironmentVariable("FRONTEND_BASEURL", frontend_baseurl);

// Tus implementation code from:
// https://github.com/tusdotnet/tusdotnet/blob/master/Source/TestSites/AspNetCore_netcoreapp3.1_TestApp/Startup.cs
//
// TODO:
//  - Implement authorization for tusdotnet
//  - Implement file unzip for uploaded zip files
//    SampleImages have to be uploaded as zip files with .vsi file and
//    a folder "_<vsi image name>_" containing the a "stack1" folder with the "frame_t.ets" file
DefaultTusConfiguration CreateTusConfigurationForCleanupService()
{
    // Simplified configuration just for the ExpiredFilesCleanupService to show load order of configs.
    return new DefaultTusConfiguration
    {
        Store = new TusDiskStore(builder.Configuration.GetValue<string>("TusConfig:UploadPath")),
        Expiration = new AbsoluteExpiration(TimeSpan.FromMinutes(builder.Configuration.GetValue<double>("TusConfig:Expiration")))
    };
}

// Helper method to delete TUS files after they are no longer required
async Task DeleteTusFileAsync(ITusStore store, string fileId, CancellationToken cancellationToken)
{
    try
    {
        await ((TusDiskStore)store).DeleteFileAsync(fileId, cancellationToken);
        Log.Info($"TUS file {fileId} deleted");
    }
    catch (Exception ex)
    {
        Log.Error($"Failed to delete TUS file {fileId}: {ex.Message}");
    }
}

Task<DefaultTusConfiguration> TusConfigurationFactory(HttpContext httpContext)
{
    // Change the value of EnableOnAuthorize in appsettings.json to enable or disable
    // the new authorization event.
    var tusStore = httpContext.RequestServices.GetRequiredService<TusDiskStore>();
    var enableAuthorize = builder.Configuration.GetValue<bool>("EnableOnAuthorize");
    var config = new DefaultTusConfiguration
    {
        // Store = new TusDiskStore(
        //     builder.Configuration.GetValue<string>("TusConfig:UploadPath"),
        //     deletePartialFilesOnConcat: true,
        //     bufferSize: TusDiskBufferSize.Default
        //     // fileIdProvider: new FileNameProvider()
        //     ),
        Store = tusStore,
        MetadataParsingStrategy = MetadataParsingStrategy.AllowEmptyValues,
        UsePipelinesIfAvailable = true,
        Events = new Events
        {
            // TODO: Implement authorization for tusdotnet
            // OnAuthorizeAsync = ctx =>
            // {
            //
            // },

            OnBeforeCreateAsync = async ctx =>
            {

                // Partial files are not complete so we do not need to validate
                // the metadata in our example.
                if (ctx.FileConcatenation is tusdotnet.Models.Concatenation.FileConcatPartial)
                {
                    return;
                }

                if (!ctx.Metadata.ContainsKey("name") || ctx.Metadata["name"].HasEmptyValue)
                {
                    ctx.FailRequest("name metadata must be specified. ");
                }

                if (!ctx.Metadata.ContainsKey("contentType") || ctx.Metadata["contentType"].HasEmptyValue)
                {
                    ctx.FailRequest("contentType metadata must be specified. ");
                }

                if (!ctx.Metadata.ContainsKey("fileType") || ctx.Metadata["fileType"].HasEmptyValue){
                    ctx.FailRequest("fileType metadata must be specified.");        
                }
                
                if ((!ctx.Metadata.ContainsKey("groupId") || ctx.Metadata["groupId"].Equals("0")) && ctx.Metadata["fileType"].GetString(Encoding.UTF8) == "sampleImage"){
                    ctx.FailRequest("SampleImage record creating error");        
                }

                if ((!ctx.Metadata.ContainsKey("sampleId") || ctx.Metadata["sampleId"].HasEmptyValue) && ctx.Metadata["fileType"].GetString(Encoding.UTF8) == "caustry"){
                    ctx.FailRequest("Caustry record creating error");        
                }
                if ((!ctx.Metadata.ContainsKey("folderId") || ctx.Metadata["folderId"].HasEmptyValue) && ctx.Metadata["fileType"].GetString(Encoding.UTF8) == "folderFile"){
                    ctx.FailRequest("Caustry record creating error");        
                }
                
                return;
            },
            OnCreateCompleteAsync = ctx =>
            {
                Log.Info($"Created file {ctx.FileId} using {ctx.Store.GetType().FullName}");
                return Task.CompletedTask;
            },
            OnFileCompleteAsync = async ctx =>
            {
                Log.Info($"Upload of {ctx.FileId} is complete.");
                // If the store implements ITusReadableStore one could access the completed file here.
                // The default TusDiskStore implements this interface:
                var file = await ctx.GetFileAsync();
                var metadata = await file.GetMetadataAsync(ctx.CancellationToken);
                var name = metadata["name"].GetString(Encoding.UTF8);
                var fileType = metadata["fileType"].GetString(Encoding.UTF8);
                var adminService = httpContext.RequestServices.GetRequiredService<AdminService>();
                var uploadPath = builder.Configuration.GetValue<string>("TusConfig:UploadPath");
                var currentPath = Path.Combine(uploadPath, file.Id);
                // var contentType = metadata["contentType"].GetString(Encoding.UTF8);

                try
                {
                    if (fileType == "sampleImage")
                    {
                        string parentFolder = Directory.GetParent(uploadPath)?.FullName;
                        var groupId = metadata["groupId"].GetString(Encoding.UTF8);
                        if (string.IsNullOrEmpty(parentFolder))
                        {
                            throw new Exception("Cannot determine parent folder of uploadPath.");
                        }

                        var sampleImageFolderPath = Path.Combine(parentFolder, groupId);
                        string imageFilePath = "";

                        string[] recognizedExtensions =
                            builder.Configuration.GetValue<string[]>("TusConfig:RecognizedExtensions")
                            ?? ["*.vsi", "*.bif", "*.svs", "*.tif", "*.tiff", "*.mrxs", "*.ndpi"];

                        if (Path.GetExtension(name).Equals(".zip", StringComparison.OrdinalIgnoreCase))
                        {
                            try
                            {
                                ZipFile.ExtractToDirectory(currentPath, sampleImageFolderPath);
                                Log.Info($"Zip file extracted to {sampleImageFolderPath}");

                                foreach (var extension in recognizedExtensions)
                                {
                                    var files = Directory.GetFiles(sampleImageFolderPath, extension);
                                    if (files.Length > 0)
                                    {
                                        imageFilePath = files[0];
                                        Log.Info($"Found microscopy image file: {imageFilePath}");
                                        break;
                                    }
                                }

                                if (string.IsNullOrEmpty(imageFilePath))
                                {
                                    throw new Exception($"No supported microscopy image file found in extracted zip. " +
                                                        $"Supported extensions: {string.Join(", ", recognizedExtensions)}");
                                }
                            }
                            catch (Exception ex)
                            {
                                throw new Exception($"Error during unzipping: {ex.Message}");
                            }
                        }
                        else
                        {
                            var sampleImageTarget = Path.Combine(sampleImageFolderPath, name);
                            File.Move(currentPath, sampleImageTarget);
                            imageFilePath = sampleImageTarget;
                        }

                        var result = await adminService.UpdateSampleImageAfterUpload(groupId, imageFilePath);

                        if (result.IsError)
                        {
                            throw new Exception("Cannot update SampleImageRecord");
                        }

                    }
                    else if (fileType == "caustry")
                    {
                        var sampleId = metadata["sampleId"].GetString(Encoding.UTF8);
                        var result = await adminService.AddCaustryFileToSampleImage(name, sampleId);
                        if (result.IsError)
                        {
                            Log.Error("Cannot add caustry file to SampleImage");
                        }
                        else
                        {
                            var caustryTarget = Path.Combine(result.Value, name);
                            File.Move(currentPath, caustryTarget);
                        }

                    }
                    else if (fileType == "folderFile")
                    {
                        var learningService = httpContext.RequestServices.GetRequiredService<LearningService>();
                        var folderId = metadata["folderId"].GetString(Encoding.UTF8);
                        var result = await learningService.AddFileToDirectory(name, folderId);
                        if (result.IsError)
                        {
                            Log.Error("Cannot add file to Folder");
                        }
                        else
                        {
                            var folderFiletarget = Path.Combine(result.Value.Path, name);
                            File.Move(currentPath, folderFiletarget);
                        }
                    }
                    else
                    {
                        Log.Error("Unknown fileType was uploaded");
                    }
                }
                catch (Exception ex)
                {
                    Log.Error($"Error processing file {ctx.FileId}: {ex.Message}");
                }
                finally
                {
                    await DeleteTusFileAsync(ctx.Store, ctx.FileId, ctx.CancellationToken);
                }

                return;
            }

        },
        // Set an expiration time where incomplete files can no longer be updated.
        // This value can either be absolute or sliding.
        // Absolute expiration will be saved per file on create
        // Sliding expiration will be saved per file on create and updated on each patch/update.
        Expiration = new AbsoluteExpiration(TimeSpan.FromMinutes(builder.Configuration.GetValue<double>("TusConfig:Expiration"))),
    };

    return Task.FromResult(config);
}

Func<HttpContext, Task<DefaultTusConfiguration>> GetTusConfigurationFactory()
{
    return TusConfigurationFactory;
}

builder.Services.AddDbContext<AspNetIdentityDbContext>(options =>
    options.UseNpgsql(webApiDatabase,
        b => b.MigrationsAssembly(assembly)));

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add database context for web api
// Alos so that DbInitializer can be used and context can be injected
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(webApiDatabase,
        b => b.MigrationsAssembly(assembly)));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(opt =>
    {
        opt.SignIn.RequireConfirmedEmail = true;
        opt.Tokens.EmailConfirmationTokenProvider = "emailconfirmation";
        opt.Tokens.PasswordResetTokenProvider = "passwordreset";
    })
    .AddEntityFrameworkStores<AspNetIdentityDbContext>()
    .AddDefaultTokenProviders()
    .AddTokenProvider<EmailConfirmationTokenProvider<ApplicationUser>>("emailconfirmation")
    .AddTokenProvider<PasswordResetTokenProvider<ApplicationUser>>("passwordreset");

builder.Services.Configure<EmailConfirmationTokenProviderOptions>(opt =>
    opt.TokenLifespan = TimeSpan.FromDays(1));

builder.Services.Configure<PasswordResetTokenProviderOptions>(opt =>
    opt.TokenLifespan = TimeSpan.FromHours(2));

if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
{
    Console.WriteLine("HIPA-BE is running in development mode.");
    // Add services to the container.
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options => { options.SupportNonNullableReferenceTypes(); });
    // Add logging for debugging
    // builder.Host.UseSerilog();
}

// Overwrite default model validation response provided by "ApiController" attribute
builder.Services.AddMvc().ConfigureApiBehaviorOptions(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        if (context.ModelState.Values.First().Errors.Count != 0)
        {
            // Select first error from possible multiple model validation errors
            // we don't have to return every error since it is handeled with more detail on frontend
            var firstError = context.ModelState.Values.First().Errors.First();
            var errorResponse = new
            {
                type = "https://tools.ietf.org/html/rfc9110#section-15.5.1",
                title = firstError.ErrorMessage,
                status = StatusCodes.Status400BadRequest,
                traceId = Activity.Current?.Id ?? context.HttpContext.TraceIdentifier
            };
            return new BadRequestObjectResult(errorResponse);
        }
        return null;
    };
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
    policy =>
    {
        policy
            .WithOrigins(Environment.GetEnvironmentVariable("FRONTEND_BASEURL") ?? "http://localhost")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
            .WithExposedHeaders("Location", "Upload-Offset", "Authorization");
    });
});

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.Authority = $"http://{Environment.GetEnvironmentVariable("ASPNETCORE_HTTP_HOST")}:{Environment.GetEnvironmentVariable("ASPNETCORE_HTTP_PORT")}";
        options.RequireHttpsMetadata = false;
        options.Configuration = new OpenIdConnectConfiguration();

        string keyJson = File.ReadAllText("tempkey.jwk");

        RSAParameters rsaParameters = RSAKey.ParseJsonToRsaParameters(keyJson);
        var rsaSecurityKey = new RsaSecurityKey(rsaParameters);

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false, // Validates the audience of the token TODO: check why JWT doesnt contain AUD claim
            ValidateIssuerSigningKey = true, // Validates the signing key upon token validation
            IssuerSigningKey = rsaSecurityKey,
            ValidateIssuer = true,
            ValidIssuers = [
                $"http://{Environment.GetEnvironmentVariable("ASPNETCORE_HTTP_HOST")}:{Environment.GetEnvironmentVariable("ASPNETCORE_HTTP_PORT")}",
                $"https://{Environment.GetEnvironmentVariable("ASPNETCORE_HTTPS_HOST")}:{Environment.GetEnvironmentVariable("ASPNETCORE_HTTPS_PORT")}",
                "http://localhost:81"
            ],
            ValidateLifetime = true, // Validates token expiration
            ClockSkew = TimeSpan
                .FromMinutes(5), // Clock skew compensates for server time drift. We recommend 5 minutes or less:

            // Other parameters can also be set to ensure the token's integrity:
            RequireExpirationTime = true, // Requires the token to have an 'exp' claim
            RequireSignedTokens = true, // Requires the token to have been signed

            // You could also enforce that certain claims must be present:
            // (Example: The token must have a 'sub' claim)
            NameClaimType = "sub",
            RoleClaimType = "user_role"
        };
        options.IncludeErrorDetails = true;
    });


builder.Services.AddAuthorization();

// Configure Identity core password requirements
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = false;
    options.Password.RequireDigit = true;
});

builder.Services.AddIdentityServer(options =>
        {
            options.UserInteraction.LoginUrl =
                $"{Environment.GetEnvironmentVariable("FRONTEND_BASEURL")}/login";
        }
    )
    .AddAspNetIdentity<ApplicationUser>()
    .AddProfileService<ProfileService>()
    .AddConfigurationStore(options =>
    {
        options.ConfigureDbContext = b =>
            b.UseNpgsql(webApiDatabase, opt => opt.MigrationsAssembly(assembly));
    })
    .AddOperationalStore(options =>
    {
        options.ConfigureDbContext = b =>
            b.UseNpgsql(webApiDatabase, opt => opt.MigrationsAssembly(assembly));
    })
    .AddInMemoryIdentityResources(Config.IdentityResources)
    .AddInMemoryClients(Config.Clients)
    // only use in developer environment
    .AddDeveloperSigningCredential()
    .Services.AddTransient<ICorsPolicyService>((container) =>
    {
        var logger = container.GetRequiredService<ILogger<DefaultCorsPolicyService>>();
        return new DefaultCorsPolicyService(logger)
        {
            AllowedOrigins =
            {
                Environment.GetEnvironmentVariable("FRONTEND_BASEURL")
            } // list of origins that are allowed to access the server (frontend)
        };
    });

if (builder.Environment.IsDevelopment())
{
    // FOR DEV PURPOSES ONLY
    // Addded Transient service for database seeding
    builder.Services.AddTransient<DbInitializer>();
}

// create folder for tus uploads
var uploadPath = builder.Configuration.GetValue<string>("TusConfig:UploadPath");
if (!Directory.Exists(uploadPath)) Directory.CreateDirectory(uploadPath);

//TusDiskStore
builder.Services.AddSingleton<TusDiskStore>(sp => 
    new TusDiskStore(
        uploadPath,
        deletePartialFilesOnConcat: true,
        bufferSize: TusDiskBufferSize.Default
    )
);

// Service registration
builder.Services.AddScoped<ApplicationUserService>();
builder.Services.AddTransient<EmailSenderService>();
builder.Services.AddScoped<AuthorizationService>();
builder.Services.AddTransient<OrganService>();
builder.Services.AddTransient<DiagnosisService>();
builder.Services.AddTransient<SampleImageService>();
builder.Services.AddTransient<SampleImageAnnotationService>();
builder.Services.AddTransient<BodySystemService>();
builder.Services.AddTransient<AdminService>();
builder.Services.AddTransient<LearningService>();
builder.Services.AddHostedService<DbCleanupService>();
builder.Services.AddTransient<ConversionService>();
builder.Services.AddSingleton(GetTusConfigurationFactory());
builder.Services.AddSingleton(CreateTusConfigurationForCleanupService());
builder.Services.AddScoped<ITableReaderService>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    var logger = sp.GetRequiredService<ILogger<TableReaderService>>();
    return new TableReaderService(configuration, logger);
});

//builder.Services.AddScoped<StudentImportService>();

builder.Services.AddControllers();



var app = builder.Build();

// Run migrations
var retries = 10;
for (int i = 1; i <= retries; i++)
{
    try
    {
        var scope = app.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        dbContext.Database.Migrate();

        var identityDbContext = scope.ServiceProvider.GetRequiredService<AspNetIdentityDbContext>();
        identityDbContext.Database.Migrate();

        var configurationDbContext = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
        configurationDbContext.Database.Migrate();

        var persistentGrantDbContext = scope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>();
        persistentGrantDbContext.Database.Migrate();

        break;
    }
    catch (Exception e)
    {
        if (i == retries)
        {
            Log.Fatal("Could not attempt to execute migrations after maximum attempts. Aborting.");
            System.Environment.Exit(1);
        }
        Log.Warn($"Attempt to execute migrations failed. {retries - i} retries remaining.", e);
        Thread.Sleep(2000);
    }
}

app.UseCors();

async Task SeedDummyData(IHost app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();
    if (scopedFactory == null) throw new Exception("Could not get service scope factory");

    using (var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<DbInitializer>();
        if (service == null) throw new Exception("Could not get db initializer service");

        await service.Initialize();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Seed database with dummy data
    await SeedDummyData(app);

    // Add logging for debugging
    // Log.Logger = new LoggerConfiguration()
    //    .MinimumLevel.Debug()
    //    .WriteTo.Console()
    //    .CreateLogger();

    Microsoft.IdentityModel.Logging.IdentityModelEventSource.ShowPII = true;
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    // Add production logging
    //Log.Logger = new LoggerConfiguration()
    //    .MinimumLevel.Debug()
    //    .WriteTo.Console()
    //    .CreateLogger();

    app.UseHttpsRedirection();
}

app.UseRouting();

app.UseExceptionHandler("/error");

app.UseIdentityServer();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    // Setup tusdotnet for the /files/ path.
    _ = endpoints.MapTus(app.Configuration.GetValue<string>("TusConfig:UploadUrl"), TusConfigurationFactory);
});

// Route for static images
app.UseStaticFiles(options: new StaticFileOptions
{
    OnPrepareResponse = (ctx) =>
    {
        var context = ctx.Context;
        //The request has to contain authentication information to access protected resources
        if (context.Request.Path.Value.StartsWith("/static/admin/protected"))
        {
            if (!context.User.IsAuthenticated() || !context.User.IsInRole("Admin"))
            {
                context.Response.StatusCode = 403;
                context.Response.ContentLength = 0;
                context.Response.Body = Stream.Null;
            }
            return;
        }
    },
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "assets/public")),
    RequestPath = "/static",
    ServeUnknownFileTypes = false,
});

// WSI serving
string wsiPath = builder.Configuration.GetValue<string>("AssetConfiguration:SampleImagesPath") ?? "/media/wsi";
if (Directory.Exists(wsiPath))
{
    app.UseStaticFiles(new StaticFileOptions
    {
        OnPrepareResponse = (ctx) =>
        {
            var context = ctx.Context;
            if ((!context.Request.Path.Value!.EndsWith(".dzi", StringComparison.OrdinalIgnoreCase) && !context.Request.Path.Value!.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase)) &&
                !context.User.IsAuthenticated())
            {
                context.Response.StatusCode = 403;
                context.Response.ContentLength = 0;
                context.Response.Body = Stream.Null;
            }
            return;
        },

        FileProvider = new PhysicalFileProvider(wsiPath),
        RequestPath = "/wsi",
        ServeUnknownFileTypes = true
    });
}
// string pdfsPath = builder.Configuration.GetValue<string>("AssetConfiguration:PdfsPath") ?? "/media/pdfs";
string pdfsPath = Path.Combine(builder.Environment.ContentRootPath, "media", "pdfs");
if (!Directory.Exists(pdfsPath))
{
    Directory.CreateDirectory(pdfsPath);   
}

app.UseStaticFiles(new StaticFileOptions
    {
        OnPrepareResponse = (ctx) =>
        {
            var context = ctx.Context;
            if (!context.Request.Path.Value!.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase)  &&
                !context.User.IsAuthenticated())
            {
                context.Response.StatusCode = 403;
                context.Response.ContentLength = 0;
                context.Response.Body = Stream.Null;
            }
        },
        FileProvider = new PhysicalFileProvider(pdfsPath), 
        RequestPath = "/files"

    });

app.MapControllers();

//old XLS code pages support
System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);


Log.Info("Application starting.");
app.Run();
