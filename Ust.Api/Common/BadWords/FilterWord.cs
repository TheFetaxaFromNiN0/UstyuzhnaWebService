using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ust.Api.Common.BadWords
{
    public static class FilterWord
    {
        public static bool IsModerate(ApplicationContext db, string commentMessage)
        {
            var badWords = db.BadWords.ToList();
            var normalizeComentMessage = commentMessage.ToUpper();

            foreach (var badWord in badWords)
            {
                if (normalizeComentMessage.Contains(badWord.Word))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
