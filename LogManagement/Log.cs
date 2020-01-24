using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LogManagement
{
    public enum LogType
    {
        Error,
        Info,
        Warning,
        Debug
    }

    public enum LogStorageType
    {
        Database,
        Cache,
        File
    }

    public class Log
    {
        public Guid Id { get; set; }

        public LogType LogType { get; set; }

        public LogStorageType LogStorageType { get; set; }

        public string Platform { get; set; }

        public string Category { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string EntityId { get; set; }

        public DateTime RecordDate { get; set; }

        public Log(string category,
            string name,
            string description,
            LogType logType = LogType.Error,
            LogStorageType logStorageType = LogStorageType.Cache,
            string entityId = null,
            string platform = null)
        {
            Id = Guid.NewGuid();

            Category = category;

            Name = name;

            Description = description;

            LogType = logType;

            LogStorageType = logStorageType;

            EntityId = entityId;

            Platform = platform ?? Assembly.GetCallingAssembly().GetName().Name;

            RecordDate = DateTime.Now;
        }

        public Log(string description,
            LogType logType = LogType.Error,
            LogStorageType logStorageType = LogStorageType.Cache,
            string entityId = null,
            string platform = null,
            int frameIndex = 1)
        {
            Id = Guid.NewGuid();

            var methodBase = new StackTrace().GetFrame(frameIndex).GetMethod();

            Category = methodBase.DeclaringType.Name;

            Name = methodBase.Name;

            Description = description;

            LogType = logType;

            LogStorageType = logStorageType;

            EntityId = entityId;

            Platform = platform ?? Assembly.GetCallingAssembly().GetName().Name;

            RecordDate = DateTime.Now;
        }

        public static void Create(string description,
            LogType logType = LogType.Error,
            LogStorageType logStorageType = LogStorageType.Cache,
            string entityId = null,
            string platform = null)
        {
            var log = new Log(description,
                                logType,
                                logStorageType,
                                entityId,
                                platform ?? Assembly.GetCallingAssembly().GetName().Name,
                                2);

            if (log.LogStorageType == LogStorageType.Database)
            {
                Task.Run(()=> 
                {
                    //Db process will be run
                });
            }

            LogManager.AddRelation(log, entityId);
        }
    }
}
