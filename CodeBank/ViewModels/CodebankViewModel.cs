using System.Collections.Generic;
using CodeBank.Models;

namespace CodeBank.ViewModels
{
    public class CodebankViewModel
    {
        public IEnumerable<Code> Code { get; set; }

        public SearchViewModel Search { get; set; }
    }
}
