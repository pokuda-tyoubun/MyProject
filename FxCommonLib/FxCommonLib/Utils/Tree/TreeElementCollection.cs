#region ' using 

using System;
using System.Collections;

#endregion
namespace FxCommonLib.Utils.Tree {
	/// <summary>
	/// �K�v�ɉ����ăT�C�Y�����I�ɑ������� TreeElement �^���Ǘ�����R���N�V������񋟂��܂��B</summary>
	///
	public class TreeElementCollection : ICloneable,
	                                     ICollection,
	                                     IEnumerable
	{
        
		// constructor 
		#region '   ~ constructor ( TreeElement ) 
		/// <summary>
		/// TreeElementCollection �̐V�����C���X�^���X�� TreeElement �ŏ��������܂��B</summary>
		///
		/// <exception cref="System.ArgumentNullException">
		/// anOwnerElement �� null �ł��B</exception>
		///
		internal TreeElementCollection( TreeElement anOwnerElement )
		{
			if ( anOwnerElement == null ) throw new ArgumentNullException( "anOwnerElement" );

			this._ownerElement = anOwnerElement;
		}
		#endregion
        
        
		// field 
		#region ' field 
        
		private ArrayList _list = new ArrayList();
		private TreeElement _ownerElement = null;
        
		#endregion
        
        
		// property
		#region ' + BottomItem : TreeElement { get,set } 
		/// <summary>
		/// �R���N�V�����̈�ԉ��ɑ��݂��Ă��� TreeElement ���擾���܂��B</summary>
		///
		public TreeElement BottomItem
		{
			get { return ( TreeElement )this._list[ 0 ]; }
			set { this._list[ 0 ] = value; }
		}
		#endregion
		#region ' + Capacity : Int32 { get,set } 
		/// <summary>
		/// TreeElementCollection �Ɋi�[�ł���v�f�̐����擾�܂��͐ݒ肵�܂��B</summary>
		///
		public Int32 Capacity
		{
			get { return this._list.Capacity; }
			set { this._list.Capacity = value; }
		}
		#endregion
		#region ' + Count : Int32 { get } 
		/// <summary>
		/// TreeElementCollection �Ɏ��ۂɊi�[����Ă���v�f�̐����擾���܂��B</summary>
		///
		public Int32 Count
		{
			get { return this._list.Count; }
		}
		#endregion
		#region ' + HasItem : Boolean { get } 
		/// <summary>
		/// TreeElementCollections �̃R���N�V�������ɗv�f�����݂��邩�ǂ����������l���擾���܂��B</summary>
		///
		public Boolean HasItem
		{
			get
			{
				if ( 0 < this._list.Count )
				{
					return true;
				}
				else
				{
					return false;
				}
			}
		}
		#endregion
		#region ' + IsFixedSize : Boolean { get } 
		/// <summary>
		/// TreeElementCollection ���Œ�T�C�Y�̏ꍇ�� true �B����ȊO�̏ꍇ�� false �B
		/// ����l�� false �ł��B</summary>
		///
		public Boolean IsFixedSize
		{
			get { return this._list.IsFixedSize; }
		}
		#endregion
		#region ' + IsReadOnly : Boolean { get } 
		/// <summary>
		/// TreeElementCollection ���ǂݎ���p�̏ꍇ�� true �B����ȊO�̏ꍇ�� false �B
		/// ����l�� false �ł��B</summary>
		///
		public Boolean IsReadOnly
		{
			get { return this._list.IsReadOnly; }
		}
		#endregion
		#region ' + IsSynchronized : Boolean { get } 
		/// <summary>
		/// TreeElementCollections �ւ̃A�N�Z�X��
		/// ��������Ă��� (�X���b�h �Z�[�t�ł���) �ꍇ�� true �B����ȊO�̏ꍇ�� false �B
		/// ����l�� false �ł��B</summary>
		///
		public Boolean IsSynchronized
		{
			get { return this._list.IsSynchronized; }
		}
		#endregion
		#region ' + Item [ Int32 ] : TreeElement { get } 
		/// <summary>
		/// �w�肵���C���f�b�N�X�ɂ��� TreeElement ���擾�܂��͐ݒ肵�܂��B</summary>
		///
		public TreeElement this[ Int32 anIndex ]
		{
			get { return ( TreeElement )this._list[ anIndex ]; }
		}
		#endregion
		#region ' + Item [ String ] : TreeElement { get } 
		/// <summary>
		/// �R���N�V����������͂��߂Ɍ��������A
		/// �w�肵�����O������ TreeElement ���擾���܂��B
		/// ������Ȃ���� null ��Ԃ��܂��B</summary>
		///
		public TreeElement this[ String anElementName ]
		{
			get
			{
				if ( anElementName == null ) throw new ArgumentNullException( "anElementName" );
				if ( anElementName.Trim().Length == 0 ) throw new ArgumentException( "��ł��B", "anElementName" );
				
				foreach ( TreeElement element in this._list )
				{
					if ( element.Name.Equals( anElementName ) )
					{
						return element;
					}
				}
				
				return null;
				
			}
		}
		#endregion
		#region ' + SyncRoot : Object { get } 
		/// <summary>
		/// TreeElementCollection �ւ̃A�N�Z�X�𓯊����邽�߂̎g�p�ł���I�u�W�F�N�g���擾���܂��B</summary>
		///
		public Object SyncRoot
		{
			get { return this._list.SyncRoot; }
		}
		#endregion
		#region ' + TopItem : TreeElement { get,set } 
		/// <summary>
		/// �R���N�V�����̈�ԏ�ɑ��݂��Ă��� TreeElement ���擾���܂��B</summary>
		///
		public TreeElement TopItem
		{
			get { return ( TreeElement )_list[ this.Count - 1 ]; }
			set { this._list[ this.Count - 1 ] = value; }
		}
		#endregion
		#region '   - OwnerElement : TreeElement { get } 
		/// <summary>
		/// ���̃R���N�V������ێ����Ă��� TreeElement ���擾���܂��B</summary>
		///
		private TreeElement OwnerElement
		{
			get { return this._ownerElement; }
		}
		#endregion

        
		// method 
		#region ' + IsContaints ( TreeElement ) : Boolean 
		/// <summary>
		/// ����v�f�� TreeElementCollection ���ɂ��邩�ǂ������f���܂��B</summary>
		///
		public Boolean IsContains( TreeElement anItem )
		{
			return this._list.Contains( anItem );
		}
		#endregion
		#region ' + Clear () : void 
		/// <summary>
		/// TreeElementCollections ����S�Ă̗v�f���폜���܂��B</summary>
		///
		public void Clear()
		{
			foreach ( TreeElement element in this._list )
			{
				this.Remove( element );
			}
		}
		#endregion
		#region ' + Clone () : Object 
		/// <summary>
		/// TreeElementCollection �̃f�B�[�v�R�s�[���쐬���܂��B</summary>
		///
		public Object Clone()
		{
			TreeElementCollection cloneElementCollection = new TreeElementCollection( ( TreeElement )this.OwnerElement.Clone() );

			foreach ( TreeElement element in this._list )
			{
				cloneElementCollection.Add( ( TreeElement )element.Clone() );

				if ( element.HasChild )
				{
					cloneElementCollection.AddRange( ( TreeElementCollection )element.ChildElements.Clone() );
				}
			}

			return cloneElementCollection;
		}
		#endregion
		#region ' + CopyTo ( Array ) : void 
		/// <summary>
		/// TreeElementCollections �S�̂��݊����̂��� 1 ������ Array �ɃR�s�[���܂��B
		/// �R�s�[����́A�R�s�[��̔z��̐擪����n�܂�܂��B</summary>
		/// 
		///    <param name="anArray">
		///     TreeElementCollections ����v�f���R�s�[����� 1 ������ Array �B 
		///     Array �ɂ́A0 ����n�܂�C���f�b�N�X�ԍ����K�v�ł��B</summary>
		/// 
		public void CopyTo( Array anArray )
		{
			this.CopyTo( anArray, 0 );
		}
		#endregion
		#region ' + CopyTo ( Array, Int32 ) : void 
		/// <summary>
		/// TreeElementCollection �S�̂��݊����̂��� 1 ������ Array �ɃR�s�[���܂��B
		/// �R�s�[����́A�R�s�[��̔z��̐擪����n�܂�܂��B</summary>
		/// 
		///    <param name="anArray">
		///     TreeElementCollections ����v�f���R�s�[����� 1 ������ Array �B 
		///     Array �ɂ́A0 ����n�܂�C���f�b�N�X�ԍ����K�v�ł��B</summary>
		///    <param name="anArrayIndex">
		///    �R�s�[�̊J�n�ʒu�ƂȂ�AanArray �� 0 ����n�܂�C���f�b�N�X�ԍ��B</param>
		///    
		public void CopyTo( Array anArray, Int32 anArrayIndex )
		{
			this.CopyTo( 0, anArray, anArrayIndex, this._list.Count );
		}
		#endregion
		#region ' + CopyTo ( Int32, Array, Int32, Int32 ) : void 
		/// <summary>
		/// TreeElementCollection �S�̂��݊����̂��� 1 ������ Array �ɃR�s�[���܂��B
		/// �R�s�[����́A�R�s�[��̔z��̐擪����n�܂�܂��B</summary>
		/// 
		///     <param name="anIndex">
		///     �R�s�[�̊J�n�ʒu�ƂȂ�Aarray �� 0 ����n�܂�C���f�b�N�X�ԍ��B</param>
		///     <param name="anArray">
		///     TreeElementCollection ����v�f���R�s�[����� 1 ������ Array �B 
		///     Array �ɂ́A0 ����n�܂�C���f�b�N�X�ԍ����K�v�ł��B</summary>
		///     <param name="anArrayIndex">
		///     �R�s�[�̊J�n�ʒu�ƂȂ�AanArray �� 0 ����n�܂�C���f�b�N�X�ʒu�B</summary>
		///     <param name="aCount">
		///     �R�s�[����v�f�̐��B</summary>
		///    
		public void CopyTo( Int32 anIndex, Array anArray, Int32 anArrayIndex, Int32 aCount )
		{
			Array.Copy( this._list.ToArray(), 0, anArray, anIndex, this._list.Count );
		}
		#endregion
		#region ' + GetEnumerator () : IEnumerator 
		/// <summary>
		/// �R���N�V�����𔽕������ł���񋓎q��Ԃ��܂��B </summary>
		/// 
		///    <returns>
		///    �R���N�V�����𔽕��������邽�߂Ɏg�p�ł��� IEnumerator�B</returns>
		/// 
		public IEnumerator GetEnumerator()
		{
			return new TreeElementCollectionsEnumerator( this );
		}
		#endregion
		#region ' + GetRange ( Int32, Int32 ) : TreeElementCollection 
		/// <summary>
		/// ���� TreeElementCollections ���̗v�f�̃T�u�Z�b�g��\�� TreeElementCollections ��Ԃ��܂��B</summary>
		///
		///    <param name="anIndex">
		///    �͈͂��J�n����ʒu�́A0����n�܂� TreeElementCollection �̃C���f�b�N�X�ԍ��B</param>
		///    <param name="aCount">
		///    �͈͓��̗v�f�̐��B</param>
		///
		public TreeElementCollection GetRange( Int32 anIndex, Int32 aCount )
		{
			TreeElementCollection list = new TreeElementCollection( this.OwnerElement );
			
			list.AddRange( this._list.GetRange( anIndex, aCount ));
			
			return list;
		}
		#endregion
		#region ' + IndexOf ( TreeElement ) : Int32 
		/// <summary>
		/// �w�肵�� TreeElement ���������A TreeElementCollections �S�̓��ōŏ��Ɍ��������ʒu�� 
		/// 0 ����n�܂�C���f�b�N�X��Ԃ��܂��B</summary>
		///
		public Int32 IndexOf( TreeElement anItem )
		{
			return this._list.IndexOf( anItem );
		}
		#endregion
		#region ' + IndexOf ( TreeElement, Int32 ) : Int32 
		/// <summary>
		/// �w�肵�� TreeElement ���������A�w�肵���C���f�b�N�X����Ō�̗v�f�܂ł�
		/// TreeElementCollections �̃Z�N�V�������ōŏ��ɏo������ʒu�� 
		/// 0 ����n�܂�C���f�b�N�X�ԍ���Ԃ��܂��B</summary>
		///
		///    <param name="anItem">
		///    TreeElementCollections ���Ō�������� TreeElement �B</param>
		///    <param name="aStartIndex">
		///    �����̊J�n�ʒu������ 0 ����n�܂�C���f�b�N�X�B</param>
		/// 
		///    <returns>
		///    aStartIndex ����Ō�̗v�f�܂ł� TreeElementCollection �̃Z�N�V��������
		///    anItem �����������ꍇ�́A�ŏ��Ɍ��������ʒu�� 0 ����n�܂�C���f�b�N�X�ԍ��B
		///    ����ȊO�̏ꍇ�� -1 �B</returns>
		/// 
		public Int32 IndexOf( TreeElement anItem, Int32 aStartIndex )
		{
			return this._list.IndexOf( anItem, aStartIndex );
		}
		#endregion
		#region ' + IndexOf ( TreeElement, Int32, Int32 ) : Int32 
		/// <summary>
		/// �w�肵�� TreeElement ���������A�w�肵���C���f�b�N�X����n�܂���
		/// �w�肵�����̗v�f���i�[���� TreeElementCollection �̃Z�N�V�������ōŏ��ɏo������ʒu�� 
		/// 0 ����n�܂�C���f�b�N�X�ԍ���Ԃ��܂��B</summary>
		///
		///    <param name="anItem">
		///    TreeElementCollections ���Ō�������� TreeElement �B</param>
		///    <param name="aStartIndex">
		///    �����̊J�n�ʒu������ 0 ����n�܂�C���f�b�N�X�B</param>
		///    <param name="aCount">
		///    �����Ώۂ͈͓̔��ɂ���v�f�̐��B</param>
		/// 
		///    <returns>
		///    aStartIndex ����n�܂��� aCount �̗v�f���i�[���� TreeElementCollection �̃Z�N�V�������� 
		///    value �����������ꍇ�́A�ŏ��Ɍ��������ʒu�� 0 ����n�܂�C���f�b�N�X�ԍ��B
		///    ����ȊO�̏ꍇ�� -1 �B</returns>
		/// 
		public Int32 IndexOf( TreeElement anItem, Int32 aStartIndex, Int32 aCount )
		{
			return this._list.IndexOf( anItem, aStartIndex, aCount );
		}
		#endregion
		#region ' + LastIndexOf ( TreeElement ) : Int32 
		/// <summary>
		/// TreeElementCollection ���܂��͂��̈ꕔ�ɂ���l�̂����A
		/// �Ō�ɏo������l�́A0 ����n�܂�C���f�b�N�X�ԍ���Ԃ��܂��B</summary>
		///
		///    <param name="anItem">
		///    TreeElementCollection ���Ō�������� TreeElement �B</param>
		///
		public Int32 LastIndexOf( TreeElement anItem )
		{
			return this._list.LastIndexOf( anItem );
		}
		#endregion
		#region ' + LastIndexOf ( TreeElement, Int32 ) : Int32 
		/// <summary>
		/// �w�肵�� TreeElement ���������A�ŏ��̗v�f����A�w�肵���C���f�b�N�X�܂ł� 
		/// TreeElementCollectionst �̃Z�N�V�������ōŌ�ɏo������ʒu�� 
		/// 0 ����n�܂�C���f�b�N�X�ԍ���Ԃ��܂��B</summary>
		///
		///    <param name="anItem">
		///    TreeElementCollections ���Ō�������� TreeElement �B</param>
		///    <param name="aStartIndex">
		///    ��������̊J�n�ʒu������ 0 ����n�܂�C���f�b�N�X�B</param>
		///
		public Int32 LastIndexOf( TreeElement anItem, Int32 aStartIndex )
		{
			return this._list.LastIndexOf( anItem, aStartIndex );
		}
		#endregion
		#region ' + LastIndexOf ( TreeElement, Int32, Int32 ) : Int32 
		/// <summary>
		/// �w�肵�� TreeElement ���������āA�w�肵�����̗v�f���i�[���A
		/// �w�肵���C���f�b�N�X�̈ʒu�ŏI������ TreeElementCollections �̃Z�N�V��������
		/// �ŏ��ɏo������ʒu�� 0 ����n�܂�C���f�b�N�X�ԍ���Ԃ��܂��B</summary>
		///
		///    <param name="anItem">
		///    TreeElementCollections ���Ō�������� TreeElement �B</param>
		///    <param name="aStartIndex">
		///    ��������̊J�n�ʒu������ 0 ����n�܂�C���f�b�N�X�B</param>
		///    <param name="aCount">
		///    �����Ώۂ͈͓̔��ɂ���v�f�̐��B</param>
		///
		public Int32 LastIndexOf( TreeElement anItem, Int32 aStartIndex, Int32 aCount )
		{
			return this._list.LastIndexOf( anItem, aStartIndex, aCount );
		}
		#endregion
		#region ' + Reverse () : void 
		/// <summary>
		/// TreeElementCollections ������т��̈ꕔ�̗v�f�̏����𔽓]�����܂��B</summary>
		///
		public void Reverse()
		{
			this._list.Reverse();
		}
		#endregion
		#region ' + Reverse ( Int32, Int32 ) : void 
		/// <summary>
		/// �w�肵���͈̗͂v�f�̏����𔽓]�����܂��B</summary>
		///
		///    <param name="anIndex">
		///    ���]������͈͂̊J�n�ʒu������ 0 ����n�܂�C���f�b�N�X�B</param>
		///    <param name="aCount">
		///    ���]������͈͓��ɂ���v�f�̐��B</param>
		///
		public void Reverse( Int32 anIndex, Int32 aCount )
		{
			this._list.Reverse( anIndex, aCount );
		}
		#endregion
		#region ' + Sort () : void 
		/// <summary>
		/// �e�v�f�� IComparable �������g�p���āATreeElementCollections �S�̓��̗v�f����בւ��܂��B</summary>
		///
		public void Sort()
		{
			this._list.Sort();
		}
		#endregion
		#region ' + Sort ( IComparer ) : void 
		/// <summary>
		/// �w�肵����r���Z�q���g�p���āATreeElementCollection �S�̓��̗v�f����בւ��܂��B</summary>
		///
		///    <param name="aComparer">
		///    �v�f���r����ꍇ�Ɏg�p���� IComparer �����B</param>
		/// 
		public void Sort( IComparer aComparer )
		{
			this._list.Sort( aComparer );
		}
		#endregion
		#region ' + Sort ( Int32, Int32, IComparer ) : void 
		/// <summary>
		/// �w�肵����r���Z�q���g�p���āATreeElementCollection �̃Z�N�V�������̗v�f����בւ��܂��B</summary>
		///
		///    <param name="anIndex">
		///    ���בւ���͈͂̊J�n�ʒu������ 0 ����n�܂�C���f�b�N�X�B</param>
		///    <param name="aCount">
		///    ���בւ���͈͂̒����B</param>
		///    <param name="aComparer">
		///    �v�f���r����ꍇ�Ɏg�p���� IComparer �����B</param>
		/// 
		public void Sort( Int32 anIndex, Int32 aCount, IComparer aComparer )
		{
			this._list.Sort( anIndex, aCount, aComparer );
		}
		#endregion
		#region ' + ToArray () : Object[] 
		/// <summary>
		/// TreeElementCollection �̗v�f��
		/// �V�����z��ɃR�s�[���܂��B</summary>
		///
		public Object[] ToArray()
		{
			if ( this._list.Count == 0 )
			{
				return new Object[] { };
			}

			Object[] newObjects = new Object[this._list.Count - 1];
			Array.Copy( this._list.ToArray(), 0, newObjects, 0, this._list.Count - 1 );

			return newObjects;
		}
		#endregion
		#region ' + TrimToSize () : void 
		/// <summary>
		/// �e�ʂ� TreeElementCollections ���ɂ�����ۂ̗v�f���ɐݒ肵�܂��B</summary>
		///
		public void TrimToSize()
		{
			this._list.TrimToSize();
		}
		#endregion
		#region '   ~ Add ( TreeElement ) : Int32 
		/// <summary>
		/// TreeElementCollection �̖����� TreeElement ��ǉ����܂��B</summary>
		///
		///    <param name="aValue">
		///    �ǉ����� TreeElement �B</param>
		///
		///    <returns>
		///    aValue ���ǉ����ꂽ�ʒu�̃C���f�b�N�X�B</returns>
		/// 
		internal Int32 Add( TreeElement aValue )
		{
			return this._list.Add( aValue );
		}
		#endregion
		#region '   ~ AddRange ( ICollection ) : void 
		/// <summary>
		/// ICollection �̗v�f�� TreeElementCollections �̖����֒ǉ����܂��B</summary>
		///
		///    <param name="someElements">
		///    TreeElementCollections �̖����ɗv�f���ǉ������ ICollection �B</param>
		///
		internal void AddRange( ICollection someElements )
		{
			foreach ( TreeElement element in someElements )
			{
				this.Add( element );
			}
		}
		#endregion
		#region '   ~ AddSubElement ( TreeElement ) : void 
		/// <summary>
		/// </summary>
		///
		internal void AddSubElement( TreeElement anElement )
		{
			if ( anElement == null ) throw new ArgumentNullException( "anElement" );

			this._list.Add( anElement );
		}
		#endregion
		#region '   ~ AddRangeSubElements ( TreeElementCollection ) : void 
		/// <summary>
		/// </summary>
		///
		internal void AddRangeSubElements( TreeElementCollection someElements )
		{
			if ( someElements == null ) throw new ArgumentNullException( "someElements" );

			foreach ( TreeElement element in someElements )
			{
				this.AddSubElement( element );
			}
		}
		#endregion
		#region '   ~ Insert ( Int32, TreeElement ) : void 
		/// <summary>
		/// TreeElementCollection ���̎w�肵���C���f�b�N�X�̈ʒu�ɗv�f��}�����܂��B</summary>
		///
		///    <param name="anIndex">
		///    anItem ��}������ʒu�́A 0 ����n�܂�C���f�b�N�X�ԍ��B</param>
		///    <param name="anItem">
		///    �}������ TreeElement �B</param>
		///
		internal void Insert( Int32 anIndex, TreeElement anItem )
		{
			this._list.Insert( anIndex, anItem );
		}
		#endregion
		#region '   ~ InsertRange ( Int32, TreeElementCollection ) : void 
		/// <summary>
		/// �R���N�V�����̗v�f�� TreeElementCollection ���̎w�肵���C���f�b�N�X�̈ʒu�ɑ}�����܂��B</summary>
		///
		///    <param name="anItem">
		///    �V�����v�f���}�������ʒu�� 0 ����n�܂�C���f�b�N�X�B</param>
		///    <param name="someElements">
		///    TreeElementCollection �ɗv�f��}������ ICollection �B</param>
		///
		internal void InsertRange( Int32 anItem, TreeElementCollection someElements )
		{

			foreach ( TreeElement element in someElements )
			{
				element.SetParentElement( this.OwnerElement );
			}

			this._list.InsertRange( anItem, someElements );
		}
		#endregion
		#region '   ~ Remove ( TreeElement ) : void 
		/// <summary>
		/// TreeElementCollections ���ōŏ��Ɍ�����������̃I�u�W�F�N�g���폜���܂��B</summary>
		///
		///    <param name="anItem">
		///    TreeElementCollections ����폜���� TreeElement �B</param>
		///
		internal void Remove( TreeElement anItem )
		{
			anItem._parent = null;
			this._list.Remove( anItem );
		}
		#endregion
		#region '   ~ RemoveAt ( Int32 ) : void 
		/// <summary>
		/// TreeElementCollection �̎w�肵���C���f�b�N�X�ɂ���v�f���폜���܂��B</summary>
		///
		///    <param name="anIndex">
		///    �폜����v�f�́A0 ����n�܂�C���f�b�N�X�ԍ��B</param>
		///
		internal void RemoveAt( Int32 anIndex )
		{
			( _list[ anIndex ] as TreeElement )._parent = null;
			this._list.RemoveAt( anIndex );
		}
		#endregion
		#region '   ~ RemoveBottom () : void 
		/// <summary>
		/// �R���N�V�����̈�ԉ��̗v�f���폜���܂��B</summary>
		///
		internal void RemoveBottom()
		{
			this.RemoveAt( 0 );
		}
		#endregion
		#region '   ~ RemoveTop () : void 
		/// <summary>
		/// �R���N�V�����̈�ԏ�̗v�f���폜���܂��B</summary>
		///
		internal void RemoveTop()
		{
			this._list.RemoveAt( this.Count - 1 );
		}
		#endregion
		#region '   ~ RemoveRange ( Int32, Int32 ) : void 
		/// <summary>
		/// TreeElementCollections ����v�f�͈̔͂��폜���܂��B</summary>
		///
		///    <param name="anIndex">
		///    �폜����v�f�͈̔͂̊J�n�ʒu������ 0 ����n�܂�C���f�b�N�X�ԍ��B</param>
		///    <param name="aCount">
		///    �폜����v�f�̐�</param>
		///
		internal void RemoveRange( Int32 anIndex, Int32 aCount )
		{
			for ( Int32 index = anIndex; index < anIndex + aCount; index++ )
			{
				this.RemoveAt( index ); 
			}
			
		}
		#endregion
		
		
		// innertype 
		#region '   - TreeElementCollectionsEnumerator : IEnumerator 
		/// <summary>
		/// �R���N�V�����ɑ΂���P���Ȕ����������T�|�[�g���܂��B</summary>
		///
		private class TreeElementCollectionsEnumerator : IEnumerator
		{
			
			// constructor 
			#region ' + constructor ( TreeElementCollection ) 
			/// <summary>
			/// �V���� TreeElementCollectionsEnumerator �̃C���X�^���X�� TreeElementCollection �ŏ��������܂��B</summary>
			/// 
			public TreeElementCollectionsEnumerator( TreeElementCollection aList )
			{
				this._list = aList;
			}
			#endregion
			
			
			// field 
			#region ' field 
			private TreeElementCollection _list = null;
			private Int32 _currentIndex = -1;
			#endregion
			
			
			// property 
			#region ' + Current : Object { get } 
			/// <summary>
			/// �R���N�V�������̌��݂̗v�f���擾���܂��B</summary>
			/// 
			public Object Current
			{
				get { return this._list[ this._currentIndex ]; }
			}
			#endregion
			
			
			// method 
			#region ' + Reset () : void 
			/// <summary>
			/// �R���N�V�����̍ŏ��̗v�f�̑O�ɐݒ肵�܂��B</summary>
			/// 
			public void Reset()
			{
				this._currentIndex = -1;
			}
			#endregion
			#region ' + MoveNext () : Boolean 
			/// <summary>
			/// �񋓎q���R���N�V�����̎��̗v�f�ɐi�߂܂��B</summary>
			/// 
			public Boolean MoveNext()
			{
				this._currentIndex ++;

				if ( this._list.Count <=
					this._currentIndex )
				{
					return false;
				}
				else
				{
					return true;
				}
			}
			#endregion
			
		}
		#endregion
		
	}
}
