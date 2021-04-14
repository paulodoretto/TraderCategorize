using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TradeCategories.Domain;
using TradeCategories.Infra.Interfaces;
using TradeCategories.Infra.Repositories;
using TradeCategories.Services.DTO;
using TradeCategories.Services.Interfaces;
using TradeCategories.Services.Services;

namespace PlayerUI
{
    public static class Program
    {
        //..
        
        private static void ConfigureServices(ServiceCollection services)
        {
            var autoMapperconfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Category, CategoryDTO>().ReverseMap();
                cfg.CreateMap<Trader, TraderDTO>().ReverseMap();
            });

            services.AddSingleton(autoMapperconfig.CreateMapper());

            services.AddTransient<frmMenu>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ITradeService, TradeService>();
            services.AddScoped<ICategoryService, CategoryService>();
            

            services.AddTransient<IDbConnection>(db =>
                                                {
                                                    var conection = new OracleConnection(ConfigurationManager.ConnectionStrings["ORACLEDBSTRING"].ConnectionString);
                                                    conection.Open();
                                                    return conection;
                                                });
            
        }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var services = new ServiceCollection();
            ConfigureServices(services);

            using (ServiceProvider serviceProvider = services.BuildServiceProvider())
            {
                var form1 = serviceProvider.GetRequiredService<frmMenu>();
                Application.Run(form1);
            }


            
        }
    }


}
