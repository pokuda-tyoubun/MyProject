namespace FxCommonLib.Utils.Tree {
	/// <summary>
	/// TreeElement �̗v�f�̎�ނ�\���܂��B</summary>
	///
	public enum TreeElementKind : int
	{
		/// <summary>
		/// ����l�B</summary>
		None = 0,

		/// <summary>
		/// �q�v�f�����v�f��\���܂��B</summary>
		Composite = 1,

		/// <summary>
		/// �q�v�f�������Ȃ��v�f��\���܂��B</summary>
		Simplex = 2,

		/// <summary>
		/// ����v�f�փ����N����v�f��\���܂��B</summary>
		Link = 4,
	}
}
