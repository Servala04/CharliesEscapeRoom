using System;
using System.Threading;

namespace libs
{
    public class Dialog
    {
        private DialogNode _currentNode;
        private DialogNode _endNode;

        public Dialog(DialogNode startingNode)
        {
            _currentNode = startingNode;
            _endNode = new DialogNode("End of dialog.");
        }

        public void Start()
        {
            while (_currentNode != null)
            {
                Console.WriteLine($"Current Node: {_currentNode.dialogID} - {_currentNode.Text}");
                for (int i = 0; i < _currentNode.Responses.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {_currentNode.Responses[i].ResponseText}");
                }

                if (_currentNode.Responses.Count == 0)
                {
                    Console.WriteLine("No responses available." +  _currentNode.Text);
                    break;
                }

                int choice;
                while (true)
                {
                    Console.Write("Choose an option: ");
                    if (int.TryParse(Console.ReadLine(), out choice) && choice > 0 && choice <= _currentNode.Responses.Count)
                    {
                     
                        break;
                    }
                    Console.WriteLine("Invalid choice, please try again.");
                }

                var nextNode = _currentNode.Responses[choice - 1].NextNode;
                if (nextNode != null)
                {
                    Console.WriteLine($"Transitioning to Node: {nextNode.dialogID} - {nextNode.Text}");
                       Console.WriteLine("Next Node Responses:");
                    for (int i = 0; i < nextNode.Responses.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {nextNode.Responses[i].ResponseText}");
                    }
                    
                }
                else
                {
                    Console.WriteLine("Next node is null.");
                }

                _currentNode = nextNode;
            }

            Console.WriteLine(_endNode.Text);
            Thread.Sleep(2000);
        }
    }
}
