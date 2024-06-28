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
      //  Console.WriteLine($"Reading dialog JSON file from path: {Path.GetFullPath(jsonFilePath)}");

        string jsonString = File.ReadAllText(jsonFilePath);
     //   Console.WriteLine("Dialog JSON file content:");
      //  Console.WriteLine(jsonString); WORKS

        var dialogData = JsonSerializer.Deserialize<DialogData>(jsonString);
       // Console.WriteLine("Deserialized dialog data successfully.");

        // Initialize dialog nodes and responses
        var dialogNodes = new Dictionary<string, DialogNode>();
       foreach (var nodeData in dialogData.Nodes)
{
    Console.WriteLine($"Processing node: {nodeData.DialogID}");

    var dialogNode = new DialogNode(nodeData.DialogID, nodeData.Text);

    if (nodeData.Responses != null)
    {
        foreach (var responseData in nodeData.Responses)
        {
            Console.WriteLine($"Processing response: {responseData.ResponseText} -> {responseData.NextNodeID}");
            if (string.IsNullOrEmpty(responseData.NextNodeID))
            {
                Console.WriteLine("Warning: NextNodeID is null or empty.");
                continue;
            }

            var nextNode = dialogData.Nodes.FirstOrDefault(n => n.DialogID == responseData.NextNodeID);
            if (nextNode == null)
            {
                Console.WriteLine($"Error: NextNodeID '{responseData.NextNodeID}' not found in dialog nodes.");
                continue;
            }

            dialogNode.AddResponse(responseData.ResponseText, new DialogNode(nextNode.DialogID, nextNode.Text));
        }
    }

    dialogNodes.Add(dialogNode.dialogID, dialogNode);
}

        // Set starting node
        if (dialogData.StartingNode != null && dialogNodes.ContainsKey(dialogData.StartingNode.DialogID))
        {
            var startingNode = dialogNodes[dialogData.StartingNode.DialogID];
            dialog = new Dialog(startingNode);
        //    Console.WriteLine("Dialog initialized successfully.");
            //Console.WriteLine($"Starting Node Text: {startingNode.Text}");
        }
        else
        {
            Console.WriteLine("Starting node is missing or not found in dialog nodes.");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error loading dialog from JSON: {ex.Message}");
        // Handle error as needed
    }
}
    }}