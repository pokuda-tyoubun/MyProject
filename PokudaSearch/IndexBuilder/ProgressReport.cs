using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokudaSearch.IndexBuilder {
    public class ProgressReport {
        public int TotalCount = 0;
        public int ProgressCount = 0;
        public int Percent = 0;
        public TimeSpan EstimateRemain;
    }
}
