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
                Console.WriteLine($"{_currentNode.Text}");
                for (int i = 0; i < _currentNode.Responses.Count; i++)
                {

                    Console.WriteLine($"{i + 1}. {_currentNode.Responses[i].ResponseText}");
                }

                if (_currentNode.Responses.Count == 0)
                {
                    Console.WriteLine(_currentNode.Text);
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
                if (nextNode == null)
                {
                    break;
                }

                _currentNode = nextNode;
            }

            Console.WriteLine(_endNode.Text);
            Thread.Sleep(2000);
        }
    }
}
