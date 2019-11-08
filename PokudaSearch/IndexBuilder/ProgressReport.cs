using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokudaSearch.IndexBuilder {
    public class ProgressReport {
        public enum ProgressStatus : int {
            None = 1,
            Start,
            Processing,
            Finished
        }

        public ProgressStatus Status = ProgressStatus.None;
        public int ProgressCount = 0;
        public int Percent = 0;
        public int TargetCount = 0;
        public TimeSpan EstimateRemain;
    }
}
