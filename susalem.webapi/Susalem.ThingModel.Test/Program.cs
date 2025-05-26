using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Susalem.Infrastructure.Services.DbInitializerService;
using Serilog;
using Microsoft.AspNetCore.Hosting;
using Susalem.ThingModel.Test.MobudsThing;
using Quartz.Impl;
using Quartz;
using Susalem.Infrastructure.ThingModel;
using Newtonsoft.Json;
using Susalem.Messages.Features.Channel;

namespace Susalem.ThingModel.Test
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            CreateTrigger();
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<Program>>();

                try
                {
                    var dbInitializer = services.GetRequiredService<IDbInitializerService>();
                    logger.LogInformation($"Running database migration/seed");
                    dbInitializer.Migrate();
                    await dbInitializer.SeedAsync();

                    var thingService = services.GetRequiredService<IModbusThingService>();
                    await thingService.StartMonitor();

                }
                catch (Exception e)
                {
                    logger.LogError(e, "An error occurred while running database migration.");
                }
            }
            host.Run();
            
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseWindowsService();

        public static void CreateTrigger()
        {
            StdSchedulerFactory factory = new StdSchedulerFactory();
            IScheduler scheduler = factory.GetScheduler().Result;
            IJobDetail job = JobBuilder.Create<ThingConnectTask>()
                         .WithIdentity("jobConnect", "jobGroup")      //唯一标识
                         .StoreDurably(true)                     //即时没有指定的触发器，该job也会被存储
                         .WithDescription("定时重连")        //该作业的描述信息
                         .RequestRecovery(true)                  //如果当前任务崩溃，则会重新执行该作业
                         .Build();

            //创建一个触发器
            ITrigger trigger =
                TriggerBuilder.Create()                                  //获取TriggerBuilder
                              .StartNow()                                //马上开始
                              .ForJob(job)                               //将触发器关联给指定的job
                              .WithPriority(10)                          //优先级，当触发时间一样时，优先级大的触发器先执行
                              .WithIdentity("tname1", "group1")          //添加名字和分组
                              .WithSimpleSchedule(x => x.WithInterval(TimeSpan.FromMilliseconds(2000)) //调度，一秒执行一次，执行三次
                                                        .WithRepeatCount(100)
                                                        .Build())
                              .Build();
            scheduler.ScheduleJob(job, trigger);
            scheduler.Start();
        }


        public void InstantiateDevice()
        {
            string txt = File.ReadAllText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,@"Demos\Demo.json"));
            ThingObject thing = JsonConvert.DeserializeObject<ThingObject>(txt);
            Appsession.Devices.Add(thing);
        }
    }
}
