#region ' using 

using System;
using System.Collections;
using System.Text;

#endregion
namespace FxCommonLib.Utils.Tree {
	/// <summary>
	/// �K�w�\���̗v�f�������L�̋@�\��񋟂��܂��B</summary>
	///
	public class TreeElement : IEnumerable,
	                           ICloneable
	{
	
		// constructor 
		#region ' + constructor ( Object ) 
		/// <summary>
		/// TreeElement �̐V�����C���X�^���X�� Object �ŏ��������܂��B</summary>
		///
		///     <param name="aValueObject">
		///     �v�f�̒l�ƂȂ�I�u�W�F�N�g�B</summary>
		/// 
		/// <exception cref="System.ArgumentNullException">
		/// aValueObject �� null �ł��B</exception>
		///
		public TreeElement( Object aValueObject )
		{
			this._valueObject = aValueObject;
			this._childElements = new TreeElementCollection( this );
		}
		#endregion
		#region ' + constructor ( Object, String ) 
		/// <summary>
		/// TreeElement �̐V�����C���X�^���X�� Object ����� String �ŏ��������܂��B</summary>
		///
		///     <param name="aValueObject">
		///     �l�ƂȂ�I�u�W�F�N�g�B</summary>
		///     <param name="aName">
		///     �v�f�Ɋ֘A�Â����閼�O�B</summary>
		/// 
		/// <exception cref="System.ArgumentNullException">
		/// aValueObject �܂��� aName �� null �ł��B</exception>
		/// <exception cref="System.ArgumentException">
		/// aName ����ł��B</exception>
		///
		public TreeElement( Object aValueObject, String aName )
		{
			if ( aValueObject == null ) throw new ArgumentNullException( "aValueObject" );
			if ( aName == null ) throw new ArgumentNullException( "aName" );
			if ( aName.Trim().Length == 0 ) throw new ArgumentException( "��ł��B", "aName" );

			this._valueObject = aValueObject;
			this._name = aName;
			this._childElements = new TreeElementCollection( this );
		}
		#endregion
		#region ' + constructor ( Object, String, TreeElementKind ) 
		/// <summary>
		/// TreeElement �̐V�����C���X�^���X�� 
		/// Object ����� String ����� TreeElementKind �ŏ��������܂��B</summary>
		///
		/// <exception cref="System.ArgumentNullException">
		/// aValueObject �܂��� aName �� null �ł��B</exception>
		///
		public TreeElement( Object aValueObject, String aName, TreeElementKind anElementKind )
		{
			if ( aValueObject == null ) throw new ArgumentNullException( "aValueObject" );
			if ( aName == null ) throw new ArgumentNullException( "aName" );
			if ( aName.Trim().Length == 0 ) throw new ArgumentException( "��ł��B", "aName" );
			if ( anElementKind == TreeElementKind.None ) throw new ArgumentException( "TreeElementKind.None �ɐݒ肷�邱�Ƃ͏o���܂���B", "anElementKind" );
			if ( anElementKind == TreeElementKind.Link ) throw new ArgumentException( "TreeElementKind.Link �ŏ��������邱�Ƃ͏o���܂���B", "anElementKind" );

			this._valueObject = aValueObject;
			this._name = aName;
			this._kind = anElementKind;
			this._childElements = new TreeElementCollection( this );
		}
		#endregion
		#region ' + constructor ( Object, TreeElementKink ) 
		/// <summary>
		/// TreeElement �̐V�����C���X�^���X�� Object ����� TreeElementKind �ŏ��������܂��B</summary>
		///
		///     <param name="aValueObject">
		///     �l�ƂȂ�I�u�W�F�N�g�B</summary>
		///     <param name="anElementKind">
		///     �v�f�̎�ށB</summary>
		/// 
		/// <exception cref="System.ArgumentNullException">
		/// aValueObject �� null �ł��B</exception>
		///
		public TreeElement( Object aValueObject, TreeElementKind anElementKind )
		{
			if ( aValueObject == null ) throw new ArgumentNullException( "aValueObject" );
			if ( anElementKind == TreeElementKind.None ) throw new ArgumentException( "TreeElementKind.None �ɐݒ肷�邱�Ƃ͏o���܂���B", "anElementKind" );
			if ( anElementKind == TreeElementKind.Link ) throw new ArgumentException( "TreeElementKind.Link �ŏ��������邱�Ƃ͏o���܂���B", "anElementKind" );

			this._valueObject = aValueObject;
			this._kind = anElementKind;
			this._childElements = new TreeElementCollection( this );
		}
		#endregion

		
		// field 
		#region ' field 

		internal TreeElement _parent = null;
		private Object _valueObject = null;
		private TreeElementCollection _childElements = null;
		private String _name = null;
		private String _pathSeparator = "/";
		private TreeElementKind _kind = TreeElementKind.Composite;
		private Boolean _isVisibleLinkPrefix = true;
		private ArrayList _structureReferenceElements = new ArrayList();

		#endregion
		
		
		// property 
		#region ' + AllChildElements : TreeElementCollection { get } 
		/// <summary>
		/// ���ʊK�w�ɑ��݂���S�Ă̎q�v�f�̃R���N�V���������擾���܂��B</summary>
		///
		public TreeElementCollection AllChildElements
		{
			get
			{
				TreeElementCollection childElements = new TreeElementCollection( this );

				foreach ( TreeElement element in this.ChildElements )
				{
					childElements.AddSubElement( element );

					if ( element.HasChild )
					{
						childElements.AddRangeSubElements( element.AllChildElements );
					}
				}

				return childElements;
			}
		}
		#endregion
		#region ' + AllCount : Int32 { get } 
		/// <summary>
		/// ���̗v�f���܂߂����ʊK�w�̎q�v�f�S�Ă̐����擾���܂��B</summary>
		///
		public Int32 AllCount
		{
			get { return this.AllChildCount + 1; }
		}
		#endregion
		#region ' + AllChildCount : Int32 { get } 
		/// <summary>
		/// ���ʊK�w�̎q�v�f���܂߂đS�Ă̎q�v�f�̐����擾���܂��B</summary>
		///
		public Int32 AllChildCount
		{
			get
			{
				Int32 count = this.ChildCount;

				if ( this.HasChild )
				{
					foreach ( TreeElement element in this.ChildElements )
					{
						if ( element.HasChild )
						{
							count += element.AllChildCount;
						}
					}
				}
				return count;
			}
		}
		#endregion
		#region ' + ChildCount : Int32 { get } 
		/// <summary>
		/// �q�v�f�̐����擾���܂��B</summary>
		///
		public Int32 ChildCount
		{
			get
			{
				if ( this.HasChild )
				{
					return this.ChildElements.Count;
				}
				else
				{
					return 0;
				}
			}
		}
		#endregion
		#region ' + ChildElements : TreeElementCollection { get } 
		/// <summary>
		/// �q�v�f�̃R���N�V�������擾���܂��B</summary>
		///
		public TreeElementCollection ChildElements
		{
			get { return this._childElements; }
		}
		#endregion
		#region ' + Depth : Int32 { get } 
		/// <summary>
		/// 0 ����n�܂邱�̗v�f�̐[����\�������擾���܂��B</summary>
		///
		public Int32 Depth
		{
			get
			{
				Int32 depth = 0;

				TreeElement element = this;

				while ( element.IsTop == false )
				{
					depth++;
					element = element.Parent;
				}

				return depth;
			}
		}
		#endregion
		#region ' + FullPath : String { get } 
		/// <summary>
		/// ���̗v�f��\���t���p�X���擾���܂��B</summary>
		///
		public String FullPath
		{
			get
			{
				ArrayList names = new ArrayList();
				names.Add( this.Name );

				TreeElement currentElement = this;

				Boolean gotRootName = false;

				while ( gotRootName == false )
				{
					if ( currentElement.IsTop == false )
					{
						currentElement = currentElement.Parent;
						names.Add( currentElement.Name );
					}
					else
					{
						gotRootName = true;
					}
				}

				names.Reverse();

				StringBuilder path = new StringBuilder();
				foreach ( String name in names )
				{
					path.Append( name + this.PathSeparator );
				}

				return path.ToString().Remove( path.ToString().Length - this.PathSeparator.Length, this.PathSeparator.Length );
			}
		}
		#endregion
		#region ' + FirstChild : TreeElement { get } 
		/// <summary>
		/// �q�v�f�̂͂��߂̗v�f���擾���܂��B</summary>
		///
		public TreeElement FirstChild
		{
			get
			{
				if ( this.HasChild )
				{
					return this.ChildElements[ 0 ];
				}
				else
				{
					return null;
				}
			}
		}
		#endregion
		#region ' + HasChild : Boolean { get } 
		/// <summary>
		/// �q�v�f�����݂��邩�ǂ����������l���擾���܂��B</summary>
		///
		public Boolean HasChild
		{
			get
			{
				if ( this.ChildElements == null ||
				     this.ChildElements.Count == 0 )
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
		#region ' + HasValue : Boolean { get } 
		/// <summary>
		/// �֘A�Â����Ă���l�ƂȂ�I�u�W�F�N�g�������Ă��邩�ǂ����������l���擾�܂��܂��B</summary>
		///
		public Boolean HasValue
		{
			get
			{
				if ( this.Value == null )
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
		#region ' + IsTop : Boolean { get } 
		/// <summary>
		/// �v�f���ŏ�ʂ̊K�w�ɑ��݂��邩�ǂ����������l���擾�܂��܂��B</summary>
		///
		public Boolean IsTop
		{
			get
			{
				if ( this.Parent == null )
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
		#region ' + IsVisibleLinkPrefix : Boolean { get,set } 
		/// <summary>
		/// �v�f�̎�ނ� Link �̏ꍇ�ɕ\�����郊���N��\���v���t�B�b�N�X��
		/// �\�����邩�ǂ����������l���擾�܂��͐ݒ肵�܂��B
		/// ����l�� true �ł��B</summary>
		///
		public Boolean IsVisibleLinkPrefix
		{
			get { return _isVisibleLinkPrefix; }
			set { _isVisibleLinkPrefix = value; }
		}
		#endregion
		#region ' + Index : Int32 { get } 
		/// <summary>
		/// �e�v�f���猩�����̗v�f�� 0 ����n�܂�C���f�b�N�X�ʒu���擾���܂��B
		/// �v�f���ŏ�ʂ̊K�w�Ɉʒu����ꍇ�� 0 ��Ԃ��܂��B</summary>
		///
		public Int32 Index
		{
			get
			{
				if ( this.IsTop )
				{
					return 0;
				}
				else
				{
					return Parent.ChildElements.IndexOf( this );
				}
			}
		}
		#endregion
		#region ' + IndentedPath : String { get } 
		/// <summary>
		/// �e�v�f�̖��O�ŃC���f���g�������̗v�f�̃p�X���擾���܂��B</summary>
		///
		public String IndentedPath
		{
			get
			{
				Int32 parentPathLength = 0;

				if ( IsTop )
				{
					return Name;
				}

				TreeElement parent = Parent;

				while ( parent.IsTop == false )
				{
					parentPathLength += parent.Name.Length;
					parent = parent.Parent;
				}

				parentPathLength += parent.Name.Length;
				
				String indent = new String( ' ', parentPathLength );

				return indent + Name;
			}
		}
		#endregion
		#region ' + IsFirstChild : Boolean { get } 
		/// <summary>
		/// �e�v�f����݂Ă��̗v�f���ŏ��̗v�f���ǂ����������l���擾�܂��܂��B</summary>
		///
		public Boolean IsFirstChild
		{
			get
			{
				if ( this.IsTop )
				{
					throw new InvalidOperationException( "���[�g�v�f�ɂ��̑���͎g�p�ł��܂���B" );
				}

				if ( this.Index == 0 )
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
		#region ' + IsLastChild : Boolean { get } 
		/// <summary>
		/// �e�v�f����݂Ă��̗v�f���Ō�̗v�f���ǂ����������l���擾�܂��܂��B</summary>
		///
		public Boolean IsLastChild
		{
			get
			{
				if ( this.IsTop )
				{
					throw new InvalidOperationException( "���[�g�v�f�ɂ��̑���͎g�p�o���܂���B" );
				}
				if ( this.Index ==
				     Parent.ChildCount - 1 )
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
		#region ' + Kind : TreeElementKind { get } 
		/// <summary>
		/// �v�f�̎�ނ��擾���܂��B
		/// ����l�� Composite �ł��B</summary>
		///
		public TreeElementKind Kind
		{
			get { return _kind; }
		}
		#endregion
		#region ' + LastChild : TreeElement { get } 
		/// <summary>
		/// �Ō���̎q�v�f���擾���܂��B</summary>
		///
		public TreeElement LastChild
		{
			get
			{
				if ( this.HasChild )
				{
					return this.ChildElements[ this.ChildElements.Count - 1 ];
				}
				else
				{
					return null;
				}
			}
		}
		#endregion
		#region ' + Name : String { get,set } 
		/// <summary>
		/// �v�f�̖��O���擾�܂��͐ݒ肵�܂��B
		/// �擾�̍ۂɒl���ݒ肳��Ă��Ȃ��ꍇ�AValue �v���p�e�B�ɐݒ肳��Ă���
		/// Object �� ToString ���\�b�h����l��Ԃ��܂��B
		/// Value �v���p�e�B�� Object �� null �Q�Ƃ̏ꍇ�A
		/// Kind �v���p�e�B�� TreeElementKind �񋓌^�� ToString ���\�b�h����
		/// �l��Ԃ��܂��B</summary>
		///
		public String Name
		{
			get
			{
				String name = "";

				if ( this.Kind == TreeElementKind.Link )
				{
					
					if ( this.IsVisibleLinkPrefix )
					{
						name = "<Link>";
					}

					if ( this._name == null || this._name.Trim().Length == 0 )
					{
						TreeElement linkElement = this.Value as TreeElement;

						if ( linkElement != null )
						{
							name += linkElement.Name;
						}
					}
				}
				else
				{
					if ( this._name == null || this._name.Trim().Length == 0 )
					{
						if ( this.Value != null )
						{
							name = this.Value.ToString();
						}
					}
					else
					{
						name = this._name;
					}

				}

			return name;
				
			}
			set
			{
				if ( value == null ) throw new ArgumentNullException( "value" );
				if ( value.Trim().Length == 0 ) throw new ArgumentException( "��ł��B", "value" );

				this._name = value;
			}
		}
		#endregion
		#region ' + NextSibling : TreeElement { get } 
		/// <summary>
		/// �����K�w�ɂ��鎟�̗v�f���擾���܂��B</summary>
		///
		public TreeElement NextSibling
		{
			get
			{
				if ( this.IsTop )
				{
					throw new InvalidOperationException( "���[�g�v�f�ɂ��̑���͎g�p�ł��܂���B" );
				}
				else
				{
					if ( this.IsLastChild )
					{
						return null;
					}
					else
					{
						return Parent.ChildElements[ this.Index + 1 ];
					}
				}
			}
		}
		#endregion
		#region ' + Parent : TreeElement { get } 
		/// <summary>
		/// �e�v�f���擾���܂��B
		/// �l�̐ݒ�� TreeElement �N���X���ł̂ݎg�p���܂��B</summary>
		///
		public TreeElement Parent
		{
			get { return this._parent; }
		}
		#endregion
		#region ' + PathSeparator : String { get,set } 
		/// <summary>
		/// �p�X�̊K�w�̋�؂��\����������擾�܂��͐ݒ肵�܂��B
		/// ����l�� "/" �ł��B</summary>
		///
		public String PathSeparator
		{
			get { return this._pathSeparator; }
			set
			{
				if ( value == null ) throw new ArgumentNullException( "value" );
				if ( value.Trim().Length == 0 ) throw new ArgumentException( "��ł��B", "value" );

				if ( value != this._pathSeparator )
				{
					this._pathSeparator = value;
				}
			}
		}
		#endregion
		#region ' + PreviousSibling : TreeElement { get } 
		/// <summary>
		/// �����K�w�ɂ���O�̗v�f���擾���܂��B</summary>
		///
		public TreeElement PreviousSibling
		{
			get
			{
				if ( this.IsTop )
				{
					throw new InvalidOperationException( "���[�g�v�f�ɂ��̑���͎g�p�ł��܂���B" );
				}
				else
				{
					if ( this.IsFirstChild )
					{
						return null;
					}
					else
					{
						return Parent.ChildElements[ this.Index - 1 ];
					}
				}
			}
		}
		#endregion
		#region ' + Top : TreeElement { get } 
		/// <summary>
		/// �ŏ�ʂ̗v�f���擾���܂��B</summary>
		///
		public TreeElement Top
		{
			get
			{
				TreeElement element = this;

				while ( element.IsTop == false )
				{
					element = element.Parent;
				}

				return element;
			}
		}
		#endregion
		#region ' + Value : Object { get,set } 
		/// <summary>
		/// �v�f�Ɋ֘A�Â����Ă���I�u�W�F�N�g���擾���܂��B</summary>
		///
		public Object Value
		{
			get { return _valueObject; }
			set
			{
				if ( value == null ) throw new ArgumentNullException( "value" );

				_valueObject = value;
			}
		}
		#endregion
		#region '   ~ StructureReferenceElements : ArrayList { get } 
		/// <summary>
		/// ���̗v�f�̊K�w���ŎQ�Ƃ���Ă��� TreeElement �̃R���N�V�������擾���܂��B
		/// �N���C�A���g���炱�̃����o�ɃA�N�Z�X���Ȃ��ł��������B</summary>
		///
		internal ArrayList StructureReferenceElements
		{
			get
			{
				if ( this.IsTop )
				{
					return this._structureReferenceElements;
				}
				else
				{
					return this.Top.StructureReferenceElements;
				}

			}
		}
		#endregion
		
		
		// method 
		#region ' + Add ( TreeElement ) : void 
		/// <summary>
		/// �w�肵���v�f���q�v�f�֒ǉ����܂��B</summary>
		///
		public void Add( TreeElement anElement )
		{
			if ( anElement == null ) throw new ArgumentNullException( "anElement" );
			if ( this.Kind != TreeElementKind.Composite ) throw new InvalidOperationException( "��ނ� Composite �ȊO�̗v�f�Ɏq�v�f��ǉ����邱�Ƃ͏o���܂���B" );
			if ( this.IsContainsStructure( anElement ) ) throw new InvalidOperationException( "�ǉ����悤�Ƃ����v�f�͊��ɂ��̗v�f���܂ފK�w���ɑ��݂��Ă��܂��B" );

			this.StructureReferenceElements.Add( anElement );
			anElement.SetParentElement( this );
			this._childElements.Add( anElement );
			
		}
		#endregion
		#region ' + AddElements ( TreeElementCollection ) : void 
		/// <summary>
		/// �v�f�̃R���N�V�������q�v�f�֒ǉ����܂��B</summary>
		///
		public void AddElements( TreeElementCollection someElements )
		{
			if ( someElements == null ) throw new ArgumentNullException( "someElements" );

			foreach ( TreeElement element in someElements )
			{
				this.Add( element );
			}
		}
		#endregion
		#region ' + AddPrependChild ( TreeElement ) : void 
		/// <summary>
		/// ���̗v�f�̎q�v�f�̐擪�֎w�肵���v�f��}�����܂��B</summary>
		///
		public void AddPrependChild( TreeElement anElement )
		{
			if ( anElement == null ) throw new ArgumentNullException( "anElement" );
			if ( Kind != TreeElementKind.Composite ) throw new InvalidOperationException( "��ނ� Composite �ȊO�̗v�f�֎q�v�f��ǉ����邱�Ƃ͏o���܂���B" );
			if ( this.IsContainsStructure( anElement ) ) throw new InvalidOperationException( "�ǉ����悤�Ƃ����v�f�͊��ɂ��̗v�f���܂ފK�w���ɑ��݂��Ă��܂��B" );

			if ( this.HasChild )
			{
				this.StructureReferenceElements.Add( anElement );
				anElement.SetParentElement( this );
				this.ChildElements.Insert( 0, anElement );
			}
			else
			{
				this.Add( anElement );
			}
		}
		#endregion
		#region ' + Clone () : Object 
		/// <summary>
		/// TreeElement �̃f�B�[�v�R�s�[���擾���܂��B</summary>
		///
		public Object Clone()
		{
			TreeElement cloneElement = new TreeElement( Value, Name, Kind );

			if ( HasChild )
			{
				foreach ( TreeElement element in ChildElements )
				{
					TreeElement newSubElement = new TreeElement( element.Value, element.Name, element.Kind );
					cloneElement.Add( newSubElement );

					if ( element.HasChild )
					{
						TreeElement childElement = ( TreeElement )element.Clone();
						newSubElement.AddElements( childElement.ChildElements );
					}
				}
			}

			return cloneElement;
		}
		#endregion
		#region ' + CreateLinkElement () : TreeElement 
		/// <summary>
		/// ���̗v�f�̃����N�𐶐����Ď擾���܂��B</summary>
		///
		public TreeElement CreateLinkElement()
		{
			
			TreeElement linkElement;
			
			if ( this._name == null )
			{
				linkElement = new TreeElement( this );
				linkElement.SetLinkKind();
			}
			else
			{
				linkElement = new TreeElement( this, this._name );
				linkElement.SetLinkKind();
			}

			return linkElement;
		}
		#endregion
		#region ' + CopyTo ( TreeElement ) : void 
		/// <summary>
		/// ���̗v�f���w�肵���v�f�̎q�v�f�Ƃ��ăR�s�[���܂��B</summary>
		///
		public void CopyTo( TreeElement aTargetElement )
		{
			if ( aTargetElement == null ) throw new ArgumentNullException( "aTargetElement" );

			aTargetElement.Add( ( TreeElement )this.Clone() );
		}
		#endregion
		#region ' + GetEnumerator () : IEnumerator 
		/// <summary>
		/// �R���N�V�����𔽕������ł���񋓎q��Ԃ��܂��B</summary>
		/// 
		/// <returns>
		/// �R���N�V�����𔽕��������邽�߂Ɏg�p�ł��� <see cref="T:System.Collections.IEnumerator"/>�B</returns>
		/// 
		public IEnumerator GetEnumerator()
		{
			return new TreeElementEnumerator( this.AllChildElements );
		}
		#endregion
		#region ' + InsertAfter ( TreeElement ) : void 
		/// <summary>
		/// �����K�w�ł��̗v�f�̒���Ɏw�肵���v�f��}�����܂��B</summary>
		///
		public void InsertAfter( TreeElement anElement )
		{
			if ( anElement == null ) throw new ArgumentNullException( "anElement" );
			if ( this.IsTop ) throw new InvalidOperationException( "���[�g�v�f�̑O��ɗv�f��}�����邱�Ƃ͏o���܂���B" );
			if ( this.IsContainsStructure( anElement	) ) throw new InvalidOperationException( "�ǉ����悤�Ƃ����v�f�͊��ɂ��̗v�f���܂ލ\�����Ɋ܂܂�Ă��܂��B" );

			if ( this.IsLastChild )
			{
				
				Parent.Add( anElement );
			}
			else
			{
				this.StructureReferenceElements.Add( anElement );
				anElement.SetParentElement( this.Parent );
				this.Parent.ChildElements.Insert( this.Index + 1, anElement );
			}
			
		}
		#endregion
		#region ' + InsertBefore ( TreeElement ) : void 
		/// <summary>
		/// �����K�w�ł��̗v�f�̒��O�Ɏw�肵���v�f��}�����܂��B</summary>
		///
		public void InsertBefore( TreeElement anElement )
		{
			if ( anElement == null ) throw new ArgumentNullException( "anElement" );
			if ( this.IsTop ) throw new InvalidOperationException( "���[�g�v�f�̑O��ɗv�f��}�����邱�Ƃ͏o���܂���B" );
			if ( this.IsContainsStructure( anElement	) ) throw new InvalidOperationException( "�ǉ����悤�Ƃ����v�f�͊��ɂ��̗v�f���܂ލ\�����Ɋ܂܂�Ă��܂��B" );

			if ( this.Index == 0 )
			{
				this.StructureReferenceElements.Add( anElement );
				anElement.SetParentElement( this.Parent );
				this.Parent.ChildElements.Insert( 0, anElement );
			}
			else
			{
				this.StructureReferenceElements.Add( anElement );
				anElement.SetParentElement( this.Parent );
				this.Parent.ChildElements.Insert( this.Index, anElement );
			}
			
		}
		#endregion
		#region ' + IsContains ( TreeElement ) : Boolean 
		/// <summary>
		/// TreeElement ���q�v�f�Ɋ܂܂�Ă��邩�A
		/// �܂��͂��̗v�f�ƈ�v���邩�ǂ��������l���擾���܂��B</summary>
		///
		public Boolean IsContains( TreeElement anElement )
		{
			if ( anElement == null ) throw new ArgumentNullException( "anElement" );

			foreach ( TreeElement element in this )
			{
				if ( element.Equals( anElement ) )
				{
					return true;
				}
			}

			return false;
		}
		#endregion
		#region ' + IsContainsStructure( TreeElement ) : Boolean 
		/// <summary>
		/// �w�肵�� TreeElement �����̗v�f���܂ލ\�����Ɋ܂܂�Ă��邩�ǂ����������l���擾���܂��B</summary>
		///
		public Boolean IsContainsStructure ( TreeElement anElement )
		{
			if ( anElement == null ) throw new ArgumentNullException( "anElement" );

			return this.StructureReferenceElements.Contains( anElement );

		}
		#endregion
		#region ' + MoveTo ( TreeElement ) : void 
		/// <summary>
		/// ���̗v�f���w�肵���v�f�̎q�v�f�Ƃ��Ĉړ����܂��B</summary>
		///
		public void MoveTo( TreeElement aTargetElememnt )
		{
			if ( aTargetElememnt == null ) throw new ArgumentNullException( "aTargetElememnt" );
			if ( this.IsContains( aTargetElememnt ) ) throw new InvalidOperationException( "�ړ���̗v�f�͂��̗v�f�̎q�v�f�ł��B�܂��͂��̗v�f���g�ł��B" );

			TreeElement moveElement = this;
			this.Remove();
			aTargetElememnt.Add( moveElement );
			
		}
		#endregion
		#region ' + Remove () : void 
		/// <summary>
		/// ���̗v�f��e�v�f����폜���܂��B</summary>
		///
		/// <exception cref="System.InvalidOperationException">
		/// ���[�g�v�f���폜���邱�Ƃ͏o���܂���B</exception>
		/// 
		public void Remove()
		{
			if ( this.IsTop ) throw new InvalidOperationException( "�g�b�v�v�f���폜���邱�Ƃ͏o���܂���B" );

			this.Parent.RemoveChild( this );
			this.Top.StructureReferenceElements.Remove( this );
			
		}
		#endregion
		#region ' + RemoveAllChild () : void 
		/// <summary>
		/// �q�v�f��S�č폜���܂��B</summary>
		///
		public void RemoveAllChild()
		{
			if ( Kind != TreeElementKind.Composite ) throw new InvalidOperationException( "��ނ� Composite �ȊO�̗v�f�ɂ��̑���͎g�p�ł��܂���B" );

			if ( this.HasChild )
			{
				foreach ( TreeElement childElement in this.ChildElements )
				{
					this.RemoveChild( childElement );
				}
			}
		}
		#endregion
		#region ' + RemoveChild ( TreeElement ) : void 
		/// <summary>
		/// �w�肵���q�v�f���폜���܂��B
		/// �w�肵���v�f���q�v�f�ɑ��݂��Ȃ��ꍇ�A���̏����͖�������܂��B</summary>
		///
		public void RemoveChild( TreeElement anElement )
		{
			if ( anElement == null ) throw new ArgumentNullException( "anElement" );
			if ( Kind != TreeElementKind.Composite ) throw new InvalidOperationException( "��ނ� Composite �ȊO�̗v�f�ɂ��̑���͎g�p�ł��܂���B" );

			if ( this.ChildElements.IsContains( anElement ) )
			{
				this.ChildElements.Remove( anElement );
				this.Top.StructureReferenceElements.Remove( anElement );
			}
		}
		#endregion
		#region '   ~ SetParentElement ( TreeElement ) : void 
		/// <summary>
		/// ���̗v�f�̐e�v�f��ݒ肵�܂��B
		/// �N���C�A���g���炱�̃����o�փA�N�Z�X���Ȃ��ł��������B</summary>
		///
		internal void SetParentElement ( TreeElement aParentElement )
		{
			if ( aParentElement == null ) throw new ArgumentNullException ( "aParentElement" );
 
			this._parent = aParentElement;
 
		}
		#endregion
		#region '   # SetLinkKind () : void 
		/// <summary>
		/// ���̗v�f�̎�ނ������N�v�f�Ƃ��Đݒ肵�܂��B
		/// �N���C�A���g���炱�̃����o�փA�N�Z�X���Ȃ��ł��������B</summary>
		///
		protected internal void SetLinkKind()
		{
			this._kind = TreeElementKind.Link;
		}
		#endregion


		// innertype 
		#region '   - TreeElementEnumerator : IEnumerator 
		/// <summary>
		/// �R���N�V�����ɑ΂���P���Ȕ����������T�|�[�g���܂��B</summary>
		///
		private class TreeElementEnumerator : IEnumerator
		{
            
			// constructor 
			#region ' + constructor ( TreeElementCollection ) 
			/// <summary>
			/// �V���� TreeElementEnumerator �̃C���X�^���X�� TreeElementCollection �ŏ��������܂��B</summary>
			/// 
			public TreeElementEnumerator( TreeElementCollection aList )
			{
				_list = aList;
			}
			#endregion
            
            
			// field 
			#region ' field 
            
			private TreeElementCollection _list = null;
			private Int32 _currentIndex = -2;
            
			#endregion
            
            
			// property 
			#region ' + Current : Object { get } 
			/// <summary>
			/// �R���N�V�������̌��݂̗v�f���擾���܂��B</summary>
			/// 
			public Object Current
			{
				get
				{
					if ( _currentIndex == -1 )
					{
						return _list[ 0 ].Parent;
					}
					else
					{
						return _list[ _currentIndex ];
					}
				}
			}
			#endregion
            
            
			// method 
			#region ' + Reset () : void 
			/// <summary>
			/// �R���N�V�����̍ŏ��̗v�f�̑O�ɐݒ肵�܂��B</summary>
			/// 
			public void Reset()
			{
				_currentIndex = -2;
			}
			#endregion
			#region ' + MoveNext () : Boolean 
			/// <summary>
			/// �񋓎q���R���N�V�����̎��̗v�f�ɐi�߂܂��B</summary>
			/// 
			public Boolean MoveNext()
			{
				_currentIndex ++;

				if ( _list.Count == 0 ||
				     _list.Count == _currentIndex )
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
