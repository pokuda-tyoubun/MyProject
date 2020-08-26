#region ' using 

using System;


#endregion
namespace FxCommonLib.Utils.Tree {
	/// <summary>
	/// TreeElement �̊K�w���̗v�f�Ԃ��ړ������삷��@�\��񋟂��܂��B</summary>
	///
	public class TreeElementVisitor
	{
	
		// constructor 
		#region ' + constructor ( TreeElement ) 
		/// <summary>
		/// TreeElementVisitor �̐V�����C���X�^���X�� TreeElement �ŏ��������܂��B</summary>
		///
		/// <exception cref="System.ArgumentNullException">
		/// aSourceElement �� null �ł��B</exception>
		///
		public TreeElementVisitor( TreeElement aSourceElement )
		{
			if ( aSourceElement == null ) throw new ArgumentNullException( "aSourceElement" );

			this._currentElement = aSourceElement;
		}
		#endregion

		
		// field 
		#region ' field 

		private TreeElement _currentElement = null;	

		#endregion

		
		// property 
		#region ' + Depth : Int32 { get } 
		/// <summary>
		/// ���݂̐[����\�� 0 ����n�܂鐔���擾���܂��B</summary>
		///
		public Int32 Depth
		{
			get { return this.CurrentElement.Depth; }
		}
		#endregion
		#region ' + CurrentElement : TreeElement { get } 
		/// <summary>
		/// ���݂̈ʒu�ɑ��݂���v�f���擾���܂��B</summary>
		///
		public TreeElement CurrentElement
		{
			get { return this._currentElement; }
		}
		#endregion
		#region ' + HasChild : Boolean { get } 
		/// <summary>
		/// ���݂̗v�f�Ɏq�v�f�����݂��邩�ǂ����������l���擾���܂��B</summary>
		///
		public Boolean HasChild
		{
			get { return CurrentElement.HasChild; }
		}
		#endregion
		#region ' + HasNext : Boolean { get } 
		/// <summary>
		/// ���̗v�f�����݂��邩�ǂ����������l���擾���܂��B</summary>
		///
		public Boolean HasNext
		{
			get
			{
				if ( this.CurrentElement.HasChild )
				{
					return true;
				}

				TreeElement element = this.CurrentElement;

				if ( element.IsLastChild == false )
				{
					return true;
				}

				while ( element.IsLastChild )
				{
					element = element.Parent;

					if ( element.IsTop )
					{
						return false;
					}
				}

				return true;
			}
		}
		#endregion
		#region ' + HasNextSibling : Boolean { get } 
		/// <summary>
		/// �����K�w�Ɏ��̗v�f�����݂��邩�ǂ����������l���擾���܂��B</summary>
		///
		public Boolean HasNextSibling
		{
			get
			{
				if ( CurrentElement.IsLastChild )
				{
					return false;
				}
				else
				{
					return true;
				}
			}
		}
		#endregion
		#region ' + HasPrevious : Boolean { get } 
		/// <summary>
		/// �O�̗v�f�����݂��邩�ǂ����������l���擾���܂��B</summary>
		///
		public Boolean HasPrevious
		{
			get
			{
				if ( this.CurrentElement.IsTop )
				{
					return false;
				}

				return true;
			}
		}
		#endregion
		#region ' + HasPreviousSibling : Boolean { get } 
		/// <summary>
		/// �����K�w�ɑO�̗v�f�����݂��邩�ǂ����������l���擾���܂��B</summary>
		///
		public Boolean HasPreviousSibling
		{
			get
			{
				if ( CurrentElement.IsFirstChild )
				{
					return false;
				}
				else
				{
					return true;
				}
			}
		}
		#endregion
		#region ' + Index : Int32 { get } 
		/// <summary>
		/// ���݂̗v�f�̈ʒu��\�� 0 ����͂��܂�C���f�b�N�X���擾���܂��B</summary>
		///
		public Int32 Index
		{
			get { return this.CurrentElement.Index; }
		}
		#endregion
		#region ' + IsTop : Boolean { get } 
		/// <summary>
		/// ���݂̗v�f���g�b�v�v�f���ǂ����������l���擾�܂��܂��B</summary>
		///
		public Boolean IsTop
		{
			get { return this.CurrentElement.IsTop; }
		}
		#endregion
		#region ' + ParentElement : TreeElement { get } 
		/// <summary>
		/// �e�v�f�����擾���܂��B</summary>
		///
		public TreeElement ParentElement
		{
			get
			{
				if ( this.CurrentElement.IsTop )
					throw new InvalidOperationException( "���݂̗v�f�̓g�b�v�v�f�ł��B" +
					                                     "�g�b�v�v�f����e�v�f���擾���邱�Ƃ͏o���܂���B" );

				return this.CurrentElement.Parent;
			}
		}
		#endregion
		#region ' + TopElement : TreeElement { get } 
		/// <summary>
		/// �g�b�v�K�w�̗v�f���擾���܂��B</summary>
		///
		public TreeElement TopElement
		{
			get { return this.CurrentElement.Top; }
		}
		#endregion
		#region ' + Value : Object { get,set } 
		/// <summary>
		/// ���݂̈ʒu�ɑ��݂���v�f�̒l���擾�܂��͐ݒ肵�܂��B</summary>
		///
		public Object Value
		{
			get { return CurrentElement.Value; }
			set { CurrentElement.Value = value; }
		}
		#endregion

		
		// method 
		#region ' + Move ( Int32 ) : void 
		/// <summary>
		/// �w�肵���񐔕��A�O��̗v�f�ֈړ����܂��B</summary>
		///
		public void Move( Int32 aCount )
		{
			if ( aCount < 0 )
			{
				this.MovePrevious( aCount );
			}
			else if ( 0 < aCount )
			{
				MoveNext( aCount );
			}
		}
		#endregion
		#region ' + MoveNext () : void 
		/// <summary>
		/// ���̗v�f�ֈړ����܂��B</summary>
		///
		public void MoveNext()
		{
			if ( this.CurrentElement.HasChild )
			{
				this._currentElement = this.CurrentElement.FirstChild;
				return;
			}

			if ( this.CurrentElement.IsLastChild == false )
			{
				this._currentElement = this.CurrentElement.NextSibling;
				return;
			}

			TreeElement element = this.CurrentElement;

			while ( element.IsLastChild )
			{
				element = element.Parent;

				if ( element.IsTop )
				{
					throw new InvalidOperationException( "���̗v�f�͑��݂��܂���B" );
				}
			}

			this._currentElement = element.NextSibling;
			
		}
		#endregion
		#region ' + MoveNext ( Int32 ) : void 
		/// <summary>
		/// �w�肵���񐔕����̗v�f�ֈړ����܂��B</summary>
		///
		public void MoveNext( Int32 aCount )
		{
			if ( aCount <= -1 ) throw new ArgumentOutOfRangeException( "-1 �ȉ��ł�", "aCount" );

			for ( Int32 i = 0; i < aCount; i++ )
			{
				MoveNext();
			}
		}
		#endregion
		#region ' + MoveNextSibling () : void 
		/// <summary>
		/// �����K�w�ɑ��݂��鎟�̗v�f�ֈړ����܂��B</summary>
		///
		public void MoveNextSibling ()
		{
			if ( HasNextSibling == false )
			{
				throw new InvalidOperationException( "�����K�w�Ɏ��̗v�f�͑��݂��܂���B" );
			}
			
			_currentElement = CurrentElement.NextSibling;
	
		}
		#endregion
		#region ' + MovePrevious () : void 
		/// <summary>
		/// �O�̗v�f�ֈړ����܂��B</summary>
		///
		public void MovePrevious()
		{
			if ( this.CurrentElement.IsTop ) throw new InvalidOperationException( "�O�̗v�f�͑��݂��܂���B" );

			if ( this.ParentElement.IsTop &&
			     this.CurrentElement.IsFirstChild )
			{
				this._currentElement = this.CurrentElement.Top;
				return;
			}

			if ( this.CurrentElement.IsFirstChild == false &&
			     this.CurrentElement.IsLastChild == false )
			{
				this._currentElement = this.CurrentElement.PreviousSibling;
				return;
			}

			if ( this.CurrentElement.IsFirstChild )
			{
				this._currentElement = this.ParentElement;
				return;
			}

			if ( this.CurrentElement.IsLastChild )
			{
				this._currentElement = this.CurrentElement.PreviousSibling;
				this.MoveToMostBottom();
				return;
			}
		}
		#endregion
		#region ' + MovePrevious ( Int32 ) : void 
		/// <summary>
		/// �w�肵���񐔕��O�̗v�f�ֈړ����܂��B</summary>
		///
		public void MovePrevious( Int32 aCount )
		{
			if ( aCount <= -1 ) throw new ArgumentOutOfRangeException( "-1 �ȉ��ł�", "aCount" );

			for ( Int32 i = 0; i < aCount; i++ )
			{
				this.MovePrevious();
			}
		}
		#endregion
		#region ' + MovePreviousSibling () : void 
		/// <summary>
		/// �����K�w�ɑ��݂���O�̗v�f�ֈړ����܂��B</summary>
		///
		public void MovePreviousSibling ()
		{
			if ( HasPreviousSibling == false )
			{
				throw new InvalidOperationException( "�����K�w�ɑO�̗v�f�͑��݂��܂���B" );
			}
			
			_currentElement = CurrentElement.PreviousSibling;
	
		}
		#endregion
		#region ' + MoveToLastChild () : void 
		/// <summary>
		/// ���݂̗v�f�̈�ԍŌ�̈ʒu�ɑ��݂���q�v�f�ֈړ����܂��B</summary>
		///
		public void MoveToLastChild()
		{
			if ( this.CurrentElement.HasChild == false )
			{
				throw new InvalidOperationException( "�q�v�f�͑��݂��܂���B" );
			}

			this._currentElement = this.CurrentElement.LastChild;
		}
		#endregion
		#region ' + MoveToTop () : void 
		/// <summary>
		/// �g�b�v�v�f�ֈړ����܂��B</summary>
		///
		public void MoveToTop()
		{
			if ( this.CurrentElement.IsTop == false )
			{
				this._currentElement = this.CurrentElement.Top;
			}
		}
		#endregion
		#region ' + MoveTo ( TreeElement ) : void 
		/// <summary>
		/// �w�肵�� TreeElment �ֈړ����܂��B</summary>
		///
		public void MoveTo ( TreeElement anElement )
		{
			if ( anElement == null ) throw new ArgumentNullException ( "anElement" );

			if ( this.TopElement.IsContainsStructure( anElement ) == false )
			{
				throw new InvalidOperationException( "�w�肵���v�f�͍\�����ɑ��݂��܂���B" );
			}
			
			this._currentElement = anElement;
 
		}
		#endregion
		#region ' + MoveUp () : void 
		/// <summary>
		/// ���݂̗v�f�̐e�v�f�ֈړ����܂��B</summary>
		///
		public void MoveUp()
		{
			if ( this.IsTop ) throw new InvalidOperationException( "�g�b�v�v�f�ɐe�v�f�����݂��Ȃ����߈ړ��ł��܂���B" );

			this._currentElement = this.CurrentElement.Parent;
		}
		#endregion
		#region ' + MoveUp ( Int32 ) : void 
		/// <summary>
		/// �w�肵���񐔕���̊K�w�ֈړ����܂��B</summary>
		///
		public void MoveUp( Int32 aCount )
		{
			if ( aCount <= -1 ) throw new ArgumentOutOfRangeException( "-1 �ȉ��ł�", "aCount" );

			for ( Int32 count = 0; count < aCount; count++ )
			{
				MoveUp();
			}
		}
		#endregion
		#region '   - MoveToMostBottom () : void 
		/// <summary>
		/// ���݈ʒu��������Ƃ��[���Ō�̗v�f�ֈړ����܂��B</summary>
		///
		private void MoveToMostBottom()
		{
			while ( this.CurrentElement.HasChild )
			{
				this._currentElement = this.CurrentElement.LastChild;
			}
		}
		#endregion
		
	}
}
