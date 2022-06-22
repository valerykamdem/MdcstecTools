namespace MdcstecTools.Shared
{
    public class ParameterDto
    {
        public string? BlockName { get; set; }
        public string? ParamName { get; set; }
        public string? ParamValue { get; set; }
        //public Parameters? Parameters { get; set; }
    }

    public class ParametersDto
    {
        public string? BlockName { get; set; }
        public Parameters? Parameters { get; set; }
    }

    public class ConnectionDto
    {
        public string? BlockName { get; set; }
        public int? BlockId { get; set; }

        public string? InputEnd { get; set; }

        public string? OutputEnd { get; set; }

        public string? ConnectionForm { get; set; }

        public string? GraphicalForm { get; set; }

        public List<Vertex>? Vertex { get; set; }
    }

    public class ConnectionsDto
    {
        public string? BlockName { get; set; }
        public Connections? Connections { get; set; }
    }

    public class BlockDto
    {
        public string? BlockName { get; set; }
        public int BlockId { get; set; }
    }

    public class BlockDefDto
    {

        public string? BlockName { get; set; }

        public string? EntityName { get; set; }

        public int BlockId { get; set; }

        public string? BlockGUID { get; set; }

        public string? BlockDesc { get; set; }

        public string? TemplateName { get; set; }

        public string? ClassName { get; set; }

        public string? BaseTemplateName { get; set; }

        //public object? CreateType { get; set; }

        public int Attribute { get; set; }

        //public Coord? Coord { get; set; }

        public string? LifeCycleState { get; set; }

        public string? AssignedTo { get; set; }

        //public object? Container { get; set; }
    }
}
