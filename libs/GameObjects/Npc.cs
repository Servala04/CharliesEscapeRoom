using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using libs; // Adjust namespace as per your project structure

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

    try
    {
        string jsonString = File.ReadAllText(jsonFilePath);
        var dialogData = JsonSerializer.Deserialize<DialogData>(jsonString);

        // Initialize dialog nodes and responses
        var dialogNodes = new Dictionary<string, DialogNode>();
        foreach (var nodeData in dialogData.Nodes)
        {
            var dialogNode = new DialogNode(nodeData.DialogID, nodeData.Text);

            if (nodeData.Responses != null)
            {
                foreach (var responseData in nodeData.Responses)
                {
                    var nextNode = dialogData.Nodes.FirstOrDefault(n => n.DialogID == responseData.NextNodeID);
                    if (nextNode != null)
                    {
                        dialogNode.AddResponse(responseData.ResponseText, new DialogNode(nextNode.DialogID, nextNode.Text));
                    }
                }
            }

            dialogNodes.Add(dialogNode.dialogID, dialogNode);
        }

        // Set starting node
        var startingNode = dialogNodes[dialogData.StartingNode.DialogID];
   // Create Dialog instance
        dialog = new Dialog(startingNode);
        
       // Console.WriteLine("Dialog loaded successfully." + dialogData.StartingNode.Text);
     
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error loading dialog from JSON: {ex.Message}");
        // Handle error as needed
    }
}
    }
   
}
