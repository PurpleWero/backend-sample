using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Firebase.Database;


    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddSingleton(new FirebaseClient("https://sample-db-app-2312-default-rtdb.firebaseio.com/"));
            // Configuración de DbContext
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
           
           
           services.AddControllers();



            // Configuración de Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Employees",
                Version = "v1",
                Description = "API CRUD",
                Contact = new OpenApiContact
                {
                    Name = "Cristian Almanza",
                    Email = "calm2312@hotmail.com",
                },
                License = new OpenApiLicense
                {
                    Name = "CALM@2023",
                    
                }
            });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            // Habilitar el middleware de Swagger
            app.UseSwagger();

            // Especificar el endpoint de Swagger
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API V1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

