using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokudaSearch.Exceptions {
    public class UnlinkedIndexException : Exception{
        public string TargetIndex;
    }
}
