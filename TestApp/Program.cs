using LogManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TestApp
{
    public class TestObject
    {
        public string Name { get; set; }

        public Guid Id { get; set; }

        public int Number { get; set; }
    }

    class Program
    {
        public static TestObject to = new TestObject();

        static void Main(string[] args)
        {
            Func();

            Console.WriteLine(LogManager.Relationships.ToString());

            var logs = LogManager.GetLogsByEntity(to.Id.ToString());
        }

        static void Func()
        { 
            try
            {
                to.Name = "test";
                to.Id = Guid.NewGuid();
                var g = Guid.Parse("abcded");
            }
            catch (Exception ex)
            {
                Log.Create(description: ex.Message, entityId: to.Id.ToString());
            }
        }
    }
}
