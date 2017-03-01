using System.Collections.Generic;
using System.IO;
using System.Linq;
using Assets.Classes;
using NUnit.Framework;

namespace Assets.Testing.Editor
{
    public class SpeechHandlerTests
    {
        [Test]
        public void loadData()
        {
            List<string> speechLines = new List<string>();
            JSONObject obj = new JSONObject(File.ReadAllText("testSpeech.JSON"));
            List<string> responses = new List<string>();
            responses = SpeechHandler.AccessData(obj, "TestCharacter").ToList();
            speechLines.Add("These are test lines");
            speechLines.Add("TEST");

            //check if they are the same as the file
            Assert.AreEqual(speechLines, responses );
        }
    }
}