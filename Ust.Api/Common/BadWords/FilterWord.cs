using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;

namespace Ust.Api.Common.BadWords
{
    public static class FilterWord
    {
        public static bool IsModerate(ApplicationContext db, string commentMessage)
        {
            var badWords = db.BadWords.Select(b => b.Word).ToList();
            var normalizeCommentWords = Regex.Replace(commentMessage.ToUpper(), "[-.?!)(,:]", "").Split(" ");

            foreach (var normalizeCommentWord in normalizeCommentWords)
            {
                if (badWords.Contains(normalizeCommentWord))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
