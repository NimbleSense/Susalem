using Susalem.Infrastructure.ThingModel;
using System.Linq.Expressions;

namespace Susalem.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            List<Property> properties = new List<Property>();
            List<ReadConfig> readConfigs = new List<ReadConfig>();
            List<CommandConfig> commandConfigs = new List<CommandConfig>();

            properties.Add(new Property()
            {
                Key= "light",
                Name = "µÆ×´Ì¬",
                Address = 30001,
                Length=1,
                DataType = new DataType("Bool","ABCD"),
                Expression = string.Empty,
                Metadata = new Metadata(string.Empty,1)
            });

            readConfigs.Add(new ReadConfig()
            {
                Name = "×´Ì¬"
            });

            commandConfigs.Add(new CommandConfig()
            {

            });
            DeviceModel deviceModel = new DeviceModel()
            {
                Name = "Alerter",
                Description = "±¨¾¯Æ÷",
                Properties = properties,
                ReadConfigs = readConfigs,
                CommandConfigs = commandConfigs
            };
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}