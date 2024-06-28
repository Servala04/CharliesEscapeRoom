using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace libs
{
  public class DialogData
    {
        public List<DialogNodeData> Nodes { get; set; }
        public DialogNodeData StartingNode { get; set; }
    }

    public class DialogNodeData
    {
        public string DialogID { get; set; }
        public string Text { get; set; }
        public List<ResponseData> Responses { get; set; }
    }

    public class ResponseData
    {
        public string ResponseText { get; set; }
        public string NextNodeID { get; set; }
    }
}