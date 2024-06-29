using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace libs
{
    public class Npc : GameObject
    {
        public Dialog dialog;

        public Npc() : base()
        {
            Type = GameObjectType.Npc;
            CharRepresentation = '⚇';
            Color = ConsoleColor.DarkGreen;

            string jsonFilePath = @"..\libs\Dialog\Dialogs\dialog1.json";
                string jsonString = File.ReadAllText(jsonFilePath);
                DialogTexts dialogTexts = JsonSerializer.Deserialize<DialogTexts>(jsonString);


                // Initialize dialog nodes and responses
                Dictionary<string, DialogNode> dialogDir = new Dictionary<string, DialogNode>();
                foreach (var nodeData in dialogTexts.Nodes)
                {
                    dialogDir[nodeData.DialogID] = new DialogNode(nodeData.Text);
                }
                foreach (var nodeData in dialogTexts.Nodes){
              DialogNode currentNode =  dialogDir[nodeData.DialogID];
              foreach (var response in nodeData.Responses){
                currentNode.AddResponse(response.ResponseText, dialogDir[response.NextNodeID]);
                }
                }
                List<DialogNode> dialogNodes = new List<DialogNode>(dialogDir.Values);
                dialog = new Dialog(dialogDir["1"]);

        }
        
    }
    public class DialogTexts
    {
        public List<DialogTextData> Nodes { get; set; }
    
    }
    public class DialogTextData
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
