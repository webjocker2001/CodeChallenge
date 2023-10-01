using Logic.DB;
using Logic.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using System.IO;

namespace Logic.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CodeChallengeRepoController : ControllerBase
    {
        private LogicContext _logicContext; 

        public CodeChallengeRepoController(LogicContext context) 
        { 
            _logicContext = context;
        }

        [HttpGet]
        [Route("BulkFile")]
        public async Task bulkFile(string path, int type) {
            string[] lines = System.IO.File.ReadAllLines(path);

            //path: ./csvFiles/Questions.csv = Questions, ./csvFiles/Answers.csv = Answers
            //type: 0 = Question, 1 = Answer 
            for (int i = 0; i < lines.Length; i++)
            {
                string[] row = lines[i].Split(';');
                if (type == 0)
                {
                    Question question = new Question() { Description = row[0] };
                    if(row.Length > 1) 
                    {
                        Tag tag = new Tag() { Name = row[1], QuestionId = question.QuestionId };
                        _logicContext.Tags.Add(tag);
                    }

                    _logicContext.Questions.Add(question);
                }
                else {
                    Answer answer = new Answer() { Description = row[0], QuestionId = Int32.Parse(row[1]) };

                    _logicContext.Answers.Add(answer);
                }
                _logicContext.SaveChanges();

            }
        }

        [HttpPost]
        [Route("PostQuestion")]
        public async Task<IActionResult> postQuestion(Question question)
        {
            _logicContext.Questions.Add(question);
            _logicContext.SaveChanges();

            return Ok();
        }

        [HttpPost]
        [Route("PostAnswer")]
        public async Task<IActionResult> postAnswer(Answer answer)
        {
            _logicContext.Answers.Add(answer);
            _logicContext.SaveChanges();

            return Ok();
        }

        [HttpGet]
        [Route("GetQuestion")]
        public async Task<string> getQuestionwithAnswer(int questionId)
        {
            string result = "";

            var question = _logicContext.Questions.Where(x => x.QuestionId == questionId).FirstOrDefault();
            if (question != null)
            {
                var answer = _logicContext.Answers.Where(x => x.QuestionId == question.QuestionId).FirstOrDefault();

                if (answer != null)
                    result = string.Format("Question: {0}, Answer: {1}", question.Description, answer.Description);
            }

            return result;
        }

        [HttpGet]
        [Route("GetQuestionbyTag")]
        public async Task<string> getQuestionbyTag(string tag)
        {
            string result = "";
            var _tag = _logicContext.Tags.Where(x => x.Name == tag).FirstOrDefault();
            if (_tag != null) 
            { 
                var question = _logicContext.Questions.Where(x => x.QuestionId == _tag.QuestionId).FirstOrDefault();
                result = string.Format("Question: {0}, Tag: {1}", question.Description, _tag.Name);
            }

            return result;
        }

    }
}
