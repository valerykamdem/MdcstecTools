using Microsoft.AspNetCore.Components;
using System.Xml.Serialization;
using System.Xml.Linq;
using System.Xml;
using System.Text;
using Microsoft.JSInterop;
using MudBlazor;
using Microsoft.AspNetCore.Components.Forms;
using MdcstecTools.Shared;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;

namespace MdcstecTools.Client.Pages
{
    [Authorize]
    public class ConfigFileBase : ComponentBase
    {
        #region Declaration
        [Inject] public IJSRuntime JSRuntime { get; set; }
        [Inject] public ISnackbar Snackbar { get; set; }
        [Inject] public HttpClient Http { get; set; }

        protected bool ShowGrid { get; set; }
        protected string _param = string.Empty;
        protected string BlockName = string.Empty;

        protected bool Clearing = false;
        protected static string DefaultDragClass = "relative rounded-lg border-2 border-dashed pa-4 mt-4 mud-width-full mud-height-full";
        protected string DragClass = DefaultDragClass;
        protected readonly List<IBrowserFile> loadedFiles = new();

        protected readonly List<ErdbVersion> ErdbVersions = new();
        protected readonly List<BlockDef> BlockDefs = new();
        protected readonly List<Coord> Coords = new();
        protected List<Vertex> VertexList = new();

        //protected MultiBlock MultiBlock = new();
        protected ErdbVersion ErdbVersion = new();
        protected BlockDef BlockDef = new();
        protected Coord Coord = new();
        protected Parameter Parameter = new();
        protected Parameters Parameters = new();
        protected SymbolAttr SymbolAttr = new();
        protected SymbolAttrs SymbolAttrs = new();
        protected Connection Connection = new();
        protected Connections Connections = new();
        protected Vertex Vertex = new();

        // Dtos
        protected ParameterDto ParameterDto = new();
        protected ParametersDto ParametersDto = new();
        protected List<ParameterDto> ParameterDtos = new();       
        protected List<ParametersDto> ParametersDtos = new();

        protected ConnectionDto ConnectionDto = new();
        protected ConnectionsDto ConnectionsDto = new();
        protected List<ConnectionDto> ConnectionDtos = new();
        protected List<ConnectionsDto> ConnectionsDtos = new();

        protected BlockDto BlockDto = new();
        protected List<BlockDto> BlockDtos = new();
        protected BlockDefDto BlockDefDto = new();
        protected List<BlockDefDto> BlockDefDtos = new();

        protected List<string> ErdbVersionProps = new();
        protected List<string> BlockProps = new();
        protected List<string> BlockDefProps = new();
        protected List<string> CoordProps = new();
        protected List<string> ParameterProps = new();
        protected List<string> SymbolAttrProps = new();
        protected List<string> EmbBlockProps = new();
        protected List<string> ConnectionProps = new();
        protected List<string> VertexProps = new();
        protected List<string> ParameterDtoProps = new();
        protected List<string> ConnectionDtoProps = new();
        protected List<string> BlockDtoProps = new();
        protected List<string> BlockDefDtoProps = new();

        protected List<MultiBlock> MultiBlocks = new();

        #endregion

        protected override void OnInitialized()
        {

            //var content = await Http.GetStringAsync(@"C:\Users\valery.kamdem\Projects\Michel\michel3\michel3");

            //string[] dirs = Directory.GetFiles(@"c:\", "c*");
            //Console.WriteLine("The number of files starting with c is {0}.", dirs.Length);

            //await JSRuntime.InvokeAsync<object>("console.log", content);
            //await JSRuntime.InvokeAsync<object>("fileScript");

            foreach (var propertyInfo in typeof(ErdbVersion).GetProperties())
            {
                ErdbVersionProps.Add(propertyInfo.Name);
            }

            foreach (var propertyInfo in typeof(BlockDef).GetProperties())
            {
                BlockDefProps.Add(propertyInfo.Name);
            }

            foreach (var propertyInfo in typeof(Coord).GetProperties())
            {
                CoordProps.Add(propertyInfo.Name);
            }

            foreach (var propertyInfo in typeof(Parameter).GetProperties())
            {
                ParameterProps.Add(propertyInfo.Name);
            }

            foreach (var propertyInfo in typeof(SymbolAttr).GetProperties())
            {
                SymbolAttrProps.Add(propertyInfo.Name);
            }

            foreach (var propertyInfo in typeof(Connection).GetProperties())
            {
                ConnectionProps.Add(propertyInfo.Name);
            }

            foreach (var propertyInfo in typeof(Vertex).GetProperties())
            {
                VertexProps.Add(propertyInfo.Name);
            }

            foreach (var propertyInfo in typeof(ParameterDto).GetProperties())
            {
                ParameterDtoProps.Add(propertyInfo.Name);
            }

            foreach (var propertyInfo in typeof(ConnectionDto).GetProperties())
            {
                ConnectionDtoProps.Add(propertyInfo.Name);
            }

            foreach (var propertyInfo in typeof(BlockDto).GetProperties())
            {
                BlockDtoProps.Add(propertyInfo.Name);
            }

            foreach (var propertyInfo in typeof(BlockDefDto).GetProperties())
            {
                BlockDefDtoProps.Add(propertyInfo.Name);
            }
        }

