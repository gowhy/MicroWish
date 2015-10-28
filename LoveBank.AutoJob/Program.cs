using System;
using Microsoft.Owin.Hosting;
using Owin;
using Quartz;
using Quartz.Impl;
using LoveBank.Common;
using LoveBank.Common.Data;
using LoveBank.Common.Config;


namespace LoveBank.AutoJob
{
    class Program {
        static void Start(IAppBuilder app) {

           


            // First, initialize Quartz.NET as usual. In this sample app I'll configure Quartz.NET by code.
            var schedulerFactory = new StdSchedulerFactory();
            var scheduler = schedulerFactory.GetScheduler();
            scheduler.Start();

            // I'll add some global listeners
            //scheduler.ListenerManager.AddJobListener(new GlobalJobListener());
            //scheduler.ListenerManager.AddTriggerListener(new GlobalTriggerListener());

            // A sample trigger and job
            var trigger = TriggerBuilder.Create()
                .WithIdentity("myTrigger")
                .WithSchedule(DailyTimeIntervalScheduleBuilder.Create()
                    .WithIntervalInSeconds(60*10))//10分钟执行一次
                .StartNow()
                .Build();
            var job = new JobDetailImpl("crawlInfoJob", null, typeof(CrawlInfoJob));
            scheduler.ScheduleJob(job, trigger);

         

        
       
        }
 

        private static void Main(string[] args) {

         
            using (WebApp.Start("http://localhost:12345", Start))
                Console.ReadLine();
        }
    }
}