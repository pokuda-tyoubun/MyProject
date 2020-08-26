namespace FxCommonLib.Utils.Tree {
	/// <summary>
	/// TreeElement の要素の種類を表します。</summary>
	///
	public enum TreeElementKind : int
	{
		/// <summary>
		/// 既定値。</summary>
		None = 0,

		/// <summary>
		/// 子要素を持つ要素を表します。</summary>
		Composite = 1,

		/// <summary>
		/// 子要素を持たない要素を表します。</summary>
		Simplex = 2,

		/// <summary>
		/// ある要素へリンクする要素を表します。</summary>
		Link = 4,
	}
}
