using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogManagement
{
    public class LogEntityRelationship
    {
        public Guid Id { get; set; }

        public Log Log { get; set; }

        public string EntityId { get; set; }

        public LogEntityRelationship(Log log, string entityId)
        {
            Id = Guid.NewGuid();

            Log = log;

            EntityId = entityId;
        }
    }

    public class LogManager
    {
        public static List<Log> UnrelatedLogs = new List<Log>();

        public static List<LogEntityRelationship> Relationships = new List<LogEntityRelationship>();

        public static void AddRelation(Log log, string entityId = null)
        {
            if (entityId != null)
            {
                if (!Relationships.Any(r => r.Log.Id == log.Id && r.EntityId == entityId))
                {
                    Relationships.Add(new LogEntityRelationship(log, entityId));
                }
            }
            else
            {
                if (!UnrelatedLogs.Any(ul => ul.Id == log.Id))
                {
                    UnrelatedLogs.Add(log);
                }
            }
        }


        public static List<LogEntityRelationship> GetRelations(Func<LogEntityRelationship,bool> predicate,int? toTake = null)
        {
            var results = Relationships.Where(predicate)
                                .OrderByDescending(r => r.Log.RecordDate)
                                .ToList();

            return toTake != null ? results.Take(toTake.Value).ToList() : results;
        }

        public static List<Log> GetLogsByEntity(string entityId, int? toTake = null)
        {
            return GetRelations(ler => ler.EntityId == entityId, toTake).Select(ler => ler.Log).ToList();
        }
    }
}
