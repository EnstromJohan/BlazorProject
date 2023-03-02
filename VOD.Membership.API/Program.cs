var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(policy => {
    policy.AddPolicy("CorsAllAccessPolicy", opt =>
    opt.AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod()
    );
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

ConfigAutoMapper();

builder.Services.AddDbContext<VODContext>(
options => options.UseSqlServer(
builder.Configuration.GetConnectionString("VODConnection")));

builder.Services.AddScoped<IDbService, DbService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CorsAllAccessPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();

void ConfigAutoMapper()
{
    var config = new AutoMapper.MapperConfiguration(cfg =>
    {
        cfg.CreateMap<Director, DirectorDTO>().ReverseMap();

        cfg.CreateMap<Film, FilmDTO>()
        .ReverseMap()
        .ForMember(dest => dest.FilmGenres, src => src.Ignore());

        cfg.CreateMap<FilmCreateDTO, Film>()
        .ForMember(dest => dest.FilmGenres, src => src.Ignore());

        cfg.CreateMap<FilmEditDTO, Film>()
        .ForMember(dest => dest.FilmGenres, src => src.Ignore());

        cfg.CreateMap<FilmGenre, FilmGenreDTO>().ReverseMap();

        cfg.CreateMap<Genre, GenreDTO>().ReverseMap();
        cfg.CreateMap<SimilarFilms, SimilarFilmsDTO>()
        .ReverseMap()
        .ForMember(dest => dest.Film, src => src.Ignore());
    });

    var mapper = config.CreateMapper();
    builder.Services.AddSingleton(mapper);
}