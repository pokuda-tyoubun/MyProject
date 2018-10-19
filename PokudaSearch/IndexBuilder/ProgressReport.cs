using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokudaSearch.IndexBuilder {
    public class ProgressReport {
        public int TotalCount;
        public int ProgressCount;
        public int Percent;
        public TimeSpan EstimateRemain;
    }
}
