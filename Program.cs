using System;
using System.Collections.Generic;
using System.Linq;

namespace CGMCodingChallenge
{
	internal class Program
	{
		private static readonly Dictionary<string, List<string>> qandaDictionary = new Dictionary<string, List<string>>();
		static void Main(string[] args)
		{
			while (true)
			{
				Console.WriteLine("CGM - Challenge");
				Console.WriteLine("");
				Console.WriteLine("Options:");
				Console.WriteLine("1. Ask a specific question");
				Console.WriteLine("2. Add a question and its answers");
				Console.WriteLine("3. Exit");
				Console.Write("Enter your choice: ");

				switch (Console.ReadLine())
				{
					case "1":
						AskQuestion();
						break;
					case "2":
						AddQuestion();
						break;
					case "3":
						return;
					default:
						Console.WriteLine("Invalid input. Please enter 1, 2, or 3.");
						break;
				}
			}
        }

		private static void AskQuestion()
        {
            Console.Write("Ask Question: ");

            if (qandaDictionary.TryGetValue(Console.ReadLine()!.Replace("?", ""), out var value))
                foreach (string answer in value)
                    Console.WriteLine(answer);
            else
                Console.WriteLine($"The answer to life, universe and everything is {Convert.ToInt32("2A", 16)}");
        }

		private static void AddQuestion()
        {
            Console.Write("Enter the question and its answers in the format '<question>? \"<answer1>\" \"<answer2>\" ...': ");
            string input = Console.ReadLine();

            if (input == null) return;
            string[] parts = input.Split('?');
            if (parts.Length != 2)
            {
	            Console.WriteLine("Invalid input format.");
	            return;
            }

            string question = parts[0].Trim();
            string[] answers = parts[1].Split('"');

            List<string> validAnswers = answers.Select(answer => answer.Trim()).Where(trimmedAnswer => !string.IsNullOrEmpty(trimmedAnswer)).ToList();

            if (validAnswers.Count == 0)
            {
	            Console.WriteLine("At least one answer is required.");
	            return;
            }

            if (question.Length > 255)
            {
	            Console.WriteLine("Question exceeds maximum length of 255 characters.");
	            return;
            }

            if (validAnswers.Any(ans => ans.Length > 255))
            {
	            Console.WriteLine("Answer exceeds maximum length of 255 characters.");
	            return;
            }

            if (!qandaDictionary.TryAdd(question, validAnswers))
            {
	            Console.WriteLine("Question already exists. Adding answers to existing question.");
	            qandaDictionary[question].AddRange(validAnswers);
            }
            else
	            Console.WriteLine("Question and answers added successfully.");
        }

    }
}
