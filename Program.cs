using System;
using System.Collections.Generic;
using System.Linq;

namespace CGMCodingChallenge
{
	public class QuestionAnswerManager
	{
		public Dictionary<string, List<string>> QuestionAnswers { get; } = new Dictionary<string, List<string>>();
		public void AddQuestion(string input)
		{
			if (input == null) return;
			string[] parts = input.Split('?');
			if (parts.Length != 2)
			{
				Console.WriteLine("Invalid input format.");
				return;
			}

			string question = parts[0].Trim();
			if (question.Length > 255)
			{
				Console.WriteLine("Question exceeds maximum length of 255 characters.");
				return;
			}

			string[] answers = parts[1].Split('"');
			List<string> validAnswers = answers.Select(answer => answer.Trim()).Where(trimmedAnswer => !string.IsNullOrEmpty(trimmedAnswer)).ToList();

			if (validAnswers.Count == 0)
			{
				Console.WriteLine("At least one answer is required.");
				return;
			}

			if (validAnswers.Any(ans => ans.Length > 255))
			{
				Console.WriteLine("Answer exceeds maximum length of 255 characters.");
				return;
			}


			if (!QuestionAnswers.TryAdd(question, validAnswers))
			{
				Console.WriteLine("Question already exists. Adding answers to existing question.");
				QuestionAnswers[question].AddRange(validAnswers);
			}
			else
			{
				Console.WriteLine("Question and answers added successfully.");
			}
		}


		public List<string> GetAnswers(string question)
		{
			return QuestionAnswers.TryGetValue(question!.Replace("?", ""), out var answers) ? answers : new List<string> { $"The answer to life, universe and everything is { Convert.ToInt32("2A", 16) }" };
		}
	}

	public class Program
	{
		private static QuestionAnswerManager manager = new QuestionAnswerManager();
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

		public static void AskQuestion()
        {
            Console.Write("Ask Question: ");

            var answerList = manager.GetAnswers(Console.ReadLine());
            foreach (var answer in answerList)
            {
	            Console.WriteLine(answer);
            }
        }

		public static void AddQuestion()
        {
            Console.Write("Enter the question and its answers in the format '<question>? \"<answer1>\" \"<answer2>\" ...': ");
            manager.AddQuestion(Console.ReadLine());
        }

    }
}