        protected void OnInputFileChanged(InputFileChangeEventArgs e)
        {
            ClearDragClass();
            int MAXALLOWEDFILE = 100;
            var files = e.GetMultipleFiles(MAXALLOWEDFILE);

            foreach (var file in files)
            {
                try
                {
                    loadedFiles.Add(file);
                }
                catch (Exception ex)
                {
                    Snackbar.Configuration.PositionClass = Defaults.Classes.Position.TopCenter;
                    Snackbar.Add($"File: Error {file.Name} {ex.Message}", Severity.Error);
                    //Logger.LogError("File: {Filename} Error: {Error}",
                    //    file.Name, ex.Message);
                }
            }

            Upload();
        }

        protected async Task Clear()
        {
            Clearing = true;
            loadedFiles.Clear();
            ClearDragClass();
            await Task.Delay(100);
            Clearing = false;
        }

        protected async void Upload()
        {
            //Upload the files here
            Snackbar.Configuration.PositionClass = Defaults.Classes.Position.TopCenter;
            int fileExist = 0;

            long MAXALLOWEDSIZE = 250000000;

            foreach (var file in loadedFiles)
            {

                Stream stream = file.OpenReadStream(MAXALLOWEDSIZE);
                MemoryStream ms = new();
                await file.OpenReadStream(MAXALLOWEDSIZE).CopyToAsync(ms);

                //stream.Close();
                var outputFileString = Encoding.Unicode.GetString(ms.ToArray());

                
                string _byteOrderMarkUtf8 = Encoding.Unicode.GetString(Encoding.Unicode.GetPreamble());
                if (outputFileString.StartsWith(_byteOrderMarkUtf8))
                {
                    outputFileString = outputFileString.Remove(0, _byteOrderMarkUtf8.Length);
                    outputFileString = Regex.Replace(outputFileString, "xmlns=.*\">", ">", RegexOptions.IgnoreCase);
                }

                //XmlDocument doc = new();
                //doc.LoadXml(outputFileString);
                //string jsonText = JsonConvert.SerializeXmlNode(doc);
                //await JSRuntime.InvokeAsync<object>("console.log", jsonText);

                try
                {
                    MultiBlock multiBlock = new();

                    _param = string.Empty;
                    XmlSerializer xmlSerializer = new(typeof(MultiBlock));
                    XDocument xDocument = XDocument.Parse(outputFileString);
                    multiBlock = (MultiBlock)xmlSerializer.Deserialize(xDocument.CreateReader());

                    // Check if file already exist
                    bool contains = MultiBlocks.Any(m => m.Block.BlockDef.BlockName == multiBlock.Block.BlockDef.BlockName);

                    if (!contains)
                        // add multiblock list
                        MultiBlocks.Add(multiBlock);
                    else
                        fileExist++;
                }
                catch (XmlException e)
                {
                    Snackbar.Add(e.Message, Severity.Error);
                }
            }

            if (fileExist > 0)
                Snackbar.Add("" + fileExist + " File(s) already exist", Severity.Info);

            await Clear();

            StateHasChanged();
        }

        protected void ClearList()
        {
            ParameterDtos.Clear();
            ParametersDtos.Clear();
            ConnectionDtos.Clear();
            ConnectionsDtos.Clear();
            BlockDtos.Clear();
            BlockDefDtos.Clear();

            ErdbVersions.Clear();
            BlockDefs.Clear();
            Coords.Clear();
            VertexList.Clear();

            StateHasChanged();
        }

