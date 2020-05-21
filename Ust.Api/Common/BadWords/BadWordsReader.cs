using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ust.Api.Models.ModelDbObject;
using File = System.IO.File;

namespace Ust.Api.Common.BadWords
{
    public static class BadWordsReader
    {
        public static void Insert(ApplicationContext db, string path)
        {
            var badWordText = File.ReadAllText(path);
            var badWords = badWordText.Split(", ");

            var badWordList = new List<BadWord>();
            foreach (var badWord in badWords)
            {
                badWordList.Add(new BadWord
                {
                    Word = badWord.ToUpper()
                });
            }

            foreach (var badWord in badWordList)
            {
                db.BadWords.Add(badWord);
            }

            db.SaveChanges();
        }
    }
}
