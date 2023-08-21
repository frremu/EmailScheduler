
using EmailMicroService;
using EmailMicroService.Model;
using EmailMicroService.Services;
using Hangfire;
using Hangfire.Mongo.Migration.Strategies.Backup;
using Hangfire.Mongo.Migration.Strategies;
using Hangfire.Mongo;
using Hangfire.SqlServer;
using MongoDB.Driver;
using System;



class Program
{
    static void Main(string[] args)
    {
        
        var options = new MongoStorageOptions
        {
            MigrationOptions = new MongoMigrationOptions
            {
                MigrationStrategy = new DropMongoMigrationStrategy(),
                BackupStrategy = new NoneMongoBackupStrategy()
            }
        };
        var mongoStorage = new MongoStorage(
                        MongoClientSettings.FromConnectionString("mongodb+srv://ufarooq:R3fundUnt!l3355@cluster0.hjwxxbp.mongodb.net/?retryWrites=true&w=majority"),
                        "Mbot_HangFire", // database name
                        options);



        GlobalConfiguration.Configuration.UseStorage(mongoStorage);

        



        var message = new Message(new string[] { "umerfarooqnu@gmail.com" }, "test", "Email successfully sent from k201625");

        var emailService = new ServiceEmailcs();

        emailService.EnqueueEmail(message);

        //BackgroundJob.Enqueue(() => emailService.EnqueueEmail(message));


        //BackgroundJob.Enqueue(() => DemoJob());
        //BackgroundJob.Enqueue(() => SendEmail(message));
        //Console.WriteLine("Demo job enqueued!");

        Console.WriteLine("Email job enqueued!");

        using (new BackgroundJobServer(mongoStorage))
        {
            Console.WriteLine("Hangfire server started. Press any key to exit...");
            Console.ReadKey();
        }
        


    }

    //public static void SendEmail(Message message)
    //{
    //    var emailService = new ServiceEmailcs(); 
    //    emailService.EnqueueEmail(message);
    //}

    public static void DemoJob()
    {
        Console.WriteLine("This is a demo background job!");
    }
}
