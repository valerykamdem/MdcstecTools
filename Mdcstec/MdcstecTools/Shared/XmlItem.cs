using System.Xml.Serialization;

namespace MdcstecTools.Shared
{
	// using System.Xml.Serialization;
	// XmlSerializer serializer = new XmlSerializer(typeof(MultiBlock));
	// using (StringReader reader = new StringReader(xml))
	// {
	//    var test = (MultiBlock)serializer.Deserialize(reader);
	// }

	[Serializable, XmlRoot(ElementName = "ErdbVersion")]
	public class ErdbVersion
	{

		[XmlElement(ElementName = "DbVersion")]
		public double DbVersion { get; set; }

		[XmlElement(ElementName = "DbDesc")]
		public string? DbDesc { get; set; }

		[XmlElement(ElementName = "DbGUID")]
		public string? DbGUID { get; set; }

		[XmlElement(ElementName = "DbLangID")]
		public string? DbLangID { get; set; }
	}

	[Serializable, XmlRoot(ElementName = "Coord")]
	public class Coord
	{

		[XmlElement(ElementName = "Left")]
		public int Left { get; set; }

		[XmlElement(ElementName = "Top")]
		public int Top { get; set; }

		[XmlElement(ElementName = "Right")]
		public int Right { get; set; }

		[XmlElement(ElementName = "Bottom")]
		public int Bottom { get; set; }
	}

	[Serializable, XmlRoot(ElementName = "BlockDef")]
	public class BlockDef
	{

		[XmlElement(ElementName = "BlockName")]
		public string? BlockName { get; set; }

		[XmlElement(ElementName = "EntityName")]
		public string? EntityName { get; set; }

		[XmlElement(ElementName = "BlockId")]
		public int BlockId { get; set; }

		[XmlElement(ElementName = "BlockGUID")]
		public string? BlockGUID { get; set; }

		[XmlElement(ElementName = "BlockDesc")]
		public string? BlockDesc { get; set; }

		[XmlElement(ElementName = "TemplateName")]
		public string? TemplateName { get; set; }

		[XmlElement(ElementName = "ClassName")]
		public string? ClassName { get; set; }

		[XmlElement(ElementName = "BaseTemplateName")]
		public string? BaseTemplateName { get; set; }

		[XmlElement(ElementName = "CreateType")]
		public object? CreateType { get; set; }

		[XmlElement(ElementName = "Attribute")]
		public int Attribute { get; set; }

		[XmlElement(ElementName = "Coord")]
		public Coord? Coord { get; set; }

		[XmlElement(ElementName = "LifeCycleState")]
		public string? LifeCycleState { get; set; }

		[XmlElement(ElementName = "AssignedTo")]
		public string? AssignedTo { get; set; }

		[XmlElement(ElementName = "Container")]
		public object? Container { get; set; }
	}

	[Serializable, XmlRoot(ElementName = "Parameter")]
	public class Parameter
	{

		[XmlElement(ElementName = "ParamName")]
		public string? ParamName { get; set; }

		[XmlElement(ElementName = "ParamValue")]
		public string? ParamValue { get; set; }
	}

	[Serializable, XmlRoot(ElementName = "Parameters")]
	public class Parameters
	{

		[XmlElement(ElementName = "Parameter")]
		public List<Parameter>? Parameter { get; set; }
	}

	[Serializable, XmlRoot(ElementName = "SymbolAttr")]
	public class SymbolAttr
	{

		[XmlElement(ElementName = "ParamName")]
		public string? ParamName { get; set; }

		[XmlElement(ElementName = "AttrType")]
		public string? AttrType { get; set; }

		[XmlElement(ElementName = "AttrOrder")]
		public int AttrOrder { get; set; }

		[XmlElement(ElementName = "AttrViewValue")]
		public string? AttrViewValue { get; set; }

		[XmlElement(ElementName = "AttrViewLabel")]
		public string? AttrViewLabel { get; set; }

		[XmlElement(ElementName = "XCoord")]
		public int XCoord { get; set; }

		[XmlElement(ElementName = "YCoord")]
		public int YCoord { get; set; }
	}

	[Serializable, XmlRoot(ElementName = "SymbolAttrs")]
	public class SymbolAttrs
	{

		[XmlElement(ElementName = "SymbolAttr")]
		public List<SymbolAttr>? SymbolAttr { get; set; }
	}

	[Serializable, XmlRoot(ElementName = "Vertex")]
	public class Vertex
	{

		[XmlElement(ElementName = "XVertex")]
		public int? XVertex { get; set; }

		[XmlElement(ElementName = "YVertex")]
		public int? YVertex { get; set; }
	}

	[Serializable, XmlRoot(ElementName = "Connection")]
	public class Connection
	{

		[XmlElement(ElementName = "BlockId")]
		public int? BlockId { get; set; }

		[XmlElement(ElementName = "OutputEnd")]
		public string? OutputEnd { get; set; }

		[XmlElement(ElementName = "InputEnd")]
		public string? InputEnd { get; set; }

		[XmlElement(ElementName = "ConnectionForm")]
		public string? ConnectionForm { get; set; }

		[XmlElement(ElementName = "GraphicalForm")]
		public string? GraphicalForm { get; set; }

		[XmlElement(ElementName = "Vertex")]
		public List<Vertex>? Vertex { get; set; }
	}

	[Serializable, XmlRoot(ElementName = "Connections")]
	public class Connections
	{

		[XmlElement(ElementName = "Connection")]
		public List<Connection>? Connection { get; set; }
	}

	[Serializable, XmlRoot(ElementName = "Block")]
	public class Block
	{

		[XmlElement(ElementName = "BlockDef")]
		public BlockDef? BlockDef { get; set; }

		[XmlElement(ElementName = "Parameters")]
		public Parameters? Parameters { get; set; }

		[XmlElement(ElementName = "SymbolAttrs")]
		public SymbolAttrs? SymbolAttrs { get; set; }

		[XmlElement(ElementName = "Connections")]
		public Connections? Connections { get; set; }

		[XmlElement(ElementName = "EmbBlocks")]
		public EmbBlocks? EmbBlocks { get; set; }
	}

	[Serializable, XmlRoot(ElementName = "EmbBlocks")]
	public class EmbBlocks
	{

		[XmlElement(ElementName = "Block")]
		public List<Block>? Block { get; set; }
	}

	[Serializable, XmlRoot(ElementName = "MultiBlock")]
	public class MultiBlock
	{

		[XmlElement(ElementName = "ErdbVersion")]
		public ErdbVersion? ErdbVersion { get; set; }

		[XmlElement(ElementName = "Block")]
		public Block? Block { get; set; }

		//[XmlAttribute(AttributeName = "xmlns")]
		//public string Xmlns { get; set; }

		//[XmlText]
		//public string Text { get; set; }
	}

}
