
namespace FxCommonLib.Models {
    public class ValuePair {
        public string InternalValue { get; set; }
        public string DisplayValue { get; set; }

        public ValuePair(string internalValue, string displayValue) {
            InternalValue = internalValue;
            DisplayValue = displayValue;
        }

        public override string ToString() {
            return DisplayValue;
        }
    }
}
