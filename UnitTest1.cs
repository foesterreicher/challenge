using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CGMCodingChallenge;
using Xunit;


namespace CGMCodingChallengeTest
{
	public class UnitTest1
	{
		[Fact]
		public void AddQuestion_ValidInput_SuccessfullyAddsQuestionAndAnswers_Test()
		{
			var manager = new QuestionAnswerManager();
			var QandA = "What is your favourite programming language? \"Phyton\" \"C#\" \"C++\"";
			manager.AddQuestion(QandA);

			string[] parts = QandA.Split('?');

			Assert.True(manager.QuestionAnswers.ContainsKey(parts[0]));
			Assert.Equal(3, manager.QuestionAnswers[parts[0]].Count);
			Assert.Equal("Phyton", manager.QuestionAnswers[parts[0]][0]);
			Assert.Equal("C#", manager.QuestionAnswers[parts[0]][1]);
			Assert.Equal("C++", manager.QuestionAnswers[parts[0]][2]);

		}

		[Fact]
		public void AskQuestion_QuestionExists_PrintsAnswers_Test()
		{
			var manager = new QuestionAnswerManager();
			var QandA = "What is a prime number? \"2\" \"3\" \"5\" \"7\"";
			manager.AddQuestion(QandA);

			string[] parts = QandA.Split('?');
			var answers = manager.GetAnswers(parts[0]);

			string expected = "2357";
			var combineanswer = answers.Aggregate("", (current, answer) => current + answer);
			Assert.Equal(expected, combineanswer);

		}

		[Fact]
		public void AddQuestion_QuestionNotExists_PrintsDefaultAnswers_Test()
		{
			var manager = new QuestionAnswerManager();
			var q = "Is the answer to life, universe and everything 42?";
			var answer = manager.GetAnswers(q);

			string expected = "The answer to life, universe and everything is 42";
			Assert.Equal(expected, answer[0]);
		}

		[Fact]
		public void AddQuestion_InvalidInput_NoQuestionAdded_Test()
		{
			var manager = new QuestionAnswerManager();
			string QandA = "Invalid Input,Invalid Input,Invalid Input,Invalid Input,Invalid Input,Invalid Input,Invalid Input,Invalid Input,Invalid Input,Invalid Input,Invalid Input,Invalid Input,Invalid Input,Invalid Input,Invalid Input,Invalid Input,Invalid Input,Invalid Input,Invalid Input,Invalid Input,Invalid Input,Invalid Input,Invalid Input,Invalid Input? \"A\" \"B\" \"C\"";
			manager.AddQuestion(QandA);

			Assert.Empty(manager.QuestionAnswers);
		}
	}
}
