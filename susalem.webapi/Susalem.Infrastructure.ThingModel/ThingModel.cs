namespace Susalem.Infrastructure.ThingModel
{
    public class DeviceModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Property> Properties { get; set; }
        public List<ReadConfig> ReadConfigs { get; set; }
        public List<CommandConfig> CommandConfigs { get; set; }
    }

    public class Property
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public int Address { get; set; }
        public int Length { get; set; }
        public DataType DataType { get; set; }
        public string Expression { get; set; }
        public Metadata Metadata { get; set; }
    }

    public class DataType
    {
        public string Type { get; set; }
        public string ByteOrder { get; set; }
    }

    public class Metadata
    {
        public string Unit { get; set; }
        public int Precision { get; set; }
    }

    public class ReadConfig
    {
        public string Name { get; set; }
        public int FunctionCode { get; set; }
        public string Mode { get; set; }
        public Trigger Trigger { get; set; }
        public List<string> PropertyKeys { get; set; }
        public Optimization Optimization { get; set; }
    }

    public class Trigger
    {
        public string Type { get; set; }
        public int Interval { get; set; }
    }

    public class Optimization
    {
        public bool MergeAdjacent { get; set; }
        public string RetryPolicy { get; set; }
    }

    public class CommandConfig
    {
        public string Name { get; set; }
        public int FunctionCode { get; set; }
        public List<CommandParameter> Parameters { get; set; }
    }

    public class CommandParameter
    {
        public string Name { get; set; }
        public string DataType { get; set; }
        public int Address { get; set; }
        public string ByteOrder { get; set; }
        public Transform Transform { get; set; }
    }

    public class Transform
    {
        public string Expression { get; set; }
        public Validation Validation { get; set; }
    }

    public class Validation
    {
        public float Min { get; set; }
        public float Max { get; set; }
    }
}