        private void GetDetails(MultiBlock multiBlock)
        {
            ShowGrid = false;

            // BlockDef
            BlockDefDto blockDefDto = new()
            {
                BlockName = multiBlock.Block.BlockDef.BlockName,

                EntityName = multiBlock.Block.BlockDef.EntityName,

                BlockId = multiBlock.Block.BlockDef.BlockId,

                BlockGUID = multiBlock.Block.BlockDef.BlockGUID,

                BlockDesc = multiBlock.Block.BlockDef.BlockDesc,

                TemplateName = multiBlock.Block.BlockDef.TemplateName,

                ClassName = multiBlock.Block.BlockDef.ClassName,

                BaseTemplateName = multiBlock.Block.BlockDef.BaseTemplateName,

                Attribute = multiBlock.Block.BlockDef.Attribute,

                LifeCycleState = multiBlock.Block.BlockDef.LifeCycleState,

                AssignedTo = multiBlock.Block.BlockDef.AssignedTo,

            };

            BlockDefDtos.Add(blockDefDto);


            // Parameters
            ParametersDto.BlockName = multiBlock.Block.BlockDef.BlockName;
            ParametersDto.Parameters = multiBlock.Block.Parameters;
            ParametersDtos.Add(ParametersDto);

            // Blocks
            BlockName = multiBlock.Block.BlockDef.BlockName;
            BlockDto.BlockName = multiBlock.Block.BlockDef.BlockName;
            BlockDto.BlockId = multiBlock.Block.BlockDef.BlockId;
            BlockDtos.Add(BlockDto);


            foreach (var param in multiBlock.Block.EmbBlocks.Block)
            {
                ParametersDto = new()
                {
                    BlockName = param.BlockDef.BlockName,
                    Parameters = param.Parameters
                };
                ParametersDtos.Add(ParametersDto);

                BlockDto = new()
                {
                    BlockName = param.BlockDef.BlockName,
                    BlockId = param.BlockDef.BlockId
                };
                BlockDtos.Add(BlockDto);
            }

            foreach (var param in multiBlock.Block.Parameters.Parameter)
            {
                ParameterDto ParameterDto = new()
                {
                    BlockName = multiBlock.Block.BlockDef.BlockName,
                    ParamName = param.ParamName,
                    ParamValue = param.ParamValue
                };

                ParameterDtos.Add(ParameterDto);
            }

            foreach (var block in multiBlock.Block.EmbBlocks.Block)
            {
                foreach (var param in block.Parameters.Parameter)
                {
                    ParameterDto ParameterDto = new()
                    {
                        BlockName = block.BlockDef.BlockName,
                        ParamName = param.ParamName,
                        ParamValue = param.ParamValue
                    };

                    ParameterDtos.Add(ParameterDto);
                }
            }

            // Connections
            if (multiBlock.Block.Connections != null)
            {

                ConnectionsDto ConnectionsDto = new()
                {
                    BlockName = multiBlock.Block.BlockDef.BlockName,
                    Connections = multiBlock.Block.Connections
                };
                ConnectionsDtos.Add(ConnectionsDto);

                foreach (var conn in multiBlock.Block.Connections.Connection)
                {
                    ConnectionDto = new()
                    {
                        BlockName = multiBlock.Block.BlockDef.BlockName,
                        BlockId = conn.BlockId,
                        InputEnd = conn.InputEnd,
                        OutputEnd = conn.OutputEnd,
                        ConnectionForm = conn.ConnectionForm,
                        GraphicalForm = conn.GraphicalForm
                    };

                    ConnectionDtos.Add(ConnectionDto);
                }
            }

            // Connection in EmbBlock
            foreach (var block in multiBlock.Block.EmbBlocks.Block)
            {
                if (block.Connections != null)
                {
                    foreach (var conn in block.Connections.Connection)
                    {
                        ConnectionDto = new()
                        {
                            BlockName = block.BlockDef.BlockName,
                            BlockId = conn.BlockId,
                            InputEnd = conn.InputEnd,
                            OutputEnd = conn.OutputEnd,
                            ConnectionForm = conn.ConnectionForm,
                            GraphicalForm = conn.GraphicalForm
                        };

                        ConnectionDtos.Add(ConnectionDto);
                    }

                    ConnectionsDto = new()
                    {
                        BlockName = block.BlockDef.BlockName,
                        Connections = block.Connections
                    };
                    ConnectionsDtos.Add(ConnectionsDto);
                }
            }

            ShowGrid = true;

            StateHasChanged();
        }

        protected void Onclick(string param, dynamic blockObj, MultiBlock multiBlock)
        {
            ClearList();

            GetDetails(multiBlock);

            _param = param;
            switch (param)
            {
                case "ErdbVersion":
                    ErdbVersions.Add(blockObj);
                    break;
                case "Parameters":
                    ParameterDtos = blockObj;
                    break;
                case "Connections":
                    ConnectionDtos = blockObj;
                    break;
                case "BlockDtos":
                    BlockDtos = blockObj;
                    break;
                case "Block":
                    BlockDefs.Add(blockObj.BlockDef);
                    Coords.Add(blockObj.BlockDef.Coord);
                    Parameters = blockObj.Parameters;
                    SymbolAttrs = blockObj.SymbolAttrs;
                    Connections = blockObj.Connections;
                    if (Connections != null)
                    {
                        foreach (var connection in blockObj.Connections.Connection)
                        {
                            foreach (var vertex in connection.Vertex)
                            {
                                VertexList.Add(vertex);
                            }
                        }
                    }
                    break;
                default:
                    // code block
                    break;
            }

            StateHasChanged();
        }

        protected void SetDragClass()
        {
            DragClass = $"{DefaultDragClass} mud-border-primary";
        }

        protected void ClearDragClass()
        {
            DragClass = DefaultDragClass;
        }
    }
}
